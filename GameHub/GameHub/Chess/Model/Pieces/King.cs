using GameHub.Chess.Service;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Board;

namespace GameHub.Chess.Model.Pieces
{
    public class King : Piece
    {
        private ChessGame _mathc;

        public King(ChessBoard board, Color color, ChessGame match) : base(color, board)
        {
            _mathc = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool TestRookToCastle(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.AmountOfMoves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] canMoveTo = new bool[Board.Line, Board.Column];
            Position position = new Position(0, 0);

            position.SetValues(Position.Line - 1, Position.Column);

            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                canMoveTo[position.Line, position.Column] = true;

            if (AmountOfMoves == 0 && !_mathc.Check)
            {
                Position positionLeftRook = new Position(Position.Line, Position.Column - 4);

                if (TestRookToCastle(positionLeftRook))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                        canMoveTo[Position.Line, Position.Column - 2] = true;
                }

                Position positionRightRook = new Position(Position.Line, Position.Column + 3);

                if (TestRookToCastle(positionRightRook))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                        canMoveTo[Position.Line, Position.Column + 2] = true;
                }
            }

            return canMoveTo;
        }
    }
}
