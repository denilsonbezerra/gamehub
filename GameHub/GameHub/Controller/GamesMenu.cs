using GameHub.Model;
using GameHub.View;
using GameHub.Utilities;
using GameHub.Chess.Service;
using GameHub.NavalBattle.Service;
using GameHub.TicTacToe.Service;

namespace GameHub.Controllers
{
    public class GamesMenu
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Hub _hub;
        private readonly HubView _hubView = new HubView();

        public GamesMenu(Player player1, Player player2, Hub hub)
        {
            _player1 = player1;
            _player2 = player2;
            _hub = hub;
        }

        public void Menu(bool tutorial)
        {
            string? chosenOption;

            do
            {
                if (!tutorial)
                {
                    _hubView.ShowGamesMenu();
                    chosenOption = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(
                        $"Olá, {_player1.Username} e {_player2.Username}!\n" +
                         "Um de vocês está jogando pela primeira vez.\n" +
                         "Vou direcionar para os Tutoriais.\n" +
                         "Caso não queiram ver algum tutorial, basta voltar ao menu principal.");
                    Utilities.Utilities.PressAnyButton();

                    chosenOption = "3";
                    tutorial = false;
                }

                switch (chosenOption)
                {
                    case "1":
                        Play();
                        break;
                    case "2":
                        PlayersHistory();
                        break;
                    case "3":
                        Tutorials();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida, escolha uma opção de 0 a 3.");
                        break;
                }
            } while (chosenOption != "0");
        }

        private void Play()
        {
            string chosenOption;

            do
            {
                _hubView.ShowGameChooseMenu();
                chosenOption = Console.ReadLine();

                if (chosenOption != "0")

                    switch (chosenOption)
                    {
                        case "1":
                            NewTicTacToe(false);
                            chosenOption = "0";
                            break;
                        case "2":
                            NewNavalBattle(false);
                            chosenOption = "0";
                            break;
                        case "3":
                            NewChess(false);
                            chosenOption = "0";
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Opção inválida, escolha uma opção de 0 a 3.");
                            break;
                    }
            } while (chosenOption != "0");
        }

        private void Tutorials()
        {
            string chosenOption;
            do
            {
                _hubView.ShowTutorialChooseMenu();

                chosenOption = Console.ReadLine();
                switch (chosenOption)
                {
                    case "1":
                        NewTicTacToe(true);
                        break;
                    case "2":
                        NewNavalBattle(true);
                        break;
                    case "3":
                        NewChess(true);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida, escolha uma opção de 0 a 3.");
                        break;
                }
            } while (chosenOption != "0");
        }

        private void NewChess(bool tutorial)
        {
            if (tutorial)
                new ChessGame();
            else
            {
                new ChessGame(_player1, _player2);
                _hub.UploadPlayersListToRepository();
            }
        }

        private void NewNavalBattle(bool tutorial)
        {
            if (tutorial)
                new NavalBattleGame();
            else
            {
                new NavalBattleGame(_player1, _player2);
                _hub.UploadPlayersListToRepository();
            }
        }

        private void NewTicTacToe(bool tutorial)
        {
            if (tutorial)
                new TicTacToeGame();
            else
            {
                new TicTacToeGame(_player1, _player2);
                _hub.UploadPlayersListToRepository();
            }
        }

        private void PlayersHistory()
        {
            string chosenOption;
            do
            {
                _hubView.ShowHistoryMenu(_player1.Username, _player2.Username);

                chosenOption = Console.ReadLine();
                switch (chosenOption)
                {
                    case "1":
                        _hubView.ShowMatchHistory(_player1.Username);
                        _hubView.ShowPlayerHistory(_player1.MatchHistory);
                        chosenOption = "0";
                        break;
                    case "2":
                        _hubView.ShowMatchHistory(_player2.Username);
                        _hubView.ShowPlayerHistory(_player2.MatchHistory);
                        chosenOption = "0";
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida, escolha uma opção de 0 a 2.");
                        break;
                }
            } while (chosenOption != "0");
        }
    }
}