using GameHub.NavalBattle.Model;
using GameHub.NavalBattle.View;
using GameHub.Repository;
using GameHub.Model;
using System.Text.RegularExpressions;
using Match = GameHub.Repository.Match;

namespace GameHub.NavalBattle.Service
{
    internal class NavalBattleGame
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly BoardNavalBattle _boardPlayer1;
        private readonly BoardNavalBattle _boardPlayer2;
        private BoardNavalBattle _currentBoard;
        private List<Position> _previousShotsPlayer1;
        private List<Position> _previousShotsPlayer2;
        private readonly NavalBattleView _navalBattleView = new NavalBattleView();
        public int Player1Score { get; private set; }
        public int Player2Score { get; private set; }

        public NavalBattleGame()
        {
            _player1 = new Player("Jogador 1", "");
            _player2 = new Player("Jogador 2", "");

            _boardPlayer1 = new BoardNavalBattle();
            _boardPlayer2 = new BoardNavalBattle();

            _previousShotsPlayer1 = new List<Position>();
            _previousShotsPlayer2 = new List<Position>();

            _currentBoard = _boardPlayer1;

            Tutorial();
        }

        public NavalBattleGame(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;

            _boardPlayer1 = new BoardNavalBattle();
            _boardPlayer2 = new BoardNavalBattle();
            _currentBoard = _boardPlayer1;

            _previousShotsPlayer1 = new List<Position>();
            _previousShotsPlayer2 = new List<Position>();

            Play();
        }

        private void Tutorial()
        {
            Console.CursorVisible = false;

            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine(GameHub.View.Tutorials.NavalBattleTutorial[i]);

                Utilities.Utilities.PressAnyButton();
                if (i == 0)
                {
                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(_player1));
                    Console.WriteLine("\nAbaixo do tabuleiro é mostrado o número de navios restantes!");
                }
                else if (i == 1)
                {
                    _navalBattleView.ShowPlayerTurn(1);
                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(_player1));

                    Position position = FoundPositionToShootTutorial(_currentBoard, true);

                    Console.WriteLine($"Coordenada para atirar: {position.ToBoardPosition()}");
                    Utilities.Utilities.PressAnyButton();

