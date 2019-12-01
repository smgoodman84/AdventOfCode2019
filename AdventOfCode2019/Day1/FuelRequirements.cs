using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day1
{
    public class FuelRequirements
    {
        public static FuelRequirements LoadFromFile(string filename)
        {
            var moduleMasses = File.ReadAllLines(filename)
                .Select(int.Parse);

            return new FuelRequirements(moduleMasses);
        }

        private readonly IEnumerable<int> _moduleMasses;

        public FuelRequirements(IEnumerable<int> moduleMasses)
        {
            _moduleMasses = moduleMasses;
        }

        public int GetFuelRequirements()
        {
            return _moduleMasses
                .Select(GetFuelRequirement)
                .Sum();
        }

        private int GetFuelRequirement(int moduleMass)
        {
            return (moduleMass / 3) - 2;
        }

        public int GetFuelRequirementsWithFuelForFuel()
        {
            return _moduleMasses
                .Select(GetFuelRequirementWithFuelForFuel)
                .Sum();
        }

        private int GetFuelRequirementWithFuelForFuel(int moduleMass)
        {
            var totalFuelMass = GetFuelRequirement(moduleMass);

            var unfueledMass = totalFuelMass;
            while (true)
            {
                unfueledMass = GetFuelRequirement(unfueledMass);
                if (unfueledMass <= 0)
                {
                    break;
                }

                totalFuelMass += unfueledMass;
            }

            return totalFuelMass;
        }
    }
}
