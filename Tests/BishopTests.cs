using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BishopTests
    {
        [TestMethod]
        public void TestSingleCentralBishopSingleMoveStep()
        {
            // Arrange
            var board = new Board();
            board.Squares[4,4].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, CharacterCode = 'B' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(13, moves.Count);
        }

        [TestMethod]
        public void TestTwoCentralBishopsSingleMoveStepEach()
        {
            // Arrange
            var board = new Board();
            board.Squares[3, 3].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Bishop, CharacterCode = 'B' };
            board.Squares[3, 4].Piece = new Piece() { PieceColour = PieceColour.Black, PieceType = PieceType.Bishop, CharacterCode = 'B' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 2);

            // Test
            Assert.AreEqual(13, moves.Count, "First step moves incorrect");
            int moveNumber = 1;
            foreach (var move in moves)
            {
                Assert.AreEqual(13, move.SubsequentMoves.Count, $"Second step subsequent moves incorrect. Move number: {moveNumber}");
                moveNumber++;
            }
        }
    }
}
