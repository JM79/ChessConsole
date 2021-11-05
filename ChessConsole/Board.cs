using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole
{
    public struct Square
    {
        public Piece? Piece { get; set; }
        public bool DarkSquare { get; set; }

        public Square GetCopy()
        {
            var copy = new Square() { DarkSquare = DarkSquare };
            if (Piece.HasValue)
            { copy.Piece = Piece.Value.GetCopy(); }
            return copy;
        }
    }

    public class Board
    {
        public const int Ranks = 8;
        public const int Files = 8;
        public Square[,] Squares { get; set; } = new Square[Files, Ranks];
        public PieceColour ColourToMoveNext { get; set; }
        public bool WhiteKingInCheck { get; set; }
        public bool BlackKingInCheck { get; set; }

        public Board()
        {
            bool darkSquare = false;
            ColourToMoveNext = PieceColour.White;

            for (int y = 0; y < Ranks; y++)
            {
                for (int x = 0; x < Files; x++)
                {
                    Squares[x, y] = new Square() { DarkSquare = darkSquare };
                    darkSquare = (darkSquare == false); /* Toggle */
                }
                darkSquare = (darkSquare == false); /* Toggle */
            }
        }


        public Board GetCopy()
        {
            var newCopy = new Board();
            newCopy.ColourToMoveNext = ColourToMoveNext;
            newCopy.BlackKingInCheck = BlackKingInCheck;
            newCopy.WhiteKingInCheck = WhiteKingInCheck;

            for (int y = 0; y < Ranks; y++)
            {
                for (int x = 0; x < Files; x++)
                { newCopy.Squares[x, y] = Squares[x, y].GetCopy(); }
            }

            return newCopy;
        }
    }
}
