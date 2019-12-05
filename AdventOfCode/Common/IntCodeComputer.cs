using System;

namespace AdventOfCode.Common
{
    public class IntCodeComputer
    {
        private const int FIRST_PARAM = 1;
        private const int SECOND_PARAM = 2;
        private const int RESULT_OFFSET = 3;

        public int[] RAM { get; private set; }
        public int PC { get; private set; }

        public int Output { get; private set; }
        public int? Input { get; set; }

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
            var instruction = ram[pc].ToString().PadLeft(4, '0');
            var opcode = (Opcode) int.Parse(instruction[new Range(2, 4)]);
            var firstParamMode = (ParameterMode)int.Parse(instruction[new Range(1, 2)]);
            var secondParamMode = (ParameterMode)int.Parse(instruction[new Range(0, 1)]);
            // third parameter is omitted

            Console.WriteLine($"[program_counter={pc}] [opcode={opcode}] [firstParamMode={firstParamMode}] [secondParamMode={secondParamMode}]");
            return opcode switch
            {
                Opcode.Add => ((Func<int>)(() =>
                {
                    var inputOne = RetrieveParamValue(firstParamMode, ram, pc + FIRST_PARAM);
                    var inputTwo = RetrieveParamValue(secondParamMode, ram, pc + SECOND_PARAM);

                    Console.WriteLine($"ADD: {inputOne} + {inputTwo} | STORE AT -> {ram[pc + RESULT_OFFSET]}");
                    ram[ram[pc + RESULT_OFFSET]] = inputOne + inputTwo;
                    return 4; // increment PC by 4 (opcode + param1 + param2 + param3)
                }))(),

                Opcode.Mult => ((Func<int>)(() =>
                {
                    var inputOne = RetrieveParamValue(firstParamMode, ram, pc + FIRST_PARAM);
                    var inputTwo = RetrieveParamValue(secondParamMode, ram, pc + SECOND_PARAM);

                    Console.WriteLine($"MULT: {inputOne} * {inputTwo} = {inputOne * inputTwo} | STORE AT -> {ram[pc + RESULT_OFFSET]}");
                    ram[ram[pc + RESULT_OFFSET]] = inputOne * inputTwo;
                    return 4; // increment PC by 4 (opcode + param1 + param2 + param3)
                }))(),

                Opcode.Input => ((Func<int>)(() =>
                {
                    ram[ram[pc + FIRST_PARAM]] = Input ?? int.Parse(Console.ReadLine());
                    return 2; // increment PC by 2 (opcode + only parameter)
                }))(),

                Opcode.Output => ((Func<int>)(() =>
                {
                    Output = ram[ram[pc + FIRST_PARAM]];
                    return 2; // increment PC by 2 (opcode + only parameter)
                }))(),

                /// <summary>
                /// Jumps if first param is NOT zero, otherwise does nothing.
                /// </summary>
                Opcode.Jnz => ((Func<int>)(() =>
                {
                    if (RetrieveParamValue(firstParamMode, ram, pc + FIRST_PARAM) != 0)
                    {
                        return RetrieveParamValue(secondParamMode, ram, pc + SECOND_PARAM) - pc;
                    }
                    return 3; // increment PC by 3 (opcode + param 1 + param 2)
                }))(),

                /// <summary>
                /// Jumps if first param is zero, otherwise does nothing.
                /// </summary>
                Opcode.Jz => ((Func<int>)(() =>
                {
                    if (RetrieveParamValue(firstParamMode, ram, pc + FIRST_PARAM) == 0)
                    {
                        return RetrieveParamValue(secondParamMode, ram, pc + SECOND_PARAM) - pc;
                    }
                    return 3; // increment PC by 3 (opcode + param 1 + param 2)
                }))(),

                Opcode.LessThan => ((Func<int>)(() =>
                {
                    if (RetrieveParamValue(firstParamMode, ram, pc + FIRST_PARAM) <
                        RetrieveParamValue(secondParamMode, ram, pc + SECOND_PARAM))
                        ram[ram[pc + RESULT_OFFSET]] = 1;
                    else
                        ram[ram[pc + RESULT_OFFSET]] = 0;
                    return 4; // increment PC by 4 (opcode + 3 parameters)
                }))(),

                Opcode.Equals => ((Func<int>)(() =>
                {
                    if (RetrieveParamValue(firstParamMode, ram, pc + FIRST_PARAM) ==
                        RetrieveParamValue(secondParamMode, ram, pc + SECOND_PARAM))
                        ram[ram[pc + RESULT_OFFSET]] = 1;
                    else
                        ram[ram[pc + RESULT_OFFSET]] = 0;
                    return 4; // increment PC by 4 (opcode + 3 parameters)
                }))(),

                Opcode.Halt => ((Func<int>)(() =>
                {
                    Console.WriteLine("HALT RECEIVED!");
                    return 0;
                }))(),

                _ => throw new Exception($"UNK OPCODE: {opcode}")
            };
        }

        private int RetrieveParamValue(ParameterMode mode, int[] ram, int pc)
        {
            return mode switch
            {
                ParameterMode.Position => ram[ram[pc]],
                ParameterMode.Immediate => ram[pc],

                _ => throw new Exception("UNK PARAMETERMODE")
            };
        }

        private enum Opcode
        {
            Add = 1,
            Mult = 2,
            Input = 3,
            Output = 4,
            Jnz = 5,
            Jz = 6,
            LessThan = 7,
            Equals = 8,

            Halt = 99
        }

        private enum ParameterMode
        {
            Position = 0,
            Immediate = 1,
        }
    }
}
