using GameHub.Model.Interface;

namespace GameHub.NavalBattle.Model
{
    public class BoardNavalBattle : GameHub.Model.Board, IRefreshBoard
    {
        public PartsOfShip?[,] ShipsArray { get; private set; }
        public List<Ship> Ships { get; private set; }
        public int QuantityOfShips => Ships.Count;

        public BoardNavalBattle()
        {
            Size = 10;
            ShipsArray = new PartsOfShip?[10, 10];
            FillArray();
        }

        private void FillArray()
        {
            Ships = new List<Ship>();
            AddShip(4);
            AddShip(4);
            AddShip(3);
            AddShip(3);
            AddShip(2);
            AddShip(2);
            AddShip(2);
        }

        private void AddShip(int size)
        {
            Ship ship = new Ship(size);
            Position position = new Position();
            Position auxPosition = new Position(position.Line, position.Column);

            for (int i = 0; i < ship.Size; i++)
            {
                if (ShipsArray[auxPosition.Line, auxPosition.Column] != null)
                {
                    AddShip(size);
                    return;
                }

                if (ship.Direction == Enum.Direction.Horizontal)
                    auxPosition.Line++;
                else
                    auxPosition.Column++;

                if (auxPosition.Column >= 10 || auxPosition.Line >= 10)
                {
                    AddShip(size);
                    return;
                }
            }

            for (int k = 0; k < ship.Size; k++)
            {
                ship.Parts[k].Position.ChangePosition(position.Line, position.Column);
                ShipsArray[position.Line, position.Column] = ship.Parts[k];
                if (ship.Direction == Enum.Direction.Horizontal)
                    position.Line++;
                else
                    position.Column++;
            }
            Ships.Add(ship);
        }


        public bool CheckDestroyedShip()
        {
            for (int i = 0; i < QuantityOfShips; i++)
            {
                Ship ship = Ships[i];

                if (ship.IsDestroyed())
                {
                    Ships.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void ChangeBoardToRegister()
        {
            BoardArray = new string[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    string aux = "   ";

                    if (ShipsArray[i, j] != null)
                    {
                        if (ShipsArray[i, j].Destroyed)
                            aux = " x ";
                        else
                            aux = " i ";
                    }

                    BoardArray[i, j] = aux;
                }
            }
        }

    }
}
