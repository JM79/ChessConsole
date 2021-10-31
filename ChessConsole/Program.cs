using System;
using System.Text;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            //PrintBoard(board);
            SetDefaultBoard(board);
            PrintBoard(board);

            var moveGenerator = new MoveGenerator();
            //var moves = moveGenerator.GetPossibleMovesForPiece(board, 5, 6);
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 2);

            //foreach (var move in moves)
            //{ PrintBoard(move.Board); }
            Console.WriteLine($"Total moves: {moves.Count}");

            Console.WriteLine("Press enter to close");
            var temp = Console.ReadKey();
            //for (int i = 0; i < 8; i++)
            //{
            //    var WhitePawn = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn };
            //    board.Squares[1, 0].Piece = WhitePawn;
            //}
        }

        public static void SetDefaultBoard(Board board)
        {
            var WhitePawn   = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, Unicode = '\u2659' };
            var BlackPawn   = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, Unicode = '\u265F' };
            
            // Set Pawns
            int y = 1;
            for (int x = 0; x < 8; x++)
            { board.Squares[x, y].Piece = WhitePawn; }
            y = 6;
            for (int x = 0; x < 8; x++)
            { board.Squares[x, y].Piece = BlackPawn; }

            board.Squares[0, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook,   Unicode = '\u2656' }; 
            board.Squares[1, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658' }; 
            board.Squares[2, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, Unicode = '\u2657' };
            board.Squares[3, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Queen,  Unicode = '\u2655' };
            board.Squares[4, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.King,   Unicode = '\u2654' };
            board.Squares[5, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, Unicode = '\u2657' };
            board.Squares[6, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658' };
            board.Squares[7, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook,   Unicode = '\u2656' };

            board.Squares[0, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Rook,   Unicode = '\u265C' };
            board.Squares[1, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E' };
            board.Squares[2, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, Unicode = '\u265D' };
            board.Squares[3, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Queen,  Unicode = '\u265B' };
            board.Squares[4, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.King,   Unicode = '\u2654' };
            board.Squares[5, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, Unicode = '\u265D' };
            board.Squares[6, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E' };
            board.Squares[7, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Rook,   Unicode = '\u265C' };
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
                    char pieceUnicode = (board.Squares[x, y].Piece.HasValue) ? ((Piece)board.Squares[x, y].Piece).Unicode : '_';
                    if (pieceUnicode == '\u265F') /* Special case fix for Black Pawn Unicode */
                    { Console.Write($" {pieceUnicode}"); }
                    else if (pieceUnicode != '_') /* Not an mpty square */
                    { Console.Write($" {pieceUnicode} "); }
                    else
                    {
                        var foreColour = Console.ForegroundColor;
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.Write($" {pieceUnicode} ");
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
    }
}
