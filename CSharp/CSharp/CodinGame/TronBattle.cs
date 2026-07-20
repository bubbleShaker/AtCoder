using System;
using System.Linq;
using System.Collections.Generic;

enum Direction { UP, DOWN, LEFT, RIGHT }

struct Point
{
    public int X;
    public int Y;
    public Point(int x, int y) { X = x; Y = y; }
}

class TronPlayer
{
    public int Id;
    public Point CurrentPosition;
    // サーバーが -1 を送ってきたら脱落しているという判定。これは正しいです。
    public bool IsAlive => CurrentPosition.X != -1;

    public TronPlayer(int id)
    {
        Id = id;
    }
}

class GameState
{
    public const int Width = 30;
    public const int Height = 20;
    public int MyId;
    public int PlayerCount;
    public List<TronPlayer> Players = new List<TronPlayer>();
    public int[,] Grid = new int[Width, Height];

    public GameState()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                Grid[x, y] = -1;
    }

    // Minimax用にディープコピーを作成するメソッドを追加
    public GameState Clone()
    {
        var newState = new GameState { MyId = this.MyId, PlayerCount = this.PlayerCount };
        newState.Players = this.Players.Select(p => new TronPlayer(p.Id) { CurrentPosition = p.CurrentPosition }).ToList();
        Array.Copy(this.Grid, newState.Grid, this.Grid.Length);
        return newState;
    }

    public void UpdateGrid(int playerId, Point p)
    {
        if (p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height)
            Grid[p.X, p.Y] = playerId;
    }

    public void ClearPlayerTrace(int playerId)
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (Grid[x, y] == playerId) Grid[x, y] = -1;
    }
}

class InputReader
{
    public void Update(GameState state)
    {
        string line = Console.ReadLine();
        if (line == null) return;

        string[] inputs = line.Split(' ');
        state.PlayerCount = int.Parse(inputs[0]);
        state.MyId = int.Parse(inputs[1]);

        if (state.Players.Count == 0)
        {
            for (int i = 0; i < state.PlayerCount; i++)
                state.Players.Add(new TronPlayer(i));
        }

        for (int i = 0; i < state.PlayerCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            var p = state.Players[i];

            int x1 = int.Parse(inputs[2]);
            int y1 = int.Parse(inputs[3]);

            // 前のターンまで生きていて、このターンで脱落した（-1になった）場合
            if (p.IsAlive && x1 == -1)
            {
                state.ClearPlayerTrace(i);
            }

            p.CurrentPosition = new Point(x1, y1);

            if (p.IsAlive)
            {
                state.UpdateGrid(i, p.CurrentPosition);
            }
        }
    }
}

class TronAi
{
    // Step 1: Enum.GetValues キャッシュ（GCプレッシャー排除）
    private static readonly Direction[] Directions = (Direction[])Enum.GetValues(typeof(Direction));

    // Step 3: 時間管理フィールド
    private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
    private bool _timeUp = false;
    private const int TIME_LIMIT_MS = 80;

    public Direction ComputeNextMove(GameState state)
    {
        var me = state.Players[state.MyId];
        var enemy = state.Players
            .Where(p => p.Id != state.MyId && p.IsAlive)
            .OrderBy(p => Math.Abs(p.CurrentPosition.X - me.CurrentPosition.X) + Math.Abs(p.CurrentPosition.Y - me.CurrentPosition.Y))
            .FirstOrDefault();

        if (enemy == null) return Direction.LEFT;

        // 安全な手を事前列挙
        var safeMoves = new List<Direction>();
        foreach (Direction dir in Directions)
        {
            Point next = GetNextPoint(me.CurrentPosition, dir);
            if (IsSafe(state, next))
                safeMoves.Add(dir);
        }

        if (safeMoves.Count == 0) return Direction.UP;
        if (safeMoves.Count == 1) return safeMoves[0];

        // Step 3: Iterative Deepening
        _stopwatch.Restart();
        _timeUp = false;

        Direction bestDir = safeMoves[0];
        int reachedDepth = 0;

        for (int depth = 1; !_timeUp; depth++)
        {
            Direction depthBestDir = safeMoves[0];
            int depthBestScore = int.MinValue;
            bool allSearched = true;

            foreach (Direction dir in safeMoves)
            {
                Point next = GetNextPoint(me.CurrentPosition, dir);
                GameState nextState = state.Clone();
                nextState.UpdateGrid(state.MyId, next);
                nextState.Players[state.MyId].CurrentPosition = next;

                int score = Minimax(nextState, depth, false, int.MinValue, int.MaxValue, enemy.Id);

                if (_timeUp) { allSearched = false; break; }

                if (score > depthBestScore)
                {
                    depthBestScore = score;
                    depthBestDir = dir;
                }
            }

            if (allSearched)
            {
                bestDir = depthBestDir;
                reachedDepth = depth;
            }
        }

        Console.Error.WriteLine($"depth={reachedDepth}");
        return bestDir;
    }

