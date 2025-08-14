using ASimulatorForAveva.Objects;
using System;

namespace ASimulatorForAveva.Models.Simulation
{
    public class Mixer
    {
        public int mixerId { get; set; }
        public int mixingDurationSP { get; set; }
        public int motorSpeedSP { get; set; }

        public Valve inletValve1 { get; set; } = new Valve();
        public Valve inletValve2 { get; set; } = new Valve();
        public Valve outletValve { get; set; } = new Valve();
        public Pump ingredient1Pump { get; set; } = new Pump();
        public Pump ingredient2Pump { get; set; } = new Pump();
        public Pump agitator { get; set; } = new Pump();
        public LevelSensor level { get; set; } = new LevelSensor();
        public TemperatureSensor temperatureSensor { get; set; } = new TemperatureSensor();
        public Totalizer ing1CurrentLiters { get; set; } = new Totalizer();
        public Totalizer ing2CurrentLiters { get; set; } = new Totalizer();
        public Totalizer combinedCurrentLiters { get; set; } = new Totalizer();
        public HeatExchanger heatExchanger { get; set; } = new HeatExchanger();
        public int currentProcessStep { get; set; } = 0;
        public DateTime mixingStartTime { get; set; }
        public int ing1TargetLiters { get; set; } = 245;
        public int ing2TargetLiters { get; set; } = 163;
        public int LEVEL_MULTIPLIER { get; set; } = 100;

        public Mixer(int id, int duration, int speed)
        {
            mixerId = id;
            mixingDurationSP = duration;
            motorSpeedSP = speed;
        }
    }
}