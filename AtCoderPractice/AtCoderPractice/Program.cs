using System;
using System.Linq;

namespace AtcoderPractice;

public class Program
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        List<int> a = Console.ReadLine().Split().Select(item=>int.Parse(item)).ToList();
        int ans = Enumerable.Range(0, 100)
            .TakeWhile(_ => {
                if (!a.All(x => x % 2 == 0)){
                    return false;
                }
                a = a.Select(item => item /= 2).ToList();
                return true;
            }).Count();
        Console.WriteLine(ans);
    }
}