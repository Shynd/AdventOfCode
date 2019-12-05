using AdventOfCode.Common;
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

            var input = Input[0].Split(',');
            var program = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
                program[i] = int.Parse(input[i]);

            // restore gravity assist program
            program[1] = 12;
            program[2] = 2;

            Computer = new IntCodeComputer();
            Computer.LoadProgram(program);

            Console.WriteLine($"Result part one: {PartOne()}");
            Console.WriteLine($"Result part two: {PartTwo()}");
        }

        public string PartOne() => Computer.Solve(false);

        public string PartTwo() => string.Empty;
    }
}
