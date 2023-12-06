using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.Boards
{
    public class RandomBoard : BaseBoard
    {
        public double Density { get; set; } = 0.75;

        public RandomBoard(int width, int height, int bombs) : base(width, height, bombs)
        {
        }

        public override void Initialize(int bombs)
        {
            if (Width * Height < bombs)
                throw new Exception("Amount of bombs cannot be larger than the board size.");
            if (Density >= 1 || Density <= 0)
                throw new Exception("Random board density must be within 0 to 1!");

            Cells = new Cell?[Width, Height];
            var target = (int)(Width * (double)Height * Density);
            var rnd = new Random();
            for (int i = 0; i < target; i++)
            {
                var x = rnd.Next(0, Width);
                var y = rnd.Next(0, Height);
                var check = Cells[x, y];
                if (check == null)
                    Cells[x, y] = new Cell(x, y);
                else
                    i--;
            }

            PlaceBombs(bombs);
            SetDistanceCount();
        }
    }
}
