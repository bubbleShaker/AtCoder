using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Program
{
    public static void Main(string[] args)
    {
        // --- Stage 1: Generate and Print Cards (A) ---
        int n = ConsoleEx.ReadInt();
        int m = ConsoleEx.ReadInt();
        long l = ConsoleEx.ReadLong();
        long u = ConsoleEx.ReadLong();

        var cardsToCreate = new List<(long value, int originalIndex)>();
        
        // 1.1: Generate M base cards
        for (int i = 0; i < m; i++)
        {
            cardsToCreate.Add((l, i + 1));
        }

        // 1.2: Generate N-M adjustment cards (powers of 2)
        long currentPowerOf2 = 1;
        for (int i = 0; i < n - m; i++)
        {
            cardsToCreate.Add((currentPowerOf2, m + i + 1));
            currentPowerOf2 *= 2;
            // Reset if it gets too large to avoid overflow, cycle through smaller powers
            if (currentPowerOf2 > u - l || currentPowerOf2 <= 0) 
            {
                currentPowerOf2 = 1;
            }
        }

        // 1.3: Print the generated card values
        ConsoleEx.WriteLine(string.Join(" ", cardsToCreate.Select(c => c.value)));
        ConsoleEx.Flush();

        // --- Stage 2: Read Targets (B) and Assign Cards (X) ---
        
        // 2.1: Read target B values
        var targetsB = ConsoleEx.ReadLongArray(m);

        // 2.2: Initialize assignments and sums
        var finalAssignments = new int[n + 1]; // 1-based index
        long[] mountainSums = new long[m];

        // 2.3: Assign base cards (first M cards) to each mountain
        var baseCards = cardsToCreate.GetRange(0, m);
        for (int i = 0; i < m; i++)
        {
            var card = baseCards[i];
            mountainSums[i] += card.value;
            finalAssignments[card.originalIndex] = i + 1; // Assign to mountain i+1
        }

        // 2.4: Distribute adjustment cards using the two-phase greedy algorithm
        var adjustmentCards = cardsToCreate.GetRange(m, n - m);
        adjustmentCards.Sort((a, b) => b.value.CompareTo(a.value)); // Sort descending

        bool[] usedAdjustmentCards = new bool[n - m];

        // Phase 1: Fill without overshooting
        for (int i = 0; i < adjustmentCards.Count; i++)
        {
            var card = adjustmentCards[i];
            int bestMountain = -1;
            long maxNeed = -1;

            for (int j = 0; j < m; j++)
            {
                if (mountainSums[j] + card.value <= targetsB[j])
                {
                    long need = targetsB[j] - mountainSums[j];
                    if (need > maxNeed)
                    {
                        maxNeed = need;
                        bestMountain = j;
                    }
                }
            }

            if (bestMountain != -1)
            {
                mountainSums[bestMountain] += card.value;
                finalAssignments[card.originalIndex] = bestMountain + 1;
                usedAdjustmentCards[i] = true;
            }
        }

        // Phase 2: Overshoot for improvement
        for (int i = 0; i < adjustmentCards.Count; i++)
        {
            if (usedAdjustmentCards[i]) continue;

            var card = adjustmentCards[i];
            int bestMountain = -1;
            double maxImprovement = 0;

            for (int j = 0; j < m; j++)
            {
                double currentError = Math.Abs((double)mountainSums[j] - targetsB[j]);
                double newError = Math.Abs((double)mountainSums[j] + card.value - targetsB[j]);
                double improvement = currentError - newError;

                if (improvement > maxImprovement)
                {
                    maxImprovement = improvement;
                    bestMountain = j;
                }
            }

            if (bestMountain != -1)
            {
                mountainSums[bestMountain] += card.value;
                finalAssignments[card.originalIndex] = bestMountain + 1;
                usedAdjustmentCards[i] = true;
            }
        }

        // 2.5: Print the final assignments
        var outputAssignments = new List<int>();
        for(int i = 1; i <= n; i++)
        {
            outputAssignments.Add(finalAssignments[i]);
        }
        ConsoleEx.WriteLine(string.Join(" ", outputAssignments));
        ConsoleEx.Flush();
    }
}


/// <summary>
/// Fast I/O for competitive programming
/// </summary>
public static class ConsoleEx
{
    private static readonly System.IO.StreamReader _streamReader = new System.IO.StreamReader(Console.OpenStandardInput());
    private static readonly System.IO.StreamWriter _streamWriter = new System.IO.StreamWriter(Console.OpenStandardOutput());
    private static string[] _input = new string[0];
    private static int _index = 0;

    public static string ReadLine() => _streamReader.ReadLine();

    public static string Read()
    {
        if (_index >= _input.Length)
        {
            _input = ReadLine().Split(' ');
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