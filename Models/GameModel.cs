using PushStart_Console.Repository;
using System.Collections.Generic;

namespace PushStart_Console.Models
{
    public class GameModel
    {
        public List<PieceModel> Pieces { get; set; }
        public int Columns { get; set; }
        public int Lines { get; set; }
    }
}
