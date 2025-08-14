using ASimulatorForAveva.Models.Simulation;
using System;
using System.Text;

namespace ASimulatorForAveva
{
    public static class GetMixerStatsAsText
    {
        public static String get(Mixer mixer)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"MIXER {mixer.mixerId} - CURRENT PROCESS STEP: {mixer.currentProcessStep}");
            sb.AppendLine($" ");
            sb.AppendLine($"Current tank Level: {mixer.level.Value} liters");
            sb.AppendLine($"Tank Heater Temp: {mixer.temperatureSensor.Value:F2} °C");
            sb.AppendLine($" ");
            sb.AppendLine($"Ingredient 1: {mixer.ing1CurrentLiters.Value} of {mixer.ing1TargetLiters} liters");
            sb.AppendLine($"Ingredient 2: {mixer.ing2CurrentLiters.Value} of {mixer.ing2TargetLiters} liters");
            sb.AppendLine($"----------------------------------------");
            sb.AppendLine($"Combined total: {mixer.combinedCurrentLiters.Value} liters");
            sb.AppendLine($" ");
            sb.AppendLine($"HeatExchanger: TC1={mixer.heatExchanger.TC1}, TC2={mixer.heatExchanger.TC2}, TC3={mixer.heatExchanger.TC3}, TC4={mixer.heatExchanger.TC4}");
            sb.AppendLine($" ");

            return sb.ToString();
        }
    }
}