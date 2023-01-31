using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Board;

namespace GameHub.Chess.Model.Pieces
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int AmountOfMoves { get; protected set; }
        public ChessBoard Board { get; protected set; }

        public Piece(Color color, ChessBoard board)
        {
            Position = null;
            Color = color;
            Board = board;
            AmountOfMoves = 0;
        }

        public bool HasPossibleMoves()
        {
            bool[,] matriz = PossibleMoves();

            for (int i = 0; i < Board.Line; i++)
            {
                for (int j = 0; j < Board.Column; j++)
                {
                    if (matriz[i, j])
                        return true;
                }
            }

            return false;
        }

        public void IncreaseAmountOfMoves() => AmountOfMoves++;

        public void DecreaseAmountOfMoves() => AmountOfMoves--;

        public bool PossibleMoves(Position position)
        {
            return PossibleMoves()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMoves();

        public bool SamePieceType(Piece piece)
        {
            if (piece.Color.Equals(Color) && piece.ToString().Equals(this.ToString())) return true;

            return false;
        }
    }
}
