using Newtonsoft.Json;

namespace GameHub.Repository
{
    public static class Matches
    {
        private static string _path = @"..\..\..\GameHub\Repository\Data\MatchesHistory.json";
        public static List<Match> MatchHistory { get; private set; } = new();

        public static void LoadMatch()
        {
            try
            {
                string stringJson = File.ReadAllText(_path);
                if (!(string.IsNullOrEmpty(stringJson)))
                    MatchHistory = JsonConvert.DeserializeObject<List<Match>>(stringJson);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
            }
        }

        public static void SaveMatches()
        {
            string json = JsonConvert.SerializeObject(MatchHistory);
            try
            {
                File.WriteAllText(_path, json);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
                File.WriteAllText(_path, json);
            }
        }
    }
}
