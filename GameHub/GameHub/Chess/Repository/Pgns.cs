using System.Text;

namespace GameHub.Chess.Repository
{
    public static class Pgns
    {
        private static string _path = @"..\..\..\GameHub\Chess\Repository\MatchesPGN\";

        public static void CreatePgnFile(Pgn pgn)
        {
            string pathFinal = _path + $"{pgn.Id}.pgn";
            try
            {
                File.Create(pathFinal).Close();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(_path);
                File.Create(pathFinal).Close();
            }

            File.WriteAllText(pathFinal, PgnToString(pgn));
        }

        public static string PgnToString(Pgn pgn)
        {
            StringBuilder sb = new StringBuilder();

            string date = $"{pgn.Date.Year}.{pgn.Date.Month}.{pgn.Date.Day}";
            sb.AppendLine(
                $"[Event \"{pgn.Event}\"]\n" +
                $"[Site \"{pgn.Site}\"]\n" +
                $"[Date \"{date}\"]\n" +
                $"[Round \"{pgn.Round}\"]\n" +
                $"[White \"{pgn.White}\"]\n" +
                $"[Black \"{pgn.Black}\"]\n" +
                $"[Result \"{pgn.Result}\"]\n");

            foreach (string round in pgn.Rounds)
            {
                sb.Append(round + " ");
            }

            sb.Append($"{pgn.Result}");

            return sb.ToString();
        }

    }
}