    // Step 3 + Step 4: 時間チェック付き Minimax + Move Ordering
    private int Minimax(GameState state, int depth, bool isMaximizing, int alpha, int beta, int enemyId)
    {
        if (_stopwatch.ElapsedMilliseconds >= TIME_LIMIT_MS)
        {
            _timeUp = true;
            return 0;
        }

        if (depth == 0 || !state.Players[state.MyId].IsAlive || !state.Players[enemyId].IsAlive)
            return Evaluate(state, enemyId);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (Direction dir in OrderMoves(state, state.MyId, true))
            {
                if (_timeUp) return 0;
                Point next = GetNextPoint(state.Players[state.MyId].CurrentPosition, dir);
                if (IsSafe(state, next))
                {
                    GameState nextState = state.Clone();
                    nextState.UpdateGrid(state.MyId, next);
                    nextState.Players[state.MyId].CurrentPosition = next;

                    int eval = Minimax(nextState, depth - 1, false, alpha, beta, enemyId);
                    if (_timeUp) return 0;
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha) break;
                }
            }
            return maxEval == int.MinValue ? -10000 : maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (Direction dir in OrderMoves(state, enemyId, false))
            {
                if (_timeUp) return 0;
                Point next = GetNextPoint(state.Players[enemyId].CurrentPosition, dir);
                if (IsSafe(state, next))
                {
                    GameState nextState = state.Clone();
                    nextState.UpdateGrid(enemyId, next);
                    nextState.Players[enemyId].CurrentPosition = next;

                    int eval = Minimax(nextState, depth - 1, true, alpha, beta, enemyId);
                    if (_timeUp) return 0;
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha) break;
                }
            }
            return minEval == int.MaxValue ? 10000 : minEval;
        }
    }

    // Step 2: Voronoi 評価
    private int Evaluate(GameState state, int enemyId)
    {
        if (!state.Players[state.MyId].IsAlive) return -2000;
        if (!state.Players[enemyId].IsAlive) return 2000;

        var voronoi = ComputeVoronoi(state);
        int myArea = voronoi.ContainsKey(state.MyId) ? voronoi[state.MyId] : 0;
        int enemyArea = voronoi.ContainsKey(enemyId) ? voronoi[enemyId] : 0;
        return myArea - enemyArea;
    }

    // Step 2: 全生存プレイヤーから同時 BFS で Voronoi 領域を計算
    private Dictionary<int, int> ComputeVoronoi(GameState state)
    {
        int[,] owner = new int[GameState.Width, GameState.Height];
        int[,] dist = new int[GameState.Width, GameState.Height];
        for (int x = 0; x < GameState.Width; x++)
            for (int y = 0; y < GameState.Height; y++)
            {
                owner[x, y] = -1;
                dist[x, y] = int.MaxValue;
            }

        var queue = new Queue<Point>();
        foreach (var player in state.Players)
        {
            if (!player.IsAlive) continue;
            Point pos = player.CurrentPosition;
            dist[pos.X, pos.Y] = 0;
            owner[pos.X, pos.Y] = player.Id;
            queue.Enqueue(pos);
        }

        while (queue.Count > 0)
        {
            Point p = queue.Dequeue();
            int pOwner = owner[p.X, p.Y];
            if (pOwner == -2) continue; // 競合セルからは展開しない

            foreach (Direction dir in Directions)
            {
                Point n = GetNextPoint(p, dir);
                if (n.X < 0 || n.X >= GameState.Width || n.Y < 0 || n.Y >= GameState.Height) continue;
                if (state.Grid[n.X, n.Y] != -1) continue; // 壁・軌跡はスキップ

                int newDist = dist[p.X, p.Y] + 1;
                if (newDist < dist[n.X, n.Y])
                {
                    dist[n.X, n.Y] = newDist;
                    owner[n.X, n.Y] = pOwner;
                    queue.Enqueue(n);
                }
                else if (newDist == dist[n.X, n.Y] && owner[n.X, n.Y] != pOwner && owner[n.X, n.Y] != -2)
                {
                    owner[n.X, n.Y] = -2; // 同距離で複数プレイヤー → 競合
                }
            }
        }

        var result = new Dictionary<int, int>();
        for (int x = 0; x < GameState.Width; x++)
            for (int y = 0; y < GameState.Height; y++)
            {
                int o = owner[x, y];
                if (o >= 0)
                {
                    if (!result.ContainsKey(o)) result[o] = 0;
                    result[o]++;
                }
            }
        return result;
    }

    // Step 4: Move Ordering（広い手を優先 / 狭い手を優先）
    private IEnumerable<Direction> OrderMoves(GameState state, int playerId, bool maximizing)
    {
        var scored = new List<(Direction dir, int score)>();
        foreach (Direction dir in Directions)
        {
            Point next = GetNextPoint(state.Players[playerId].CurrentPosition, dir);
            if (IsSafe(state, next))
                scored.Add((dir, QuickMoveScore(state, next)));
        }
        if (maximizing)
            scored.Sort((a, b) => b.score.CompareTo(a.score));
        else
            scored.Sort((a, b) => a.score.CompareTo(b.score));
        foreach (var (dir, _) in scored)
            yield return dir;
    }

    // Step 4: O(4) ヒューリスティック（周囲の空きマス数）
    private int QuickMoveScore(GameState state, Point p)
    {
        int score = 0;
        foreach (Direction dir in Directions)
        {
            Point n = GetNextPoint(p, dir);
            if (IsSafe(state, n)) score++;
        }
        return score;
    }

    private bool IsSafe(GameState state, Point p)
    {
        return p.X >= 0 && p.X < GameState.Width &&
               p.Y >= 0 && p.Y < GameState.Height &&
               state.Grid[p.X, p.Y] == -1;
    }

    private int CountReachableArea(GameState state, Point start)
    {
        int count = 0;
        bool[,] visited = new bool[GameState.Width, GameState.Height];
        Queue<Point> queue = new Queue<Point>();
        queue.Enqueue(start);
        visited[start.X, start.Y] = true;

        while (queue.Count > 0)
        {
            Point p = queue.Dequeue();
            count++;
            foreach (Direction dir in Directions)
            {
                Point n = GetNextPoint(p, dir);
                if (IsSafe(state, n) && !visited[n.X, n.Y])
                {
                    visited[n.X, n.Y] = true;
                    queue.Enqueue(n);
                }
            }
        }
        return count;
    }

    private Point GetNextPoint(Point p, Direction dir)
    {
        switch (dir)
        {
            case Direction.UP: return new Point(p.X, p.Y - 1);
            case Direction.DOWN: return new Point(p.X, p.Y + 1);
            case Direction.LEFT: return new Point(p.X - 1, p.Y);
            case Direction.RIGHT: return new Point(p.X + 1, p.Y);
            default: return p;
        }
    }
}

class Player
{
    static void Main(string[] args)
    {
        var state = new GameState();
        var reader = new InputReader();
        var ai = new TronAi();

        while (true)
        {
            reader.Update(state);
            Direction nextMove = ai.ComputeNextMove(state);
            Console.WriteLine(nextMove.ToString());
        }
    }
}