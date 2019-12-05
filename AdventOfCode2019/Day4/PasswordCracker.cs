using System.Linq;

namespace AdventOfCode2019.Day4
{
    class PasswordCracker
    {
        public static int GetPossiblePasswords(string passwordRange)
        {
            var range = passwordRange
                .Split('-')
                .Select(x => int.Parse(x.Trim()))
                .ToArray();

            var min = range[0];
            var max = range[1];

            var possiblePasswords = Enumerable
                .Range(min, max - min + 1)
                .Select(GetChars)
                .Where(AdjacentDigitsMatch)
                .Where(DigitsIncrease)
                .Count();

            return possiblePasswords;
        }

        private static bool AdjacentDigitsMatch(int[] input)
        {
            var lastC = -1;
            foreach(var c in input)
            {
                if (c == lastC)
                {
                    return true;
                }

                lastC = c;
            }

            return false;
        }

        private static bool DigitsIncrease(int[] input)
        {
            var lastC = -1;
            foreach (var c in input)
            {
                if (c < lastC)
                {
                    return false;
                }

                lastC = c;
            }

            return true;
        }

        private static int[] GetChars(int input)
        {
            return input.ToString()
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToArray();
        }
    }
}
