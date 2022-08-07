using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole
{
    public class Move
    {
        public Board PreviousBoard { get; set; }
        public Board Board { get; set; }
        public List<Move> SubsequentMoves { get; set; } = new List<Move>();
        public string MoveText { get; set; }
    }
}