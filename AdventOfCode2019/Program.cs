using System;
using AdventOfCode2019.Day1;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var fuelRequirements = FuelRequirements.LoadFromFile("Day1/ModuleMasses.txt").GetFuelRequirements();
            Console.WriteLine($"Day 1 Part 1: {fuelRequirements}");

            Console.ReadKey();
        }
    }
}
