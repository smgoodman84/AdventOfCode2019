using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day2
{
    class Intcode
    {
        public static Intcode LoadFromFile(string filename)
        {
            var memory = File.ReadAllLines(filename)
                .First()
                .Split(",")
                .Select(int.Parse)
                .ToArray();

            return new Intcode(memory);
        }


        private int[] _memory;
        private int _programCounter = 0;
        private Dictionary<int, Action> _operations;

        private Intcode(int[] memory)
        {
            _memory = memory;

            _operations = new Dictionary<int, Action>
            {
                {1, Add },
                {2, Multiply },
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

        public Intcode Execute()
        {
            while (true)
            {
                var opCode = _memory[_programCounter];
                if (opCode == 99)
                {
                    break;
                }

                _operations[opCode]();
            }

            return this;
        }

        private void Add()
        {
            var aPointer = _memory[_programCounter + 1];
            var bPointer = _memory[_programCounter + 2];
            var cPointer = _memory[_programCounter + 3];

            _memory[cPointer] = _memory[aPointer] + _memory[bPointer];

            _programCounter += 4;
        }

        private void Multiply()
        {
            var aPointer = _memory[_programCounter + 1];
            var bPointer = _memory[_programCounter + 2];
            var cPointer = _memory[_programCounter + 3];

            _memory[cPointer] = _memory[aPointer] * _memory[bPointer];

            _programCounter += 4;
        }
    }
}
