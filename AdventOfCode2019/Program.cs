using System;
using AdventOfCode2019.Day1;
using AdventOfCode2019.Day2;
using AdventOfCode2019.Day3;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var fuelRequirements = FuelRequirements.LoadFromFile("Day1/ModuleMasses.txt").GetFuelRequirements();
            Console.WriteLine($"Day 1 Part 1: {fuelRequirements}");

            var totalFuelRequirements = FuelRequirements.LoadFromFile("Day1/ModuleMasses.txt").GetFuelRequirementsWithFuelForFuel();
            Console.WriteLine($"Day 1 Part 2: {totalFuelRequirements}");

            var day2Part1Result = Intcode.LoadFromFile("Day2/GravityAssistProgram.txt").Repair(1, 12).Repair(2, 2).Execute().ReadMemory(0);
            Console.WriteLine($"Day 2 Part 1: {day2Part1Result}");

            var day2Part2Result = Intcode.FindNounAndVerb("Day2/GravityAssistProgram.txt", 19690720);
            Console.WriteLine($"Day 2 Part 2: {day2Part2Result}");

            var day3Part1Result = WireList.LoadFromFile("Day3/Wires.txt").FindClosestIntersection(0, 1);
            Console.WriteLine($"Day 3 Part 1: {day3Part1Result}");

            var day3Part2Result = WireList.LoadFromFile("Day3/Wires.txt").FindClosestSignalIntersection(0, 1);
            Console.WriteLine($"Day 3 Part 2: {day3Part2Result}");

            Console.ReadKey();
        }
    }
}
