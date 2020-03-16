using ConnectFour.Logic;
using System;

namespace ConnectFour.UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Board board = new Board();
            while (!board.HasGameEnded)
            {
                try
                {
                    byte input = Convert.ToByte(Console.ReadLine());
                    board.AddStone(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            if (board.Winner == 0)
            {
                Console.WriteLine("Tied");
                return;
            }
            Console.WriteLine($"Winner: {board.Winner}");
        }
    }
}
