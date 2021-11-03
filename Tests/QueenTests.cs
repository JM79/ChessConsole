using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class QueenTests
    {
        [TestMethod]
        public void TestSingleCentralQueen()
        {
            // Arrange
            var board = new Board();
            board.Squares[3,3].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.Queen, CharacterCode = 'Q' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(27, moves.Count);
        }
    }
}
