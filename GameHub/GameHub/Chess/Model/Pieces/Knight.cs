using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Board;

namespace GameHub.Chess.Model.Pieces
{
    public class Knight : Piece
    {
        public Knight(ChessBoard board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "N";
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

            position.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            return canMoveTo;
        }
    }
}
