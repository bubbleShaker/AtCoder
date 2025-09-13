using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

public class Program
{
    public static void Main(string[] args)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // --- Stage 1: Generate and Print Cards (A) ---
        int n = ConsoleEx.ReadInt();
        int m = ConsoleEx.ReadInt();
        long l = ConsoleEx.ReadLong();
        long u = ConsoleEx.ReadLong();

        var cardsToCreate = new List<(long value, int originalIndex)>();

        // base cards (all l)
        for (int i = 0; i < m; i++) cardsToCreate.Add((l, i + 1));

        // adjustment cards: powers of two within [1, u-l], overflow-safe
        long range = Math.Max(1L, u - l);
        long currentPowerOf2 = 1;
        for (int i = 0; i < Math.Max(0, n - m); i++)
        {
            cardsToCreate.Add((currentPowerOf2, m + i + 1));
            if (currentPowerOf2 <= range / 2) currentPowerOf2 <<= 1;
            else currentPowerOf2 = 1;
        }

        // print A
        ConsoleEx.WriteLine(string.Join(" ", cardsToCreate.Select(c => c.value)));
        ConsoleEx.Flush();

        // --- Stage 2: Read B and compute X ---
        var targetsB = ConsoleEx.ReadLongArray(m);

        var finalAssignments = new int[n + 1]; // by originalIndex (1-based)
        long[] mountainSums = new long[m];

        var baseCards = cardsToCreate.GetRange(0, Math.Min(m, cardsToCreate.Count));
        var adjustmentCards = (cardsToCreate.Count > m)
            ? cardsToCreate.GetRange(m, cardsToCreate.Count - m)
            : new List<(long value, int originalIndex)>();

        // base assign
        for (int i = 0; i < baseCards.Count; i++)
        {
            var card = baseCards[i];
            mountainSums[i] += card.value;
            finalAssignments[card.originalIndex] = i + 1;
        }

        // no adjustment cards -> output and finish
        if (adjustmentCards.Count == 0)
        {
            var out0 = new List<int>();
            for (int i = 1; i <= n; i++) out0.Add(finalAssignments[i]);
            ConsoleEx.WriteLine(string.Join(" ", out0));
            ConsoleEx.Flush();
            return;
        }

        // --- Initial greedy (desc by value): place to best error reduction (allow over) ---
        var adj = adjustmentCards.ToArray();
        Array.Sort(adj, (a, b) => b.value.CompareTo(a.value));

        var assign = new int[adj.Length]; // 1..m
        long[] sums = (long[])mountainSums.Clone();

        for (int i = 0; i < adj.Length; i++)
        {
            var card = adj[i];
            int bestJ = 0;
            long bestGain = long.MinValue;
            for (int j = 0; j < m; j++)
            {
                long before = Math.Abs(sums[j] - targetsB[j]);
                long after = Math.Abs(sums[j] + card.value - targetsB[j]);
                long gain = before - after;
                if (gain > bestGain) { bestGain = gain; bestJ = j; }
            }
            sums[bestJ] += card.value;
            assign[i] = bestJ + 1;
        }

        // --- Deterministic local search (coordinate descent 1-opt) ---
        // 確実にスコアを下げる：各カードを今の和に対してベスト山へ再配置。改善がなくなるまで反復。
        // 2〜4周くらいで十分。時間制限も見る。
        int LS_ROUNDS = 4;
        for (int round = 0; round < LS_ROUNDS; round++)
        {
            bool improved = false;
            for (int i = 0; i < adj.Length; i++)
            {
                var card = adj[i];
                int cur = assign[i] - 1;

                // 現在の誤差寄与を除いた後で最適な山を再計算
                long bestDelta = 0;
                int bestJ = cur;

                // remove from current temporarily
                sums[cur] -= card.value;
                long curBefore = Math.Abs(sums[cur] + card.value - targetsB[cur]);

                for (int j = 0; j < m; j++)
                {
                    long before = (j == cur) ? curBefore : Math.Abs(sums[j] - targetsB[j]);
                    long after = Math.Abs(((j == cur) ? sums[j] : sums[j]) + (j == cur ? card.value : 0) + (j != cur ? card.value : 0) - targetsB[j]);
                    // 実際には j==cur の式は簡略化できるがわかりやすさ優先
                    long gain = before - after; // 正なら改善
                    if (gain > bestDelta)
                    {
                        bestDelta = gain;
                        bestJ = j;
                    }
                }

                if (bestJ != cur)
                {
                    sums[bestJ] += card.value;
                    assign[i] = bestJ + 1;
                    improved = true;
                }
                else
                {
                    // 戻す
                    sums[cur] += card.value;
                }

                // 軽いタイムガード
                if (stopwatch.Elapsed.TotalSeconds > 1.0) break;
            }
            if (!improved || stopwatch.Elapsed.TotalSeconds > 1.0) break;
        }

