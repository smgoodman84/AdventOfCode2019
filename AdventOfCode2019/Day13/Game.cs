using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2019.Day09.Intcode;

namespace AdventOfCode2019.Day13
{
    public class Game
    {
        private readonly Intcode _game;
        private readonly IOPipe _gameOutput = new IOPipe();
        private readonly IOPipe _gameInput = new IOPipe();
        private readonly Dictionary<int, Dictionary<int, char>> _canvas = new Dictionary<int, Dictionary<int, char>>();

        private readonly Dictionary<int, char> _characters = new Dictionary<int, char>
        {
            {0, ' '},
            {1, '#'},
            {2, '+'},
            {3, '*'},
            {4, 'o'},
        };
        
        public static Game LoadFromFile(string filename)
        {
            return new Game(Intcode.LoadFromFile(filename));
        }

        private Game(Intcode game)
        {
            _game = game;
            _game.SetOutput(_gameOutput);
        }

        public Game Execute(bool render)
        {
            ExecuteAsync(render).Wait();
            return this;
        }

        private async Task ExecuteAsync(bool render)
        {
            _render = render;
            await _game.Execute();

            if (_render)
            {
                Console.Clear();
            }

            while (_gameOutput.HasInputToRead())
            {
                var x = (int)await _gameOutput.ReadInput();
                var y = (int)await _gameOutput.ReadInput();
                var character = (int)await _gameOutput.ReadInput();

                Render(x, y, _characters[character]);
            }

            if (_render)
            {
                Console.WriteLine();
            }
        }

        public Game ExecuteWithInput(bool render)
        {
            ExecuteWithInputAsync(render).Wait();
            return this;
        }

        private int _score = 0;
        private void SetScore(int score)
        {
            _score = score;
            var x = 0;
            foreach (var c in $"Score: {_score.ToString().PadLeft(10, '0')}")
            {
                Render(x, 0, c);
                x += 1;
            }
        }

        public int GetScore()
        {
            return _score;
        }

        private int _paddleX;
        private int _ballX;
        private Task _gameTask;
        private bool _render;
        private async Task ExecuteWithInputAsync(bool render)
        {
            _render = render;
            _game.Repair(0, 2);
            _game.SetInput(_gameInput);
            _gameTask = _game.Execute();

            var inputTask = HandleInput();

            if (_render)
            {
                Console.Clear();
            }
            SetScore(0);
            while (!_gameTask.IsCompleted || _gameOutput.HasInputToRead())
            {
                if (_gameTask.IsCompleted)
                {
                    var stop = true;
                }
                var x = (int)await _gameOutput.ReadInput();
                var y = (int)await _gameOutput.ReadInput();
                var character = (int)await _gameOutput.ReadInput();

                switch (character)
                {
                    case 3: _paddleX = x; break;
                    case 4: _ballX = x; break;
                }

                if (x == -1)
                {
                    SetScore(character);
                }
                else
                {
                    Render(x, y + 2, _characters[character]);
                }
            }
            if (_render)
            {
                Console.WriteLine();
            }
        }


        private IEnumerable<int> Left(int count) => RepeatingList(-1, count);
        private IEnumerable<int> Right(int count) => RepeatingList(1, count);
        private IEnumerable<int> Stop(int count) => RepeatingList(0, count);

        private IEnumerable<int> RepeatingList(int value, int count)
        {
            while (count > 0)
            {
                yield return value;
                count -= 1;
            }
        }

        private IEnumerable<int> ParseInputLine(string line)
        {
            var direction = line[0];
            var count = int.Parse(line.Substring(1));
            switch(direction)
            {
                case 'L': return Left(count);
                case 'R': return Right(count);
                case 'S': return Stop(count);
            }
            return new List<int>();
        }

        public async Task HandleInput()
        {
            await Task.Run(async () => 
            {
                var i = 0;
                var inputFile = "Day13/GameInput.txt";
                var preparedInput = File.ReadAllLines(inputFile)
                    .Select(ParseInputLine)
                    .SelectMany(x => x)
                    .ToArray();

                while (!_gameTask.IsCompleted)
                {
                    var output = 0;
                    
                    if (i < preparedInput.Length)
                    {
                        output = preparedInput[i];
                        i += 1;
                    }
                    else
                    {
                        if (true)
                        {

                            if (_paddleX < _ballX)
                            {
                                File.AppendAllLines(inputFile, new[] { "R1" });
                                output = 1;
                            }
                            else if (_ballX < _paddleX)
                            {
                                output = -1;
                                File.AppendAllLines(inputFile, new[] { "L1" });
                            }
                            else
                            {
                                output = 0;
                                File.AppendAllLines(inputFile, new[] { "S1" });
                            }
                        }
                        else
                        {
                            var input = Console.ReadKey();
                            switch (input.Key)
                            {
                                case ConsoleKey.LeftArrow:
                                    output = -1;
                                    File.AppendAllLines(inputFile, new[] { "L1" });
                                    break;
                                case ConsoleKey.RightArrow:
                                    File.AppendAllLines(inputFile, new[] { "R1" });
                                    output = 1;
                                    break;
                            }
                        }
                    }
                    
                    _gameInput.Output(output);
                    await Task.Delay(1);
                }
            });
        }

        public int CountCharacters(char character)
        {
            var result = _canvas.SelectMany(x => x.Value.Values)
                .Count(c => c == character);

            return result;
        }
        
        private void Render(int x, int y, char character)
        {
            if (!_canvas.ContainsKey(x))
            {
                _canvas.Add(x, new Dictionary<int, char>());
            }

            if (!_canvas[x].ContainsKey(y))
            {
                _canvas[x].Add(y, character);
            }
            else
            {
                _canvas[x][y] = character;
            }

            if (!_render)
            {
                return;
            }

            Console.SetCursorPosition(x, y + 1);
            Console.Write(character);
            Console.SetCursorPosition(0, 0);
        }
    }
}