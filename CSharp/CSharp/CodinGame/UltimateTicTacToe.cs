using System;
using System.Linq;
using System.Collections.Generic;

enum Owner { None = 0, Me = 1, Opponent = 2, Draw = 3 }

class Move
{
    public int Row { get; }
    public int Col { get; }
    public Move(int row, int col) { Row = row; Col = col; }
    public override string ToString() => $"{Row} {Col}";
}

// 3x3のボードを管理するクラス（スモールボードおよびメインボードに使用）
class Board
{
    public const int Size = 3;
    public Owner[,] Cells { get; } = new Owner[Size, Size];
    public Owner Winner { get; private set; } = Owner.None;

    public void SetCell(int r, int c, Owner owner)
    {
        Cells[r, c] = owner;
        UpdateWinner();
    }

    public void RecomputeWinner()
    {
        Winner = Owner.None;
        UpdateWinner();
    }

    private void UpdateWinner()
    {
        for (int i = 0; i < Size; i++)
        {
            if (Cells[i, 0] != Owner.None && Cells[i, 0] != Owner.Draw && Cells[i, 0] == Cells[i, 1] && Cells[i, 1] == Cells[i, 2]) { Winner = Cells[i, 0]; return; }
            if (Cells[0, i] != Owner.None && Cells[0, i] != Owner.Draw && Cells[0, i] == Cells[1, i] && Cells[1, i] == Cells[2, i]) { Winner = Cells[0, i]; return; }
        }
        if (Cells[0, 0] != Owner.None && Cells[0, 0] != Owner.Draw && Cells[0, 0] == Cells[1, 1] && Cells[1, 1] == Cells[2, 2]) { Winner = Cells[0, 0]; return; }
        if (Cells[0, 2] != Owner.None && Cells[0, 2] != Owner.Draw && Cells[0, 2] == Cells[1, 1] && Cells[1, 1] == Cells[2, 0]) { Winner = Cells[0, 2]; return; }

        if (IsFull()) Winner = Owner.Draw;
    }

    public bool IsFull()
    {
        foreach (var cell in Cells) if (cell == Owner.None) return false;
        return true;
    }
}

class GameState
{
    // 9つのスモールボード (3x3のグリッド状に配置)
    public Board[,] SubBoards { get; } = new Board[Board.Size, Board.Size];
    // 各スモールボードの勝者を記録するメインボード
    public Board MainBoard { get; } = new Board();
    public List<Move> ValidActions { get; set; } = new List<Move>();

    public GameState()
    {
        for (int r = 0; r < Board.Size; r++)
            for (int c = 0; c < Board.Size; c++)
                SubBoards[r, c] = new Board();
    }

    public static (int br, int bc, int lr, int lc) DecomposeMove(Move move)
        => (move.Row / Board.Size, move.Col / Board.Size,
            move.Row % Board.Size, move.Col % Board.Size);

    public void RecordMove(Move move, Owner owner)
    {
        var (br, bc, lr, lc) = DecomposeMove(move);
        SubBoards[br, bc].SetCell(lr, lc, owner);
        // スモールボードの結果をメインボードに反映
        MainBoard.SetCell(br, bc, SubBoards[br, bc].Winner);
    }

    public List<Move> GetValidMoves(int br, int bc)
    {
        List<Move> moves = new List<Move>();
        // 指定されたスモールボードが既に決着しているか満杯なら、どこでも打てる
        bool freeMove = SubBoards[br, bc].Winner != Owner.None;

        for (int r = 0; r < Board.Size * Board.Size; r++)
        {
            for (int c = 0; c < Board.Size * Board.Size; c++)
            {
                int targetBr = r / Board.Size;
                int targetBc = c / Board.Size;
                if (!freeMove && (targetBr != br || targetBc != bc)) continue;

                if (SubBoards[targetBr, targetBc].Cells[r % Board.Size, c % Board.Size] == Owner.None &&
                    SubBoards[targetBr, targetBc].Winner == Owner.None)
                {
                    moves.Add(new Move(r, c));
                }
            }
        }
        return moves;
    }

    public GameState Clone()
    {
        var clone = new GameState();
        for (int br = 0; br < Board.Size; br++)
            for (int bc = 0; bc < Board.Size; bc++)
                for (int lr = 0; lr < Board.Size; lr++)
                    for (int lc = 0; lc < Board.Size; lc++)
                        if (SubBoards[br, bc].Cells[lr, lc] != Owner.None)
                            clone.SubBoards[br, bc].SetCell(lr, lc, SubBoards[br, bc].Cells[lr, lc]);
        for (int br = 0; br < Board.Size; br++)
            for (int bc = 0; bc < Board.Size; bc++)
                if (SubBoards[br, bc].Winner != Owner.None)
                    clone.MainBoard.SetCell(br, bc, SubBoards[br, bc].Winner);
        clone.ValidActions = new List<Move>(ValidActions);
        return clone;
    }
}

