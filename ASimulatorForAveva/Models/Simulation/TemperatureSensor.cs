using System;

namespace ASimulatorForAveva.Objects
{
    public class TemperatureSensor
    {
        public double Value { get; set; }

        public void Update(int level)
        {
            var rnd = new Random().NextDouble();
            Value = 15.00 + (-0.0001 * Math.Pow(level, 2)) + (0.5 * level) + rnd * 100;
        }
    }
}