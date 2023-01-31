using GameHub.Model.Interface;
using GameHub.Model;

namespace GameHub.TicTacToe.Models
{
    public class BoardTicTacToe : Board, IRefreshBoard
    {
        public List<string>? possibleMoves { get; protected set; }

        public BoardTicTacToe(int size)
        {
            Size = size * 2 - 1;
            BoardArray = GenerateBoard();
            possibleMoves = ListPossibleMoves();

        }

        public BoardTicTacToe(string[,] boardArray, int size) : base(boardArray, size)
        {
            BoardArray = boardArray;
            Size = (size + 1) / 2;
        }

        private string[,] GenerateBoard()
        {
            int cont = 1;
            string[,] boardArray = new string[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            if (cont > 9)
                                boardArray[i, j] = $" {cont}";
                            else if (cont > 99)
                                boardArray[i, j] = $"{cont}";
                            else
                                boardArray[i, j] = $" {cont} ";
                            cont++;
                        }
                        else
                            boardArray[i, j] = "║";
                    }
                    else
                    {
                        if (j % 2 != 0)
                            boardArray[i, j] = "╬";
                        else
                            boardArray[i, j] = "═══";
                    }
                }
            }

            return boardArray;
        }

        private List<string> ListPossibleMoves()
        {
            List<string> possibleMoves = new List<string>();

            for (int i = 1; i <= Math.Pow((Size + 1) / 2, 2); i++)
            {
                possibleMoves.Add($"{i}");
            }

            return possibleMoves;
        }

        public void ChangeBoardToRegister()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (int.TryParse(BoardArray[i, j], out _))
                        BoardArray[i, j] = "   ";
                }
            }
        }
    }
}