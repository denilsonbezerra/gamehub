using GameHub.Model;
using GameHub.Chess.Model;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Pieces;
using GameHub.Chess.Model.Board;
using GameHub.Chess.Repository;

namespace GameHub.Chess.View
{
    public class ChessView
    {
        public void ShowMatch(Service.ChessGame match)
        {
            ShowBoard(match.Board);

            ConsoleColor color = match.CurrentColor == Color.Branco ? ConsoleColor.White : ConsoleColor.Black;
            ShowCapturedPieces(match);

            string username = match.CurrentColor == Color.Branco ? match.player1.Username : match.player2.Username;


            Console.ForegroundColor = color;
            Console.WriteLine("\nTurno: " + match.Round);

            if (!match.Finished)
            {
                Console.WriteLine($"Aguardando jogada: {username}({match.CurrentColor})");
                if (match.Check)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Xeque!");
                    Console.ForegroundColor = color;
                }
            }
            else
            {
                if (match.Draw)
                    Console.WriteLine("Empate!");
                else
                {
                    if (!match.ToResign)
                        Console.WriteLine("XEQUEMATE");
                    Console.WriteLine($"Vencedor: {username}({match.CurrentColor})");
                }
            }
        }

        public void ShowCapturedPieces(Service.ChessGame match)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.WriteLine("\nPeças capturadas:");

            Console.ForegroundColor = ConsoleColor.White;
            ShowMatchCapturedPieces(match.CapturedPieces(Color.Branco));

            Console.ForegroundColor = ConsoleColor.Black;
            ShowMatchCapturedPieces(match.CapturedPieces(Color.Preto));

            Console.ForegroundColor = aux;
        }

        public void ShowMatchCapturedPieces(HashSet<Piece> matchCaptuderPieces)
        {
            int aux = 0;
            Console.Write("[");
            foreach (Piece piece in matchCaptuderPieces)
            {
                aux++;
                if (aux % 8 == 0)
                    Console.Write("\n   ");

                Console.Write(piece + " ");
            }

            Console.WriteLine("]");
        }


        public void ShowBoard(ChessBoard board)
        {
            Console.WriteLine("\n      a  b  c  d  e  f  g  h");
            Console.Write("    ");
            Console.WriteLine("┌────────────────────────┐");

            for (int i = 0; i < board.Line; i++)
            {
                Console.Write($"  {8 - i} ");
                Console.Write("│");

                for (int j = 0; j < board.Column; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.Green;
                        else
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        else
                            Console.BackgroundColor = ConsoleColor.Green;
                    }
                    ShowPiece(board.Piece(i, j));
                }
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("│");
                if (i < board.Line - 1)
                    Console.WriteLine();
            }

            Console.Write("\n    ");
            Console.WriteLine("└────────────────────────┘");
        }

        public void ShowChessBoard(ChessBoard board, bool[,] possibleMoves)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            ConsoleColor possibleMovesColor = ConsoleColor.Gray;
            ConsoleColor foreGroundColor = Console.ForegroundColor;

            Console.WriteLine("\n      a  b  c  d  e  f  g  h");
            Console.Write("    ");
            Console.WriteLine("┌────────────────────────┐");

            for (int i = 0; i < board.Line; i++)
            {
                Console.Write($"  {8 - i} ");
                Console.Write("│");

                for (int j = 0; j < board.Column; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.Green;
                        else
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        else
                            Console.BackgroundColor = ConsoleColor.Green;
                    }

                    if (possibleMoves[i, j])
                        Console.BackgroundColor = possibleMovesColor;

                    ShowPiece(board.Piece(i, j));
                    Console.BackgroundColor = defaultColor;
                }

                Console.Write("│");

                if (i < board.Line - 1)
                    Console.WriteLine();
            }

            Console.WriteLine("\n    └────────────────────────┘");
        }

        public void ShowBoard(Board board)
        {
            Console.WriteLine(" ┌────────────────────────┐");

            for (int i = 0; i < board.Size; i++)
            {
                Console.Write(" │");

                for (int j = 0; j < board.Size; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.Green;
                        else
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        if (j % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        else
                            Console.BackgroundColor = ConsoleColor.Green;
                    }

                    if (board.BoardArray[i, j].StartsWith('x'))
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write(board.BoardArray[i, j].Replace('x', ' '));
                }

                Console.Write("│");

                if (i < board.Size - 1)
                    Console.WriteLine();
            }

            Console.WriteLine("\n └────────────────────────┘");
        }

        public void ShowPgn(Pgn pgn)
        {
            Console.Clear();
            Console.WriteLine(
                $"\nEste é o registro .pgn da partida!\n" +
                $"Ele será salvo na pasta 'GameHub > Chess > Repository > MatchesPGN'.\n" +
                $"Nome do arquivo desta partida: {pgn.Id}.pgn\n\n");

            Console.WriteLine(Pgns.PgnToString(pgn));
        }

        public ChessPosition ReadChessPosition(string position)
        {
            position = position.ToLower();
            char coluna = position[0];
            int linha = int.Parse(position[1] + "");
            return new ChessPosition(coluna, linha);
        }

        public void ShowPiece(Piece piece)
        {
            ConsoleColor defaultForeGround = Console.ForegroundColor;

            if (piece == null)
                Console.Write("   ");
            else
            {
                if (piece.Color == Color.Branco)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" {piece} ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {piece} ");
                }
            }

            Console.ForegroundColor = defaultForeGround;
        }
    }
}
