using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2019.Day16
{
    class FlawedFrequencyTransmission
    {
        public static FlawedFrequencyTransmission LoadFromFile(string filename)
        {
            var signal = File.ReadAllText(filename)
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()));

            return new FlawedFrequencyTransmission(signal);
        }

        private readonly List<int[]> _signal = new List<int[]>();
        private int _signalLength;

        public FlawedFrequencyTransmission(IEnumerable<int> signal)
        {
            var signalArray = signal.ToArray();
            _signalLength = signalArray.Length;
            _signal.Add(signalArray);
        }

        public string ProcessSignal(int phases)
        {
            for (var phase = 0; phase < phases; phase++)
            {
                var phaseOutput = new int[_signalLength];
                for (var row = 0; row < _signalLength; row++)
                {
                    var input = _signal[phase];
                    var pattern = PatternForOutputElement(row);

                    var index = 0;
                    var total = 0;
                    foreach(var patternElement in pattern)
                    {
                        //Console.Write($"{input[index]}*{patternElement} + ");
                        total += input[index] * patternElement;
                        index += 1;

                        if (index >= input.Length)
                        {
                            break;
                        }
                    }
                    phaseOutput[row] = Math.Abs(total) % 10;
                    //Console.WriteLine($" = {total} = {phaseOutput[row]}");
                    //Console.WriteLine(string.Join(",", pattern));
                }
                _signal.Add(phaseOutput);
            }

            return string.Join("", _signal[phases].Take(8));
        }

        private IEnumerable<int> PatternForOutputElement(int elementNumber)
        {
            var basePattern = new[] { 0, 1, 0, -1 };
            for (var index = 1; true; index++)
            {
                yield return basePattern[(index / (elementNumber + 1)) % 4];
            }
        }
    }
}
