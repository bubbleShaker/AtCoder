using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

enum Owner { Cloud = -1, Me = 0, Opponent = 1 }
enum Module { SAMPLES, DIAGNOSIS, MOLECULES, LABORATORY, START_UP }
enum Command { GOTO, CONNECT, WAIT }

class Project
{
    public Dictionary<char, int> requirements = new Dictionary<char, int>();
}

class Sample
{
    public int sampleId;
    public Owner carriedBy;
    public int rank;
    public string gain;
    public int health;
    public Dictionary<char, int> costs = new Dictionary<char, int>();
}

class Robot
{
    public Module target;
    public int eta;
    public int score;
    public Dictionary<char, int> storage = new Dictionary<char, int>();
    public Dictionary<char, int> expertise = new Dictionary<char, int>();
}

class GameState
{
    public List<Sample> Samples = new List<Sample>();
    public List<Robot> Robots = new List<Robot>();
    public List<Project> Projects = new List<Project>();
    public Dictionary<char, int> available = new Dictionary<char, int>();
    public Dictionary<char, int> prevAvailable = new Dictionary<char, int>();
    public Dictionary<char, int> opponentTargetMolecules = new Dictionary<char, int>();

    public GameState()
    {
        Robots.Add(new Robot());
        Robots.Add(new Robot());
        foreach (char c in moleculeList)
        {
            available[c] = 0;
            prevAvailable[c] = 0;
            opponentTargetMolecules[c] = 0;
        }
    }

    public void TrackOpponent()
    {
        var opponent = Robots[(int)Owner.Opponent];
        foreach (char m in moleculeList) opponentTargetMolecules[m] = 0;

        var opponentSamples = Samples.Where(s => s.carriedBy == Owner.Opponent && s.health != -1).ToList();
        foreach (var s in opponentSamples)
        {
            foreach (char m in moleculeList)
            {
                int needed = Math.Max(0, s.costs[m] - (opponent.expertise[m] + opponent.storage[m]));
                opponentTargetMolecules[m] += (int)needed;
            }
        }

        if (opponentSamples.Any())
        {
            Console.Error.Write("Opponent needs: ");
            foreach (char m in moleculeList) Console.Error.Write($"{m}:{opponentTargetMolecules[m]} ");
            Console.Error.WriteLine();
        }
    }

    public void SaveCurrentAvailable()
    {
        foreach (var kvp in available) prevAvailable[kvp.Key] = kvp.Value;
    }

    static public string moleculeList = "ABCDE";
}

class InputReader
{
    public void InitializeConfig(GameState state)
    {
        string line = Console.ReadLine();
        if (line == null) return;
        int projectCount = int.Parse(line);
        for (int i = 0; i < projectCount; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            var project = new Project();
            for (int j = 0; j < 5; j++) project.requirements[GameState.moleculeList[j]] = int.Parse(inputs[j]);
            state.Projects.Add(project);
        }
    }

    public void UpdateState(GameState state)
    {
        for (int i = 0; i < 2; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            var robot = state.Robots[i];
            Enum.TryParse(inputs[0], out robot.target);
            robot.eta = int.Parse(inputs[1]);
            robot.score = int.Parse(inputs[2]);
            for (int j = 0; j < 5; j++)
            {
                char m = GameState.moleculeList[j];
                robot.storage[m] = int.Parse(inputs[3 + j]);
                robot.expertise[m] = int.Parse(inputs[8 + j]);
            }
        }
        string[] availInputs = Console.ReadLine().Split(' ');
        for (int i = 0; i < 5; i++) state.available[GameState.moleculeList[i]] = int.Parse(availInputs[i]);

        var sampleCount = int.Parse(Console.ReadLine());
        state.Samples.Clear();
        for (int i = 0; i < sampleCount; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            var sample = new Sample
            {
                sampleId = int.Parse(inputs[0]),
                carriedBy = (Owner)int.Parse(inputs[1]),
                rank = int.Parse(inputs[2]),
                gain = inputs[3],
                health = int.Parse(inputs[4]),
            };
            for (int j = 0; j < 5; j++) sample.costs[GameState.moleculeList[j]] = int.Parse(inputs[5 + j]);
            state.Samples.Add(sample);
        }
    }
}

