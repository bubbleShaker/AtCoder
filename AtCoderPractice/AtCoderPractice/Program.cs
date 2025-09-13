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
            if (currentPowerOf2 > u - l || currentPowerOf2 <= 0) 
            {
                currentPowerOf2 = 1;
            }
        }

        // 1.3: Print the generated card values
        ConsoleEx.WriteLine(string.Join(" ", cardsToCreate.Select(c => c.value)));
        ConsoleEx.Flush();

        // --- Stage 2: Read Targets (B) and Assign Cards (X) ---
        var targetsB = ConsoleEx.ReadLongArray(m);

        var finalAssignments = new int[n + 1];
        long[] mountainSums = new long[m];

        var baseCards = cardsToCreate.GetRange(0, m);
        var adjustmentCards = cardsToCreate.GetRange(m, n - m);

        // Assign base cards
        for (int i = 0; i < m; i++)
        {
            var card = baseCards[i];
            mountainSums[i] += card.value;
            finalAssignments[card.originalIndex] = i + 1;
        }

        // --- Initial Solution via Greedy ---
        var currentAdjAssignments = new int[n - m];
        long[] greedySums = (long[])mountainSums.Clone();
        
        adjustmentCards.Sort((a, b) => b.value.CompareTo(a.value));
        bool[] usedInGreedy = new bool[n - m];

        for (int i = 0; i < adjustmentCards.Count; i++)
        {
            var card = adjustmentCards[i];
            int bestMountain = -1;
            long maxNeed = -1;
            for (int j = 0; j < m; j++){
                if (greedySums[j] + card.value <= targetsB[j]){
                    long need = targetsB[j] - greedySums[j];
                    if (need > maxNeed){ maxNeed = need; bestMountain = j; }
                }
            }
            if (bestMountain != -1){
                greedySums[bestMountain] += card.value;
                currentAdjAssignments[i] = bestMountain + 1;
                usedInGreedy[i] = true;
            }
        }
        for (int i = 0; i < adjustmentCards.Count; i++){
            if (usedInGreedy[i]) continue;
            var card = adjustmentCards[i];
            int bestMountain = -1;
            double maxImprovement = 0;
            for (int j = 0; j < m; j++){
                double greedyCurrentError = Math.Abs((double)greedySums[j] - targetsB[j]);
                double newError = Math.Abs((double)greedySums[j] + card.value - targetsB[j]);
                double improvement = greedyCurrentError - newError;
                if (improvement > maxImprovement){ maxImprovement = improvement; bestMountain = j; }
            }
            if (bestMountain != -1){
                greedySums[bestMountain] += card.value;
                currentAdjAssignments[i] = bestMountain + 1;
            }
        }

        // --- Simulated Annealing ---
        var rand = new Random();
        double T_start = 1e12;
        double T_end = 1e6;
        double timeLimit = 1.95;

        long[] currentSums = (long[])mountainSums.Clone();
        for(int i=0; i<adjustmentCards.Count; i++)
        {
            if(currentAdjAssignments[i] > 0)
            {
                currentSums[currentAdjAssignments[i]-1] += adjustmentCards[i].value;
            }
        }

        long currentError = 0;
        for(int i=0; i<m; i++) currentError += Math.Abs(currentSums[i] - targetsB[i]);

        long bestError = currentError;
        var bestAdjAssignments = (int[])currentAdjAssignments.Clone();

        while (stopwatch.Elapsed.TotalSeconds < timeLimit)
        {
            double timeRatio = stopwatch.Elapsed.TotalSeconds / timeLimit;
            double temp = T_start * Math.Pow(T_end / T_start, timeRatio);

            int cardIndex = rand.Next(adjustmentCards.Count);
            var card = adjustmentCards[cardIndex];
            int currentMount = currentAdjAssignments[cardIndex];
            int newMount = rand.Next(m + 1);

            if (currentMount == newMount) continue;

            long oldMountSum = (currentMount == 0) ? 0 : currentSums[currentMount - 1];
            long newMountSum = (newMount == 0) ? 0 : currentSums[newMount - 1];

            long deltaError = 0;
            if(currentMount > 0) deltaError -= Math.Abs(oldMountSum - targetsB[currentMount-1]);
            if(newMount > 0) deltaError -= Math.Abs(newMountSum - targetsB[newMount-1]);

            if(currentMount > 0) deltaError += Math.Abs(oldMountSum - card.value - targetsB[currentMount-1]);
            if(newMount > 0) deltaError += Math.Abs(newMountSum + card.value - targetsB[newMount-1]);

            if (deltaError < 0 || rand.NextDouble() < Math.Exp(-deltaError / temp))
            {
                currentError += deltaError;
                if(currentMount > 0) currentSums[currentMount-1] -= card.value;
                if(newMount > 0) currentSums[newMount-1] += card.value;
                currentAdjAssignments[cardIndex] = newMount;

                if (currentError < bestError)
                {
                    bestError = currentError;
                    bestAdjAssignments = (int[])currentAdjAssignments.Clone();
                }
            }
        }

        // Apply best found assignments
        for(int i=0; i<adjustmentCards.Count; i++)
        {
            finalAssignments[adjustmentCards[i].originalIndex] = bestAdjAssignments[i];
        }

        // Print final result
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