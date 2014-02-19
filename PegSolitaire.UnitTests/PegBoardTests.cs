using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PegSolitaire.UnitTests
{
    [TestFixture]
    public class PegBoardTests
    {
        [Test]
        public void CopyCreatesADeepCopy()
        {
            var pegBoard1 = InitialPegBoard();
            var pegBoard2 = pegBoard1.Copy();

            Assert.AreNotSame(pegBoard1, pegBoard2);
            Assert.AreEqual(pegBoard1, pegBoard2);
        }

        [Test]
        public void EqualityTest()
        {
            var board1 = new PegBoard() {Pegs = {new Peg(PegState.Red), new Peg(PegState.Red)}};
            var board2 = new PegBoard() {Pegs = {new Peg(PegState.Red), new Peg(PegState.Red)}};

            Assert.IsTrue(board1.Equals(board2));
        }

        [Test]
        public void InequalityTest()
        {
            var board1 = new PegBoard() { Pegs = { new Peg(PegState.Red), new Peg(PegState.Red), new Peg(PegState.Red) } };
            var board2 = new PegBoard() { Pegs = { new Peg(PegState.Red), new Peg(PegState.Red), new Peg(PegState.Blue) } };

            Assert.IsFalse(board1.Equals(board2));
        }

        [Test]
        public void CanFindFirstRedNextToEmpty()
        {
            var pegBoard = InitialPegBoard();

            Assert.AreEqual(3, pegBoard.FindFirstRedNextToEmpty());
        }

        [Test]
        public void CanFindFirstBlueNextToEmpty()
        {
            var pegBoard = InitialPegBoard();

            Assert.AreEqual(6, pegBoard.FindFirstBlueNextToEmpty());
        }

        [Test]
        public void CanFindFirstRedNextToBlue()
        {
            var pegBoard = new PegBoard();
            pegBoard.Pegs = new List<Peg>
                       {
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue)
                       };

            Assert.AreEqual(4, pegBoard.FindFirstRedNextToBlue());
        }

        [Test]
        public void SwapForFirstRedNextToEmptyIsDoneProperly()
        {
            var pegBoard = InitialPegBoard();
            pegBoard.SwapRedNextToEmpty();

            Assert.IsTrue(pegBoard.Pegs[3].State == PegState.Empty);
            Assert.IsTrue(pegBoard.Pegs[4].State == PegState.Red);
        }

        [Test]
        public void JumpForFirstRedOverBlueIsDoneProperly()
        {
            var pegBoard = new PegBoard();
            pegBoard.Pegs = new List<Peg>
                       {
                           new Peg(PegState.Empty),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue)
                       };
            pegBoard.RedJump();

            Assert.IsTrue(pegBoard.Pegs[4].State == PegState.Empty);
            Assert.IsTrue(pegBoard.Pegs[6].State == PegState.Red);
        }

        [Test]
        public void CanFindFirstBlueNextToRed()
        {
            var pegBoard = new PegBoard();
            pegBoard.Pegs = new List<Peg>
                       {
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue)
                       };

            Assert.AreEqual(5, pegBoard.FindFirstBlueNextToRed());
        }

        [Test]
        public void SwapForFirstBlueNextToEmptyIsDoneProperly()
        {
            var pegBoard = InitialPegBoard();
            pegBoard.SwapBlueNextToEmpty();

            Assert.IsTrue(pegBoard.Pegs[6].State == PegState.Empty);
            Assert.IsTrue(pegBoard.Pegs[5].State == PegState.Blue);
        }

        [Test]
        public void JumpForFirstBlueOverRedIsDoneProperly()
        {
            var pegBoard = new PegBoard();
            pegBoard.Pegs = new List<Peg>
                       {
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Red),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue)
                       };
            pegBoard.BlueJump();

            Assert.IsTrue(pegBoard.Pegs[3].State == PegState.Blue);
            Assert.IsTrue(pegBoard.Pegs[5].State == PegState.Empty);
        }

        [Test]
        public void RedJumpOverRed()
        {
            var pegBoard = new PegBoard();
            pegBoard.Pegs = new List<Peg>
                       {
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue)
                       };
            pegBoard.RedJump();

            Assert.IsTrue(pegBoard.Pegs[2].State == PegState.Empty);
            Assert.IsTrue(pegBoard.Pegs[4].State == PegState.Red);
        }

        [Test]
        public void BlueJumpOverBlue()
        {
            var pegBoard = new PegBoard();
            pegBoard.Pegs = new List<Peg>
                       {
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Red),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Empty),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue),
                           new Peg(PegState.Blue)
                       };
            pegBoard.BlueJump();

            Assert.IsTrue(pegBoard.Pegs[5].State == PegState.Blue);
            Assert.IsTrue(pegBoard.Pegs[7].State == PegState.Empty);
        }

        [Test]
        public void MovesByOnePegAreCorrectlyGenerated()
        {
            var pegBoard = InitialPegBoard();
            var jumpedRed = JumpedRedPegBoard();
            var swappedRed = SwappedRedPegBoard();
            var swappedBlue = SwappedBluePegBoard();
            var jumpedBlue = JumpedBluePegBoard();
            var moves = pegBoard.GenerateMoves();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(jumpedRed));
            Assert.IsTrue(moves.Contains(swappedRed));
            Assert.IsTrue(moves.Contains(swappedBlue));
            Assert.IsTrue(moves.Contains(jumpedBlue));
        }

        [Test]
        public void MovesAfterJumpedRedPegBoardAreCorrectlyGenerated()
        {
            var moves = JumpedRedPegBoard().GenerateMoves();

            Assert.AreEqual(6, moves.Count);
        }

        [Test]
        public void MovesAfterJumpedBluePegBoardAreCorrectlyGenerated()
        {
            var moves = JumpedBluePegBoard().GenerateMoves();

            Assert.AreEqual(6, moves.Count);
        }

        [Test]
        public void BlueSwapForFirstPegBeingEmptyIsCorrectlyGenerated()
        {
            var board = new PegBoard
                            {
                                Pegs =
                                    {
                                        new Peg(PegState.Empty),
                                        new Peg(PegState.Blue),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Blue)
                                    }
                            };
            var expectedMove = new PegBoard
                            {
                                Pegs =
                                    {
                                        new Peg(PegState.Blue),
                                        new Peg(PegState.Empty),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Blue)
                                    }
                            };
            var moves = board.GenerateMoves();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves[0].Equals(expectedMove));
        }

        [Test]
        public void BlueJumpForFirstPegBeingEmptyIsCorrectlyGenerated()
        {
            var board = new PegBoard
            {
                Pegs =
                                    {
                                        new Peg(PegState.Empty),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Blue),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Blue)
                                    }
            };
            var expectedMove = new PegBoard
            {
                Pegs =
                                    {
                                        new Peg(PegState.Blue),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Empty),
                                        new Peg(PegState.Red),
                                        new Peg(PegState.Blue)
                                    }
            };
            var moves = board.GenerateMoves();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves[0].Equals(expectedMove));
        }

        [Test]
        public void FindAllRedsToSwap()
        {
            var pegBoard = JumpedRedPegBoard(); //RRERREBBBB
            var reds = pegBoard.FindAllRedsNextToEmpty();

            Assert.IsTrue(reds.Contains(1));
            Assert.IsTrue(reds.Contains(4));
        }

        [Test]
        public void FindAllRedsToJump()
        {
            var pegBoard = JumpedRedPegBoard(); //RRERREBBBB
            var reds = pegBoard.FindAllRedsToJump();

            Assert.IsTrue(reds.Contains(0));
            Assert.IsTrue(reds.Contains(3));
        }

        [Test]
        public void FindAllBluesToSwap()
        {
            var pegBoard = JumpedBluePegBoard(); //RRRREBBEBB
            var blues = pegBoard.FindAllBluesNextToEmpty();

            Assert.IsTrue(blues.Contains(5));
            Assert.IsTrue(blues.Contains(8));
        }

        [Test]
        public void FindAllBluesToJump()
        {
            var pegBoard = JumpedBluePegBoard(); //RRRREBBEBB
            var blues = pegBoard.FindAllBluesToJump();

            Assert.IsTrue(blues.Contains(6));
            Assert.IsTrue(blues.Contains(9));
        }

        [Test]
        public void PossibleSecondLastStateGeneratesMoveToFinalState()
        {
            var pegBoard = new PegBoard(
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Empty),
              new Peg(PegState.Red),
              new Peg(PegState.Empty),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red));
            var moves = pegBoard.GenerateMoves();

            Assert.IsTrue(moves.Count == 1);
            Assert.IsTrue(moves.Contains(FinalPegBoard()));
        }

        private PegBoard InitialPegBoard()
        {
            return new PegBoard(
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Empty),
              new Peg(PegState.Empty),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue));
        }

        private PegBoard JumpedBluePegBoard()
        {
            return new PegBoard(
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Empty),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Empty),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue));
        }

        private PegBoard SwappedBluePegBoard()
        {
            return new PegBoard(
               new Peg(PegState.Red),
               new Peg(PegState.Red),
               new Peg(PegState.Red),
               new Peg(PegState.Red),
               new Peg(PegState.Empty),
               new Peg(PegState.Blue),
               new Peg(PegState.Empty),
               new Peg(PegState.Blue),
               new Peg(PegState.Blue),
               new Peg(PegState.Blue));
        }

        private PegBoard SwappedRedPegBoard()
        {
            return new PegBoard(
               new Peg(PegState.Red),
               new Peg(PegState.Red),
               new Peg(PegState.Red),
               new Peg(PegState.Empty),
               new Peg(PegState.Red),
               new Peg(PegState.Empty),
               new Peg(PegState.Blue),
               new Peg(PegState.Blue),
               new Peg(PegState.Blue),
               new Peg(PegState.Blue));
        }

        private PegBoard JumpedRedPegBoard()
        {
            return new PegBoard(
                new Peg(PegState.Red),
                new Peg(PegState.Red),
                new Peg(PegState.Empty),
                new Peg(PegState.Red),
                new Peg(PegState.Red),
                new Peg(PegState.Empty),
                new Peg(PegState.Blue),
                new Peg(PegState.Blue),
                new Peg(PegState.Blue),
                new Peg(PegState.Blue));
        }

        private PegBoard FinalPegBoard()
        {
            return new PegBoard(
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Blue),
              new Peg(PegState.Empty),
              new Peg(PegState.Empty),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red),
              new Peg(PegState.Red));
        }
    }
}