class RobotAi
{
    private GameState _state;
    public Action CurrentAction;

    public RobotAi(GameState state)
    {
        _state = state;
        CurrentAction = GoToSamples;
    }

    private int GetDistance(Module from, Module to)
    {
        if (from == to) return 0;
        if (from == Module.START_UP) return 2;
        switch (from)
        {
            case Module.SAMPLES: return 3;
            case Module.DIAGNOSIS: return (to == Module.LABORATORY) ? 4 : 3;
            case Module.MOLECULES: return 3;
            case Module.LABORATORY: return (to == Module.DIAGNOSIS) ? 4 : 3;
            default: return 3;
        }
    }

    public int CalculateTurnsToComplete(Sample sample, Robot robot)
    {
        int collectionTurns = 0;
        foreach (char m in GameState.moleculeList)
        {
            int needed = Math.Max(0, sample.costs[m] - (robot.expertise[m] + robot.storage[m]));
            if (needed > _state.available[m] && needed > 0) return 999;
            collectionTurns += needed;
        }
        return GetDistance(robot.target, Module.MOLECULES) + collectionTurns + GetDistance(Module.MOLECULES, Module.LABORATORY) + 1;
    }

    void Send(Command cmd, string target = "") => Console.WriteLine($"{cmd} {target}".Trim());
    void Send(Command cmd, Module target) => Send(cmd, target.ToString());

    bool IsNeededForProject(char molecule)
    {
        var myRobot = _state.Robots[(int)Owner.Me];
        return _state.Projects.Any(p => p.requirements.ContainsKey(molecule) && p.requirements[molecule] > myRobot.expertise[molecule]);
    }

    double GetStrategicScore(Sample s, Robot robot)
    {
        int turns = CalculateTurnsToComplete(s, robot);
        if (turns >= 999) return -1.0;

        double score = s.health;
        if (s.gain != "NONE")
        {
            char gainMolecule = s.gain[0];
            foreach (var p in _state.Projects)
            {
                if (p.requirements.ContainsKey(gainMolecule) && p.requirements[gainMolecule] > robot.expertise[gainMolecule])
                {
                    // プロジェクトの各要件との差を合計して、あとどれくらいで達成か見る
                    int projectRemaining = 0;
                    foreach (var kvp in p.requirements)
                    {
                        projectRemaining += Math.Max(0, kvp.Value - robot.expertise[kvp.Key]);
                    }

                    if (projectRemaining <= 3) score += 300; // 超王手
                    else if (projectRemaining <= 5) score += 150; // 王手
                    else score += 50;
                }
            }
        }

        return score / turns;
    }

    void GoToSamples()
    {
        Send(Command.GOTO, Module.SAMPLES);
        CurrentAction = CollectSamples;
    }

    // --- 追加プログラム3: 動的なランク選択 ---
    void CollectSamples()
    {
        var myRobot = _state.Robots[(int)Owner.Me];

        if (myRobot.eta > 0)
        {
            Console.Error.WriteLine($"CollectSamples : myRobot.eta is {myRobot.eta}");
            Send(Command.WAIT);
            return;
        }

        var mySamples = _state.Samples.Where(s => s.carriedBy == Owner.Me).ToList();

        int myCount = mySamples.Count;
        int totalExp = myRobot.expertise.Values.Sum();

        int rankOneSampleCount = mySamples.Count(s => s.rank == 1);
        int rankTwoSampleCount = mySamples.Count(s => s.rank == 2);
        int rankThreeSampleCount = mySamples.Count(s => s.rank == 3);

        Console.Error.WriteLine($"totalExp is {totalExp}");
        Console.Error.WriteLine($"rankOneSampleCount is {rankOneSampleCount}");
        Console.Error.WriteLine($"rankTwoSampleCount is {rankTwoSampleCount}");
        Console.Error.WriteLine($"rankThreeSampleCount is {rankThreeSampleCount}");

        const int CountThreeBorderExp = 8;
        const int CountTwoBorderExp = 5;

        if (myCount == 3)
        {
            Console.Error.WriteLine($"goto_case3 : myCount is {myCount}, totalExp is {totalExp}");
            Send(Command.GOTO, Module.DIAGNOSIS);
            CurrentAction = DecideSearchSampleId;
            return;
        }

        if (myCount == 2 && totalExp < CountTwoBorderExp)
        {
            Console.Error.WriteLine($"goto_case1 : myCount is {myCount}, totalExp is {totalExp}");
            Send(Command.GOTO, Module.DIAGNOSIS);
            CurrentAction = DecideSearchSampleId;
            return;
        }

        if (myCount == 2)
        {
            string targetRank = (rankThreeSampleCount == 2) ? "2" : "1";
            Send(Command.CONNECT, targetRank);
        }
        else
        {
            string targetRank = "1";

            if (totalExp >= CountThreeBorderExp)
            {
                targetRank = "3";
            }
            else if (totalExp >= CountTwoBorderExp)
            {
                targetRank = "2";
            }

            Send(Command.CONNECT, targetRank);
        }
    }

