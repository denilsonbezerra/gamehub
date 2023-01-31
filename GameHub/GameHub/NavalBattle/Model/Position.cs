namespace GameHub.NavalBattle.Model
{
    public class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position()
        {
            Random random = new Random();
            Line = random.Next(0, 10);
            Column = random.Next(0, 10);
        }
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public void ChangePosition(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public string ToBoardPosition()
        {
            char column = 'a';

            switch (Column)
            {
                case 1:
                    column = 'b';
                    break;
                case 2:
                    column = 'c';
                    break;
                case 3:
                    column = 'd';
                    break;
                case 4:
                    column = 'e';
                    break;
                case 5:
                    column = 'f';
                    break;
                case 6:
                    column = 'g';
                    break;
                case 7:
                    column = 'h';
                    break;
                case 8:
                    column = 'i';
                    break;
                case 9:
                    column = 'j';
                    break;
            }

            return $"{column}{Line + 1}";
        }

        public static Position FromBoardPositionToPosition(string move)
        {
            int line = (int)Char.GetNumericValue(move[1]) - 1;
            char column = move[0];

            if (move.Length > 2)
                line = 9;

            return new Position(line, column - 'a');
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Position)
                return false;

            Position other = obj as Position;
            return (other.Column == Column && other.Line == Line);
        }

    }
}
