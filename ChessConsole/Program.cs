using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            SetDefaultBoard(board);
            PrintBoard(board);

            var moveGenerator = new MoveGenerator();
            //var moves = moveGenerator.GetPossibleMovesForPiece(board, 5, 6);
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 3);

            //PrintMoves(moves);
            int moveCount = CountMoves(moves);
            Console.WriteLine($"Total moves: {moveCount}");

            Console.WriteLine("Press enter to close");
            var temp = Console.ReadKey();
        }

        public static void SetDefaultBoard(Board board)
        {
            var WhitePawn   = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, Unicode = '\u2659', CharacterCode = 'P' };
            var BlackPawn   = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, Unicode = '\u265F', CharacterCode = 'P' };
            
            // Set Pawns
            int y = 1;
            for (int x = 0; x < 8; x++)
            { board.Squares[x, y].Piece = WhitePawn; }
            y = 6;
            for (int x = 0; x < 8; x++)
            { board.Squares[x, y].Piece = BlackPawn; }

            board.Squares[0, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook,   Unicode = '\u2656', CharacterCode = 'R' }; 
            board.Squares[1, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658', CharacterCode = 'N' }; 
            board.Squares[2, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, Unicode = '\u2657', CharacterCode = 'B' };
            board.Squares[3, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Queen,  Unicode = '\u2655', CharacterCode = 'Q' };
            board.Squares[4, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.King,   Unicode = '\u2654', CharacterCode = 'K' };
            board.Squares[5, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, Unicode = '\u2657', CharacterCode = 'B' };
            board.Squares[6, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658', CharacterCode = 'N' };
            board.Squares[7, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook,   Unicode = '\u2656', CharacterCode = 'R' };

            board.Squares[0, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Rook,   Unicode = '\u265C', CharacterCode = 'R' };
            board.Squares[1, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E', CharacterCode = 'N' };
            board.Squares[2, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, Unicode = '\u265D', CharacterCode = 'B' };
            board.Squares[3, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Queen,  Unicode = '\u265B', CharacterCode = 'Q' };
            board.Squares[4, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.King,   Unicode = '\u2654', CharacterCode = 'K' };
            board.Squares[5, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, Unicode = '\u265D', CharacterCode = 'B' };
            board.Squares[6, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E', CharacterCode = 'N' };
            board.Squares[7, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Rook,   Unicode = '\u265C', CharacterCode = 'R' };
        }

        public static void PrintBoard(Board board)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Console.BackgroundColor = (board.Squares[x, y].DarkSquare) ? ConsoleColor.DarkGreen : ConsoleColor.Green;
                    char pieceChar = (board.Squares[x, y].Piece.HasValue) ? ((Piece)board.Squares[x, y].Piece).CharacterCode : '_';
                    if (pieceChar != '_') /* Not an empty square */
                    { Console.Write($" {pieceChar} "); }
                    else
                    {
                        var foreColour = Console.ForegroundColor;
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.Write($" {pieceChar} ");
                        Console.ForegroundColor = foreColour;
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("#\n");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine("_");
        }

        private static int CountMoves(List<Move> moves)
        {
            //int count = moves.Count;
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

        private static void PrintMoves(List<Move> moves)
        {
            foreach (var move in moves)
            {
                PrintBoard(move.Board);
                PrintMoves(move.SubsequentMoves);
            }
        }
    }
}
