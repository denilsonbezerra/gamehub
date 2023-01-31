using GameHub.Model;

namespace GameHub.TicTacToe.Views
{
    public class TicTacToeView
    {
        public void ShowTicTacToeBoard(Board board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                Console.Write("   ");
                for (int j = 0; j < board.Size; j++)
                {
                    ConsoleColor aux = Console.ForegroundColor;

                    if (board.BoardArray[i, j].Trim() == "X")
                        Console.ForegroundColor = ConsoleColor.Black;
                    else if (board.BoardArray[i, j].Trim() == "O")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write(board.BoardArray[i, j]);
                    Console.ForegroundColor = aux;
                }

                Console.WriteLine();
            }
        }
    }
}
