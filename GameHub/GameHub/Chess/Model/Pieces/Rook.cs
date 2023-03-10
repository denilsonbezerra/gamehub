using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Board;

namespace GameHub.Chess.Model.Pieces
{
    public class Rook : Piece
    {
        public Rook(ChessBoard board, Color color) : base(color, board)
        { }

        public override string ToString()
        {
            return "R";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] canMoveTo = new bool[Board.Line, Board.Column];
            Position position = new Position(0, 0);

            position.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                canMoveTo[position.Line, position.Column] = true;

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color) break;

                position.Line = position.Line - 1;
            }

            position.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                canMoveTo[position.Line, position.Column] = true;

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color) break;

                position.Line = position.Line + 1;
            }

            position.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                canMoveTo[position.Line, position.Column] = true;

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color) break;

                position.Column = position.Column + 1;
            }

            position.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                canMoveTo[position.Line, position.Column] = true;

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color) break;

                position.Column = position.Column - 1;
            }

            return canMoveTo;
        }
    }
}
