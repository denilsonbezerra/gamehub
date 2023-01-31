using GameHub.Model;
using GameHub.Model.Enum;
using Newtonsoft.Json;

namespace GameHub.Repository
{
    public class Match
    {
        public DateTime DateTime { get; private set; }
        public Game Game { get; private set; }
        public Result Result { get; set; }
        public string? WinnerPlayer { get; set; }
        public string MatchPlayer1 { get; set; }
        public string MatchPlayer2 { get; set; }
        public Board Board { get; set; }
        public Board? Board2 { get; set; }

        public Match(Game game, string player1, string player2, string winnerPlayer, Result result, Board board, Board? board2)
        {
            DateTime = DateTime.Now;
            WinnerPlayer = winnerPlayer;
            Game = game;
            MatchPlayer1 = player1;
            MatchPlayer2 = player2;
            Result = result;
            Board = board;
            Board2 = board2;
        }

        [JsonConstructor]
        public Match(DateTime dateTime, Game game, string player1, string player2, string winnerPlayer, Result result, Board board, Board? board2) : this(game, player1, player2, winnerPlayer, result, board, board2)
        {
            DateTime = dateTime;
            Board2 = board2;
        }
    }
}
