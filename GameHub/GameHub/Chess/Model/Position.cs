namespace GameHub.Chess.Model
{
    public class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public void SetValues(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return Line + ", " + Column;
        }

        public ChessPosition ToChessPosition()
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
            }

            return new ChessPosition(column, 8 - Line);
        }
    }
}