using Newtonsoft.Json;

namespace GameHub.Model
{
    public class Board
    {
        public virtual string[,]? BoardArray { get; protected set; }
        public virtual int Size { get; protected set; }

        public Board() { }

        public Board(int size)
        {
            Size = size;
        }

        [JsonConstructor]
        public Board(string[,] boardArray, int size) : this(size)
        {
            BoardArray = boardArray;
        }
    }
}
