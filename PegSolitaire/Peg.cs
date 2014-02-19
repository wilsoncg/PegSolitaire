using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegSolitaire
{
    public class Peg
    {
        public Peg(PegState state)
        {
            State = state;
        }

        public PegState State { get; set; }

        public override string ToString()
        {
            if (State == PegState.Red)
            {
                return "R";
            }
            if (State == PegState.Blue)
            {
                return "B";
            }
            return "E";
        }
    }

    [Flags]
    public enum PegState { Red, Blue, Empty }
}
