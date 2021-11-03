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
                      || board.Squares[x, y].Piece.Value.PieceColour != board.ColourToMoveNext)
                        continue; /* Skip empty squares or wrong colour pieces */

                    var newMoves = GetPossibleMovesForPiece(board, x, y);
                    foreach (var newMove in newMoves)
                    {
                        if (ply > 1)
                        { newMove.SubsequentMoves = GetAllPossibleMovesToDepth(newMove.Board, ply - 1); }
                        possMoves.Add(newMove);
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

            switch (piece.Value.PieceType)
            {
                case PieceType.Pawn:
                    moves.AddRange(GetPawnMoves(board, x, y));
                    break;
                case PieceType.Knight:
                    moves.AddRange(GetKnightMoves(board, x, y));
                    break;
                case PieceType.Bishop:
                    moves.AddRange(GetBishopMoves(board, x, y));
                    break;
                case PieceType.Rook:
                    moves.AddRange(GetRookMoves(board, x, y));
                    break;
                case PieceType.Queen:
                    moves.AddRange(GetQueenMoves(board, x, y));
                    break;
                case PieceType.King:
                    moves.AddRange(GetKingMoves(board, x, y));
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
            if (IsPositionOnBoard(board, newPosX, newPosY))
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
            if (IsPositionOnBoard(board, newPosX, newPosY))
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
            if (IsPositionOnBoard(board, newPosX, newPosY))
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

        public List<Move> GetKnightMoves(Board board, int x, int y)
        {
            int[,] moveDirections = new int[8, 2]
            {
                { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 },
                { 2, 1 }, { -2, 1 }, { 2, -1 }, { -2, -1 }
            };
            return GetMoves(board, x, y, moveDirections, singleMove: true);
        }

        public List<Move> GetBishopMoves(Board board, int x, int y)
        {
            int[,] moveDirections = new int[4, 2] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
            return GetMoves(board, x, y, moveDirections);
        }

        public List<Move> GetRookMoves(Board board, int x, int y)
        {
            int[,] moveDirections = new int[4, 2] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
            return GetMoves(board, x, y, moveDirections);
        }

        public List<Move> GetKingMoves(Board board, int x, int y)
        {
            int[,] moveDirections = new int[8, 2]
            {
                { 1, 0 }, { 1, 1 }, { 1, -1 },
                { 0, -1 },{ 0, 1 }, 
                { -1, 0 },{ -1, -1 }, { -1, 1 }
            };
            return GetMoves(board, x, y, moveDirections, singleMove: true);
        }

        public List<Move> GetQueenMoves(Board board, int x, int y)
        {
            int[,] moveDirections = new int[8, 2]
            {
                { 1, 0 }, { 1, 1 }, { 1, -1 },
                { 0, -1 },{ 0, 1 },
                { -1, 0 },{ -1, -1 }, { -1, 1 }
            };
            return GetMoves(board, x, y, moveDirections);
        }

        public List<Move> GetMoves(Board board, int x, int y, int[,] moveDirections, bool singleMove = false)
        {
            var currentPiece = board.Squares[x, y].Piece.Value;
            var moves = new List<Move>();
            int moveMax = singleMove ? 1 : 8;
            for (int dir = 0; dir < moveDirections.Length/2; dir++)
            {
                for (int multiplier = 1; multiplier <= moveMax; multiplier++)
                {
                    int newPosX = x + (moveDirections[dir, 0] * multiplier);
                    int newPosY = y + (moveDirections[dir, 1] * multiplier);

                    // Check not off the board
                    if (IsPositionOnBoard(board, newPosX, newPosY))
                    {
                        // Check dest is empty or opposite colour piece
                        var destSquare = board.Squares[newPosX, newPosY];
                        if ((!destSquare.Piece.HasValue)
                         || destSquare.Piece?.PieceColour != currentPiece.PieceColour)
                        {
                            moves.Add(CreateNewMove(board, x, y, newPosX, newPosY));
                        }
                        if (destSquare.Piece.HasValue)
                        { multiplier = moveMax + 1; } /* Hit another piece, no further moves possible in this direction */
                    }
                    else
                    { multiplier = moveMax + 1; } /* Already off the board, no need to continue in this direction further */
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

        private bool IsPositionOnBoard(Board board, int x, int y)
        {
            return (x < Board.Files && x >= 0 && y < Board.Ranks && y >= 0);
        }

        public static int CountMoves(List<Move> moves)
        {
            int count = 0;
            foreach (var move in moves)
            {
                if (move.SubsequentMoves.Count == 0)
                { count++; } /* Count only the final moves */
                else
                { count += CountMoves(move.SubsequentMoves); }
            }
            return count;
        }
    }
}
