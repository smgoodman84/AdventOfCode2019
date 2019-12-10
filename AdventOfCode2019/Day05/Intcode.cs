using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day05
{
    class Intcode
    {
        public static Intcode LoadFromFile(string filename)
        {
            var memory = LoadMemoryFromFile(filename);

            return new Intcode(memory);
        }

        private static int[] LoadMemoryFromFile(string filename)
        {
            return File.ReadAllLines(filename)
                .First()
                .Split(",")
                .Select(int.Parse)
                .ToArray();
        }

        const int PositionMode = 0;
        const int ImmediateMode = 1;

        private int[] _memory;
        private int _programCounter = 0;
        private int[] _parameterModes = new int[3];
        private Dictionary<int, Action> _operations;

        private Intcode(int[] memory)
        {
            _memory = memory;

            _operations = new Dictionary<int, Action>
            {
                {1, Add },
                {2, Multiply },
                {3, Input },
                {4, Output },
                {5, JumpIfTrue },
                {6, JumpIfFalse },
                {7, LessThan },
                {8, Equals },
            };
        }

        public Intcode Repair(int memoryLocation, int newValue)
        {
            _memory[memoryLocation] = newValue;
            return this;
        }

        public int ReadMemory(int memoryLocation)
        {
            return _memory[memoryLocation];
        }

        public int ReadParameter(int parameterNumber)
        {
            var parameterValue = _memory[_programCounter + parameterNumber];

            switch (_parameterModes[parameterNumber - 1])
            {
                case PositionMode:
                    return _memory[parameterValue];

                case ImmediateMode:
                default:
                    return parameterValue;
            }
        }

        public void WriteParameter(int parameterNumber, int value)
        {
            var parameterValue = _memory[_programCounter + parameterNumber];

            switch (_parameterModes[parameterNumber - 1])
            {
                case PositionMode:
                     _memory[parameterValue] = value;
                    break;

                case ImmediateMode:
                default:
                    throw new Exception("Invalid Write Mode");
            }
        }

        public Intcode Execute()
        {
            while (true)
            {
                var opCode = ReadOpcode();
                if (opCode == 99)
                {
                    break;
                }

                _operations[opCode]();
            }

            return this;
        }

        private int ReadOpcode()
        {
            var opCode = _memory[_programCounter];

            var divisor = 100;
            for (var param = 0; param < 3; param ++)
            {
                _parameterModes[param] = (opCode / divisor) % 10;
                divisor *= 10;
            }

            return opCode % 100;
        }

        private void Add()
        {
            var result = ReadParameter(1) + ReadParameter(2);
            WriteParameter(3, result);

            _programCounter += 4;
        }

        private void Multiply()
        {
            var result = ReadParameter(1) * ReadParameter(2);
            WriteParameter(3, result);

            _programCounter += 4;
        }

        private void Input()
        {
            string inputString;
            int input;
            do
            {
                Console.Write("Input: ");
                inputString = Console.ReadLine();
            } while (!int.TryParse(inputString, out input));

            WriteParameter(1, input);

            _programCounter += 2;
        }

        private void Output()
        {
            Console.WriteLine($"Output: {ReadParameter(1)}");

            _programCounter += 2;
        }

        private void JumpIfTrue()
        {
            if (ReadParameter(1) != 0)
            {
                _programCounter = ReadParameter(2);
            }
            else
            {
                _programCounter += 3;
            }
        }

        private void JumpIfFalse()
        {
            if (ReadParameter(1) == 0)
            {
                _programCounter = ReadParameter(2);
            }
            else
            {
                _programCounter += 3;
            }
        }

        private void LessThan()
        {
            if (ReadParameter(1) < ReadParameter(2))
            {
                WriteParameter(3, 1);
            }
            else
            {
                WriteParameter(3, 0);
            }

            _programCounter += 4;
        }

        private void Equals()
        {
            if (ReadParameter(1) == ReadParameter(2))
            {
                WriteParameter(3, 1);
            }
            else
            {
                WriteParameter(3, 0);
            }

            _programCounter += 4;
        }

        public static int FindNounAndVerb(string filename, int targetResult)
        {
            var memory = LoadMemoryFromFile(filename);

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var result = new Intcode(memory.ToArray())
                        .Repair(1, noun)
                        .Repair(2, verb)
                        .Execute()
                        .ReadMemory(0);

                    if (result == targetResult)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return -1;
        }
    }
}
