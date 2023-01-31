using GameHub.View;
using GameHub.Model;
using GameHub.Model.Enum;
using GameHub.Repository;
using GameHub.Chess.Model;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Pieces;
using GameHub.Chess.Model.Board;
using GameHub.Chess.Repository;
using GameHub.Chess.View;
using System.Text.RegularExpressions;

namespace GameHub.Chess.Service
{
    public class ChessGame
    {
        public ChessBoard Board { get; private set; }
        public int Round { get; private set; }
        public Color CurrentColor { get; private set; }
        public bool Finished { get; private set; }
        public Piece? PossibleEnPassant { get; private set; }
        private readonly HashSet<Piece> _pieces;
        private readonly HashSet<Piece> _captured;
        public bool Check { get; private set; }
        public bool Draw { get; private set; } = false;
        public bool ToResign { get; private set; } = false;
        private bool _castleKingside = false;
        private bool _castleQueenside = false;
        private bool _tutorial = false;
        private readonly ChessView _chessView = new();
        public readonly Player player1;
        public readonly Player player2;
        private readonly Pgn? _pgn;
        private bool _whiteRequestDraw = false;
        private bool _blackRequestDraw = false;

        public ChessGame()
        {
            Board = new ChessBoard(8, this);
            Round = 1;
            CurrentColor = Color.Branco;
            PossibleEnPassant = null;
            Check = false;
            Finished = false;
            _tutorial = true;
            _pieces = Board.StarterPieces;
            _captured = new HashSet<Piece>();

            player1 = new Player("Jogador 1", "");
            player2 = new Player("Jogador 2", "");

            Tutorial();
        }

        public ChessGame(Player matchPlayer1, Player matchPlayer2)
        {
            Board = new ChessBoard(8, this);
            Round = 1;
            CurrentColor = Color.Branco;
            PossibleEnPassant = null;
            Check = false;
            Finished = false;
            _pieces = Board.StarterPieces;
            _captured = new HashSet<Piece>();

            player1 = matchPlayer1;
            player2 = matchPlayer2;

            _pgn = new Pgn(matchPlayer1.Username, matchPlayer2.Username);

            PlayChess();
        }


        private void Tutorial()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Clear();
                Console.WriteLine(Tutorials.ChessTutorial[i]);

                Utilities.Utilities.PressAnyButton();

                if (i == 0)
                {
                    _chessView.ShowChessBoard(new ChessBoard(8, this), new bool[8, 8]);
                }
                else if (i == 1)
                {
                    _chessView.ShowMatch(this);
                    Console.WriteLine("Jogada: a2");

                    Utilities.Utilities.PressAnyButton();

                    Position selectedPosition = _chessView.ReadChessPosition("a2").ToPosition();
                    ValidateSelectedPosition(selectedPosition);
                    bool[,] possibleMoves = Board.Piece(selectedPosition).PossibleMoves();

                    Console.Clear();

                    _chessView.ShowChessBoard(Board, possibleMoves);
                    Console.WriteLine(
                        "\nFoi selecionado o Peão Branco na casa 'a2', agora podemos ver os\n" +
                        "possíveis movimentos para a peça nas posições 'a3' e 'a4'.\n" +
                        "Vamos mover o peão para 'a4'.");

                    Utilities.Utilities.PressAnyButton();

                    Position destinyPosition = _chessView.ReadChessPosition("a4").ToPosition();
                    ValidateDestinyPosition(selectedPosition, destinyPosition);

                    MakeMove(selectedPosition, destinyPosition);
                    _chessView.ShowMatch(this);
                }
                else if (i == 2)
                {
                    _chessView.ShowMatch(this);
                    Console.WriteLine("Jogada: render");
                    Utilities.Utilities.PressAnyButton();
                    Finished = true;
                    ToResign = true;
                    ChangePlayer();
                    _chessView.ShowMatch(this);
                    Console.WriteLine("\n\nJogador 2(Pretas) se rendeu, logo quem ganhou foi Jogador 1(Brancas)!");
                }

