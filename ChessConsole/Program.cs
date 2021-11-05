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
            //var board = GetTwoAdjPawnsBoard();
            PrintBoard(board);

            var moveGenerator = new MoveGenerator();
            //var moves = moveGenerator.GetPossibleMovesForPiece(board, 5, 6);
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 4);

            //PrintCountTree(moves);
            PrintEndMoves(moves);
            int moveCount = MoveGenerator.CountMoves(moves);
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

        public static Board GetBasic2BishopBoard()
        {
            var board = new Board();
            board.Squares[3, 3].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, CharacterCode = 'B' };
            board.Squares[3, 4].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, CharacterCode = 'B' };
            return board;
        }

        public static Board GetTwoAdjPawnsBoard()
        {
            var board = new Board();
            board.Squares[3, 1].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            board.Squares[4, 6].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            return board;
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
            Console.WriteLine("_\n");
        }

        private static void PrintMoves(List<Move> moves)
        {
            foreach (var move in moves)
            {
                PrintBoard(move.Board);
                PrintMoves(move.SubsequentMoves);
            }
        }

        private static void PrintEndMoves(List<Move> moves)
        {
            foreach (var move in moves)
            {
                if (move.SubsequentMoves.Count == 0)
                { PrintBoard(move.Board); } /* Count only the final moves */
                else
                { PrintEndMoves(move.SubsequentMoves); }
            }
        }

        private static void PrintCountTree(List<Move> moves, string prefix = "")
        {
            foreach (var move in moves)
            {
                if (move.SubsequentMoves.Count > 0)
                {
                    Console.WriteLine(prefix + " " + move.SubsequentMoves.Count);
                    PrintCountTree(move.SubsequentMoves, prefix + "+");
                }                
            }
        }
    }
}
