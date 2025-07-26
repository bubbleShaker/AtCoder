using System;
using System.Linq;

namespace AtcoderPractice;

public class Program
{
    public static void Main(string[] args)
    {
        var input = Console.ReadLine();
        var ans = input.Count(c => c == '1');
        Console.WriteLine(ans);
    }
}