interface IGameStrategy
{
    Move Think(GameState state);
}

class MinimaxStrategy : IGameStrategy
{
    private const int MaxDepth = 3;

    public Move Think(GameState state)
    {
        Move bestMove = state.ValidActions[0];
        int bestScore = int.MinValue;
        int alpha = int.MinValue;
        int beta = int.MaxValue;

        foreach (var move in state.ValidActions)
        {
            state.RecordMove(move, Owner.Me);
            int score = Minimax(state, move, 0, false, alpha, beta);
            UndoMove(state, move);

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
            alpha = Math.Max(alpha, bestScore);
        }
        return bestMove;
    }

    private int Minimax(GameState state, Move lastMove, int depth, bool isMaximizing, int alpha, int beta)
    {
        Owner winner = state.MainBoard.Winner;
        if (winner == Owner.Me) return 10000 - depth;
        if (winner == Owner.Opponent) return -10000 + depth;
        if (depth >= MaxDepth) return Evaluate(state);

        // 次に打てるボードを決定
        int nextBr = lastMove.Row % Board.Size;
        int nextBc = lastMove.Col % Board.Size;

        var possibleMoves = state.GetValidMoves(nextBr, nextBc);
        if (possibleMoves.Count == 0) return Evaluate(state);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (var m in possibleMoves)
            {
                state.RecordMove(m, Owner.Me);
                int eval = Minimax(state, m, depth + 1, false, alpha, beta);
                UndoMove(state, m);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var m in possibleMoves)
            {
                state.RecordMove(m, Owner.Opponent);
                int eval = Minimax(state, m, depth + 1, true, alpha, beta);
                UndoMove(state, m);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha) break;
            }
            return minEval;
        }
    }

    private void UndoMove(GameState state, Move m)
    {
        var (br, bc, lr, lc) = GameState.DecomposeMove(m);
        // 1. サブボードのセルを消す
        state.SubBoards[br, bc].Cells[lr, lc] = Owner.None;
        // 2. サブボードの Winner を Cells から再計算
        state.SubBoards[br, bc].RecomputeWinner();
        // 3. メインボードの対応セルを同期
        state.MainBoard.Cells[br, bc] = state.SubBoards[br, bc].Winner;
        // 4. メインボードの Winner を再計算
        state.MainBoard.RecomputeWinner();
    }

    private int Evaluate(GameState state)
    {
        int score = 0;
        // メインボード（大きな3x3）の評価
        score += EvaluateSmallBoard(state.MainBoard) * 100;

        // 各スモールボード内の状況も加味
        for (int r = 0; r < Board.Size; r++)
            for (int c = 0; c < Board.Size; c++)
                if (state.MainBoard.Cells[r, c] == Owner.None)
                    score += EvaluateSmallBoard(state.SubBoards[r, c]);

        return score;
    }

    private int EvaluateSmallBoard(Board b)
    {
        int s = 0;
        if (b.Winner == Owner.Me) return 10;
        if (b.Winner == Owner.Opponent) return -10;
        if (b.Cells[1, 1] == Owner.Me) s += 3;
        if (b.Cells[1, 1] == Owner.Opponent) s -= 3;
        return s;
    }
}

class MctsNode
{
    public GameState State { get; }
    public Move Move { get; }           // このノードへの手（ルートはnull）
    public Owner CurrentPlayer { get; } // この手を打ったプレイヤー
    public MctsNode Parent { get; }
    public List<MctsNode> Children { get; } = new List<MctsNode>();
    public List<Move> UntriedMoves { get; }
    public int VisitCount { get; set; } = 0;
    public double WinScore { get; set; } = 0.0; // Me視点の勝利スコア

    public MctsNode(GameState state, Move move, Owner currentPlayer, MctsNode parent)
    {
        State = state;
        Move = move;
        CurrentPlayer = currentPlayer;
        Parent = parent;
        UntriedMoves = (move == null)
            ? new List<Move>(state.ValidActions)
            : state.GetValidMoves(move.Row % Board.Size, move.Col % Board.Size);
    }

