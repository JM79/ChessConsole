using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole
{
    public enum PieceType { Pawn, Knight, Bishop, Rook, Queen, King  }
    public enum PieceColour { Black, White }

    public struct Piece
    {
        public PieceType PieceType { get; set; }
        public PieceColour PieceColour { get; set; }
        public char Unicode { get; set; }

        public Piece GetCopy()
        {
            return new Piece()
            {
                PieceType = PieceType,
                PieceColour = PieceColour,
                Unicode = Unicode
            };
        }
    }
}
