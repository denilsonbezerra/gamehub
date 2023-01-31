using GameHub.Model;
using GameHub.Model.Enum;
using GameHub.Repository;
using GameHub.View;
using System.Text.RegularExpressions;

namespace GameHub.Controllers
{
    public class Hub
    {
        private readonly HubView _hubView = new();
        private readonly Regex _usernameRegex = new(@"^[a-zA-Z]{2,30}$");
        private readonly Regex _passwordRegex = new(@"^[a-zA-Z0-9]{6,16}$");
        private readonly List<Player> _players;
        private readonly Players _playersRepository = new Players();

        public Hub()
        {
            _playersRepository.ReadPlayersList();
            Matches.LoadMatch();

            if (_playersRepository.PlayersList != null)
                _players = _playersRepository.PlayersList;
            else
                _players = new List<Player>();
        }

        public void MainMenu()
        {
            string chosenOption;

            do
            {
                _hubView.ShowMainMenu();
                chosenOption = Console.ReadLine();

                switch (chosenOption)
                {
                    case "1":
                        ShowGameMenu();
                        break;
                    case "2":
                        PlayersRank();
                        break;
                    case "3":
                        MatchHistory();
                        break;
                    case "4":
                        RegisterPlayer();
                        break;
                    case "5":
                        ListPlayers();
                        break;
                    case "6":
                        UpdatePlayer();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida, escolha uma opção de 0 a 6.");
                        Thread.Sleep(500);
                        break;
                }
            } while (chosenOption != "0");
        }

        private void ShowGameMenu()
        {
            _hubView.ShowLogin(false);
            if (_players.Count < 2)
            {
                Console.WriteLine(
                    "Precisa de dois jogadores para acessar os jogos!\n" +
                    "Por favor, registre mais um jogador!\n");

                Utilities.Utilities.PressAnyButton();
                RegisterPlayer();
                ShowGameMenu();
                return;
            }

            _hubView.ShowLogin(false);

            Player? player1 = null, player2 = null;

            int tryLogin = 0;
            do
            {
                if (++tryLogin > 5)
                {
                    WrongAcessError();
                    return;
                }

                player1 = Login(1);
            } while (player1 == null);

            tryLogin = 0;
            do
            {
                if (++tryLogin > 5)
                {
                    WrongAcessError();
                    return;
                }

                player2 = Login(2);

                if (player1 == player2)
                {
                    Console.WriteLine("\nOs 2 jogadores não podem ser iguais, acesse uma conta diferente.");
                    Utilities.Utilities.PressAnyButton();
                    return;
                }
            } while (player2 == null);

            GamesMenu gamesMenu = new(player1, player2, this);

            if (player1.MatchHistory.Count == 0 || player2.MatchHistory.Count == 0)
                gamesMenu.Menu(true);
            else
                gamesMenu.Menu(false);
        }

        private void RegisterPlayer()
        {
            _hubView.ShowRegister();

            Console.Write("Nome do Usuário: ");
            string username = Console.ReadLine();

            Console.Write("Senha: ");
            string password = ReadPassword();

            if (_usernameRegex.IsMatch(username) && _passwordRegex.IsMatch(password))
            {
                foreach (Player player in _players)
                {
                    if (username.Equals(player.Username))
                    {
                        Console.WriteLine("Jogador já cadastrado.");
                        Thread.Sleep(500);
                        return;
                    }
                }

                Player newPlayer = new Player(username, password);
                _players.Add(newPlayer);

                UploadPlayersListToRepository();
                Console.WriteLine($"\nJogador {username} cadastrado com sucesso!!");
            } else
                Console.WriteLine("\nNome de usuário ou senha inválido(s).");

            Utilities.Utilities.PressAnyButton();
        }

        private void ListPlayers()
        {
            _hubView.ShowPlayersList();

            if (_players.Count == 0)
                Console.WriteLine("\nNenhum jogador cadastrado.");
            else
            {
                foreach (Player jogador in _players)
                    Console.WriteLine($"{jogador}\n");
            }

            Utilities.Utilities.PressAnyButton();
        }

        private void PlayersRank()
        {
            List<Player> playersRank = _players.OrderByDescending(j => j.GetScore(Game.JogoDaVelha) +
                                                                  j.GetScore(Game.Xadrez)).ToList();

            if (playersRank.Count < 1)
                Console.WriteLine("\nNenhum jogador cadastrado.");
            else
            {
                _hubView.ShowRank();
                for (int i = 0; i < playersRank.Count && i < 10; i++)
                {
                    if (playersRank[i].GetScore(Game.JogoDaVelha) + playersRank[i].GetScore(Game.Xadrez) <= 0)
                        continue;

                    Console.WriteLine($"Top #{i + 1}: {playersRank[i]}\n");
                }
            }

            Utilities.Utilities.PressAnyButton();
        }

