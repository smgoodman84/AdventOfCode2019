using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day14
{
    public class OreForFuelCalculator
    {
        private class Chemical
        {
            public int Quantity { get; set; }
            public string Name { get; set; }

            public Chemical(string input)
            {
                var values = input.Trim().Split(" ").ToArray();
                Quantity = int.Parse(values[0]);
                Name = values[1];
            }

            public Chemical(string name, int quantity)
            {
                Name = name;
                Quantity = quantity;
            }

            public void Subtract(int n)
            {
                Quantity -= n;
            }

            public void Add(int n)
            {
                Quantity += n;
            }

            public Reaction ProducedBy { get; set; }

            public override string ToString()
            {
                return $"{Quantity} {Name}";
            }
        }

        private class Reaction
        {
            public List<Chemical> Inputs { get; private set; }
            public Chemical Output { get; private set; }

            private string _description;
            public Reaction(string description)
            {
                _description = description;

                var inputsOutput = description.Split("=>").ToArray();
                var inputs = inputsOutput[0];
                var output = inputsOutput[1];

                Inputs = inputs.Split(",").Select(x => new Chemical(x)).ToList();
                Output = new Chemical(output);
            }

            public override string ToString()
            {
                return _description;
            }
        }

        public static OreForFuelCalculator LoadFromFile(string filename)
        {
            var moduleMasses = File.ReadAllLines(filename)
                .Select(l => new Reaction(l));

            return new OreForFuelCalculator(moduleMasses);
        }

        private readonly IEnumerable<Reaction> _reactions;
        private OreForFuelCalculator(IEnumerable<Reaction> reactions)
        {
            _reactions = reactions.ToList();
        }

        private int _oreUsed = 0;
        private List<Chemical> _availableChemicals = new List<Chemical>();

        public int OreRequiredForChemical(string name, int quantity)
        {
            var requiredChemical = new Chemical(name, quantity);
            SetProducedBy(requiredChemical);
            ExecuteReactions(requiredChemical);
            return _oreUsed;
        }

        private void SetProducedBy(Chemical chemical)
        {
            if (chemical.Name == "ORE")
            {
                return;
            }

            var reaction = _reactions.First(r => r.Output.Name == chemical.Name);
            chemical.ProducedBy = reaction;
            foreach(var input in reaction.Inputs)
            {
                SetProducedBy(input);
            }
        }

        private Chemical GetAvailable(string name)
        {
            var available = _availableChemicals.FirstOrDefault(c => c.Name == name);

            if (available == null)
            {
                available = new Chemical(name, 0);
                _availableChemicals.Add(available);
            }

            return available;
        }

        private void ExecuteReactions(Chemical chemical)
        {
            if (chemical.Name == "ORE")
            {
                _oreUsed += chemical.Quantity;
                return;
            }

            var available = GetAvailable(chemical.Name);
            if (available.Quantity >= chemical.Quantity)
            {
                return;
            }

            var reaction = chemical.ProducedBy;
            foreach (var input in reaction.Inputs)
            {
                ExecuteReactions(input);
                var availableInput = GetAvailable(input.Name);
                availableInput.Subtract(input.Quantity);
            }

            available.Add(reaction.Output.Quantity);
            ExecuteReactions(chemical);
        }
    }
}
