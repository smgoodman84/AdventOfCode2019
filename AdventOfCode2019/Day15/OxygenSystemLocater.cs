using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using AdventOfCode2019.Day09.Intcode;

namespace AdventOfCode2019.Day15
{
    class OxygenSystemLocater
    {
        private Intcode _repairDroid { get; set; }
        private IOPipe _repairDroidInput { get; set; }
        private IOPipe _repairDroidOutput { get; set; }
        private Map _map { get; set; }
        private Point _position = new Point(0, 0); 

        private enum Direction
        {
            North = 1,
            South = 2,
            West = 3,
            East = 4
        }

        private enum StatusCode
        {
            HitWall = 0,
            Moved = 1,
            FoundOxygenSystem = 2
        }

        public OxygenSystemLocater()
        {
            _repairDroid = Intcode.LoadFromFile("Day15/RepairDroid.txt");
            _repairDroidInput = new IOPipe();
            _repairDroidOutput = new IOPipe();
            _repairDroid.SetInput(_repairDroidInput);
            _repairDroid.SetOutput(_repairDroidOutput);
            _map = new Map();
        }

        public int GetShortestRoutToOxygen()
        {
            Explore().Wait();
            return _oxygenToHomePath.Count;
        }

        private async Task Explore()
        {
            _map.MapLocation(_position, Map.MapStatus.Passable);
            _repairDroid.Execute();
            await ExploreAllDirectionsAndReturn();
            _map.Render(_position, _trail);

            foreach (var direction in _oxygenToHomePath)
            {
                //Console.WriteLine($"{direction}");
            }
        }

        private async Task ExploreAllDirectionsAndReturn()
        {
            await Explore(Direction.North, Direction.South);
            await Explore(Direction.East, Direction.West);
            await Explore(Direction.South, Direction.North);
            await Explore(Direction.West, Direction.East);
        }

        private List<Direction> _oxygenToHomePath = new List<Direction>();
        private List<Point> _trail = new List<Point>();

        private async Task Explore(Direction direction, Direction reverseDirection)
        {
            var moved = false;

            if (!_foundOxygen && _map.GetMapStatus(GetNewLocation(direction)) == Map.MapStatus.Unmapped)
            {
                moved = await TryMove(direction);
                if (moved)
                {
                    //Console.WriteLine($"Moved {direction}");
                    await ExploreAllDirectionsAndReturn();
                }
            }

            if (moved)
            {
                //Console.WriteLine($"Return  {reverseDirection}");
                await TryMove(reverseDirection);
                if (_foundOxygen)
                {
                    _oxygenToHomePath.Add(reverseDirection);
                    _trail.Add(_position);
                }
            }
        }

        private bool _foundOxygen = false;

        private async Task<bool> TryMove(Direction direction)
        {
            _repairDroidInput.Output((int)direction);

            var newLocation = GetNewLocation(direction);
            var result = (StatusCode)await _repairDroidOutput.ReadInput();
            switch (result)
            {
                case StatusCode.HitWall:
                    _map.MapLocation(newLocation, Map.MapStatus.Wall);
                    return false;
                case StatusCode.FoundOxygenSystem:
                    _foundOxygen = true;
                    _map.MapLocation(newLocation, Map.MapStatus.Oxygen);
                    _position = newLocation;
                    return true;
                case StatusCode.Moved:
                    _map.MapLocation(newLocation, Map.MapStatus.Passable);
                    _position = newLocation;
                    return true;
            }

            throw new Exception($"Unexpected Status Code {result}");
        }

        private Point GetNewLocation(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Point(_position.X, _position.Y + 1);
                case Direction.East:
                    return new Point(_position.X + 1, _position.Y);
                case Direction.South:
                    return new Point(_position.X, _position.Y - 1);
                case Direction.West:
                    return new Point(_position.X - 1, _position.Y);
            }

            throw new Exception($"Unexpected Direction {direction}");
        }

        private class Map
        {
            public enum MapStatus
            {
                Unmapped,
                Wall,
                Passable,
                Oxygen
            }

            private Dictionary<int,Dictionary<int,MapStatus>> _map = new Dictionary<int, Dictionary<int, MapStatus>>();
            public void MapLocation(Point location, MapStatus status)
            {
                if (!_map.ContainsKey(location.X))
                {
                    _map.Add(location.X, new Dictionary<int, MapStatus>());
                }
                if (!_map[location.X].ContainsKey(location.Y))
                {
                    _map[location.X][location.Y] = status;
                }
                else
                {
                    //throw new Exception($"Already mapped {location.X},{location.Y}");
                }
            }

            public MapStatus GetMapStatus(Point location)
            {
                if (!_map.ContainsKey(location.X))
                {
                    return MapStatus.Unmapped;
                }
                if (!_map[location.X].ContainsKey(location.Y))
                {
                    return MapStatus.Unmapped;
                }

                return _map[location.X][location.Y];
            }

            public void Render(Point droidPosition, List<Point> trail)
            {
                var xMin = _map.Keys.Min();
                var xMax = _map.Keys.Max();
                var yMin = _map.SelectMany(x => x.Value.Keys).Min();
                var yMax = _map.SelectMany(x => x.Value.Keys).Max();

                Console.WriteLine($"Rendering {xMin},{yMin} -> {xMax},{yMax}");

                for(var y = yMax; y >= yMin; y--)
                {
                    for (var x = xMin; x <= xMax; x++)
                    {
                        if (x == droidPosition.X && y == droidPosition.Y)
                        {
                            Console.Write("D");
                        }
                        else if (trail.Any(t => t.X == x && t.Y == y))
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            switch (GetMapStatus(new Point(x, y)))
                            {
                                case MapStatus.Oxygen:
                                    Console.Write("o");
                                    break;
                                case MapStatus.Passable:
                                    Console.Write(" ");
                                    break;
                                case MapStatus.Wall:
                                    Console.Write("#");
                                    break;
                                case MapStatus.Unmapped:
                                    Console.Write("?");
                                    break;
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