    void DecideSearchSampleId()
    {
        var myRobot = _state.Robots[(int)Owner.Me];
        if (myRobot.eta > 0)
        {
            Console.Error.WriteLine($"DecideSearchSampleId : myRobot.eta is {myRobot.eta}");
            Send(Command.WAIT);
            return;
        }

        var mySamples = _state.Samples.Where(s => s.carriedBy == Owner.Me).ToList();

        var undiagnosed = mySamples.FirstOrDefault(s => s.health == -1);
        if (undiagnosed != null)
        {
            Console.Error.WriteLine($"undiagnosed.sampleId is {undiagnosed.sampleId.ToString()}");
            Send(Command.CONNECT, undiagnosed.sampleId.ToString()); return;
        }

        var deadSample = mySamples.FirstOrDefault(s => CalculateTurnsToComplete(s, myRobot) >= 999);
        if (deadSample != null)
        {
            Console.Error.WriteLine($"deadSample is {deadSample}");
            Send(Command.CONNECT, deadSample.sampleId.ToString());
            return;
        }

        if (mySamples.Count < 3)
        {
            Console.Error.WriteLine("CloudSample Choosing.");
            var bestCloud = _state.Samples
                .Where(s => s.carriedBy == Owner.Cloud && s.health > 0)
                .OrderByDescending(s => GetStrategicScore(s, myRobot))
                .FirstOrDefault();

            if (bestCloud != null && GetStrategicScore(bestCloud, myRobot) > 0.2)
            {
                Console.Error.WriteLine($"bestCloud is {bestCloud}");
                Send(Command.CONNECT, bestCloud.sampleId.ToString());
                return;
            }
        }

        if (mySamples.Count > 0)
        {
            bool canFinishAny = mySamples.Any(s => GameState.moleculeList.All(m =>
                myRobot.storage[m] >= Math.Max(0, s.costs[m] - myRobot.expertise[m])));
            int totalInStorage = myRobot.storage.Values.Sum();

            if (totalInStorage == 10 && !canFinishAny)
            {
                var worst = mySamples.OrderBy(s => GetStrategicScore(s, myRobot)).FirstOrDefault();
                Send(Command.CONNECT, worst.sampleId.ToString());
                Console.Error.WriteLine($"worst is {worst}");
            }
            else
            {
                Send(Command.GOTO, Module.MOLECULES);
                CurrentAction = CollectMolecules;
            }
        }
        else
        {
            Send(Command.GOTO, Module.SAMPLES);
            CurrentAction = CollectSamples;
        }
    }

