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
