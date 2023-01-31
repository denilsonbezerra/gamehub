using GameHub.Repository;
using GameHub.View;
using GameHub.Model;
using GameHub.Model.Enum;
using GameHub.TicTacToe.Models.Enums;
using GameHub.TicTacToe.Models;
using GameHub.TicTacToe.Views;

namespace GameHub.TicTacToe.Service
{
    public class TicTacToeGame
    {
        private readonly TicTacToeView _ticTacToeView = new();
        private readonly Player _player1;
        private readonly Player _player2;

        public TicTacToeGame()
        {
            _player1 = new Player("Jogador 1", "");
            _player2 = new Player("Jogador 2", "");

            Tutorial();
        }

        public TicTacToeGame(Player jogador1, Player jogador2)
        {
            _player1 = jogador1;
            _player2 = jogador2;

            Play();
        }

        private void MakeMove(string movePosition, string symbol, BoardTicTacToe board)
        {
            for (int i = 0; i < board.Size; i++)
                for (int j = 0; j < board.Size; j++)
                    if (board.BoardArray[i, j].Trim().Equals(movePosition))
                        board.BoardArray[i, j] = symbol;
        }

        private void Tutorial()
        {
            BoardTicTacToe board = new(3);
            ConsoleColor aux = Console.ForegroundColor;

            for (int i = 0; i < 4; i++)
            {
                Console.Clear();
                Console.WriteLine(Tutorials.TicTacToeTutorial[i]);
                Utilities.Utilities.PressAnyButton();

                if (i == 0)
                {
                    Console.Write("\n\nDigite o tamanho do jogo (3 a 10): 3");
                    Console.WriteLine("\nVamos usar o tabuleiro 3x3 neste tutorial!");
                    Utilities.Utilities.PressAnyButton();

                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Console.WriteLine("\nAqui está um tabuleiro 3x3!");
                }
                else if (i == 1)
                {
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Console.Write($"\n  Vez de {_player1.Username}(");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("X");
                    Console.ForegroundColor = aux;
                    Console.WriteLine(")");
                    Console.WriteLine("\n  Posição da jogada: 1");

                    MakeMove("1", $" {Symbol.X} ", board);
                    Utilities.Utilities.PressAnyButton();

                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Console.WriteLine("\n\nO primeiro jogador escolheu a posição 1,\n" +
                                      "logo o 'X' aparece na posição.\n" +
                                      "Agora é vez do segundo jogador.");
                    Utilities.Utilities.PressAnyButton();

                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Console.Write($"\n  Vez de {_player2.Username}(");

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("O");
                    Console.ForegroundColor = aux;
                    Console.WriteLine(")");
                    Console.WriteLine("\n  Posição da jogada: 5");
                    MakeMove("5", $" {Symbol.O} ", board);
                    Utilities.Utilities.PressAnyButton();

                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Console.WriteLine("\n\nO segundo jogador escolheu a posição 5,\n" +
                                      "a lógica é a mesma: 'O' aparece na posição.");
                }
                else if (i == 2)
                {
                    MakeMove("4", $" {Symbol.X} ", board);
                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Thread.Sleep(2000);
                    MakeMove("3", $" {Symbol.O} ", board);
                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    MakeMove("7", $" {Symbol.X} ", board);
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine();
                    _ticTacToeView.ShowTicTacToeBoard(board);
                    Console.WriteLine($"\nVencedor: {_player1.Username}(X)");
                }

                Utilities.Utilities.PressAnyButton();
            }
        }

