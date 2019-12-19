using System;
using System.Threading.Tasks;
using AdventOfCode2019.Day09.Intcode;

namespace AdventOfCode2019.Day19
{
    public class TractorMapper
    {
        private Intcode _droneController;
        private IOPipe _droneInput;
        private IOPipe _droneOutput;
        public TractorMapper()
        {
            InitialiseDrone();
        }

        private void InitialiseDrone()
        {
            _droneController = Intcode.LoadFromFile("Day19/DroneController.txt");
            _droneInput = new IOPipe();
            _droneOutput = new IOPipe();
            _droneController.SetInput(_droneInput);
            _droneController.SetOutput(_droneOutput);
        }

        public long GetTractorArea(int fieldSize)
        {
            return GetTractorAreaAsync(fieldSize).Result;
        }

        private async Task<long> GetTractorAreaAsync(int fieldSize)
        {
            long pullCount = 0;
            for (var y = 0; y < fieldSize; y++)
            {
                for (var x = 0; x < fieldSize; x++)
                {
                    InitialiseDrone();
                    var controllerTask = _droneController.Execute();

                    _droneInput.Output(x);
                    _droneInput.Output(y);

                    Console.WriteLine($"Scanning {x},{y}");

                    var isPulled = await _droneOutput.ReadInput();
                    pullCount += isPulled;
                }
            }
            return pullCount;
        }
    }
}