                Utilities.Utilities.PressAnyButton();
            }
        }

        private void PlayChess()
        {
            Regex moveRegex = new Regex(@"^[a-h][1-8]$");

            while (!Finished)
            {
                try
                {
                    string move;

                    do
                    {
                        Console.Clear();
                        _chessView.ShowMatch(this);

                        Console.Write("\nJogada: ");
                        move = Console.ReadLine().ToLower();

                        if (move == "render")
                        {
                            Finished = true;
                            ToResign = true;
                            ChangePlayer();
                            break;
                        }
                        else if (move == "empate")
                        {
                            if (CanRequestDraw())
                            {
                                string playerRequestedDraw = CurrentColor == Color.Branco ? player1.Username : player2.Username;
                                ChangePlayer();

                                Console.Clear();
                                _chessView.ShowMatch(this);
                                Console.WriteLine($"{playerRequestedDraw}({CurrentColor}) sugeriu um empate. Para aceitar digite 'empate'");

                                Console.Write("\nJogada: ");
                                move = Console.ReadLine().ToLower();

                                if (move == "empate")
                                {
                                    Finished = true;
                                    Draw = true;
                                    break;
                                }
                                else
                                    ChangePlayer();
                            }
                            else
                            {
                                Console.WriteLine("Cada jogador pode pedir empate apenas uma vez por partida!");
                                Utilities.Utilities.PressAnyButton();
                            }
                        }
                    } while (!moveRegex.IsMatch(move));

                    if (Finished) break;

                    Position selectedPosition = _chessView.ReadChessPosition(move).ToPosition();
                    ValidateSelectedPosition(selectedPosition);
                    bool[,] possibleMoves = Board.Piece(selectedPosition).PossibleMoves();

                    do
                    {
                        Console.Clear();
                        _chessView.ShowChessBoard(Board, possibleMoves);
                        Console.Write("\nDestino: ");
                        move = Console.ReadLine().ToLower();
                    } while (!moveRegex.IsMatch(move));


                    Position destinyPosition = _chessView.ReadChessPosition(move).ToPosition();
                    ValidateDestinyPosition(selectedPosition, destinyPosition);

                    MakeMove(selectedPosition, destinyPosition);
                }
                catch (ChessBoardException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.ReadLine();
                }
            }

            string matchWinner = player1.Username;
            string matchPlayer1 = player1.Username;
            string matchPlayer2 = player2.Username;
            Result result = Result.Decisivo;

            string pgnResult = "1-0";

            if (CurrentColor is Color.Preto)
            {
                matchWinner = player2.Username;
                pgnResult = "0-1";
            }
            
            if (Draw)
            {
                result = Result.Empate;
                matchWinner = null;
                pgnResult = "1/2-1/2";
            }

            Board.ChangeBoardToRegister();
            GameHub.Repository.Match match = new GameHub.Repository.Match(Game.Xadrez, matchPlayer1, matchPlayer2, matchWinner, result, Board, null);

            Matches.MatchHistory.Add(match);
            Matches.SaveMatches();
            player1.MatchHistory.Add(match);
            player2.MatchHistory.Add(match);

            _pgn.Result = pgnResult;
            Pgns.CreatePgnFile(_pgn);

            Console.Clear();
            _chessView.ShowMatch(this);
            Utilities.Utilities.PressAnyButton();

            _chessView.ShowPgn(_pgn);
            Utilities.Utilities.PressAnyButton();
        }


        private Piece? ExecuteMove(Position selectedPosition, Position destinyPosition)
        {
            Piece piece = Board.RemovePiece(selectedPosition);
            piece.IncreaseAmountOfMoves();

            Piece takenPiece = Board.RemovePiece(destinyPosition);
            Board.PutPiece(piece, destinyPosition);

            if (takenPiece != null)
            {
                _captured.Add(takenPiece);
            }

            if (piece is King && destinyPosition.Column == selectedPosition.Column + 2)
            {
                Position rookPosition = new Position(selectedPosition.Line, selectedPosition.Column + 3);
                Position rookDestiny = new Position(selectedPosition.Line, selectedPosition.Column + 1);

                Piece rook = Board.RemovePiece(rookPosition);
                rook.IncreaseAmountOfMoves();
                Board.PutPiece(rook, rookDestiny);

                _castleKingside = true;
            }

            if (piece is King && destinyPosition.Column == selectedPosition.Column - 2)
            {
                Position rookPosition = new Position(selectedPosition.Line, selectedPosition.Column - 4);
                Position rookDestiny = new Position(selectedPosition.Line, selectedPosition.Column - 1);

                Piece rook = Board.RemovePiece(rookPosition);
                rook.IncreaseAmountOfMoves();
                Board.PutPiece(rook, rookDestiny);

                _castleQueenside = true;
            }

            if (piece is Pawn)
            {
                if (selectedPosition.Column != destinyPosition.Column && takenPiece == null)
                {
                    Position pawnPosition;

                    if (piece.Color == Color.Branco)
                    {
                        pawnPosition = new Position(destinyPosition.Line + 1, destinyPosition.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(destinyPosition.Line - 1, destinyPosition.Column);
                    }

                    takenPiece = Board.RemovePiece(pawnPosition);
                    _captured.Add(takenPiece);
                }
            }

            return takenPiece;
        }


        private void ValidateSelectedPosition(Position position)
        {
            if (Board.Piece(position) == null)
                throw new ChessBoardException("Não existe peça na posição selecionada!");

            if (CurrentColor != Board.Piece(position).Color)
                throw new ChessBoardException("A peça escolhida não é sua!");

            if (!Board.Piece(position).HasPossibleMoves())
                throw new ChessBoardException("Não há movimentos possíveis para a peça!");
        }

        private void UndoMove(Position selectedPosition, Position destinyPosition, Piece takenPiece)
        {
            Piece piece = Board.RemovePiece(destinyPosition);
            piece.DecreaseAmountOfMoves();

            if (takenPiece != null)
            {
                Board.PutPiece(takenPiece, destinyPosition);
                _captured.Remove(takenPiece);
            }

            Board.PutPiece(piece, selectedPosition);

            if (piece is King && destinyPosition.Column == selectedPosition.Column + 2)
            {
                Position rookPosition = new Position(selectedPosition.Line, selectedPosition.Column + 3);
                Position rookDestiny = new Position(selectedPosition.Line, selectedPosition.Column + 1);

                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecreaseAmountOfMoves();

                Board.PutPiece(rook, rookPosition);
            }

            if (piece is King && destinyPosition.Column == selectedPosition.Column - 2)
            {
                Position rookPosition = new Position(selectedPosition.Line, selectedPosition.Column - 4);
                Position rookDestiny = new Position(selectedPosition.Line, selectedPosition.Column - 1);

                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecreaseAmountOfMoves();

                Board.PutPiece(rook, rookPosition);
            }

            if (piece is Pawn)
            {
                if (selectedPosition.Column != destinyPosition.Column && takenPiece == PossibleEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destinyPosition);
                    Position pawnPosition;

                    if (pawn.Color == Color.Branco)
                    {
                        pawnPosition = new Position(3, destinyPosition.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destinyPosition.Column);
                    }

                    Board.PutPiece(pawn, pawnPosition);
                }
            }
        }

        private void MakeMove(Position selectedPosition, Position destinyPosition)
        {
            string pgnRegisterMove = string.Empty;

            if (CurrentColor == Color.Branco)
                pgnRegisterMove = $"{Round}.";

            Piece selectedPiece = Board.Piece(selectedPosition);
            if (selectedPiece is not Pawn)
                pgnRegisterMove += selectedPiece.ToString();

            foreach (Piece matchPiece in PiecesInGame(selectedPiece.Color))
            {
                if (!matchPiece.SamePieceType(selectedPiece))
                    continue;

                if (matchPiece.PossibleMoves()[destinyPosition.Line, destinyPosition.Column] == true)
                {
                    if (matchPiece.Position.Equals(selectedPiece.Position))
                        continue;
                    else
                    {
                        ChessPosition position = selectedPiece.Position.ToChessPosition();
                        string differentPosition = position.Column.ToString();

                        if (matchPiece.Position.Column == selectedPiece.Position.Column)
                            differentPosition = (8 - selectedPiece.Position.Line).ToString();

                        pgnRegisterMove += differentPosition;
                    }
                }
            }

            Piece takenPiece = ExecuteMove(selectedPosition, destinyPosition);
            if (takenPiece != null)
            {
                if (selectedPiece is not Pawn)
                    pgnRegisterMove += "x";
                else
                {
                    ChessPosition position = selectedPosition.ToChessPosition();
                    string pawnColumn = position.Column.ToString();
                    pgnRegisterMove += $"{pawnColumn}x";
                }
            }

            if (IsInCheck(CurrentColor))
            {
                UndoMove(selectedPosition, destinyPosition, takenPiece);
                throw new ChessBoardException("Você não pode se colocar em xeque!");
            }

            Piece piece = Board.Piece(destinyPosition);
            pgnRegisterMove += $"{destinyPosition.ToChessPosition()}";

            if (piece is Pawn)
            {
                if (piece.Color == Color.Branco && destinyPosition.Line == 0 || piece.Color == Color.Preto && destinyPosition.Line == 7)
                {
                    string chosenOption;
                    string[] choicePiece = { "Q", "R", "B", "N" };
                    Piece newPiece = null;

                    do
                    {
                        Console.Clear();
                        _chessView.ShowBoard(Board);

                        Console.WriteLine($"Destino: {destinyPosition.ToChessPosition()}\n");
                        Console.WriteLine($"O peão foi promovido!");
                        Console.WriteLine("Escolha uma peça para o Peão se transformar: ");
                        Console.WriteLine("Q = Rainha | R = Torre | B = Bispo | N = Cavalo\n");
                        Console.Write("Peça escolhida: ");

                        chosenOption = Console.ReadLine().ToUpper();
                    } while (!choicePiece.Contains(chosenOption));

                    switch (chosenOption)
                    {
                        case "Q":
                            newPiece = new Queen(Board, piece.Color);
                            break;
                        case "R":
                            newPiece = new Rook(Board, piece.Color);
                            break;
                        case "B":
                            newPiece = new Bishop(Board, piece.Color);
                            break;
                        case "N":
                            newPiece = new Knight(Board, piece.Color);
                            break;
                    }

                    piece = Board.RemovePiece(destinyPosition);
                    _pieces.Remove(piece);
                    Board.PutPiece(newPiece, destinyPosition);
                    _pieces.Add(newPiece);

                    pgnRegisterMove += $"={newPiece.ToString()}";
                }
            }

            if (_castleKingside)
            {
                if (CurrentColor == Color.Branco)
                    pgnRegisterMove = $"{Round}.O-O";
                else
                    pgnRegisterMove = "O-O";

                _castleKingside = false;
            }
            else if (_castleQueenside)
            {
                if (CurrentColor == Color.Branco)
                    pgnRegisterMove = $"{Round}.O-O-O";
                else
                    pgnRegisterMove = "O-O-O";

                _castleQueenside = false;
            }

            if (IsInCheck(_oponnent(CurrentColor)))
            {
                Check = true;
                pgnRegisterMove += "+";
            }
            else
            {
                Check = false;
            }

            if (TestCheckmate(_oponnent(CurrentColor)))
            {
                Finished = true;
                pgnRegisterMove = pgnRegisterMove.Replace('+', '#');
            }
            else
            {
                if (CurrentColor == Color.Preto)
                    Round++;
                ChangePlayer();
            }

            if (piece is Pawn && (destinyPosition.Line == selectedPosition.Line - 2 || destinyPosition.Line == selectedPosition.Line + 2))
                PossibleEnPassant = piece;
            else
                PossibleEnPassant = null;

            if (!_tutorial)
            {
                if (!IsInCheck(piece.Color))
                    _pgn.Rounds.Add(pgnRegisterMove);
            }
        }

        private void ChangePlayer()
        {
            if (CurrentColor == Color.Branco)
                CurrentColor = Color.Preto;
            else
                CurrentColor = Color.Branco;
        }

        private void ValidateDestinyPosition(Position selectedPosition, Position destinyPosition)
        {
            if (!Board.Piece(selectedPosition).PossibleMoves(destinyPosition))
                throw new ChessBoardException("Posição de destino inválida.");
        }

        private Color _oponnent(Color color)
        {
            if (color == Color.Branco)
                return Color.Preto;
            else
                return Color.Branco;
        }

        private Piece _king(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
            {
                if (piece is King)
                    return piece;
            }

            return null;
        }

        private bool IsInCheck(Color color)
        {
            Piece king = _king(color);

            foreach (Piece piece in PiecesInGame(_oponnent(color)))
            {
                bool[,] canMoveTo = piece.PossibleMoves();
                if (canMoveTo[king.Position.Line, king.Position.Column])
                    return true;
            }

            return false;
        }

        private bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
                return false;

            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] canMoveTo = piece.PossibleMoves();
                for (int i = 0; i < Board.Line; i++)
                {
                    for (int j = 0; j < Board.Column; j++)
                    {
                        if (canMoveTo[i, j])
                        {
                            Position selectedPosition = piece.Position;
                            Position destinyPosition = new Position(i, j);

                            Piece takenPiece = ExecuteMove(selectedPosition, destinyPosition);
                            bool isInCheck = IsInCheck(color);
                            UndoMove(selectedPosition, destinyPosition, takenPiece);

                            if (!isInCheck)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in _captured)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        private HashSet<Piece> PiecesInGame(Color cor)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in _pieces)
            {
                if (piece.Color == cor)
                    aux.Add(piece);
            }

            aux.ExceptWith(CapturedPieces(cor));
            return aux;
        }

        private bool CanRequestDraw()
        {
            if (CurrentColor == Color.Branco)
            {
                if (!_whiteRequestDraw)
                {
                    _whiteRequestDraw = true;
                    return true;
                }
                return false;
            }

            if (!_blackRequestDraw)
            {
                _blackRequestDraw = true;
                return true;
            }
            return false;
        }
    }
}