    public double UCB1Score(double c = 1.41421356)
    {
        if (VisitCount == 0) return double.MaxValue;
        return WinScore / VisitCount + c * Math.Sqrt(Math.Log(Parent.VisitCount) / VisitCount);
    }

    public bool IsFullyExpanded => UntriedMoves.Count == 0;
    public bool IsTerminal => State.MainBoard.Winner != Owner.None;
}

class MctsStrategy : IGameStrategy
{
    private const double C = 1.41421356;
    private const int NormalTimeMs = 90;
    private const int FirstTimeMs = 900;
    private readonly Random _rng = new Random();
    private bool _isFirstMove = true;

    public Move Think(GameState state)
    {
        int limit = _isFirstMove ? FirstTimeMs : NormalTimeMs;
        _isFirstMove = false;

        var root = new MctsNode(state.Clone(), null, Owner.Opponent, null);
        var sw = System.Diagnostics.Stopwatch.StartNew();

        while (sw.ElapsedMilliseconds < limit)
        {
            var node = Select(root);
            if (!node.IsTerminal && !node.IsFullyExpanded)
                node = Expand(node);
            double result = Simulate(node);
            Backpropagate(node, result);
        }

        return root.Children.OrderByDescending(c => c.VisitCount).First().Move;
    }

    private MctsNode Select(MctsNode node)
    {
        while (!node.IsTerminal && node.IsFullyExpanded && node.Children.Count > 0)
            node = node.Children.OrderByDescending(c => c.UCB1Score(C)).First();
        return node;
    }

    private MctsNode Expand(MctsNode node)
    {
        int idx = _rng.Next(node.UntriedMoves.Count);
        var move = node.UntriedMoves[idx];
        node.UntriedMoves.RemoveAt(idx);

        Owner next = (node.CurrentPlayer == Owner.Me) ? Owner.Opponent : Owner.Me;
        var newState = node.State.Clone();
        newState.RecordMove(move, next);

        var child = new MctsNode(newState, move, next, node);
        node.Children.Add(child);
        return child;
    }

    private double Simulate(MctsNode node)
    {
        if (node.IsTerminal) return Score(node.State.MainBoard.Winner);

        var sim = node.State.Clone();
        Owner player = (node.CurrentPlayer == Owner.Me) ? Owner.Opponent : Owner.Me;
        Move last = node.Move;

        while (sim.MainBoard.Winner == Owner.None)
        {
            var moves = (last == null)
                ? new List<Move>(sim.ValidActions)
                : sim.GetValidMoves(last.Row % Board.Size, last.Col % Board.Size);
            if (moves.Count == 0) break;

            last = moves[_rng.Next(moves.Count)];
            sim.RecordMove(last, player);
            player = (player == Owner.Me) ? Owner.Opponent : Owner.Me;
        }
        return Score(sim.MainBoard.Winner);
    }

    private void Backpropagate(MctsNode node, double result)
    {
        for (var n = node; n != null; n = n.Parent)
        {
            n.VisitCount++;
            n.WinScore += (n.CurrentPlayer == Owner.Me) ? result : (1.0 - result);
        }
    }

    private double Score(Owner winner)
    {
        if (winner == Owner.Me) return 1.0;
        if (winner == Owner.Opponent) return 0.0;
        return 0.5;
    }
}

class InputReader
{
    public void UpdateState(GameState state)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int oppRow = int.Parse(inputs[0]);
        int oppCol = int.Parse(inputs[1]);
        if (oppRow != -1) state.RecordMove(new Move(oppRow, oppCol), Owner.Opponent);

        int validActionCount = int.Parse(Console.ReadLine());
        state.ValidActions.Clear();
        for (int i = 0; i < validActionCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            state.ValidActions.Add(new Move(int.Parse(inputs[0]), int.Parse(inputs[1])));
        }
    }

    public void SendMove(Move move) => Console.WriteLine(move.ToString());
}

class GameAi
{
    private readonly GameState _state;
    private readonly IGameStrategy _strategy;

    public GameAi(GameState state, IGameStrategy strategy = null)
    {
        _state = state;
        _strategy = strategy ?? new MctsStrategy();
    }

    public Move Think() => _strategy.Think(_state);
}

class Player
{
    static void Main(string[] args)
    {
        var state = new GameState();
        var reader = new InputReader();
        var ai = new GameAi(state);

        while (true)
        {
            reader.UpdateState(state);
            Move bestMove = ai.Think();
            state.RecordMove(bestMove, Owner.Me);
            reader.SendMove(bestMove);
        }
    }
}
