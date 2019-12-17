using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCode2019.Day09.Intcode;

namespace AdventOfCode2019.Day17
{
    public class Scaffold
    {
        private Intcode _ascii;
        private IOPipe _asciiOutput;
        public Scaffold()
        {
            _ascii = Intcode.LoadFromFile("Day17/ASCII.txt");
            _asciiOutput = new IOPipe();
            _ascii.SetOutput(_asciiOutput);
        }

        public int GetSumOfAlignmentParameters()
        {
            var task = GetSumOfAlignmentParametersAsync();
            task.Wait();
            return task.Result;
        }

        public async Task<int> GetSumOfAlignmentParametersAsync()
        {
            await _ascii.Execute();
            var mapInput = new List<char>();
            while (_asciiOutput.HasInputToRead())
            {
                var character = (char)await _asciiOutput.ReadInput();
                mapInput.Add(character);
                Console.Write(character);
            }

            var map = new Map(mapInput);

            var intersections = map.FindIntersections();

            var result = intersections.Sum(i => i.X * i.Y);

            return result;
        }

        private class Map
        {
            private int _height;
            private int _width;

            private char[,] _map;

            public Map(List<char> input)
            {
                _width = input.IndexOf('\n');
                _height = input.Count / _width;

                _map = new char[_width, _height];
                var x = 0;
                var y = 0;
                foreach(var c in input)
                {
                    if (c == '\n')
                    {
                        x = 0;
                        y += 1;
                    }
                    else
                    {
                        _map[x, y] = c;
                        x += 1;
                    }
                }
            }

            private bool IsScaffold(int x, int y)
            {
                if (x < 0 || y < 0 || x >= _width || y >= _height)
                {
                    return false;
                }

                return _map[x, y] == '#';
            }

            public IEnumerable<Point> FindIntersections()
            {
                for(var y = 0; y < _height; y++)
                {
                    for (var x = 0; x < _width; x++)
                    {
                        if (IsScaffold(x,y)
                            && IsScaffold(x + 1, y)
                            && IsScaffold(x - 1, y)
                            && IsScaffold(x, y + 1)
                            && IsScaffold(x, y - 1))
                        {
                            yield return new Point(x, y);
                        }
                    }
                }
            }
        }
    }
}