        private Player? Login(int? @int)
        {
            Player? player = null;
            if (@int != null)
            {
                _hubView.ShowLogin(false);
                Console.WriteLine($"Jogador {@int}:\n");
            } else
                _hubView.ShowLogin(true);

            Console.Write("Nome do Usuário: ");
            string? username = Console.ReadLine();

            if (!(string.IsNullOrEmpty(username)))
            {
                Console.Write("Senha: ");
                string password = ReadPassword();

                if (!(string.IsNullOrEmpty(password)))
                {
                    player = _players.Find(j => j.Username.ToLower().Equals(username.ToLower()) &&
                                                j.Password.ToUpper().Equals(password.ToUpper()));
                }

            }

            if (player != null)
                Console.WriteLine("\nJogador logado com sucesso!");
            else
                Console.WriteLine("\nJogador não encontrado");

            Utilities.Utilities.PressAnyButton();

            return player;
        }

        private string ReadPassword()
        {
            List<char> password = new List<char>();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    int cursorPositionX = Console.CursorLeft;
                    int cursorPositionY = Console.CursorTop;

                    if (password.Count() > 0)
                    {
                        Console.SetCursorPosition(cursorPositionX - 1, cursorPositionY);
                        Console.Write(" ");
                        Console.SetCursorPosition(cursorPositionX - 1, cursorPositionY);
                        Console.Write("");
                        password.RemoveAt(password.Count() - 1);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    password.Add(key.KeyChar);
                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return string.Join("", password);
        }

        private void WrongAcessError()
        {
            _hubView.ShowLogin(false);
            Console.WriteLine("Login errado 5 vezes, por favor lembre seu cadastro antes de tentar novamente.");
            Utilities.Utilities.PressAnyButton();
        }

        private void UpdatePlayer()
        {
            _hubView.ShowLogin(true);

            Player player = Login(null);
            if (player != null)
            {
                string chosenOption;

                do
                {
                    _hubView.ShowUserUpdateOptions();
                    chosenOption = Console.ReadLine();

                    switch (chosenOption)
                    {
                        case "1":
                            ChangeUsername(player);
                            break;
                        case "2":
                            ChangePassword(player);
                            break;
                        case "3":
                            if (DeleteUser(player))
                                chosenOption = "0";
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Opção inválida, escolha uma opção de 0 a 3.");
                            Thread.Sleep(500);
                            break;
                    }
                } while (chosenOption != "0");
            }
        }

        public void UploadPlayersListToRepository()
        {
            _playersRepository.SavePlayers(_players);
        }

        private void ChangeUsername(Player player)
        {
            _hubView.ShowUser();

            Console.Write("Digite o novo nome de usuário: ");
            string newUsername = Console.ReadLine();

            if (_usernameRegex.IsMatch(newUsername))
            {
                foreach (Player eachPlayer in _players)
                {
                    if (eachPlayer.Username.ToUpper().Equals(newUsername.ToUpper()))
                    {
                        Console.WriteLine("Nome já cadastrado.");
                        Utilities.Utilities.PressAnyButton();
                        return;
                    }
                }

                player.ChangeUsername(newUsername);

                Console.WriteLine($"Deu certo, {newUsername}!");
                Console.WriteLine("Nome de Usuário alterado com sucesso!");

                UploadPlayersListToRepository();
            } else
            {
                Console.WriteLine("Nome inválido.");
            }

            Utilities.Utilities.PressAnyButton();
        }

        private void ChangePassword(Player player)
        {
            _hubView.ShowUser();

            Console.Write("Digite a Nova Senha: ");
            string newPassword = ReadPassword();

            if (_passwordRegex.IsMatch(newPassword))
            {
                player.ChangePassword(newPassword);
                _hubView.ShowUser();
                Console.WriteLine("Senha alterada com sucesso!");

                UploadPlayersListToRepository();
            } else
            {
                Console.WriteLine("Senha inválida.");
            }
            Utilities.Utilities.PressAnyButton();
        }

        private bool DeleteUser(Player player)
        {
            if (!_players.Contains(player))
                return true;

            string chosenOption;

            do
            {
                _hubView.ShowUser();

                Console.WriteLine("\nTem certeza que quer deletar sua conta?");
                Console.WriteLine("[1] - Sim");
                Console.WriteLine("[2] - Não");
                Console.Write("Digite a opção desejada: ");
                chosenOption = Console.ReadLine();

                switch (chosenOption)
                {
                    case "1":
                        _players.Remove(player);
                        Console.WriteLine("\nConta excluída com sucesso.");

                        Utilities.Utilities.PressAnyButton();
                        UploadPlayersListToRepository();

                        return true;
                    case "2":
                        return false;
                    default:
                        Console.WriteLine("Opção inválida, escolha 1 ou 2.");
                        Thread.Sleep(500);
                        break;
                }
            } while (chosenOption != "0");

            return false;
        }

        private void MatchHistory()
        {
            Console.Clear();
            _hubView.ShowMatchHistory(null);

            if (Matches.MatchHistory.Count != 0)
            {
                foreach (Repository.Match match in Matches.MatchHistory)
                    _hubView.ShowMatch(match);
            } else
                Console.WriteLine("Nenhuma partida foi jogada.");

            Utilities.Utilities.PressAnyButton();
        }
    }
}