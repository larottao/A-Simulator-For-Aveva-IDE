using System;

namespace ASimulatorForAveva.Objects
{
    public class HeatExchanger
    {
        public int TC1 { get; set; }
        public int TC2 { get; set; }
        public int TC3 { get; set; }
        public int TC4 { get; set; }

        public void Update(bool active)
        {
            var rnd = new Random();
            if (active)
            {
                TC1 = 3400 + rnd.Next(0, 600);
                TC2 = 3300 + rnd.Next(0, 600);
                TC3 = 1500 + rnd.Next(0, 600);
                TC4 = 1000 + rnd.Next(0, 600);
            }
            else
            {
                TC1 = 3400;
                TC2 = 3300;
                TC3 = 1500;
                TC4 = 1000;
            }
        }
    }
}