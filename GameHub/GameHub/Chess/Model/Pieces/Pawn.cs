using GameHub.Chess.Service;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Board;

namespace GameHub.Chess.Model.Pieces
{
    public class Pawn : Piece
    {
        private ChessGame _match;

        public Pawn(ChessBoard board, Color color, ChessGame match) : base(color, board)
        {
            _match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool CanMove(Position position)
        {
            return Board.Piece(position) == null;
        }

        private bool HasEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] canMoveTo = new bool[Board.Line, Board.Column];

            Position position = new Position(0, 0);

            if (Color == Color.Branco)
            {
                position.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    canMoveTo[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line - 2, Position.Column);
                Position position2 = new Position(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(position2) && CanMove(position2) && Board.ValidPosition(position) &&
                    CanMove(position) && AmountOfMoves == 0)
                {
                    canMoveTo[position.Line, position.Column] = true;
                }

                position.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && HasEnemy(position))
                {
                    canMoveTo[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && HasEnemy(position))
                {
                    canMoveTo[position.Line, position.Column] = true;
                }

                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && HasEnemy(left) && Board.Piece(left) == _match.PossibleEnPassant)
                    {
                        canMoveTo[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && HasEnemy(right) && Board.Piece(right) == _match.PossibleEnPassant)
                    {
                        canMoveTo[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                position.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && CanMove(position))
                {
                    canMoveTo[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line + 2, Position.Column);
                Position position2 = new Position(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position2) && CanMove(position2) && Board.ValidPosition(position) &&
                    CanMove(position) && AmountOfMoves == 0)
                {
                    canMoveTo[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && HasEnemy(position))
                {
                    canMoveTo[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && HasEnemy(position))
                {
                    canMoveTo[position.Line, position.Column] = true;
                }

                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Column, Position.Column - 1);
                    if (Board.ValidPosition(left) && HasEnemy(left) && Board.Piece(left) == _match.PossibleEnPassant)
                    {
                        canMoveTo[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && HasEnemy(right) && Board.Piece(right) == _match.PossibleEnPassant)
                    {
                        canMoveTo[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return canMoveTo;
        }
    }
}
