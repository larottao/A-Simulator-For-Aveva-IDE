namespace ASimulatorForAveva.Objects
{
    public class Totalizer
    {
        public int Value { get; set; } = 0;

        public void Update(int increment, bool reset)
        {
            if (reset)
            {
                Value = 0;
            }
            else
            {
                Value += increment;
            }
        }
    }
}