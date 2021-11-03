using ChessConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class KingTests
    {
        [TestMethod]
        public void TestSingleCentralKing()
        {
            // Arrange
            var board = new Board();
            board.Squares[3,3].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.King, CharacterCode = 'K' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(8, moves.Count);
        }

        [TestMethod]
        public void TestSingleKingOnBackRank()
        {
            // Arrange
            var board = new Board();
            board.Squares[0, 4].Piece = new Piece() { PieceColour = PieceColour.White, PieceType = PieceType.King, CharacterCode = 'K' };
            var moveGenerator = new MoveGenerator();

            // Act
            var moves = moveGenerator.GetAllPossibleMovesToDepth(board, 1);

            // Test
            Assert.AreEqual(5, moves.Count, "First step moves incorrect");
        }
    }
}
