using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegSolitaire
{
    public class PegBoard
    {
        public List<Peg> Pegs { get; set; }

        public PegBoard()
        {
            Pegs = new List<Peg>();
        }

        public PegBoard(Peg peg0, Peg peg1, Peg peg2, Peg peg3, Peg peg4, 
            Peg peg5, Peg peg6, Peg peg7, Peg peg8, Peg peg9)
        {
            Pegs = new List<Peg>
                       {
                           peg0, peg1, peg2, peg3,
                           peg4, peg5, peg6, peg7,
                           peg8, peg9
                       };
        }

        public PegBoard Copy()
        {
            var pegs = new List<Peg>();
            foreach (var peg in Pegs)
            {
                var pegIndex = Pegs.IndexOf(peg);
                pegs.Insert(pegIndex, Pegs[pegIndex]);
            }
            return new PegBoard {Pegs = pegs};
        }

        public bool CanMakeMove()
        {
            return CanSwapRedNextToEmpty() | CanRedJump() | CanSwapBlueNextToEmpty() | CanBlueJump();
        }

        public List<PegBoard> GenerateMoves()
        {
            var moves = new List<PegBoard>();

            if (CanSwapRedNextToEmpty())
            {
                var redsNextToEmpty = FindAllRedsNextToEmpty();
                foreach (var red in redsNextToEmpty)
                {
                    var copyOfBoard = Copy();
                    copyOfBoard.SwapTwoPegs(red, red + 1);
                    moves.Add(copyOfBoard);
                }
            }
            if (CanRedJump())
            {
                var redsToJump = FindAllRedsToJump();
                foreach (var red in redsToJump)
                {
                    var copyOfBoard = Copy();
                    copyOfBoard.SwapTwoPegs(red, red + 2);
                    moves.Add(copyOfBoard);
                }
            }
            if (CanSwapBlueNextToEmpty())
            {
                var bluesNextToEmpty = FindAllBluesNextToEmpty();
                foreach (var blue in bluesNextToEmpty)
                {
                    var copyOfBoard = Copy();
                    copyOfBoard.SwapTwoPegs(blue, blue - 1);
                    moves.Add(copyOfBoard);
                }
            }
            if (CanBlueJump())
            {
                var bluesToJump = FindAllBluesToJump();
                foreach (var blue in bluesToJump)
                {
                    var copyOfBoard = Copy();
                    copyOfBoard.SwapTwoPegs(blue, blue - 2);
                    moves.Add(copyOfBoard);
                }
            }
            return moves;
        }

        public int FindFirstRedNextToEmpty()
        {
            return FindAllRedsNextToEmpty().OrderBy(x => x).First();
        }

        public List<int> FindAllRedsNextToEmpty()
        {
            var p = Pegs.FindAll(peg => peg.State == PegState.Red &&
                ((Pegs.IndexOf(peg) + 1) < Pegs.Count) &&
                (Pegs[Pegs.IndexOf(peg) + 1].State == PegState.Empty));

            if (p.Count == 0)
                return new List<int> { -1 };

            return p.Select(x => Pegs.IndexOf(x)).ToList();
        }

        public int FindFirstRedNextToBlue()
        {
            var p = Pegs.Find(peg => peg.State == PegState.Red &&
                ((Pegs.IndexOf(peg) + 1) < Pegs.Count) &&
                (Pegs[Pegs.IndexOf(peg) + 1].State == PegState.Blue));

            if (p == null)
                return -1;

            return Pegs.IndexOf(p);
        }

        public List<int> FindAllRedsToJump()
        {
            var p = Pegs.FindAll(peg => peg.State == PegState.Red &&
                ((Pegs.IndexOf(peg) + 1) < Pegs.Count) &&
                (Pegs[Pegs.IndexOf(peg) + 1].State != PegState.Empty) &&
                ((Pegs.IndexOf(peg) + 2) < Pegs.Count) &&
                (Pegs[Pegs.IndexOf(peg) + 2].State == PegState.Empty));

            if (p.Count == 0)
                return new List<int> {-1};

            return p.Select(x => Pegs.IndexOf(x)).ToList();
        }

        public int FindFirstRedToJump()
        {
            return FindAllRedsToJump().OrderBy(x => x).First();
        }

        public int FindFirstBlueNextToEmpty()
        {
            return FindAllBluesNextToEmpty().OrderBy(x => x).First();
        }

        public List<int> FindAllBluesNextToEmpty()
        {
            var p = Pegs.FindAll(peg => peg.State == PegState.Blue &&
                (Pegs.IndexOf(peg) - 1 >= 0) &&
                (Pegs[Pegs.IndexOf(peg) - 1].State == PegState.Empty));

            if (p.Count == 0)
                return new List<int> {-1};

            return p.Select(x => Pegs.IndexOf(x)).ToList();
        }

        public int FindFirstBlueNextToRed()
        {
            var p = Pegs.FindLast(peg => peg.State == PegState.Blue &&
                peg.State != PegState.Empty && 
                (Pegs.IndexOf(peg) - 1 > 0) &&
                (Pegs[Pegs.IndexOf(peg) - 1].State == PegState.Red));

            if (p == null)
                return -1;

            return Pegs.IndexOf(p);
        }

        public int FindFirstBlueToJump()
        {
            return FindAllBluesToJump().OrderBy(x => x).First();
        }

        public List<int> FindAllBluesToJump()
        {
            var p = Pegs.FindAll(peg => peg.State == PegState.Blue &&
                (Pegs.IndexOf(peg) - 1 > 0) &&
                (Pegs[Pegs.IndexOf(peg) - 1].State != PegState.Empty) &&
                (Pegs.IndexOf(peg) - 2 >= 0) &&
                (Pegs[Pegs.IndexOf(peg) - 2].State == PegState.Empty));

            if (p.Count == 0)
                return new List<int> {-1};

            return p.Select(x => Pegs.IndexOf(x)).ToList();
        }

        private bool CanSwapRedNextToEmpty()
        {
            return (FindFirstRedNextToEmpty() != -1);
        }

        private bool CanSwapBlueNextToEmpty()
        {
            return (FindFirstBlueNextToEmpty() != -1);
        }

        private bool CanRedJump()
        {
            var redIndex = FindFirstRedToJump();
            if (redIndex == -1)
                return false;
            if (redIndex + 2 > Pegs.Count - 1)
                return false;

            return (Pegs[redIndex + 2].State == PegState.Empty);
        }

        private bool CanBlueJump()
        {
            var blueIndex = FindFirstBlueToJump();
            if (blueIndex == -1)
                return false;
            if (blueIndex - 2 < 0)
                return false;

            return (Pegs[blueIndex - 2].State == PegState.Empty);
        }

        public void SwapRedNextToEmpty()
        {
            var firstRedIndex = FindFirstRedNextToEmpty();
            SwapTwoPegs(firstRedIndex, firstRedIndex + 1);
        }

        public void SwapBlueNextToEmpty()
        {
            var firstBlueIndex = FindFirstBlueNextToEmpty();
            SwapTwoPegs(firstBlueIndex, firstBlueIndex - 1);
        }

        public void RedJump()
        {
            var firstRedIndex = FindFirstRedToJump();
            SwapTwoPegs(firstRedIndex, firstRedIndex + 2);
        }

        public void BlueJump()
        {
            var firstBlueIndex = FindFirstBlueToJump();
            SwapTwoPegs(firstBlueIndex, firstBlueIndex - 2);
        }

        private void SwapTwoPegs(int peg1, int peg2)
        {
            var p1 = Pegs[peg1];
            var p2 = Pegs[peg2];

            Pegs[peg1] = p2;
            Pegs[peg2] = p1;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PegBoard))
                return false;

            var board = obj as PegBoard;

            var bools = Pegs.Zip(board.Pegs, (first, second) => first.State == second.State);
            return !bools.Contains(false);
        }

        public override int GetHashCode()
        {
            return (int) Pegs.Select(peg => peg.State).Aggregate((p1,p2) => p1 ^ p2);
        }

        public override string ToString()
        {
            var stringBuiler = new StringBuilder();
            stringBuiler.Append("[");
            foreach (var peg in Pegs)
            {
                stringBuiler.Append(peg);
            }
            stringBuiler.Append("]");
            return stringBuiler.ToString();
        }
    }
}
