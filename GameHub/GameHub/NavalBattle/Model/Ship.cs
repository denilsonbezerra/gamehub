using GameHub.NavalBattle.Model.Enum;

namespace GameHub.NavalBattle.Model
{

    public class Ship
    {
        // horizontal ou vertical
        public Direction Direction { get; private set; }
        public int Size { get; private set; }
        public List<PartsOfShip> Parts { get; private set; } = new List<PartsOfShip>();


        public Ship(int size)
        {
            Random random = new Random();

            if (random.Next(1, 3) == 1)
                Direction = Direction.Horizontal;
            else
                Direction = Direction.Vertical;

            Size = size;

            for (int i = 0; i < Size; i++)
                Parts.Add(new PartsOfShip());
        }

        public bool IsDestroyed()
        {
            foreach (PartsOfShip part in Parts)
            {
                if (!part.Destroyed)
                    return false;
            }

            return true;
        }
    }
}
