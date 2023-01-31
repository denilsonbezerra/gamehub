using GameHub.Model;
using Newtonsoft.Json;

namespace GameHub.Repository
{
    public class Players
    {
        private static string _path = @"..\..\..\GameHub\Repository\Data\PlayersData.json";
        public List<Player> PlayersList { get; private set; }

        public Players() { }

        public void ReadPlayersList()
        {
            try
            {
                string stringJson = File.ReadAllText(_path);
                PlayersList = JsonConvert.DeserializeObject<List<Player>>(stringJson);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
                PlayersList = new List<Player>();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(@"..\..\..\GameHub\Repository\Data");
                File.Create(_path).Close();
                PlayersList = new List<Player>();
            }
        }

        public void SavePlayers(List<Player> jogadores)
        {
            string json = JsonConvert.SerializeObject(jogadores);
            try
            {
                File.WriteAllText(_path, json);
            }
            catch (FileNotFoundException)
            {
                File.Create(_path).Close();
                File.WriteAllText(_path, json);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(@"..\..\..\Hub\Repositories\Dados");
                File.Create(_path).Close();
                PlayersList = new List<Player>();
            }
        }
    }
}
