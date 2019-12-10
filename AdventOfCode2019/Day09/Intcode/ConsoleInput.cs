using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day09.Intcode
{
    public class ConsoleInput : IInput
    {
        public Task<long> ReadInput()
        {
            string inputString;
            long input;
            do
            {
                Console.Write("Input: ");
                inputString = Console.ReadLine();
            } while (!long.TryParse(inputString, out input));

            return Task.FromResult(input);
        }
    }
}
