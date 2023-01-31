using GameHub.Model.Enum;
using GameHub.Repository;
using Newtonsoft.Json;


namespace GameHub.Model
{
    [JsonObject]
    public class Player
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Match>? MatchHistory { get; private set; } = new List<Match>();

        public Player(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [JsonConstructor]
        public Player(string username, string password, List<Match> matchesHistory) : this(username, password)
        {

            MatchHistory = matchesHistory;
        }

        public int GetScore(Game game)
        {
            int score = 0;

            foreach (Match match in MatchHistory)
            {
                if (match.Game.Equals(game))
                {
                    if (match.Result != Result.Empate)
                    {
                        if (match.WinnerPlayer.Equals(Username))
                            score += 3;
                        else
                            score--;
                    }
                    else
                        score++;
                }
            }

            return score;
        }

        public void ChangeUsername(string username) => Username = username;
        public void ChangePassword(string password) => Password = password;

        public void ShowPlayerInfo()
        {
            Console.WriteLine($"Nome do usuario: {Username}");
            Console.WriteLine($"Senha do usuario: {Password}");
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Player)
                return false;

            Player other = obj as Player;
            return Username.Equals(other.Username);
        }

        public override string ToString()
        {
            return $"{Username} | Pontuação Jogo da Velha [{GetScore(Game.JogoDaVelha)}] | Pontuação Batalha Naval [{GetScore(Game.BatalhaNaval)}] | Pontuação Xadrez [{GetScore(Game.Xadrez)}] | Total [{GetScore(Game.JogoDaVelha) + GetScore(Game.BatalhaNaval) + GetScore(Game.Xadrez)}]";
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() + Password.GetHashCode();
        }
    }
}
