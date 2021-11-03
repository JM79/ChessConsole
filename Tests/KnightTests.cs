using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class KnightTests
    {
        [TestMethod]
        public void TestSingleCentralKnightSingleMoveStep()
        {
            // Arrange
            var board = new Board();
            board.Squares[4,4].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658', CharacterCode = 'N' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(8, moves.Count);
        }

        [TestMethod]
        public void TestTwoCentralKnightSingleMoveStepEach()
        {
            // Arrange
            var board = new Board();
            board.Squares[4, 4].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Knight, Unicode = '\u2658', CharacterCode = 'N' };
            board.Squares[4, 5].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Knight, Unicode = '\u265E', CharacterCode = 'N' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 2);

            // Test
            Assert.AreEqual(8, moves.Count, "First step moves incorrect");
            foreach (var move in moves)
            { Assert.AreEqual(8, move.SubsequentMoves.Count, "Second step subsequent moves incorrect"); }
        }
    }
}