                    _navalBattleView.ShowBoard(_currentBoard, position);
                    Shoot(position, true);
                    AddShootedPosition(Position.FromBoardPositionToPosition(position.ToBoardPosition()), _player1);
                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(_player1));

                    Console.WriteLine("\nComo o jogador acertou o tiro, poderá atirar novamente!\n" +
                                      "Veja o exemplo de um erro agora.");

                    Utilities.Utilities.PressAnyButton();

                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(_player1));
                    position = FoundPositionToShootTutorial(_currentBoard, false);
                    Console.WriteLine($"Digite sua jogada: {position.ToBoardPosition()}");
                    Console.WriteLine("\nA coordenada escolhida será marcada com um X.\n" +
                                        "A marca aparece mesmo se toda vida que realizar um tiro (certo ou não).\n" +
                                        "Não é possível atirar em uma mesma coordenada mais de uma vez.");
                    Utilities.Utilities.PressAnyButton();

                    _navalBattleView.ShowBoard(_currentBoard, position);
                    Shoot(position, true);
                    AddShootedPosition(Position.FromBoardPositionToPosition(position.ToBoardPosition()), _player1);
                    Thread.Sleep(1000);
                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(_player1));
                    Thread.Sleep(500);
                    Console.WriteLine("\nComo o tiro não acertou nenhum navio, agora a vez passa para o próximo Player.");
                }

                Utilities.Utilities.PressAnyButton();
            }
            Console.CursorVisible = true;
        }

        private void Play()
        {
            Player player = _player1;
            _navalBattleView.ShowPlayerTurn(1);

            while (_currentBoard.QuantityOfShips > 0)
            {
                Regex moveRegex = new Regex(@"^[a-j]([1-9]|10)$");

                string move;
                do
                {
                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(player));
                    Console.WriteLine($"\nTurno de {player.Username}");
                    Console.CursorVisible = true;

                    Console.Write("Escolha a coordenada: ");
                    move = Console.ReadLine().ToLower();
                } while (!moveRegex.IsMatch(move));

                Console.CursorVisible = false;
                Position position = Position.FromBoardPositionToPosition(move);

                if (!CheckShotPosition(position, player))
                {
                    Console.WriteLine("A posição já foi atirada!");
                    Thread.Sleep(1000);
                    continue;
                }

                AddShootedPosition(Position.FromBoardPositionToPosition(move), player);
                bool shot = Shoot(position, false);
                _navalBattleView.ShowBoard(_currentBoard, position);
                Thread.Sleep(500);

                if (!shot)
                {
                    _navalBattleView.ShowBoard(_currentBoard, PossibleShots(player));
                    player = TurnPlayer(player);
                    Thread.Sleep(500);
                    ChangeBoard();
                    _navalBattleView.ShowPlayerTurn((player.Equals(_player1)) ? 1 : 2);
                }
                else
                {
                    if (_currentBoard.CheckDestroyedShip())
                    {
                        ConsoleColor aux = Console.ForegroundColor;

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.CursorVisible = false;
                        Console.WriteLine("\nNavio afundado!!!");
                        Thread.Sleep(1000);
                        Console.ForegroundColor = aux;
                        Console.CursorVisible = true;
                    }
                }
            }

            _navalBattleView.ShowBoard(_currentBoard, PossibleShots(player));
            Console.WriteLine($"\n  Vencedor: {player.Username}");

            _boardPlayer1.ChangeBoardToRegister();
            _boardPlayer2.ChangeBoardToRegister();


            Match match = new Match
                (GameHub.Model.Enum.Game.BatalhaNaval,
                _player1.Username,
                _player2.Username,
                player.Username,
                GameHub.Model.Enum.Result.Decisivo,
                _boardPlayer1,
                _boardPlayer2);

            Matches.MatchHistory.Add(match);
            Matches.SaveMatches();

            _player1.MatchHistory.Add(match);
            _player2.MatchHistory.Add(match);
            Utilities.Utilities.PressAnyButton();
        }


        private bool Shoot(Position position, bool tutorial)
        {
            if (_currentBoard.ShipsArray[position.Line, position.Column] != null)
            {
                _currentBoard.ShipsArray[position.Line, position.Column].Destruir();

                return true;
            }

            return false;
        }

        private Player TurnPlayer(Player player)
        {
            return (player.Equals(_player1)) ? _player2 : _player1;
        }

        private void ChangeBoard()
        {
            if (_currentBoard.Equals(_boardPlayer1))
                _currentBoard = _boardPlayer2;
            else
                _currentBoard = _boardPlayer1;
        }

        private bool CheckShotPosition(Position position, Player player)
        {
            if (player.Equals(_player1))
            {
                foreach (Position shots in _previousShotsPlayer1)
                {
                    if (shots.Equals(position))
                        return false;
                }

                return true;
            }
            else
            {
                foreach (Position shots in _previousShotsPlayer2)
                {
                    if (shots.Equals(position))
                        return false;
                }

                return true;
            }
        }

        private bool[,] PossibleShots(Player player)
        {
            List<Position> positions = (player.Equals(_player1)) ? _previousShotsPlayer1 : _previousShotsPlayer2;
            bool[,] possibleShots = new bool[_currentBoard.Size, _currentBoard.Size];

            foreach (Position position in positions)
            {
                possibleShots[position.Line, position.Column] = true;
            }

            return possibleShots;
        }

        private void AddShootedPosition(Position position, Player player)
        {
            if (player.Equals(_player1))
                _previousShotsPlayer1.Add(position);
            else
                _previousShotsPlayer2.Add(position);
        }

        private Position FoundPositionToShootTutorial(BoardNavalBattle board, bool hit)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (hit)
                    {
                        if (board.ShipsArray[i, j] != null)
                            return new Position(i, j);
                    }
                    else
                    {
                        if (board.ShipsArray[i, j] == null)
                            return new Position(i, j);
                    }
                }
            }

            return new Position(0, 0);
        }
    }
}
