using ConnectFour.Logic;
using System;
using Xunit;

namespace ConnectFour.Tests
{
    public class BoardTests
    {
        [Fact]
        public void AddWithInvalidColumnIndex()
        {
            Board b = new Board();

            Assert.Throws<ArgumentOutOfRangeException>(() => b.AddStone(7));
        }

        [Fact]
        public void PlayerChangesWhenAddingStone()
        {
            Board b = new Board();

            int oldPlayer = b.Player;
            b.AddStone(0);

            // Verify that player has changed
            Assert.NotEqual(oldPlayer, b.Player);
        }

        [Fact]
        public void AddingTooManyStonesToARow()
        {
            Board b = new Board();

            for (int i = 0; i < 6; i++)
            {
                b.AddStone(0);
            }

            int oldPlayer = b.Player;
            Assert.Throws<InvalidOperationException>(() => b.AddStone(0));
            Assert.Equal(oldPlayer, b.Player);
        }

        [Fact]
        public void CheckIfBoardIsFull()
        {
            Board b = new Board();
            for (byte column = 0; column < 7; column++)
            {
                for (byte row = 0; row < 6; row++)
                {
                    b.AddStone(column);
                }
            }

            Assert.True(b.IsBoardFull);
        }

        [Fact]
        public void CheckIfBoradIsEmpty()
        {
            Board b = new Board();

            Assert.False(b.IsBoardFull);
        }

        [Fact]
        public void CheckVerticalWinner()
        {
            Board b = new Board();

            Assert.Null(b.Winner);
            Assert.False(b.HasGameEnded);

            for (int i = 0; i < 8; i++)
            {
                b.AddStone((byte)((i % 2) + 1));
            }

            Assert.Equal(1, b.Winner);
            Assert.Equal(b.Winner, b.VerticalWinner);
            Assert.True(b.HasGameEnded);
        }


        [Fact]
        public void CheckHorizontalWinner()
        {
            Board b = new Board();

            Assert.Null(b.Winner);

            for (int i = 0; i < 7; i++)
            {
                b.AddStone((byte)((i % 2) == 0 ? i / 2 : 6));
            }

            Assert.Equal(1, b.Winner);
            Assert.Equal(b.Winner, b.HorizontalWinner);
        }


        [Fact]
        public void CheckDiagonalWinnerLeft()
        {
            Board b = new Board();

            Assert.Null(b.Winner);

            /*             (11)
             *         ( 7)( 9)
             *     ( 3)( 5)( 8)
             * ( 1)( 2)( 4)( 6)    (10)
             */
            foreach (byte col in new[] { 0, 1, 1, 2, 2, 3, 2, 3, 3, 5, 3 })
            {
                b.AddStone(col);
            }

            Assert.Equal(1, b.Winner);
            Assert.Equal(b.Winner, b.DiagonalWinner);
        }

        [Fact]
        public void CheckDiagonalWinnerRight()
        {
            Board b = new Board();

            Assert.Null(b.Winner);

            /* (11)
             * ( 8)( 9)
             * ( 4)( 5)( 7)
             * ( 1)( 2)( 6)( 3)    (10)
             */
            foreach (byte col in new[] { 0, 1, 3, 0, 1, 2, 2, 0, 1, 5, 0 })
            {
                b.AddStone(col);
            }

            Assert.Equal(1, b.Winner);
            Assert.Equal(b.Winner, b.DiagonalWinner);
        }
    }
}
