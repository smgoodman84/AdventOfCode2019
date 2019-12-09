﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day9
{
    class Intcode
    {
        public static Intcode LoadFromFile(string filename)
        {
            var memory = LoadMemoryFromFile(filename);

            return new Intcode(memory);
        }

        private static long[] LoadMemoryFromFile(string filename)
        {
            return File.ReadAllLines(filename)
                .First()
                .Split(",")
                .Select(long.Parse)
                .ToArray();
        }

        const int PositionMode = 0;
        const int ImmediateMode = 1;
        const int RelativeMode = 2;

        private long[] _memory;
        private Dictionary<long, long> _extendedMemory = new Dictionary<long, long>();
        private long _programCounter = 0;
        private long _relativeBase = 0;
        private int[] _parameterModes = new int[3];
        private Dictionary<int, Func<Task>> _operations;
        private IInput _input = new ConsoleInput();
        private IOutput _output = new ConsoleOutput();

        private Intcode(long[] memory)
        {
            _memory = memory;

            _operations = new Dictionary<int, Func<Task>>
            {
                {1, Add },
                {2, Multiply },
                {3, Input },
                {4, Output },
                {5, JumpIfTrue },
                {6, JumpIfFalse },
                {7, LessThan },
                {8, Equals },
                {9, RelativeBaseOffest },
            };
        }

        public Intcode SetInput(IInput input)
        {
            _input = input;
            return this;
        }

        public Intcode SetOutput(IOutput output)
        {
            _output = output;
            return this;
        }

        public Intcode Repair(long memoryLocation, long newValue)
        {
            WriteMemory(memoryLocation, newValue);
            return this;
        }

        private void WriteMemory(long memoryLocation, long newValue)

        {
            if (memoryLocation >= _memory.Length)
            {
                _extendedMemory[memoryLocation] = newValue;
            }
            else
            {
                _memory[memoryLocation] = newValue;
            }
        }

        public long ReadMemory(long memoryLocation)
        {
            if (memoryLocation >= _memory.Length)
            {
                if (_extendedMemory.ContainsKey(memoryLocation))
                {
                    return _extendedMemory[memoryLocation];
                }

                return 0;
            }

            return _memory[memoryLocation];
        }

        public long ReadParameter(int parameterNumber)
        {
            var parameterValue = ReadMemory(_programCounter + parameterNumber);

            switch (_parameterModes[parameterNumber - 1])
            {
                case PositionMode:
                    return ReadMemory(parameterValue);

                case RelativeMode:
                    return ReadMemory(_relativeBase + parameterValue);

                case ImmediateMode:
                default:
                    return parameterValue;
            }
        }

        public void WriteParameter(long parameterNumber, long value)
        {
            var parameterValue = ReadMemory(_programCounter + parameterNumber);

            switch (_parameterModes[parameterNumber - 1])
            {
                case PositionMode:
                     WriteMemory(parameterValue, value);
                    break;

                case RelativeMode:
                    WriteMemory(_relativeBase + parameterValue, value);
                    break;

                case ImmediateMode:
                default:
                    throw new Exception("Invalid Write Mode");
            }
        }

        public async Task<Intcode> Execute()
        {
            while (true)
            {
                var opCode = ReadOpcode();
                if (opCode == 99)
                {
                    break;
                }

                await _operations[opCode]();
            }

            return this;
        }

        private int ReadOpcode()
        {
            var opCode = ReadMemory(_programCounter);

            long divisor = 100;
            for (var param = 0; param < 3; param ++)
            {
                _parameterModes[param] = (int)((opCode / divisor) % 10);
                divisor *= 10;
            }

            return (int)(opCode % 100);
        }

        private async Task Add()
        {
            var result = ReadParameter(1) + ReadParameter(2);
            WriteParameter(3, result);

            _programCounter += 4;

            await Task.CompletedTask;
        }

        private async Task Multiply()
        {
            var result = ReadParameter(1) * ReadParameter(2);
            WriteParameter(3, result);

            _programCounter += 4;

            await Task.CompletedTask;
        }

        private async Task Input()
        {
            var input = await _input.ReadInput();

            WriteParameter(1, input);

            _programCounter += 2;
        }

        private async Task Output()
        {
            _output.Output(ReadParameter(1));

            _programCounter += 2;

            await Task.CompletedTask;
        }

        private async Task JumpIfTrue()
        {
            if (ReadParameter(1) != 0)
            {
                _programCounter = ReadParameter(2);
            }
            else
            {
                _programCounter += 3;
            }

            await Task.CompletedTask;
        }

        private async Task JumpIfFalse()
        {
            if (ReadParameter(1) == 0)
            {
                _programCounter = ReadParameter(2);
            }
            else
            {
                _programCounter += 3;
            }

            await Task.CompletedTask;
        }

        private async Task LessThan()
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

            await Task.CompletedTask;
        }

        private async Task Equals()
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

            await Task.CompletedTask;
        }

        private async Task RelativeBaseOffest()
        {
            _relativeBase += ReadParameter(1);

            _programCounter += 2;

            await Task.CompletedTask;
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
                        .Result
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
