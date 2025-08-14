using ASimulatorForAveva.Models.Simulation;
using System;

namespace ASimulatorForAveva.Objects
{
    public class HardcodedProcess
    {
        public static void SimulateStep(Mixer mixer)
        {
            int increment = new Random().Next(0, mixer.LEVEL_MULTIPLIER);

            switch (mixer.currentProcessStep)
            {
                case 0: // Initialization
                    if (mixer.level.Value <= LevelSensor.Min)
                    {
                        mixer.inletValve1 = new Valve();
                        mixer.inletValve2 = new Valve();
                        mixer.outletValve = new Valve();
                        mixer.ingredient1Pump = new Pump();
                        mixer.ingredient2Pump = new Pump();
                        mixer.agitator = new Pump();
                        mixer.ing1CurrentLiters = new Totalizer();
                        mixer.ing2CurrentLiters = new Totalizer();
                        mixer.combinedCurrentLiters = new Totalizer();
                        mixer.currentProcessStep = 1;
                    }
                    break;

                case 1: // Add Ingredient 1

                    mixer.inletValve1.Update(true);
                    mixer.ingredient1Pump.Started = true;
                    mixer.ingredient1Pump.SpeedSP = mixer.motorSpeedSP;
                    mixer.ingredient1Pump.Update();

                    if (mixer.inletValve1.IsOpen() && mixer.ingredient1Pump.Started)
                    {
                        mixer.level.Adjust(increment);
                        mixer.ing1CurrentLiters.Update(increment, false);
                    }
                    if (mixer.level.Value >= mixer.ing1TargetLiters)
                    {
                        mixer.inletValve1.Update(false);
                        mixer.ingredient1Pump.Started = false;
                        mixer.currentProcessStep = 2;
                    }
                    break;

                case 2: // Add Ingredient 2

                    mixer.inletValve2.Update(true);
                    mixer.ingredient2Pump.Started = true;
                    mixer.ingredient2Pump.SpeedSP = mixer.motorSpeedSP;
                    mixer.ingredient2Pump.Update();

                    if (mixer.inletValve2.IsOpen() && mixer.ingredient2Pump.Started)
                    {
                        mixer.level.Adjust(increment);
                        mixer.ing2CurrentLiters.Update(increment, false);
                    }
                    if (mixer.level.Value >= mixer.ing1TargetLiters + mixer.ing2TargetLiters)
                    {
                        mixer.inletValve2.Update(false);
                        mixer.ingredient2Pump.Started = false;
                        mixer.currentProcessStep = 3;
                        mixer.mixingStartTime = DateTime.Now;
                    }
                    break;

                case 3: // Mix both Ingredients

                    mixer.agitator.Started = true;
                    mixer.agitator.SpeedSP = mixer.motorSpeedSP;
                    mixer.agitator.Update();

                    TimeSpan elapsed = DateTime.Now - mixer.mixingStartTime;
                    if (elapsed.TotalSeconds >= mixer.mixingDurationSP)
                    {
                        mixer.agitator.Started = false;
                        mixer.currentProcessStep = 4;
                    }

                    break;

                case 4: // Drain Tank

                    mixer.outletValve.Update(true);
                    if (mixer.outletValve.IsOpen())
                    {
                        mixer.level.Adjust(-increment);
                        mixer.combinedCurrentLiters.Update(increment, false);
                    }
                    if (mixer.level.Value <= LevelSensor.Min)
                    {
                        mixer.outletValve.Update(false);
                        mixer.currentProcessStep = 0;
                    }
                    break;
            }

            mixer.temperatureSensor.Update(mixer.level.Value);
            mixer.heatExchanger.Update(mixer.outletValve.IsOpen());
        }
    }
}

/*

 Hardcoded Mixer Process Description:

Step 0: Initialization
Trigger: Tank level is at minimum (TRANSMITTER_MIN).
Actions:
All valves (inlet1Cmd, inlet2Cmd, outletCmd) are closed.
All motors (agitatorCmd, pump1Cmd, pump2Cmd) are stopped.
All totalizers (total1PV, total2PV, total3PV) are reset.
Process step is set to 1.
Components Involved:
Valves: Inlet 1, Inlet 2, Outlet.
Motors: Pump 1, Pump 2, Agitator.
Totalizers: Ingredient 1, Ingredient 2, Output.
Registers: Holding, Coil, Digital Input, Analog Input.
Step 1: Add Ingredient 1
Trigger: Process step = 1.
Actions:
Open Inlet Valve 1.
Start Pump 1.
Increase tank level.
Update Totalizer 1.
Completion Condition:
When level ≥ QTY_INGREDIENT1, shut down equipment and move to Step 2.
Components Involved:
Valve: Inlet 1.
Motor: Pump 1.
Sensor: Level transmitter.
Totalizer: Ingredient 1.
Step 2: Add Ingredient 2
Trigger: Process step = 2.
Actions:
Open Inlet Valve 2.
Start Pump 2.
Increase tank level.
Update Totalizer 2.
Completion Condition:
When level ≥ QTY_INGREDIENT1 + QTY_INGREDIENT2, shut down equipment and move to Step 3.
Components Involved:
Valve: Inlet 2.
Motor: Pump 2.
Sensor: Level transmitter.
Totalizer: Ingredient 2.
Step 3: Mix Ingredients
Trigger: Process step = 3.
Actions:
Start Agitator.
Record start time.
Monitor mixing duration.
Completion Condition:
When elapsed time ≥ mixingDurationSP, stop agitator and move to Step 4.
Components Involved:
Motor: Agitator.
Timer: Mixing duration.
Sensor: Temperature transmitter (updated based on level).
Alarm: Triggered if current second > 45.
Step 4: Drain Tank
Trigger: Process step = 4.
Actions:
Open Outlet Valve.
Decrease tank level.
Update Totalizer 3.
Completion Condition:
When level ≤ TRANSMITTER_MIN, shut down outlet and reset to Step 0.
Components Involved:
Valve: Outlet.
Sensor: Level transmitter.
Totalizer: Output.
🔥 Heat Exchanger
Runs Independently but is triggered by outletCmd.
Actions:
Generates four temperature readings (tc1, tc2, tc3, tc4) using random values.
Components Involved:
Heat Exchanger: Simulated with random temperature values.
Registers: Analog Input for each temperature channel.
*
*
*/