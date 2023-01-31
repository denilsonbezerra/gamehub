using GameHub.Model.Enum;
using GameHub.Repository;
using GameHub.Chess.View;

namespace GameHub.View
{
    public class HubView
    {
        private readonly static string _gameHub = @"
            ██   ██ ██    ██ ██████      ██████  ███████          ██  ██████   ██████   ██████  ███████ 
            ██   ██ ██    ██ ██   ██     ██   ██ ██               ██ ██    ██ ██       ██    ██ ██      
            ███████ ██    ██ ██████      ██   ██ █████            ██ ██    ██ ██   ███ ██    ██ ███████ 
            ██   ██ ██    ██ ██   ██     ██   ██ ██          ██   ██ ██    ██ ██    ██ ██    ██      ██ 
            ██   ██  ██████  ██████      ██████  ███████      █████   ██████   ██████   ██████  ███████ 
        ";

        private readonly static string _account = @"
             ██████  ██████  ███    ██ ████████  █████  
            ██      ██    ██ ████   ██    ██    ██   ██ 
            ██      ██    ██ ██ ██  ██    ██    ███████ 
            ██      ██    ██ ██  ██ ██    ██    ██   ██ 
             ██████  ██████  ██   ████    ██    ██   ██ 
        ";

        private readonly static string _games = @"
                 ██  ██████   ██████   ██████  ███████ 
                 ██ ██    ██ ██       ██    ██ ██      
                 ██ ██    ██ ██   ███ ██    ██ ███████ 
            ██   ██ ██    ██ ██    ██ ██    ██      ██ 
             █████   ██████   ██████   ██████  ███████ 
        ";

        private readonly static string _register = @"
            ██████  ███████  ██████  ██ ███████ ████████ ██████   █████  ██████  
            ██   ██ ██      ██       ██ ██         ██    ██   ██ ██   ██ ██   ██ 
            ██████  █████   ██   ███ ██ ███████    ██    ██████  ███████ ██████  
            ██   ██ ██      ██    ██ ██      ██    ██    ██   ██ ██   ██ ██   ██ 
            ██   ██ ███████  ██████  ██ ███████    ██    ██   ██ ██   ██ ██   ██
        ";

        private readonly static string _login = @"
            ███████ ███    ██ ████████ ██████   █████  ██████  
            ██      ████   ██    ██    ██   ██ ██   ██ ██   ██ 
            █████   ██ ██  ██    ██    ██████  ███████ ██████  
            ██      ██  ██ ██    ██    ██   ██ ██   ██ ██   ██ 
            ███████ ██   ████    ██    ██   ██ ██   ██ ██   ██ 
        ";

        private readonly static string _players = @"
                 ██  ██████   ██████   █████  ██████   ██████  ██████  ███████ ███████ 
                 ██ ██    ██ ██       ██   ██ ██   ██ ██    ██ ██   ██ ██      ██      
                 ██ ██    ██ ██   ███ ███████ ██   ██ ██    ██ ██████  █████   ███████ 
            ██   ██ ██    ██ ██    ██ ██   ██ ██   ██ ██    ██ ██   ██ ██           ██ 
             █████   ██████   ██████  ██   ██ ██████   ██████  ██   ██ ███████ ███████ 
        ";

        private readonly static string _ranking = @"
            ██████   █████  ███    ██ ██   ██ ██ ███    ██  ██████  
            ██   ██ ██   ██ ████   ██ ██  ██  ██ ████   ██ ██       
            ██████  ███████ ██ ██  ██ █████   ██ ██ ██  ██ ██   ███ 
            ██   ██ ██   ██ ██  ██ ██ ██  ██  ██ ██  ██ ██ ██    ██ 
            ██   ██ ██   ██ ██   ████ ██   ██ ██ ██   ████  ██████  
        ";

        private readonly static string _history = @"
            ██   ██ ██ ███████ ████████  ██████  ██████  ██  ██████  ██████  
            ██   ██ ██ ██         ██    ██    ██ ██   ██ ██ ██      ██    ██ 
            ███████ ██ ███████    ██    ██    ██ ██████  ██ ██      ██    ██ 
            ██   ██ ██      ██    ██    ██    ██ ██   ██ ██ ██      ██    ██ 
            ██   ██ ██ ███████    ██     ██████  ██   ██ ██  ██████  ██████  
        ";

        private readonly static string _tutorial = @"
            ████████ ██    ██ ████████  ██████  ██████  ██  █████  ██      
               ██    ██    ██    ██    ██    ██ ██   ██ ██ ██   ██ ██      
               ██    ██    ██    ██    ██    ██ ██████  ██ ███████ ██      
               ██    ██    ██    ██    ██    ██ ██   ██ ██ ██   ██ ██      
               ██     ██████     ██     ██████  ██   ██ ██ ██   ██ ███████ 
        ";

