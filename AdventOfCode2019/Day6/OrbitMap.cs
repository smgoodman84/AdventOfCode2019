using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2019.Day6
{
    class OrbitMap
    {
        public static OrbitMap LoadFromFile(string filename)
        {
            var orbits = File.ReadAllLines(filename)
                .Select(Orbit.Parse);

            return new OrbitMap(orbits);
        }

        private readonly IEnumerable<Orbit> _orbits;

        private OrbitMap(IEnumerable<Orbit> orbits)
        {
            _orbits = orbits;
        }

        public int CountOrbits()
        {
            return new OrbitCounter(_orbits).GetOrbitCount();
        }

        public int CalculateOrbitalTransfers(string start, string end)
        {
            var orbiterDictionary = _orbits.ToDictionary(o => o.Orbiter, o => o.Orbited);
            var startPath = GetPath(orbiterDictionary, start);
            var endPath = GetPath(orbiterDictionary, end);

            var commonPath = new List<string>();
            var index = 0;
            while (startPath[index] == endPath[index])
            {
                commonPath.Add(startPath[index]);
                index += 1;
            }

            return startPath.Length + endPath.Length - (commonPath.Count() * 2);
        }

        private string[] GetPath(Dictionary<string,string> orbiterDictionary, string location)
        {
            var path = new List<string>();
            while (orbiterDictionary.ContainsKey(location))
            {
                location = orbiterDictionary[location];
                path.Add(location);
            }
            return path.ToArray().Reverse().ToArray();
        }

        private class OrbitCounter
        {
            private Dictionary<string, List<string>> _orbitTree;
            private Dictionary<string, int> _orbitCounts = new Dictionary<string, int>();
            public OrbitCounter(IEnumerable<Orbit> orbits)
            {
                _orbitTree = new Dictionary<string, List<string>>();
                foreach (var orbit in orbits)
                {
                    if (!_orbitTree.ContainsKey(orbit.Orbited))
                    {
                        _orbitTree.Add(orbit.Orbited, new List<string>());
                    }

                    _orbitTree[orbit.Orbited].Add(orbit.Orbiter);
                }
            }

            public int GetOrbitCount()
            {
                return _orbitTree.Keys.Sum(CalculateOrbitCount);
            }

            private int CalculateOrbitCount(string orbited)
            {
                if (_orbitCounts.ContainsKey(orbited))
                {
                    return _orbitCounts[orbited];
                }

                var result = 0;
                if (_orbitTree.ContainsKey(orbited))
                {
                    foreach (var orbiter in _orbitTree[orbited])
                    {
                        result += 1 + CalculateOrbitCount(orbiter);
                    }
                }

                _orbitCounts.Add(orbited, result);

                return result;
            }
        }

        private class Orbit
        {
            public string Orbiter { get; private set; }
            public string Orbited { get; private set; }
            public static Orbit Parse(string orbit)
            {
                var objects = orbit.Split(")");
                return new Orbit()
                {
                    Orbited = objects[0],
                    Orbiter = objects[1]
                };
            }
        }
    }
}
