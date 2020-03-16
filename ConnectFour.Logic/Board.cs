using System;

namespace ConnectFour.Logic
{
    public class Board
    {
        /// <summary>
        /// [Column, Row]
        /// [0..6, 0..5]
        /// </summary>
        private readonly byte[,] GameBoard = new byte[7, 6];

        /// <summary>
        /// The current player (always modulo 2).
        /// </summary>
        internal int Player = 0;

        /// <summary>
        /// null => no winner
        /// 1 -> player 1
        /// 2 -> player 2
        /// </summary>
        public int? Winner
        {
            get
            {

                Console.WriteLine(HorizontalWinner);

                int? winner = HorizontalWinner ?? VerticalWinner ?? DiagonalWinner;

                return winner;
            }
        }

        /// <summary>
        /// Adds a stone to the specified column. 
        /// </summary>
        /// <param name="column">The column where the stone should be added.</param>
        public void AddStone(byte column)
        {
            if (column > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for (int row = 0; row < 6; row++)
            {
                byte cell = GameBoard[column, row];

                if (cell == 0)
                {
                    GameBoard[column, row] = (byte)(Player + 1);
                    Player = (Player + 1) % 2;
                    return;
                }
            }

            throw new InvalidOperationException("Column is full");
        }

        public bool HasGameEnded => IsBoardFull || Winner.HasValue;

        public int? HorizontalWinner
        {
            get
            {
                int columnLength = GameBoard.GetLength(0);
                int rowLength = GameBoard.GetLength(1);

                for (byte row = 0; row < rowLength; row++)
                {
                    byte currentPlayer = 0;
                    int count = 0;
                    for (byte column = 0; column < columnLength; column++)
                    {
                        if (currentPlayer != GameBoard[column, row])
                        {
                            currentPlayer = GameBoard[column, row];
                            count = 1;
                            continue;
                        }
                        count++;
                        if (count >= 4 && currentPlayer != 0)
                        {
                            return currentPlayer;
                        }
                    }
                }

                return null;
            }
        }

        public int? VerticalWinner
        {
            get
            {

                int columnLength = GameBoard.GetLength(0);
                int rowLength = GameBoard.GetLength(1);

                for (byte column = 0; column < columnLength; column++)
                {
                    byte currentPlayer = 0;
                    int count = 0;
                    for (byte row = 0; row < rowLength; row++)
                    {
                        if (currentPlayer != GameBoard[column, row])
                        {
                            currentPlayer = GameBoard[column, row];
                            count = 1;
                            continue;
                        }
                        count++;
                        if (count >= 4 && currentPlayer != 0)
                        {
                            return currentPlayer;
                        }
                    }
                }

                return null;
            }
        }

        public int? DiagonalWinner
        {
            get
            {

                int columnLength = GameBoard.GetLength(0);
                int rowLength = GameBoard.GetLength(1);

                int maxDiagonalLength = Math.Max(columnLength, rowLength) * 2;

                for (int diagonal = -maxDiagonalLength; diagonal < maxDiagonalLength; diagonal++)
                {
                    byte currentPlayerPositive = 0;
                    int countPositive = 0;
                    byte currentPlayerNegative = 0;
                    int countNegative = 0;
                    for (byte diagonalIdx = 0; diagonalIdx < Math.Min(columnLength, rowLength); diagonalIdx++)
                    {
                        byte playerPositive = 0, playerNegative = 0;
                        try
                        {
                            playerPositive = GameBoard[diagonal + diagonalIdx, diagonalIdx];
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                        try
                        {
                            playerNegative = GameBoard[diagonal - diagonalIdx, diagonalIdx];
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }

                        if (currentPlayerPositive != playerPositive)
                        {
                            currentPlayerPositive = playerPositive;
                            countPositive = 1;
                        }
                        else
                        {
                            countPositive++;
                        }
                        if (currentPlayerNegative != playerNegative)
                        {
                            currentPlayerNegative = playerNegative;
                            countNegative = 1;
                        }
                        else
                        {
                            countNegative++;
                        }
                        if (countPositive >= 4 && playerPositive != 0)
                        {
                            return playerPositive;
                        }
                        if (countNegative >= 4 && playerNegative != 0)
                        {
                            return playerNegative;
                        }
                    }
                }

                return null;
            }
        }

        public bool IsBoardFull
        {
            get
            {
                for (int i = 0; i < GameBoard.GetLength(0); i++)
                {
                    int maxRow = GameBoard.GetLength(1) - 1;
                    if (GameBoard[i, maxRow] == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
