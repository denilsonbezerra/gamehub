namespace GameHub.NavalBattle.Model
{
    public class PartsOfShip
    {
        public Position Position { get; set; }
        public bool Destroyed { get; private set; }

        public PartsOfShip()
        {
            Position = new Position(0, 0);
            Destroyed = false;
        }

        public void Destruir()
        {
            Destroyed = true;
        }
    }
}
