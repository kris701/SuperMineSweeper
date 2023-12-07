using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper
{
    public interface IBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int CellsLeft { get; }
        public Cell?[,] Cells { get; set; }
        public void Initialize(int bombs);
    }
}