    void CollectMolecules()
    {
        var myRobot = _state.Robots[(int)Owner.Me];
        var opponent = _state.Robots[(int)Owner.Opponent];
        if (myRobot.eta > 0)
        {
            Console.Error.WriteLine($"CollectMolecules : myRobot.eta is {myRobot.eta}");
            Send(Command.WAIT);
            return;
        }

        if (myRobot.target != Module.MOLECULES) { Send(Command.GOTO, Module.MOLECULES); return; }

        var mySamples = _state.Samples.Where(s => s.carriedBy == Owner.Me && s.health > 0).ToList();

        var shoppingList = new Dictionary<char, int>();
        foreach (char m in GameState.moleculeList) shoppingList[m] = 0;
        foreach (var s in mySamples)
        {
            foreach (char m in GameState.moleculeList)
            {
                shoppingList[m] += Math.Max(0, s.costs[m] - (myRobot.expertise[m] + myRobot.storage[m]));
            }
        }

        // 刷新された優先度計算
        var topChoice = GameState.moleculeList
            .Where(m => _state.available[m] > 0)
            .OrderByDescending(m => {
                double priority = 0;
                if (shoppingList[m] > 0) priority += 1000; // 自分が直接必要

                // Denial Logic: 相手の必要量と在庫の少なさを考慮
                if (_state.opponentTargetMolecules[m] > 0)
                    priority += 400.0 * _state.opponentTargetMolecules[m] / (_state.available[m] + 0.1);

                if (IsNeededForProject(m)) priority += 200;
                return priority;
            }).FirstOrDefault();

        int totalInStorage = myRobot.storage.Values.Sum();
        var canFinishSamples = mySamples.Where(s => GameState.moleculeList.All(m =>
            myRobot.storage[m] >= Math.Max(0, s.costs[m] - myRobot.expertise[m]))).ToList();

        if (canFinishSamples.Count >= 1 && (shoppingList.Values.Sum() == 0 || totalInStorage == 10))
        {
            Send(Command.GOTO, Module.LABORATORY);
            CurrentAction = MakeMedicine;
        }
        else if (totalInStorage < 10 && topChoice != '\0' && (shoppingList[topChoice] > 0 || _state.opponentTargetMolecules[topChoice] > 0))
        {
            Console.Error.WriteLine($"Picking {topChoice} for {(shoppingList[topChoice] > 0 ? "self" : "Denial")}");
            Send(Command.CONNECT, topChoice.ToString());
        }
        else if (canFinishSamples.Count >= 1)
        {
            Send(Command.GOTO, Module.LABORATORY);
            CurrentAction = MakeMedicine;
        }
        else
        {
            if (mySamples.Count == 0)
            {
                Send(Command.GOTO, Module.SAMPLES);
                CurrentAction = CollectSamples;
            }
            else
            {
                Send(Command.GOTO, Module.DIAGNOSIS);
                CurrentAction = DecideSearchSampleId;
            }
        }
    }

    void MakeMedicine()
    {
        var myRobot = _state.Robots[(int)Owner.Me];
        if (myRobot.eta > 0) { Send(Command.WAIT); return; }

        var readySample = _state.Samples
            .Where(s => s.carriedBy == Owner.Me)
            .FirstOrDefault(s => GameState.moleculeList.All(m =>
                myRobot.storage[m] >= Math.Max(0, s.costs[m] - myRobot.expertise[m])));

        if (readySample != null)
        {
            Send(Command.CONNECT, readySample.sampleId.ToString());
        }
        else
        {
            int totalInStorage = myRobot.storage.Values.Sum();

            if (!_state.Samples.Any(s => s.carriedBy == Owner.Me))
            {
                Send(Command.GOTO, Module.SAMPLES);
                CurrentAction = CollectSamples;
            }
            else if (totalInStorage == 10)
            {
                Console.Error.WriteLine($"totalInStorage is {totalInStorage}");
                Send(Command.GOTO, Module.DIAGNOSIS);
                CurrentAction = DecideSearchSampleId;
            }
            else
            {
                Send(Command.GOTO, Module.MOLECULES);
                CurrentAction = CollectMolecules;
            }
        }
    }
}

class Player
{
    static void Main(string[] args)
    {
        var state = new GameState();
        var reader = new InputReader();
        var ai = new RobotAi(state);
        reader.InitializeConfig(state);
        while (true)
        {
            reader.UpdateState(state);
            state.TrackOpponent();
            ai.CurrentAction();
            state.SaveCurrentAvailable();
        }
    }
}