        // --- SA (finishing touch) ---
        long typical = Math.Max(1L, targetsB.Sum() / Math.Max(1, m));
        long maxCard = adj.Max(c => c.value);
        double scale = Math.Max((double)typical, (double)maxCard);

        double T_start = scale;                  // 問題スケールに整合
        double T_end = Math.Max(1e-3, scale * 1e-3);
        double timeLimit = 1.95;

        // sums は最新状態になっている
        long curErr = 0;
        for (int j = 0; j < m; j++) curErr += Math.Abs(sums[j] - targetsB[j]);

        long bestErr = curErr;
        var bestAssign = (int[])assign.Clone();
        var rnd = new Random();

        while (stopwatch.Elapsed.TotalSeconds < timeLimit)
        {
            // 温度計算
            double t = stopwatch.Elapsed.TotalSeconds / timeLimit;
            double temp = T_start * Math.Pow(T_end / T_start, t);

            int i = rnd.Next(adj.Length);
            var card = adj[i];
            int cur = assign[i] - 1;
            int dst = rnd.Next(m);
            if (dst == cur) continue;

            // Δ誤差
            long delta = 0;

            // remove from cur
            {
                long before = Math.Abs(sums[cur] - targetsB[cur]);
                long after = Math.Abs((sums[cur] - card.value) - targetsB[cur]);
                delta += (after - before);
            }
            // add to dst
            {
                long before = Math.Abs(sums[dst] - targetsB[dst]);
                long after = Math.Abs((sums[dst] + card.value) - targetsB[dst]);
                delta += (after - before);
            }

            bool accept = false;
            if (delta <= 0) accept = true;
            else
            {
                double x = Math.Min(60.0, delta / Math.Max(1e-9, temp));
                double prob = Math.Exp(-x);
                accept = rnd.NextDouble() < prob;
            }

            if (accept)
            {
                sums[cur] -= card.value;
                sums[dst] += card.value;
                assign[i] = dst + 1;
                curErr += delta;

                if (curErr < bestErr)
                {
                    bestErr = curErr;
                    Array.Copy(assign, bestAssign, assign.Length);
                }
            }
        }

        // write back
        for (int i = 0; i < adj.Length; i++)
            finalAssignments[adj[i].originalIndex] = bestAssign[i];

        // base はすでに設定済み
        var outX = new List<int>();
        for (int i = 1; i <= n; i++) outX.Add(finalAssignments[i]);
        ConsoleEx.WriteLine(string.Join(" ", outX));
        ConsoleEx.Flush();
    }
}

/// Robust Fast I/O
public static class ConsoleEx
{
    private static readonly System.IO.StreamReader _streamReader =
        new System.IO.StreamReader(Console.OpenStandardInput());
    private static readonly System.IO.StreamWriter _streamWriter =
        new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };

    private static string[] _input = Array.Empty<string>();
    private static int _index = 0;

    public static string ReadLine() => _streamReader.ReadLine();

    public static string Read()
    {
        while (_index >= _input.Length)
        {
            var line = ReadLine();
            while (line != null && line.Length == 0) line = ReadLine();
            if (line == null) throw new InvalidOperationException("Unexpected EOF");
            _input = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            _index = 0;
        }
        return _input[_index++];
    }

    public static int ReadInt() => int.Parse(Read());
    public static long ReadLong() => long.Parse(Read());
    public static double ReadDouble() => double.Parse(Read());
    public static BigInteger ReadBigInteger() => BigInteger.Parse(Read());

    public static int[] ReadIntArray(int n)
    {
        var a = new int[n];
        for (int i = 0; i < n; i++) a[i] = ReadInt();
        return a;
    }

    public static long[] ReadLongArray(int n)
    {
        var a = new long[n];
        for (int i = 0; i < n; i++) a[i] = ReadLong();
        return a;
    }

    public static string[] ReadStringArray(int n)
    {
        var a = new string[n];
        for (int i = 0; i < n; i++) a[i] = Read();
        return a;
    }

    public static void Write(object obj) => _streamWriter.Write(obj);
    public static void WriteLine(object obj) => _streamWriter.WriteLine(obj);
    public static void Flush() => _streamWriter.Flush();
}
