using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day03
{
    class WireList
    {
        public static WireList LoadFromFile(string filename)
        {
            var wirepaths = File.ReadAllLines(filename)
                .Select(line => new Wire(line))
                .ToArray();

            return new WireList(wirepaths);
        }

        private Wire[] _wires;
        private WireList(Wire[] wires)
        {
            _wires = wires;
        }

        public int FindClosestIntersection(int firstWireIndex, int secondWireIndex)
        {
            var wire0 = _wires[firstWireIndex];
            var wire1 = _wires[secondWireIndex];

            var intersections = wire0.GetIntersections(wire1).ToList();

            var result = intersections.Min(c => c.Key.ManhattenDistanceFromOrigin());

            return result;
        }

        public int FindClosestSignalIntersection(int firstWireIndex, int secondWireIndex)
        {
            var wire0 = _wires[firstWireIndex];
            var wire1 = _wires[secondWireIndex];

            var intersections = wire0.GetIntersections(wire1).ToList();

            var result = intersections.Min(c => c.Value);

            return result;
        }

        private class Wire
        {
            public readonly List<Coordinate> Locations;
            public Wire(string wireDescription)
            {
                Locations = ParseWire(wireDescription);
            }

            private static List<Coordinate> ParseWire(string wireDescription)
            {
                var coordinates = new List<Coordinate>();

                var location = new Coordinate(0, 0);

                var directions = wireDescription.Split(",");
                foreach (var direction in directions)
                {
                    var dir = direction[0];
                    var distanceToMove = int.Parse(direction.Substring(1));

                    while (distanceToMove > 0)
                    {
                        location = location.NextCoordinate(dir);
                        coordinates.Add(location);
                        distanceToMove -= 1;
                    }
                }

                return coordinates;
            }

            public IEnumerable<KeyValuePair<Coordinate, int>> GetIntersections(Wire wire)
            {
                var locationDictionary = new Dictionary<int, Dictionary<int, int>>();
                var signalDelay = 1;
                foreach (var location in Locations)
                {
                    if (!locationDictionary.ContainsKey(location.X))
                    {
                        locationDictionary.Add(location.X, new Dictionary<int, int>());
                    }

                    if (!locationDictionary[location.X].ContainsKey(location.Y))
                    {
                        locationDictionary[location.X].Add(location.Y, signalDelay);
                    }

                    signalDelay += 1;
                }

                signalDelay = 1;
                foreach (var location in wire.Locations)
                {
                    if (locationDictionary.ContainsKey(location.X)
                        && locationDictionary[location.X].ContainsKey(location.Y))
                    {
                        var thisSignalDelay = locationDictionary[location.X][location.Y];
                        yield return KeyValuePair.Create(location, signalDelay + thisSignalDelay);
                    }

                    signalDelay += 1;
                }
            }
        }

        private class Coordinate
        {
            public readonly int X;
            public readonly int Y;

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Coordinate NextCoordinate(char direction)
            {
                switch (direction)
                {
                    case 'U': return new Coordinate(X, Y + 1);
                    case 'D': return new Coordinate(X, Y - 1);
                    case 'L': return new Coordinate(X - 1, Y);
                    case 'R': return new Coordinate(X + 1, Y);
                }

                throw new Exception ($"Unexpected direction: {direction}");
            }

            public int ManhattenDistanceFromOrigin()
            {
                return Math.Abs(X) + Math.Abs(Y);
            }
        }
    }
}
