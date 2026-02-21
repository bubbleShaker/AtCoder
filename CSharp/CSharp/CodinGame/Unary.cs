using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        string MESSAGE = Console.ReadLine();
        string asciiString = "";

        foreach (char c in MESSAGE)
        {
            byte[] charByte = System.Text.Encoding.UTF8.GetBytes(new char[] { c });
            BitArray charByteArray = new BitArray(charByte);
            for (int i = 6; i >= 0; i--)
            {
                asciiString += charByteArray[i] ? "1" : "0";
            }
        }

        Console.Error.WriteLine(MESSAGE);
        Console.Error.WriteLine(asciiString);
        
        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine($"{EncodeAsciiString(asciiString)}");
    }

    static string EncodeAsciiString(string asciiString)
    {
        char currentBit = asciiString[0];
        int bitCount = 1;
        string ansString = "";
        for (int i = 1; i < asciiString.Length; i++)
        {
            if (currentBit == asciiString[i])
            {
                bitCount++;
                continue;
            }
            if (asciiString[i - 1] == '0') ansString += "00 ";
            if (asciiString[i - 1] == '1') ansString += "0 ";
            for (int j = 0; j < bitCount; j++)
            {
                ansString += "0";
            }
            ansString += " ";
            bitCount = 1;
            currentBit = asciiString[i];
        }
        if (asciiString[asciiString.Length - 1] == '0') ansString += "00 ";
        if (asciiString[asciiString.Length - 1] == '1') ansString += "0 ";
        for (int j = 0; j < bitCount; j++)
        {
            ansString += "0";
        }
        return ansString;
    }
}