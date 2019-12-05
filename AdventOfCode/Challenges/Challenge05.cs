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
            // test programs
            //Input = new string[] { "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9" }; // jump test (position mode)
            //Input = new string[] { "3,3,1105,-1,9,1101,0,0,12,4,12,99,1" }; // jump test (immediate mode)
            //Input = new string[] { "3,9,8,9,10,9,4,9,99,-1,8" }; // equality test (immediate mode)
            //return true;

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
            var resOne = PartOne();
            var resTwo = PartTwo();
            Console.WriteLine($"Result part one: {resOne}");
            Console.WriteLine($"Result part two: {resTwo}");
        }

        public string PartOne()
        {
            var input = Input[0].Split(',');
            var program = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
                program[i] = int.Parse(input[i]);

            Computer = new IntCodeComputer();
            Computer.LoadProgram(program);

            Computer.Input = 1;
            Computer.Solve(false);
            return Computer.Output.ToString();
        }

        public string PartTwo()
        {
            var input = Input[0].Split(',');
            var program = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
                program[i] = int.Parse(input[i]);

            Computer = new IntCodeComputer();
            Computer.LoadProgram(program);

            Computer.Input = 5;
            Computer.Solve(false);
            return Computer.Output.ToString();
        }
    }
}
