using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2019.Day07.Intcode;

namespace AdventOfCode2019.Day07
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
                .Select(x => Intcode.Intcode.LoadFromFile("Day07/AmplifierControllerSoftware.txt"))
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
                amplifier.Execute().Wait();
            }

            var result = pipe.ReadInput().Result;

            return result;
        }

        public static async Task<int> GetMaximumFeedbackThrustSignal(List<int> inputs = null)
        {
            if (inputs == null)
            {
                return await GetMaximumFeedbackThrustSignal(new List<int>());
            }

            if (inputs.Count == 5)
            {
                return await GetFeedbackThrustSignal(inputs.ToArray());
            }

            var max = 0;
            foreach (var i in Enumerable.Range(5, 5).Where(x => !inputs.Contains(x)))
            {
                var nextInput = inputs.ToList();
                nextInput.Add(i);

                var result = await GetMaximumFeedbackThrustSignal(nextInput);
                if (result > max)
                {
                    max = result;
                }
            }

            return max;
        }

        private static async Task<int> GetFeedbackThrustSignal(params int[] phaseSettingInputs)
        {
            var amplifiers = phaseSettingInputs
                .Select(x => Intcode.Intcode.LoadFromFile("Day07/AmplifierControllerSoftware.txt"))
                .ToArray();

            var phaseSettings = new PreparedInput(phaseSettingInputs);

            var pipes = phaseSettingInputs
                .Select(InitialisedPipe)
                .ToArray();

            pipes[0].Output(0);

            var pipeIndex = 0;
            foreach (var amplifier in amplifiers)
            {
                amplifier.SetInput(pipes[pipeIndex]);
                pipeIndex += 1;
                if (pipeIndex >= phaseSettingInputs.Length)
                {
                    pipeIndex = 0;
                }
                amplifier.SetOutput(pipes[pipeIndex]);
            }

            var tasks = amplifiers.Select(a => a.Execute());
            await Task.WhenAll(tasks);

            var result = await pipes[0].ReadInput();

            return result;
        }

        private static IOPipe InitialisedPipe(int initialisedValue)
        {
            var pipe = new IOPipe();
            pipe.Output(initialisedValue);
            return pipe;
        }
    }
}
