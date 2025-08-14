using System;

namespace ASimulatorForAveva.Objects
{
    public class Pump
    {
        public bool Started { get; set; } = false;
        public double SpeedSP { get; set; }
        public double SpeedPV { get; set; }

        public void Update()
        {
            SpeedPV = Started ? SpeedSP + new Random().NextDouble() : 0;
        }
    }
}