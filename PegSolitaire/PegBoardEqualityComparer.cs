using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegSolitaire
{
    public class PegBoardEqualityComparer : IEqualityComparer<PegBoard>
    {
        public bool Equals(PegBoard x, PegBoard y)
        {
            return x.Pegs[0].State == y.Pegs[0].State &&
                   x.Pegs[1].State == y.Pegs[1].State &&
                   x.Pegs[2].State == y.Pegs[2].State &&
                   x.Pegs[3].State == y.Pegs[3].State &&
                   x.Pegs[4].State == y.Pegs[4].State &&
                   x.Pegs[5].State == y.Pegs[5].State &&
                   x.Pegs[6].State == y.Pegs[6].State &&
                   x.Pegs[7].State == y.Pegs[7].State &&
                   x.Pegs[8].State == y.Pegs[8].State &&
                   x.Pegs[9].State == y.Pegs[9].State;
        }

        public int GetHashCode(PegBoard pegBoard)
        {
            return (int) pegBoard.Pegs[0].State ^
                   (int) pegBoard.Pegs[1].State ^
                   (int) pegBoard.Pegs[2].State ^
                   (int) pegBoard.Pegs[3].State ^
                   (int) pegBoard.Pegs[4].State ^
                   (int) pegBoard.Pegs[5].State ^
                   (int) pegBoard.Pegs[6].State ^
                   (int) pegBoard.Pegs[7].State ^
                   (int) pegBoard.Pegs[8].State ^
                   (int) pegBoard.Pegs[9].State;
        }
    }
}
