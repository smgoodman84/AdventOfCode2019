using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day7
{
    public class SignalMaximizer
    {
        public static int GetMaximumThrustSignal(List<int> inputs = null)
        {
            if (inputs == null)
            {
                return GetMaximumThrustSignal(new List<int>());
            }

            if (inputs.Count == 5)
            {
                return GetThrustSignal(inputs.ToArray());
            }

            var max = 0;
            foreach (var i in Enumerable.Range(0, 5).Where(x => !inputs.Contains(x)))
            {
                var nextInput = inputs.ToList();
                nextInput.Add(i);

                var result = GetMaximumThrustSignal(nextInput);
                if (result > max)
                {
                    max = result;
                }
            }

            return max;
        }

        private static int GetThrustSignal(params int[] phaseSettingInputs)
        {
            var amplifiers = Enumerable.Range(1, 5)
                .Select(x => Intcode.LoadFromFile("Day7/AmplifierControllerSoftware.txt"))
                .ToArray();

            var phaseSettings = new PreparedInput(phaseSettingInputs);

            IInput input = new PreparedInput(0);
            IOPipe pipe = null;
            foreach(var amplifier in amplifiers)
            {
                var amplifierInputs = new CombinedInput(phaseSettings, input);

                amplifier.SetInput(amplifierInputs);

                pipe = new IOPipe();
                amplifier.SetOutput(pipe);

                input = pipe;
            }

            foreach (var amplifier in amplifiers)
            {
                amplifier.Execute();
            }

            var result = pipe.ReadInput();

            return result;
        }
    }
}
