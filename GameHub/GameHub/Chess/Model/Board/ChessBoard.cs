using GameHub.Model.Interface;
using GameHub.Chess.Model.Pieces;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Service;
using Newtonsoft.Json;

namespace GameHub.Chess.Model.Board
{
    public class ChessBoard : GameHub.Model.Board, IRefreshBoard
    {
        public int Line { get; set; }
        public int Column { get; set; }
        private readonly Piece[,] _pieces;
        [JsonIgnore]
        public HashSet<Piece> StarterPieces { get; private set; }
        private readonly ChessGame _match;

        public ChessBoard(int size, ChessGame match)
        {
            Size = size;
            Line = size;
            Column = size;

            _pieces = new Piece[Line, Column];
            BoardArray = new string[Size, Size];
            _match = match;
            StarterPieces = new HashSet<Piece>();

            PlacePieces();
        }

        public Piece Piece(int linha, int coluna)
        {
            return _pieces[linha, coluna];
        }

        public Piece Piece(Position position)
        {
            return _pieces[position.Line, position.Column];
        }

        public bool HaveAPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (HaveAPiece(position))
                throw new ChessBoardException("Há uma peça ocupando essa posição!");

            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null) return null;

            Piece aux = Piece(position);
            aux.Position = null;
            _pieces[position.Line, position.Column] = null;

            return aux;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Line) return false;
            if (position.Column < 0 || position.Column >= Column) return false;

            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
                throw new ChessBoardException("Posição inválida!");
        }

        private void PutNewPiece(char column, int line, Piece piece)
        {
            PutPiece(piece, new ChessPosition(column, line).ToPosition());
            StarterPieces.Add(piece);
        }

        private void PlacePieces()
        {
            PutNewPiece('a', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('b', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('c', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('d', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('e', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('f', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('g', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('h', 2, new Pawn(this, Color.Branco, _match));
            PutNewPiece('a', 1, new Rook(this, Color.Branco));
            PutNewPiece('b', 1, new Knight(this, Color.Branco));
            PutNewPiece('c', 1, new Bishop(this, Color.Branco));
            PutNewPiece('d', 1, new Queen(this, Color.Branco));
            PutNewPiece('e', 1, new King(this, Color.Branco, _match));
            PutNewPiece('f', 1, new Bishop(this, Color.Branco));
            PutNewPiece('g', 1, new Knight(this, Color.Branco));
            PutNewPiece('h', 1, new Rook(this, Color.Branco));

            PutNewPiece('a', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('b', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('c', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('d', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('e', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('f', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('g', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('h', 7, new Pawn(this, Color.Preto, _match));
            PutNewPiece('a', 8, new Rook(this, Color.Preto));
            PutNewPiece('b', 8, new Knight(this, Color.Preto));
            PutNewPiece('c', 8, new Bishop(this, Color.Preto));
            PutNewPiece('d', 8, new Queen(this, Color.Preto));
            PutNewPiece('e', 8, new King(this, Color.Preto, _match));
            PutNewPiece('f', 8, new Bishop(this, Color.Preto));
            PutNewPiece('g', 8, new Knight(this, Color.Preto));
            PutNewPiece('h', 8, new Rook(this, Color.Preto));
        }

        public void ChangeBoardToRegister()
        {
            for (int i = 0; i < _pieces.GetLength(1); i++)
            {
                for (int j = 0; j < _pieces.GetLength(0); j++)
                {
                    if (_pieces[i, j] != null)
                    {
                        if (_pieces[i, j].Color == Color.Branco)
                            BoardArray[i, j] = $"x{_pieces[i, j]} ";
                        else
                            BoardArray[i, j] = $" {_pieces[i, j]} ";
                    }
                    else
                        BoardArray[i, j] = "   ";
                }
                Console.WriteLine();
            }
        }
    }
}
