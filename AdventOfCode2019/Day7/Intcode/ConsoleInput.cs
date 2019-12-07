using System;

namespace AdventOfCode2019.Day7
{
    public class ConsoleInput : IInput
    {
        public int ReadInput()
        {
            string inputString;
            int input;
            do
            {
                Console.Write("Input: ");
                inputString = Console.ReadLine();
            } while (!int.TryParse(inputString, out input));

            return input;
        }
    }
}
