using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PawnTests
    {
        [TestMethod]
        public void TestSinglePawn()
        {
            // Arrange
            var board = new Board();
            board.Squares[3,1].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(2, moves.Count);
        }

        [TestMethod]
        public void TestTwoPawns2Ply()
        {
            // Arrange
            var board = new Board();
            board.Squares[3, 1].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            board.Squares[3, 6].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 2);
            var moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(4, moveCount);
        }

        [TestMethod]
        public void TestTwoPawns3Ply()
        {
            // Arrange
            var board = new Board();
            board.Squares[3, 1].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            board.Squares[3, 6].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 3);
            var moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(4, moveCount);
        }

        [TestMethod]
        public void TestTwoPawns4Ply()
        {
            // Arrange
            var board = new Board();
            board.Squares[3, 1].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            board.Squares[3, 6].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 4);
            var moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(4, moveCount);
        }

        /***
         * Should be same as TestTwoPawns3Ply but with one extra move for the potential take
         * */
        [TestMethod]
        public void TestTwoAdjFilePawns3Ply()
        {
            // Arrange
            var board = new Board();
            board.Squares[3, 1].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            board.Squares[4, 6].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Pawn, CharacterCode = 'P' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 3);
            var moveCount = MoveGenerator.CountMoves(moves);

            // Test
            Assert.AreEqual(5, moveCount);
        }
    }
}
