using System;

using System.Threading.Tasks;

namespace AdventOfCode2019.Day7
{
    public class ConsoleInput : IInput
    {
        public Task<int> ReadInput()
        {
            string inputString;
            int input;
            do
            {
                Console.Write("Input: ");
                inputString = Console.ReadLine();
            } while (!int.TryParse(inputString, out input));

            return Task.FromResult(input);
        }
    }
}