        #region Partida
        public void ShowMatch(Match match)
        {
            DateTime dateTime = new DateTime(match.DateTime.Year, match.DateTime.Month, match.DateTime.Day, match.DateTime.Hour, match.DateTime.Minute, 0, match.DateTime.Kind);
            string gameAux1 = string.Empty;
            string gameAux2 = string.Empty;

            if (match.Game == Game.JogoDaVelha)
            {
                gameAux1 = "X";
                gameAux2 = "O";
            } else
            {
                gameAux1 = "Brancas";
                gameAux2 = "Pretas";
            }

            Console.WriteLine($"{Utilities.Utilities.Line}\n");
            Console.WriteLine($"Partida de {match.Game} | {dateTime}\n");
            Console.WriteLine($"{match.MatchPlayer1}({gameAux1}) VS {match.MatchPlayer2}({gameAux2})\n");

            if (match.Game == Game.JogoDaVelha)
            {
                ChessView tela = new();
                tela.ShowBoard(match.Board);
            } else
            {
                ChessView tela = new();
                tela.ShowBoard(match.Board);
            }


            if (match.Result == Result.Empate)
                Console.WriteLine("\n  Empate");
            else
                Console.WriteLine($"\n  Vencedor: {match.WinnerPlayer}");
        }

        #endregion

        #region Hub

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine(_gameHub + "\n\n");
            Console.WriteLine("[1] - Menu de Jogos");
            Console.WriteLine("[2] - Melhores Jogadores");
            Console.WriteLine("[3] - Historico de Partidas");
            Console.WriteLine("[4] - Registrar Novo Jogador");
            Console.WriteLine("[5] - Listar Jogadores");
            Console.WriteLine("[6] - Atualizar um Jogador");
            Console.WriteLine("[0] - Encerrar");
            Console.Write("\nDigite a opção desejada: ");
        }

        public void ShowLogin(bool manipularConta)
        {
            Console.Clear();
            Console.WriteLine(_login);
            if (!manipularConta)
                Console.WriteLine("Para começar a jogar, é necessário que dois jogadores estejam logados.\n");
            else
                Console.WriteLine("Para manipular sua conta é necessário logar nela primeiro.\n");
        }

        public void ShowRegister()
        {
            Console.Clear();
            Console.WriteLine(_register + "\n");
            Console.WriteLine("Preencha as informações para cadastrar um novo jogador.");
            Console.WriteLine("[Nome de Usuário pode conter de 2 a 30 caracteres (somente letras)]");
            Console.WriteLine("[Senha pode conter de 6 a 16 caracteres (letras e números)]\n\n");
        }

        public void ShowPlayersList()
        {
            Console.Clear();
            Console.WriteLine(_players + "\n\n");
        }

        public void ShowRank()
        {
            Console.Clear();
            Console.WriteLine(_ranking);
            Console.WriteLine("\n[Vitória = 3pts | Empate = 1pt | Derrota = -1 pt]");
            Console.WriteLine("Top 10 jogadores com maior pontuação:");
        }

        public void ShowUser()
        {
            Console.Clear();
            Console.WriteLine(_account + "\n");
        }

        public void ShowUserUpdateOptions()
        {
            ShowUser();
            Console.WriteLine("Escolha uma opção para alterar sua conta.");
            Console.WriteLine("[1]- Alterar Nome de Usuário");
            Console.WriteLine("[2]- Alterar Senha");
            Console.WriteLine("[3]- Deletar Conta");
            Console.WriteLine("[0]- Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        #endregion

        #region MenuDeJogos

        public void ShowGamesMenu()
        {
            Console.Clear();
            Console.WriteLine(_games + "\n");
            Console.WriteLine("[1] - Jogar");
            Console.WriteLine("[2] - Histórico dos Jogadores");
            Console.WriteLine("[3] - Tutoriais dos Jogos");
            Console.WriteLine("[0] - Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        public void ShowGameChooseMenu()
        {
            Console.Clear();
            Console.WriteLine(_games + "\n");
            Console.WriteLine("[1] - Jogo da Velha");
            Console.WriteLine("[2] - Batalha Naval" );
            Console.WriteLine("[3] - Xadrez" );
            Console.WriteLine("[0] - Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        public void ShowMatchHistory(string? username)
        {
            Console.Clear();
            Console.WriteLine(_history + "\n");
            if (username != null)
                Console.WriteLine($"  Histórico de {username}:\n");
        }

        public void ShowHistoryMenu(string username1, string username2)
        {
            Console.Clear();
            Console.WriteLine(_history + "\n\n");
            Console.WriteLine($"[1] - Histórico de {username1}");
            Console.WriteLine($"[2] - Histórico de {username2}");
            Console.WriteLine($"[0] - Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        public void ShowTutorialChooseMenu()
        {
            Console.Clear();
            Console.WriteLine(_tutorial + "\n\n");
            Console.WriteLine($"[1] - Tutorial de Jogo da Velha");
            Console.WriteLine($"[2] - Tutorial de Xadrez");
            Console.WriteLine($"[0] - Voltar");
            Console.Write("\nDigite a opção desejada: ");
        }

        #endregion

        #region Jogador

        public void ShowPlayerHistory(List<Match> matches)
        {
            HubView tela = new();
            foreach (Match match in matches)
            {
                tela.ShowMatch(match);
            }

            Utilities.Utilities.PressAnyButton();
        }

        #endregion
    }
}
