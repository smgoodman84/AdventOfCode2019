using System;
using AdventOfCode2019.Day1;
using AdventOfCode2019.Day2;
using AdventOfCode2019.Day3;
using AdventOfCode2019.Day4;
using AdventOfCode2019.Day6;
using AdventOfCode2019.Day7;
using AdventOfCode2019.Day8;

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

            var day2Part1Result = Day2.Intcode.LoadFromFile("Day2/GravityAssistProgram.txt").Repair(1, 12).Repair(2, 2).Execute().ReadMemory(0);
            Console.WriteLine($"Day 2 Part 1: {day2Part1Result}");

            var day2Part2Result = Day2.Intcode.FindNounAndVerb("Day2/GravityAssistProgram.txt", 19690720);
            Console.WriteLine($"Day 2 Part 2: {day2Part2Result}");

            var day3Part1Result = WireList.LoadFromFile("Day3/Wires.txt").FindClosestIntersection(0, 1);
            Console.WriteLine($"Day 3 Part 1: {day3Part1Result}");

            var day3Part2Result = WireList.LoadFromFile("Day3/Wires.txt").FindClosestSignalIntersection(0, 1);
            Console.WriteLine($"Day 3 Part 2: {day3Part2Result}");

            var day4Part1Result = PasswordCracker.GetPossiblePasswords("152085-670283");
            Console.WriteLine($"Day 4 Part 1: {day4Part1Result}");

            var day4Part2Result = PasswordCracker.GetPossiblePasswordsWithDigitPair("152085-670283");
            Console.WriteLine($"Day 4 Part 2: {day4Part2Result}");

            Console.WriteLine("Running program for Day 5 Part 1 - Input should be 1");
            AdventOfCode2019.Day5.Intcode.LoadFromFile("Day5/Diagnostics.txt").Execute();

            Console.WriteLine("Running program for Day 5 Part 2 - Input should be 5");
            AdventOfCode2019.Day5.Intcode.LoadFromFile("Day5/Diagnostics.txt").Execute();

            var day6Part1Result = OrbitMap.LoadFromFile("Day6/Orbits.txt").CountOrbits();
            Console.WriteLine($"Day 6 Part 1: {day6Part1Result}");
            
            var day6Part2Result = OrbitMap.LoadFromFile("Day6/Orbits.txt").CalculateOrbitalTransfers("YOU", "SAN");
            Console.WriteLine($"Day 6 Part 2: {day6Part2Result}");

            var day7Part1Result = SignalMaximizer.GetMaximumThrustSignal();
            Console.WriteLine($"Day 7 Part 1: {day7Part1Result}");
                      
            var day7Part2Result = SignalMaximizer.GetMaximumFeedbackThrustSignal().Result;
            Console.WriteLine($"Day 7 Part 2: {day7Part2Result}");

            var day8Part1Result = SpaceImage.LoadFromFile("Day8/SpaceImage.txt", 25, 6).FindLayerWithFewestZerosAndGetNumberOfOnesTimeNumberOfTwos();
            Console.WriteLine($"Day 8 Part 1: {day8Part1Result}");

            Console.WriteLine($"Day 8 Part 2:");
            SpaceImage.LoadFromFile("Day8/SpaceImage.txt", 25, 6).RenderImage();

            Console.WriteLine($"Day 9 Part 1:");
            Day9.Intcode.LoadFromFile("Day9/Boost.txt").SetInput(new Day9.PreparedInput(1)).Execute().Wait();

            Console.ReadKey();
        }
    }
}
