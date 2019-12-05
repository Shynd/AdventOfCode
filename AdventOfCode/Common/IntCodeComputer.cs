using System;

namespace AdventOfCode.Common
{
    public class IntCodeComputer
    {
        public int[] RAM { get; private set; }
        public int PC { get; private set; }

        public void LoadProgram(int[] program)
        {
            RAM = new int[program.Length];
            for (var i = 0; i < program.Length; i++)
                RAM[i] = program[i];
        }

        public string Solve(bool partTwo)
        {
            for (var i = 0; RAM[i] != (int) Opcode.Halt; i += Process(RAM, i))
            {

            }

            if (partTwo)
                return string.Empty;
            return RAM[0].ToString();
        }

        private int Process(int[] ram, int pc)
        {
            var opcode = (Opcode)int.Parse(ram[pc].ToString().PadLeft(4, '0'));

            Console.WriteLine($"[program_counter={pc}] [opcode={opcode}]");
            return opcode switch
            {
                Opcode.Add => ((Func<int>)(() =>
                {
                    var inputOne = ram[ram[pc + 1]];
                    var inputTwo = ram[ram[pc + 2]];
                    //Console.WriteLine($"ADD: {inputOne} + {inputTwo} | STORE AT -> {ram[pc + 3]}");
                    ram[ram[pc + 3]] = inputOne + inputTwo;
                    return 4; // increment PC by 4 (opcode + param1 + param2 + param3)
                }))(),

                Opcode.Mult => ((Func<int>)(() =>
                {
                    var inputOne = ram[ram[pc + 1]];
                    var inputTwo = ram[ram[pc + 2]];
                    //Console.WriteLine($"MULT: {inputOne} * {inputTwo} = {inputOne * inputTwo} | STORE AT -> {ram[pc + 3]}");
                    ram[ram[pc + 3]] = inputOne * inputTwo;
                    return 4; // increment PC by 4 (opcode + param1 + param2 + param3)
                }))(),

                Opcode.Halt => ((Func<int>)(() =>
                {
                    Console.WriteLine("HALT RECEIVED!");
                    return 0;
                }))(),

                _ => throw new Exception($"UNK OPCODE: {opcode}")
            };
        }

        private enum Opcode
        {
            Add = 1,
            Mult = 2,

            Halt = 99
        }
    }
}
