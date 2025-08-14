namespace ASimulatorForAveva.Objects
{
    public class LevelSensor
    {
        public int Value { get; set; } = 0;
        public const int Min = 0;
        public const int Max = 4095;

        public void Adjust(int increment)
        {
            Value = Clamp(Value + increment, Min, Max);
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}