using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2019.Day09.Intcode;

namespace AdventOfCode2019.Day13
{
    public class Game
    {
        private readonly Intcode _game;
        private readonly IOPipe _gameOutput = new IOPipe();
        private readonly Dictionary<int, Dictionary<int, int>> _canvas = new Dictionary<int, Dictionary<int, int>>();

        private readonly Dictionary<int, char> _characters = new Dictionary<int, char>
        {
            {0, ' '},
            {1, '#'},
            {2, '+'},
            {3, '-'},
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

        public Game Execute()
        {
            ExecuteAsync().Wait();
            return this;
        }

        private async Task ExecuteAsync()
        {
            await _game.Execute();
            
            Console.Clear();
            while (_gameOutput.HasInputToRead())
            {
                var x = (int)await _gameOutput.ReadInput();
                var y = (int)await _gameOutput.ReadInput();
                var character = (int)await _gameOutput.ReadInput();

                Render(x, y, character);
            }
            Console.WriteLine();
        }

        public int CountCharacters(int character)
        {
            var result = _canvas.SelectMany(x => x.Value.Values)
                .Count(c => c == character);

            return result;
        }
        
        private void Render(int x, int y, int character)
        {
            if (!_canvas.ContainsKey(x))
            {
                _canvas.Add(x, new Dictionary<int, int>());
            }

            if (!_canvas[x].ContainsKey(y))
            {
                _canvas[x].Add(y, character);
            }
            else
            {
                _canvas[x][y] = character;
            }
            
            Console.SetCursorPosition(x, y);
            Console.Write(_characters[character]);
        }
    }
}