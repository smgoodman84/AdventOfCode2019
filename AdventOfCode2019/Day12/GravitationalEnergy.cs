using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day12
{
    class GravitationalEnergy
    {
        public static GravitationalEnergy LoadFromFile(string filename)
        {
            var moons = File.ReadAllLines(filename)
                .Select((line, lineNumber) => new Moon(line, lineNumber));

            return new GravitationalEnergy(moons);
        }

        private readonly IEnumerable<Moon> _moons;

        public GravitationalEnergy(IEnumerable<Moon> moons)
        {
            _moons = moons.ToList();
        }

        public GravitationalEnergy Simulate(int iterations)
        {
            foreach(var iteration in Enumerable.Range(1, iterations))
            {
                ApplyGravity();
                ApplyVelocity();
            }
            return this;
        }

        public int GetTotalEnergy() => _moons.Sum(m => m.TotalEnergy);

        private void ApplyVelocity()
        {
            foreach(var moon in _moons)
            {
                moon.ApplyVelocity();
            }
        }

        private void ApplyGravity()
        {
            foreach(var moon1 in _moons)
            {
                foreach(var moon2 in _moons.Where(m => m.Id > moon1.Id))
                {
                    if (moon1.Position.X > moon2.Position.X)
                    {
                        moon1.Velocity.X -= 1;
                        moon2.Velocity.X += 1;
                    }
                    if (moon1.Position.X < moon2.Position.X)
                    {
                        moon1.Velocity.X += 1;
                        moon2.Velocity.X -= 1;
                    }

                    if (moon1.Position.Y > moon2.Position.Y)
                    {
                        moon1.Velocity.Y -= 1;
                        moon2.Velocity.Y += 1;
                    }
                    if (moon1.Position.Y < moon2.Position.Y)
                    {
                        moon1.Velocity.Y += 1;
                        moon2.Velocity.Y -= 1;
                    }

                    if (moon1.Position.Z > moon2.Position.Z)
                    {
                        moon1.Velocity.Z -= 1;
                        moon2.Velocity.Z += 1;
                    }
                    if (moon1.Position.Z < moon2.Position.Z)
                    {
                        moon1.Velocity.Z += 1;
                        moon2.Velocity.Z -= 1;
                    }
                }
            }
        }
    }
}
