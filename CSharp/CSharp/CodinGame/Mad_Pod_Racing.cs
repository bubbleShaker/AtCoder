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
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
            int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
            int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
            int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
            inputs = Console.ReadLine().Split(' ');
            int opponentX = int.Parse(inputs[0]);
            int opponentY = int.Parse(inputs[1]);
            string thrust = "100";
            const int boostDist = 10000;
            const int bufferDist = 300;
            const int bufferAngle = 15;

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // You have to output the target position
            // followed by the power (0 <= thrust <= 100)
            // i.e.: "x y thrust"            

            if (IsSatisfiedDist(boostDist, bufferDist, nextCheckpointDist) && IsSatisfiedAngle(bufferAngle, nextCheckpointAngle))
            {
                thrust = "BOOST";
            }
            else if (nextCheckpointAngle > 90 || nextCheckpointAngle < -90)
            {
                thrust = "0";
            }
            else
            {
                thrust = "100";
            }
            Console.Error.WriteLine($"nextCheckpointDist is {nextCheckpointDist}");
            Console.WriteLine($"{nextCheckpointX} {nextCheckpointY} {thrust}");
        }
    }

    static bool IsSatisfiedDist(int boostDist, int bufferDist, int nextCheckpointDist)
    {
        return boostDist - bufferDist <= nextCheckpointDist && nextCheckpointDist <= boostDist + bufferDist;
    }

    static bool IsSatisfiedAngle(int bufferAngle, int nextCheckpointAngle)
    {
        return -bufferAngle <= nextCheckpointAngle && nextCheckpointAngle <= bufferAngle;
    }
}