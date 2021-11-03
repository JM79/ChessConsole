using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RookTests
    {
        [TestMethod]
        public void TestSingleCentralRook()
        {
            // Arrange
            var board = new Board();
            board.Squares[3,3].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Rook, CharacterCode = 'R' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(14, moves.Count);
        }
    }
}
