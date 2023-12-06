using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsVisible { get; set; } = false;
        public bool HasBomb { get; set; } = false;
        public bool IsFlagged { get; set; } = false;
        public string Item { get; set; } = "";

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string? ToString()
        {
            return $"Count: {Item}, Bomb: {HasBomb}, IsFlagged: {IsFlagged}";
        }
    }
}
