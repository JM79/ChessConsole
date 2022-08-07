using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TotalMovesTests
    {
        [TestMethod]
        public void Test1Ply()
        {
            // Arrange
            var board = GetDefaultBoard();
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);
            int moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(20, moves.Count);
        }

        [TestMethod]
        public void Test2Ply()
        {
            // Arrange
            var board = GetDefaultBoard();
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 2);
            int moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(400, moveCount);
        }

        [TestMethod]
        public void Test3Ply()
        {
            // Arrange
            var board = GetDefaultBoard();
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 3);
            int moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(8902, moveCount);
        }

        [TestMethod]
        public void Test4Ply()
        {
            // Arrange
            var board = GetDefaultBoard();
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 4);
            int moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(197281, moveCount);
        }
        
        private static Board GetDefaultBoard()
        {
            var board = new Board();
            board.ColourToMoveNext = PieceColour.White;
            var WhitePawn = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, Unicode = '\u2659', CharacterCode = 'P' };
            var BlackPawn = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, Unicode = '\u265F', CharacterCode = 'P' };

            // Set Pawns
            int y = 1;
            for (int x = 0; x < 8; x++)
            { board.Squares[x, y].Piece = WhitePawn; }
            y = 6;
            for (int x = 0; x < 8; x++)
            { board.Squares[x, y].Piece = BlackPawn; }

            board.Squares[0, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook, Unicode = '\u2656', CharacterCode = 'R' };
            board.Squares[1, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658', CharacterCode = 'N' };
            board.Squares[2, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, Unicode = '\u2657', CharacterCode = 'B' };
            board.Squares[3, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Queen, Unicode = '\u2655', CharacterCode = 'Q' };
            board.Squares[4, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.King, Unicode = '\u2654', CharacterCode = 'K' };
            board.Squares[5, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, Unicode = '\u2657', CharacterCode = 'B' };
            board.Squares[6, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658', CharacterCode = 'N' };
            board.Squares[7, 0].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook, Unicode = '\u2656', CharacterCode = 'R' };

            board.Squares[0, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Rook, Unicode = '\u265C', CharacterCode = 'R' };
            board.Squares[1, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E', CharacterCode = 'N' };
            board.Squares[2, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, Unicode = '\u265D', CharacterCode = 'B' };
            board.Squares[3, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Queen, Unicode = '\u265B', CharacterCode = 'Q' };
            board.Squares[4, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.King, Unicode = '\u2654', CharacterCode = 'K' };
            board.Squares[5, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, Unicode = '\u265D', CharacterCode = 'B' };
            board.Squares[6, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E', CharacterCode = 'N' };
            board.Squares[7, 7].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Rook, Unicode = '\u265C', CharacterCode = 'R' };

            return board;
        }
    }
}
