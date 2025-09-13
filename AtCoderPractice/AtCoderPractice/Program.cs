using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Program
{
    public static void Main()
    {
        int n = ConsoleEx.ReadInt();
        int m = ConsoleEx.ReadInt();
        long L = ConsoleEx.ReadLong();
        long U = ConsoleEx.ReadLong();

        // K レベル（二進セットの段数）。余りはダミーで埋める
        int K = Math.Max(0, (n - m) / m);
        long W = (U - L) / m;                  // 幅（整数）
        long denom = (K >= 1) ? ((1L << K) - 1) : 1L;
        long b = Math.Max(1L, W / denom);      // 最小分解能

        var A = new List<long>(n);
        var anchorIdx = new int[m];            // 各山アンカーのカード index
        var levelIdx = new int[K, m];          // 各レベル k の「山 j 用カード」の index
        int idx = 0;

        // 1) アンカー M 枚： A_j^(0) = L + j*W （j=0..m-1）
        for (int j = 0; j < m; j++)
        {
            long anchor = L + (long)j * W;
            anchor = Math.Clamp(anchor, 1L, U);
            A.Add(anchor);
            anchorIdx[j] = idx++;
        }

        // 2) 調整 K レベル × 各レベル M 枚： w_k = b * 2^k
        for (int k = 0; k < K; k++)
        {
            long wk = b << k;
            if (wk <= 0) wk = b; // 念のため安全策
            for (int j = 0; j < m; j++)
            {
                long val = Math.Min(wk, U); // 念のため上限
                A.Add(val);
                levelIdx[k, j] = idx++;
            }
        }

        // 3) 余りがあれば 1 を詰める（全部捨てる想定）
        while (A.Count < n) A.Add(1);

        // --- A を出力 & flush ---
        ConsoleEx.WriteLine(string.Join(" ", A));
        ConsoleEx.Flush();

        // B 読み込み（昇順）
        long[] B = ConsoleEx.ReadLongArray(m);

        // --- 割当 X を作る（1..m、捨てるなら 0） ---
        int[] X = new int[n]; // 既定 0

        // まず各山にアンカーを割当
        for (int j = 0; j < m; j++) X[anchorIdx[j]] = j + 1;

        if (K >= 1)
        {
            // 各山の差分を b で丸め、K ビットで割当
            long maxMask = (1L << K) - 1;
            for (int j = 0; j < m; j++)
            {
                long anchor = A[anchorIdx[j]];
                long delta = B[j] - anchor;
                if (delta <= 0) continue; // 0 以下なら調整不要（足せないので捨てるのみ）

                long y = delta / b;
                if (y > maxMask) y = maxMask; // 上に溢れない

                for (int k = 0; k < K; k++)
                {
                    if (((y >> k) & 1L) != 0)
                    {
                        int id = levelIdx[k, j];
                        X[id] = j + 1;
                    }
                }
            }
        }

        // 余り（値1など）は全捨て（X=0）

        // --- X を出力 & flush ---
        ConsoleEx.WriteLine(string.Join(" ", X));
        ConsoleEx.Flush();
    }
}

/// Robust Fast I/O
public static class ConsoleEx
{
    private static readonly System.IO.StreamReader _sr =
        new System.IO.StreamReader(Console.OpenStandardInput());
    private static readonly System.IO.StreamWriter _sw =
        new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };

    private static string[] _buf = Array.Empty<string>();
    private static int _idx = 0;

    public static string ReadLine() => _sr.ReadLine();

    public static string Read()
    {
        while (_idx >= _buf.Length)
        {
            var line = ReadLine();
            while (line != null && line.Length == 0) line = ReadLine();
            if (line == null) throw new InvalidOperationException("EOF");
            _buf = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            _idx = 0;
        }
        return _buf[_idx++];
    }

    public static int ReadInt() => int.Parse(Read());
    public static long ReadLong() => long.Parse(Read());
    public static long[] ReadLongArray(int n)
    {
        var a = new long[n];
        for (int i = 0; i < n; i++) a[i] = ReadLong();
        return a;
    }

    public static void WriteLine(object obj) => _sw.WriteLine(obj);
    public static void Flush() => _sw.Flush();
}
