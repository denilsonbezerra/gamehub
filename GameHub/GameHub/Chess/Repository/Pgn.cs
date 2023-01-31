namespace GameHub.Chess.Repository
{
    public class Pgn
    {
        public Guid Id { get; private set; }
        public List<string>? Rounds { get; private set; }
        public string Event { get; private set; } = null!;
        public string Site { get; private set; } = null!;
        public DateOnly Date { get; private set; }
        public string Round { get; private set; }
        public string White { get; private set; } = null!;
        public string Black { get; private set; } = null!;
        public string Result { get; set; }

        public Pgn(string playerWhite, string playerBlack)
        {
            Id = Guid.NewGuid();
            Rounds = new List<string>();
            Event = "Casual Game";
            Site = "Hub de Jogos";
            Date = DateOnly.FromDateTime(DateTime.Now);
            Round = "-";
            White = playerWhite;
            Black = playerBlack;
            Result = "*";
        }
    }
}
