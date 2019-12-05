using AdventOfCode.Common;
using System;
using System.IO;

namespace AdventOfCode.Challenges
{
    public class Challenge05 : IChallenge
    {
        public int Day => 05;
        public int Year => 2019;
        public string Title => "Sunny with a Chance of Asteroids";

        public string[] Input { get; private set; }

        private IntCodeComputer Computer { get; set; }

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
            var input = Input[0].Split(',');
            var program = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
                program[i] = int.Parse(input[i]);

            Computer = new IntCodeComputer();
            Computer.LoadProgram(program);

            Computer.Solve(false);
            return Computer.Output.ToString();
        }

        public string PartTwo()
        {
            return string.Empty;
        }
    }
}
