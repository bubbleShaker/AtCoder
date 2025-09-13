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

        // 1.1: Generate M base cards (all l)
        for (int i = 0; i < m; i++) cardsToCreate.Add((l, i + 1));

        // 1.2: Generate N-M adjustment cards (powers of 2, range-aware & overflow-safe)
        long range = Math.Max(1L, u - l);
        long currentPowerOf2 = 1;
        for (int i = 0; i < Math.Max(0, n - m); i++)
        {
            cardsToCreate.Add((currentPowerOf2, m + i + 1));
            if (currentPowerOf2 <= range / 2) currentPowerOf2 <<= 1; // 2倍しても範囲内
            else currentPowerOf2 = 1;
        }

        // 1.3: Print the generated card values
        ConsoleEx.WriteLine(string.Join(" ", cardsToCreate.Select(c => c.value)));
        ConsoleEx.Flush();

        // --- Stage 2: Read Targets (B) and Assign Cards (X) ---
        var targetsB = ConsoleEx.ReadLongArray(m);

        var finalAssignments = new int[n + 1];   // 1-indexed by originalIndex
        long[] mountainSums = new long[m];

        var baseCards = cardsToCreate.GetRange(0, Math.Min(m, cardsToCreate.Count));
        var adjustmentCards = (cardsToCreate.Count > m)
            ? cardsToCreate.GetRange(m, cardsToCreate.Count - m)
            : new List<(long value, int originalIndex)>();

        // Assign base cards (fixed)
        for (int i = 0; i < baseCards.Count; i++)
        {
            var card = baseCards[i];
            mountainSums[i] += card.value;
            finalAssignments[card.originalIndex] = i + 1; // mountain index is 1-based
        }

        // もし調整カードが0なら、そのまま出力して終了
        if (adjustmentCards.Count == 0)
        {
            var outputAssignments0 = new List<int>();
            for (int i = 1; i <= n; i++) outputAssignments0.Add(finalAssignments[i]);
            ConsoleEx.WriteLine(string.Join(" ", outputAssignments0));
            ConsoleEx.Flush();
            return;
        }

        // --- Initial Solution via Greedy (improved: always place to max error reduction; allow over-target) ---
        var currentAdjAssignments = new int[adjustmentCards.Count]; // 0=unassigned, else mountain index (1..m)
        long[] greedySums = (long[])mountainSums.Clone();

        // 大きいカードから
        adjustmentCards.Sort((a, b) => b.value.CompareTo(a.value));
        for (int i = 0; i < adjustmentCards.Count; i++)
        {
            var card = adjustmentCards[i];
            int bestMountain = 0;
            long bestGain = long.MinValue; // 誤差減少量（大きいほど良い）
            for (int j = 0; j < m; j++)
            {
                long before = Math.Abs(greedySums[j] - targetsB[j]);
                long after = Math.Abs(greedySums[j] + card.value - targetsB[j]);
                long gain = before - after;
                if (gain > bestGain)
                {
                    bestGain = gain;
                    bestMountain = j;
                }
            }
            greedySums[bestMountain] += card.value;
            currentAdjAssignments[i] = bestMountain + 1;
        }

        // --- Simulated Annealing (stable temperature scaling & safe acceptance) ---
        var rand = new Random();

        // スケール推定：目標の典型値に合わせる
        long typical = Math.Max(1L, targetsB.Sum() / Math.Max(1, m));
        double T_start = (double)typical;
        double T_end = Math.Max(1e-3, typical * 1e-3); // 極端に小さくならないように
        double timeLimit = 1.95;

        long[] currentSums = (long[])mountainSums.Clone();
        for (int i = 0; i < adjustmentCards.Count; i++)
        {
            int mount = currentAdjAssignments[i];
            if (mount > 0) currentSums[mount - 1] += adjustmentCards[i].value;
        }

        long currentError = 0;
        for (int i = 0; i < m; i++) currentError += Math.Abs(currentSums[i] - targetsB[i]);

        long bestError = currentError;
        var bestAdjAssignments = (int[])currentAdjAssignments.Clone();

        while (stopwatch.Elapsed.TotalSeconds < timeLimit)
        {
            double ratio = stopwatch.Elapsed.TotalSeconds / timeLimit;
            double temp = T_start * Math.Pow(T_end / T_start, ratio);

            int cardIndex = rand.Next(adjustmentCards.Count);
            var card = adjustmentCards[cardIndex];

            int currentMount = currentAdjAssignments[cardIndex]; // 0..m
            int newMount = rand.Next(m + 1); // 0..m（0はどこにも置かない、だが初期は必ず置いている）

            if (currentMount == newMount) continue;

            long deltaError = 0;

            if (currentMount > 0)
            {
                int cm = currentMount - 1;
                long before = Math.Abs(currentSums[cm] - targetsB[cm]);
                long after = Math.Abs((currentSums[cm] - card.value) - targetsB[cm]);
                deltaError += (after - before); // 悪化は正、改善は負
            }

            if (newMount > 0)
            {
                int nm = newMount - 1;
                long before = Math.Abs(currentSums[nm] - targetsB[nm]);
                long after = Math.Abs((currentSums[nm] + card.value) - targetsB[nm]);
                deltaError += (after - before);
            }

            bool accept = false;
            if (deltaError <= 0) accept = true;
            else
            {
                double denom = Math.Max(1e-9, temp);
                double x = Math.Min(60.0, deltaError / denom); // クリップで数値安定化
                double prob = Math.Exp(-x);
                accept = rand.NextDouble() < prob;
            }

            if (accept)
            {
                if (currentMount > 0) currentSums[currentMount - 1] -= card.value;
                if (newMount > 0) currentSums[newMount - 1] += card.value;
                currentAdjAssignments[cardIndex] = newMount;
                currentError += deltaError;

                if (currentError < bestError)
                {
                    bestError = currentError;
                    bestAdjAssignments = (int[])currentAdjAssignments.Clone();
                }
            }
        }

        // Apply best found assignments
        for (int i = 0; i < adjustmentCards.Count; i++)
        {
            int bestMount = bestAdjAssignments[i];
            if (bestMount > 0)
            {
                finalAssignments[adjustmentCards[i].originalIndex] = bestMount;
            }
            else
            {
                // 0（どこにも置かない）の場合、問題仕様次第だが0を入れる（未割当を明示）
                finalAssignments[adjustmentCards[i].originalIndex] = 0;
            }
        }

        // Print final result
        var outputAssignments = new List<int>();
        for (int i = 1; i <= n; i++) outputAssignments.Add(finalAssignments[i]);
        ConsoleEx.WriteLine(string.Join(" ", outputAssignments));
        ConsoleEx.Flush();
    }
}

/// <summary>
/// Fast I/O for competitive programming (robust whitespace handling)
/// </summary>
public static class ConsoleEx
{
    private static readonly System.IO.StreamReader _streamReader = new System.IO.StreamReader(Console.OpenStandardInput());
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
            while (line != null && line.Length == 0) line = ReadLine(); // 空行スキップ
            if (line == null) throw new InvalidOperationException("Unexpected EOF");
            // null 指定であらゆる空白を区切りに、空トークン除去
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
