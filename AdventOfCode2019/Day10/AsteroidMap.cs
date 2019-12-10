using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day10
{
    public class AsteroidMap
    {
        public static AsteroidMap LoadFromFile(string filename)
        {
            var asteroids = File.ReadAllLines(filename)
                .SelectMany(ReadLine)
                .ToList();

            return new AsteroidMap(asteroids);
        }

        private static IEnumerable<Asteroid> ReadLine(string line, int y)
        {
            var x = 0;
            foreach (var c in line)
            {
                if (c == '#')
                {
                    yield return new Asteroid(x, y);
                }

                x += 1;
            }
        }

        private readonly List<Asteroid> _asteroids;

        private AsteroidMap(List<Asteroid> asteroids)
        {
            _asteroids = asteroids;
        }

        public int GetMaximumVisibility()
        {
            return _asteroids
                .Select(a => VisibleFrom(a).Count())
                .OrderByDescending(count => count)
                .First();
        }

        private IEnumerable<Asteroid> VisibleFrom(Asteroid originAsteroid)
        {
            var asteroids = _asteroids.Where(a => a != originAsteroid).ToList();

            foreach (var asteroidToSee in asteroids)
            {
                var otherAsteroids = asteroids.Where(a => a != asteroidToSee);
                if (!otherAsteroids.Any(potentialBlocker => BlocksView(potentialBlocker, asteroidToSee, originAsteroid)))
                {
                    yield return asteroidToSee;
                }
            }
        }

        private bool BlocksView(Asteroid potentialBlocker, Asteroid asteroidToSee, Asteroid originAsteroid)
        {
            var asteroidToSeeX = asteroidToSee.X - originAsteroid.X;
            var asteroidToSeeY = asteroidToSee.Y - originAsteroid.Y;
            var potentialBlockerX = potentialBlocker.X - originAsteroid.X;
            var potentialBlockerY = potentialBlocker.Y - originAsteroid.Y;

            if (asteroidToSeeX >= 0 != potentialBlockerX >= 0
                || asteroidToSeeY >= 0 != potentialBlockerY >= 0)
            {
                return false;
            }
            
            // In the same sector
            // Adjust to be in the positive sector
            if (asteroidToSeeX < 0)
            {
                asteroidToSeeX *= -1;
                potentialBlockerX *= -1;
            }
                
            if (asteroidToSeeY < 0)
            {
                asteroidToSeeY *= -1;
                potentialBlockerY *= -1;
            }
                
            if (potentialBlockerX > asteroidToSeeX || potentialBlockerY > asteroidToSeeY)
            {
                return false;
            }

            return potentialBlockerX * asteroidToSeeY == asteroidToSeeX * potentialBlockerY;
        }

        private class Asteroid
        {
            public int X { get; }
            public int Y { get; }

            public Asteroid(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}