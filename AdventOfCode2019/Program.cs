using System;
using AdventOfCode2019.Day01;
using AdventOfCode2019.Day03;
using AdventOfCode2019.Day04;
using AdventOfCode2019.Day06;
using AdventOfCode2019.Day07;
using AdventOfCode2019.Day08;
using AdventOfCode2019.Day09.Intcode;
using Intcode = AdventOfCode2019.Day02.Intcode;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var fuelRequirements = FuelRequirements.LoadFromFile("Day01/ModuleMasses.txt").GetFuelRequirements();
            Console.WriteLine($"Day 1 Part 1: {fuelRequirements}");

            var totalFuelRequirements = FuelRequirements.LoadFromFile("Day01/ModuleMasses.txt").GetFuelRequirementsWithFuelForFuel();
            Console.WriteLine($"Day 1 Part 2: {totalFuelRequirements}");

            var day2Part1Result = Intcode.LoadFromFile("Day02/GravityAssistProgram.txt").Repair(1, 12).Repair(2, 2).Execute().ReadMemory(0);
            Console.WriteLine($"Day 2 Part 1: {day2Part1Result}");

            var day2Part2Result = Intcode.FindNounAndVerb("Day02/GravityAssistProgram.txt", 19690720);
            Console.WriteLine($"Day 2 Part 2: {day2Part2Result}");

            var day3Part1Result = WireList.LoadFromFile("Day03/Wires.txt").FindClosestIntersection(0, 1);
            Console.WriteLine($"Day 3 Part 1: {day3Part1Result}");

            var day3Part2Result = WireList.LoadFromFile("Day03/Wires.txt").FindClosestSignalIntersection(0, 1);
            Console.WriteLine($"Day 3 Part 2: {day3Part2Result}");

            var day4Part1Result = PasswordCracker.GetPossiblePasswords("152085-670283");
            Console.WriteLine($"Day 4 Part 1: {day4Part1Result}");

            var day4Part2Result = PasswordCracker.GetPossiblePasswordsWithDigitPair("152085-670283");
            Console.WriteLine($"Day 4 Part 2: {day4Part2Result}");

            Console.WriteLine("Running program for Day 5 Part 1 - Input should be 1");
            Day05.Intcode.LoadFromFile("Day05/Diagnostics.txt").Execute();

            Console.WriteLine("Running program for Day 5 Part 2 - Input should be 5");
            Day05.Intcode.LoadFromFile("Day05/Diagnostics.txt").Execute();

            var day6Part1Result = OrbitMap.LoadFromFile("Day06/Orbits.txt").CountOrbits();
            Console.WriteLine($"Day 6 Part 1: {day6Part1Result}");
            
            var day6Part2Result = OrbitMap.LoadFromFile("Day06/Orbits.txt").CalculateOrbitalTransfers("YOU", "SAN");
            Console.WriteLine($"Day 6 Part 2: {day6Part2Result}");

            var day7Part1Result = SignalMaximizer.GetMaximumThrustSignal();
            Console.WriteLine($"Day 7 Part 1: {day7Part1Result}");
                      
            var day7Part2Result = SignalMaximizer.GetMaximumFeedbackThrustSignal().Result;
            Console.WriteLine($"Day 7 Part 2: {day7Part2Result}");

            var day8Part1Result = SpaceImage.LoadFromFile("Day08/SpaceImage.txt", 25, 6).FindLayerWithFewestZerosAndGetNumberOfOnesTimeNumberOfTwos();
            Console.WriteLine($"Day 8 Part 1: {day8Part1Result}");

            Console.WriteLine($"Day 8 Part 2:");
            SpaceImage.LoadFromFile("Day08/SpaceImage.txt", 25, 6).RenderImage();

            Console.WriteLine($"Day 9 Part 1:");
            Day09.Intcode.Intcode.LoadFromFile("Day09/Boost.txt").SetInput(new PreparedInput(1)).Execute().Wait();

            Console.WriteLine($"Day 9 Part 2:");
            Day09.Intcode.Intcode.LoadFromFile("Day09/Boost.txt").SetInput(new PreparedInput(2)).Execute().Wait();

            Console.ReadKey();
        }
    }
}