        private void Play()
        {
            int size;
            do
            {
                Console.Clear();
                Console.Write("\nDigite o tamanho do jogo (3 a 10): ");

                if (int.TryParse(Console.ReadLine(), out size))
                {
                    if (size >= 3 && size <= 10)
                    {
                        Console.Clear();
                        break;
                    }
                }
            } while (true);

            BoardTicTacToe board = new BoardTicTacToe(size);

            int round = 1;
            string? winner = null;
            Player player = _player1;
            string move = $" {Symbol.X} ";

            while (true)
            {
                string position;

                do
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    _ticTacToeView.ShowTicTacToeBoard(board);

                    ConsoleColor aux = Console.ForegroundColor;

                    Console.Write($"\n  Vez de {player.Username} (");
                    Console.ForegroundColor = player.Equals(_player1) ? ConsoleColor.Black : ConsoleColor.DarkRed;
                    Console.Write($"{move.Trim()}");
                    Console.ForegroundColor = aux;
                    Console.WriteLine(")\n");
                    Console.Write("Posição da jogada: ");

                    position = Console.ReadLine();
                } while (!board.possibleMoves.Contains(position));

                round++;

                board.possibleMoves.Remove(position);
                MakeMove(position, move, board);

                if (round >= board.Size)
                    winner = CheckWinOrOver(board);

                if (winner == null)
                {
                    if (player == _player1)
                    {
                        player = _player2;
                        move = $" {Symbol.O} ";
                    }
                    else
                    {
                        player = _player1;
                        move = $" {Symbol.X} ";
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    _ticTacToeView.ShowTicTacToeBoard(board);

                    Result result = Result.Empate;
                    string player1 = _player1.Username;
                    string player2 = _player2.Username;
                    string playerWin = _player1.Username;

                    if (winner == "Velha")
                        Console.WriteLine("\nDeu velha!");
                    else
                    {
                        result = Result.Decisivo;
                        Console.WriteLine($"\nVencedor: {player.Username} ({winner})");

                        if (player.Equals(_player2))
                            playerWin = _player2.Username;
                    }

                    board.ChangeBoardToRegister();
                    Match match = new Match(Game.JogoDaVelha, player1, player2, playerWin, result, board, null);

                    Matches.MatchHistory.Add(match);
                    Matches.SaveMatches();

                    _player1.MatchHistory.Add(match);
                    _player2.MatchHistory.Add(match);
                    Utilities.Utilities.PressAnyButton();
                    break;
                }
            }
        }

        private string? CheckWinOrOver(BoardTicTacToe board)
        {

            int auxSize = (board.Size + 1) / 2;

            string[] mainDiagonalValue = new string[auxSize];
            string[] secondaryDiagonalValue = new string[auxSize];

            int secondaryDiagonalAux = auxSize + 1;
            if (auxSize % 2 == 0)
                secondaryDiagonalAux = auxSize + 2;

            List<string[]> columns = new List<string[]>();
            for (int i = 0; i < board.Size + 1; i += 2)
            {
                columns.Add(new string[auxSize]);
            }

            for (int i = 0; i < board.Size; i += 2)
            {
                string[] lineValue = new string[auxSize];

                for (int j = 0; j < board.Size; j += 2)
                {
                    string value = board.BoardArray[i, j].Trim();

                    int auxArrayPositionJ = (int)Math.Floor(j / 2.0);
                    int auxArrayPositionI = (int)Math.Floor(i / 2.0);

                    lineValue[auxArrayPositionJ] = value;

                    if (i == j)
                        mainDiagonalValue[auxArrayPositionJ] = value;

                    if (j == secondaryDiagonalAux)
                    {
                        secondaryDiagonalValue[auxArrayPositionJ] = value;
                        secondaryDiagonalAux -= 2;
                    }
            
                    columns[auxArrayPositionJ][auxArrayPositionI] = board.BoardArray[i, j].Trim();
                }

                lineValue = lineValue.Distinct().ToArray();
                if (lineValue.Length == 1)
                    return lineValue[0];
            }

            mainDiagonalValue = mainDiagonalValue.Distinct().ToArray();
            if (mainDiagonalValue.Length == 1)
                return mainDiagonalValue[0];

            secondaryDiagonalValue = secondaryDiagonalValue.Distinct().ToArray();
            if (secondaryDiagonalValue.Length == 1)
                return secondaryDiagonalValue[0];

            for (int i = 0; i < columns.Count; i++)
            {
                string[] column = columns[i];
                column = column.Distinct().ToArray();
                if (column.Length == 1)
                    return column[0];
            }

            if (board.possibleMoves.Count == 0)
                return "Velha";

            return null;
        }
    }
}
