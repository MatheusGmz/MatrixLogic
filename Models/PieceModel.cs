using System;

namespace PushStart_Console.Repository
{
    public class PieceModel
    {
        public int Id {get;set;}
        public string Name { get; set; }
        public int LineSize { get; set; }
        public int ColumnSize { get; set; }
        public int LinePosition { get; set; }
        public int ColumnPosition { get; set; }
        public int Quantity { get; set; }
        public ConsoleColor DialogColor { get; set; }
    }
}
