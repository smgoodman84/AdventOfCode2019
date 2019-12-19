using System;
using AdventOfCode2019.Day01;
using AdventOfCode2019.Day03;
using AdventOfCode2019.Day04;
using AdventOfCode2019.Day06;
using AdventOfCode2019.Day07;
using AdventOfCode2019.Day08;
using AdventOfCode2019.Day09.Intcode;
using AdventOfCode2019.Day10;
using AdventOfCode2019.Day12;
using AdventOfCode2019.Day13;
using AdventOfCode2019.Day14;
using AdventOfCode2019.Day15;
using AdventOfCode2019.Day16;
using AdventOfCode2019.Day17;
using AdventOfCode2019.Day19;
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

            var day10Part1Result = AsteroidMap.LoadFromFile("Day10/AsteroidMap.txt").GetMaximumVisibility();
            Console.WriteLine($"Day 10 Part 1: {day10Part1Result}");

            var day10Part2Result = AsteroidMap.LoadFromFile("Day10/AsteroidMap.txt").GetNthDestroyedAsteroidLocation(200);
            Console.WriteLine($"Day 10 Part 2: {day10Part2Result}");

            var day11Part1Result = new Day11.Robot().Execute().GetEverWhiteCount();
            Console.WriteLine($"Day 11 Part 1: {day11Part1Result}");
            
            Console.WriteLine($"Day 11 Part 2:");
            new Day11.Robot().StartOnWhite().Execute().RenderHull();
            
            var day12Part1Result  = GravitationalEnergy.LoadFromFile("Day12/MoonLocations.txt").Simulate(1000).GetTotalEnergy();
            Console.WriteLine($"Day 12 Part 1: {day12Part1Result}");

            var day12Part2Result = GravitationalEnergy.LoadFromFile("Day12/MoonLocations.txt").FindCycle();
            Console.WriteLine($"Day 12 Part 2: {day12Part2Result}");

            var day13Part1Result = Game.LoadFromFile("Day13/Game.txt").Execute().CountCharacters(2);
            Console.WriteLine($"Day 13 Part 1: {day13Part1Result}");
            
            var day13Part2Result = Game.LoadFromFile("Day13/Game.txt").ExecuteWithInput().GetScore();
            Console.WriteLine($"Day 13 Part 2: {day13Part2Result}");
            
            var day14Part1Result = OreForFuelCalculator.LoadFromFile("Day14/Reactions.txt").OreRequiredForChemical("FUEL", 1);
            Console.WriteLine($"Day 14 Part 1: {day14Part1Result}");

            //var day14Part2Result = OreForFuelCalculator.LoadFromFile("Day14/Reactions.txt").MaximumChemicalWithOre("FUEL", 1000000000000);
            //Console.WriteLine($"Day 14 Part 2: {day14Part2Result}");
            
            var day15Part1Result = new OxygenSystemLocater().GetShortestRouteToOxygen();
            Console.WriteLine($"Day 15 Part 1: {day15Part1Result}");

            var day15Part2Result = new OxygenSystemLocater().GetTimeToFillWithOxygen();
            Console.WriteLine($"Day 15 Part 2: {day15Part2Result}");
            
            var day16Part1Result = FlawedFrequencyTransmission.LoadFromFile("Day16/Signal.txt").ProcessSignal(100);
            Console.WriteLine($"Day 16 Part 1: {day16Part1Result}");
            
            var day16Part2Result = FlawedFrequencyTransmission.LoadFromFile("Day16/Signal.txt").ProcessRepeatedSignal(100, 10000);
            Console.WriteLine($"Day 16 Part 2: {day16Part2Result}");
            
            var day17Part1Result = new Scaffold().GetSumOfAlignmentParameters();
            Console.WriteLine($"Day 17 Part 1: {day17Part1Result}");
            
            var day17Part2Result = new Scaffold().CollectSpaceDust().Result;
            Console.WriteLine($"Day 17 Part 2: {day17Part2Result}");
            
            var day19Part1Result = new TractorMapper().GetTractorArea(50);
            Console.WriteLine($"Day 19 Part 1: {day19Part1Result}");

            Console.ReadKey();
        }
    }
}
