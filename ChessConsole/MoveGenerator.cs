using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole
{
    public class MoveGenerator
    {
        public Board StartingBoard { get; set; }

        public List<Move> GetAllPossibleMovesToDepth(Board board, int ply)
        {
            var possMoves = new List<Move>();
            for(int x = 0; x < Board.Files; x++)
            {
                for (int y = 0; y < Board.Ranks; y++)
                {
                    if (!board.Squares[x, y].Piece.HasValue
                      || board.Squares[x,y].Piece.Value.PieceColour != board.ColourToMoveNext)
                        continue; /* Skip empty squares or wrong colour pieces */

                    for (int depth = 1; depth <= ply; depth++)
                    {
                        var newMoves = GetPossibleMovesForPiece(board, x, y);
                        foreach (var newMove in newMoves)
                        {
                            possMoves.Add(newMove);
                            possMoves.AddRange(GetAllPossibleMovesToDepth(newMove.Board, ply-depth));
                        }
                    }
                }
            }
            return possMoves;
        }

        public List<Move> GetPossibleMovesForPiece(Board board, int x, int y)
        {
            var moves = new List<Move>();
            var piece = board.Squares[x, y].Piece;
            
            if (!piece.HasValue)
            { return moves; }

            switch ((PieceType)piece.Value.PieceType)
            {
                case PieceType.Pawn:
                    moves.AddRange(GetPawnMoves(board, x, y));
                    break;
                default:
                    break;
            }

            return moves;
        }

        private List<Move> GetPawnMoves(Board board, int x, int y)
        {
            var moves = new List<Move>();
            int newPosX, newPosY;
            var currentPiece = board.Squares[x, y].Piece.Value;

            int diagMoveLeft, diagMoveRight, forwardMove;
            bool firstPawnMove = ((currentPiece.PieceColour == PieceColour.Black && y == 6)
                               || (currentPiece.PieceColour != PieceColour.Black && y == 1));
            if (currentPiece.PieceColour == PieceColour.Black)
            {
                diagMoveLeft = 1;
                diagMoveRight = -1;
                forwardMove = -1;
            }
            else
            {
                diagMoveLeft = -1;
                diagMoveRight = 1;
                forwardMove = 1;
            }

            // One step forward
            newPosX = x;
            newPosY = y + forwardMove;
            // Check off the board
            if (!(newPosX >= Board.Files || newPosX < 0 || newPosY >= Board.Ranks || newPosY < 0))
            {
                // Check dest is empty
                var destSquare = board.Squares[newPosX, newPosY];
                if (!destSquare.Piece.HasValue)
                {
                    moves.Add(CreateNewMove(board, x, y, newPosX, newPosY));
                }
            }

            // Two step forward first move
            if (firstPawnMove)
            {
                newPosX = x;
                newPosY = y + (forwardMove*2);
                    
                // Check dest is empty
                var destSquare = board.Squares[newPosX, newPosY];
                if (!destSquare.Piece.HasValue)
                {
                    moves.Add(CreateNewMove(board, x, y, newPosX, newPosY));
                }
            }

            // Diagonal take left
            newPosX = x + diagMoveLeft;
            newPosY = y + forwardMove;
            if (!(newPosX >= Board.Files || newPosX < 0 || newPosY >= Board.Ranks || newPosY < 0))
            {
                // Check dest is an opposite colour piece
                var destSquare = board.Squares[newPosX, newPosY];
                if (destSquare.Piece.HasValue
                    && destSquare.Piece.Value.PieceColour != currentPiece.PieceColour)
                {
                    moves.Add(CreateNewMove(board, x, y, newPosX, newPosY));
                }
            }

            // Diagonal take right
            newPosX = x + diagMoveRight;
            newPosY = y + forwardMove;
            if (!(newPosX >= Board.Files || newPosX < 0 || newPosY >= Board.Ranks || newPosY < 0))
            {
                // Check dest is an opposite colour piece
                var destSquare = board.Squares[newPosX, newPosY];
                if (destSquare.Piece.HasValue
                    && destSquare.Piece.Value.PieceColour != currentPiece.PieceColour)
                {
                    moves.Add(CreateNewMove(board, x, y, newPosX, newPosY));
                }
            }

            return moves;
        }

        private Move CreateNewMove(Board board, int x, int y, int newPosX, int newPosY)
        {
            var newBoard = board.GetCopy();
            newBoard.Squares[newPosX, newPosY].Piece = board.Squares[x, y].Piece;
            newBoard.Squares[x, y].Piece = null;
            newBoard.ColourToMoveNext = board.ColourToMoveNext == PieceColour.White ? PieceColour.Black : PieceColour.White;
            return new Move()
            {
                Board = newBoard,
                PreviousBoard = board,
            };
        }
    }
}
