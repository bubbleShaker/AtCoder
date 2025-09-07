using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AtcoderPractice;

public class StrClass
{
    public static string Reverse(string input)
    {
        return new string(input.Reverse().ToArray());
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string s = Console.ReadLine();
        s = StrClass.Reverse(s);
        List<string> strArray = new List<string> { "dream", "dreamer", "erase", "eraser" };
        List<string> revStrArray = new List<string>();
        foreach(string str in strArray)
        {
            revStrArray.Add(StrClass.Reverse(str));
        }
        while (s.Length > 0)
        {
            bool isEnd = true;
            foreach (string str in revStrArray)
            {
                try
                {
                    string tmpStr = s.Remove(str.Length);
                    if (tmpStr == str)
                    {
                        s = s.Remove(0, str.Length);
                        isEnd = false;
                        break;
                    }
                }
                catch (Exception e) { 
                }
            }
            if (isEnd) break;
        }
        if (s.Length == 0)
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }
}