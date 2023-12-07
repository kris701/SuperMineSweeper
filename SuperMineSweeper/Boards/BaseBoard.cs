using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.Boards
{
    public abstract class BaseBoard : IBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int CellsLeft { get {
                int count = 0;
                foreach (var cell in Cells)
                    if (cell != null && !cell.IsVisible)
                        count++;
                return count;
            } 
        }
        public Cell?[,] Cells { get; set; }

        public BaseBoard(int width, int height, int bombs)
        {
            Width = width;
            Height = height;
            Cells = new Cell?[Width, Height];
            Initialize(bombs);
        }

        public void Initialize(int bombs)
        {
            SetupBoard(bombs);
            PlaceBombs(bombs);
            SetDistanceCount();
        }

        internal abstract void SetupBoard(int bombs);

        internal void PlaceBombs(int bombs)
        {
            var rnd = new Random();
            for (int i = 0; i < bombs; i++)
            {
                var x = rnd.Next(0, Width);
                var y = rnd.Next(0, Height);
                var check = Cells[x, y];
                if (check != null && !check.HasBomb)
                {
                    check.HasBomb = true;
                    check.Item = "X";

                }
                else
                {
                    i--;
                    continue;
                }
            }
        }

        internal void SetDistanceCount()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var cell = Cells[x, y];
                    if (cell != null && !cell.HasBomb)
                    {
                        var bombCount = 0;
                        if (IsBomb(x - 1, y)) bombCount++;
                        if (IsBomb(x - 1, y - 1)) bombCount++;
                        if (IsBomb(x - 1, y + 1)) bombCount++;
                        if (IsBomb(x + 1, y)) bombCount++;
                        if (IsBomb(x + 1, y - 1)) bombCount++;
                        if (IsBomb(x + 1, y + 1)) bombCount++;
                        if (IsBomb(x, y - 1)) bombCount++;
                        if (IsBomb(x, y + 1)) bombCount++;
                        if (bombCount == 0)
                            cell.Item = " ";
                        else
                            cell.Item = $"{bombCount}";
                    }
                }
            }
        }

        internal bool IsBomb(int x, int y)
        {
            if (x < 0)
                return false;
            if (x >= Width)
                return false;
            if (y < 0)
                return false;
            if (y >= Height)
                return false;
            var cell = Cells[x, y];
            if (cell != null)
                if (cell.HasBomb)
                    return true;
            return false;
        }

        public bool IsValidCell(int x, int y)
        {
            if (x < 0)
                return false;
            if (x >= Width)
                return false;
            if (y < 0)
                return false;
            if (y >= Height)
                return false;

            if (Cells[x, y] == null)
                return false;
            return true;
        }
    }
}
