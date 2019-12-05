using AdventOfCode.Challenges;
using System;

namespace AdventOfCode
{
    class Program
    {
        private static IChallenge Challenge { get; set; }

        static void Main(string[] args)
        {
            if (args.Length < 1)
                ExitWithMessage("please provide a day.");

            Challenge = LoadChallenge(int.Parse(args[0]));
            if (Challenge.LoadInput())
            {
                Challenge.Solve();
            }
            else
            {
                ExitWithMessage($"could not find input file for challenge number {Challenge.Day}");
            }

            Console.ReadLine();
        }

        private static IChallenge LoadChallenge(int day)
        {
            return day switch
            {
                1 => new Challenge01(),
                2 => new Challenge02(),
                3 => new Challenge03(),
                4 => new Challenge04(),

                _ => throw new Exception("puzzle not found!"),
            };
        }

        private static void ExitWithMessage(string message)
        {
            Console.WriteLine(message);
            Environment.Exit(-1);
        }
    }
}
