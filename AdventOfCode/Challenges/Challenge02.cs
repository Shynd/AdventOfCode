using System;
using System.IO;

namespace AdventOfCode.Challenges
{
    public class Challenge02 : IChallenge
    {
        public int Day => 02;
        public int Year => 2019;
        public string Title => "Program Alarm";

        public string[] Input { get; private set; }

        public bool LoadInput()
        {
            if (File.Exists($"./input/{Day}/input"))
            {
                Input = File.ReadAllLines($"./input/{Day}/input");
                return true;
            }

            return false;
        }

        public void Solve()
        {
            Console.WriteLine($"Challenge {Day}: {Title}");
            Console.WriteLine($"Result part one: {PartOne()}");
            Console.WriteLine($"Result part two: {PartTwo()}");
        }

        public string PartOne()
        {
            return string.Empty;
        }

        public string PartTwo()
        {
            return string.Empty;
        }
    }
}
