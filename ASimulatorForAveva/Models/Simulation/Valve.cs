using System;

namespace ASimulatorForAveva.Objects
{
    public class Valve
    {
        public enum State
        { CLOSED, TRANSIT, OPEN }

        public State CurrentState { get; set; } = State.CLOSED;
        public int Position { get; set; } = 0;

        public const int MaxPosition = 10;

        public const int MinPosition = 0;

        public void Update(bool command)
        {
            if (command)
            {
                if (Position < MaxPosition)
                {
                    Position++;
                    CurrentState = State.TRANSIT;
                }
                else
                {
                    CurrentState = State.OPEN;
                }
            }
            else
            {
                if (Position > MinPosition)
                {
                    Position--;
                    CurrentState = State.TRANSIT;
                }
                else
                {
                    CurrentState = State.CLOSED;
                }
            }
        }

        public Boolean IsOpen()
        {
            return CurrentState == State.OPEN;
        }
    }
}