using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.Boards
{
    public class SquareBoard : BaseBoard
    {
        public SquareBoard(int width, int height, int bombs) : base(width, height, bombs)
        {
        }

        internal override void SetupBoard(int bombs)
        {
            if (Width * Height < bombs)
                throw new Exception("Amount of bombs cannot be larger than the board size.");

            Cells = new Cell?[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Cells[x, y] = new Cell(x, y);
        }
    }
}
