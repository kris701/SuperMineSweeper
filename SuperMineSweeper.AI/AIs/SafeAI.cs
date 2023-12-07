using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.AI.AIs
{
    public class SafeAI : BaseAI
    {
        public SafeAI(int moveInterval, IMineSweeper game) : base(moveInterval, game)
        {
        }

        internal override IMineSweeper.ActionResult PerformAction()
        {
            if (Game.Board.CellsLeft <= Game.Bombs)
            {
                foreach (var cell in Game.Board.Cells)
                {
                    if (cell != null && !cell.IsVisible && !cell.IsFlagged)
                    {
                        Game.FlagCell(cell.X, cell.Y);
                        return IMineSweeper.ActionResult.Success;
                    }
                }
            }

            var safest = GetSafestCell();
            if (safest != null)
            {
                var actionResult = Game.SelectCell(safest.X, safest.Y);
                if (actionResult == IMineSweeper.ActionResult.Died)
                {

                }
                return actionResult;
            }

            var rndMove = DoRandomMove();
            if (rndMove != IMineSweeper.ActionResult.Success)
                return rndMove;

            return IMineSweeper.ActionResult.Success;
        }

        private Cell? GetSafestCell()
        {
            int bestScore = int.MaxValue;
            Cell? bestCell = null;
            foreach(var cell in Game.Board.Cells)
            {
                if (cell != null && !cell.IsVisible && IsBordering(cell.X, cell.Y))
                {
                    var value = SorrundingValue(cell.X, cell.Y);
                    if (value < bestScore)
                    {
                        bestScore = value;
                        bestCell = cell;
                    }
                }
            }

            return bestCell;
        }

        private int BorderCells(int x, int y)
        {
            // If its in a corner
            if (x == 0 && y == 0)
                return 5;
            if (x == Game.Board.Width - 1 && y == 0)
                return 5;
            if (x == Game.Board.Width - 1 && y == Game.Board.Height - 1)
                return 5;
            if (x == 0 && y == Game.Board.Height - 1)
                return 5;

            // If its on one of the sides
            if (x == Game.Board.Width - 1)
                return 3;
            if (x == 0)
                return 3;
            if (y == Game.Board.Height - 1)
                return 3;
            if (y == 0)
                return 3;

            // Otherwise, its a normal cell
            return 0;
        }

        private bool IsBordering(int x, int y)
        {
            int counter = 0;
            if (IsCellBordering(x + 1, y)) counter++;
            if (IsCellBordering(x + 1, y + 1)) counter++;
            if (IsCellBordering(x + 1, y - 1)) counter++;
            if (IsCellBordering(x - 1, y)) counter++;
            if (IsCellBordering(x - 1, y + 1)) counter++;
            if (IsCellBordering(x - 1, y - 1)) counter++;
            if (IsCellBordering(x, y - 1)) counter++;
            if (IsCellBordering(x, y + 1)) counter++;
            if (counter > 1 && counter != 8)
                return true;
            return false;
        }

        private bool IsCellBordering(int x, int y) => Game.Board.IsValidCell(x, y) && Game.Board.Cells[x, y] is Cell cell1 && cell1.IsVisible;

        private int SorrundingValue(int x, int y)
        {
            int counter = 0;

            var borderCells = BorderCells(x, y);
            var visibleCells = 0;
            if (IsCellVisible(x + 1, y)) visibleCells++;
            if (IsCellVisible(x + 1, y + 1)) visibleCells++;
            if (IsCellVisible(x + 1, y - 1)) visibleCells++;
            if (IsCellVisible(x - 1, y)) visibleCells++;
            if (IsCellVisible(x - 1, y + 1)) visibleCells++;
            if (IsCellVisible(x - 1, y - 1)) visibleCells++;
            if (IsCellVisible(x, y + 1)) visibleCells++;
            if (IsCellVisible(x, y - 1)) visibleCells++;
            if (visibleCells == 8 - borderCells)
                return int.MaxValue;

            counter += GetCellValue(x + 1, y);
            counter += GetCellValue(x + 1, y + 1);
            counter += GetCellValue(x + 1, y - 1);
            counter += GetCellValue(x - 1, y);
            counter += GetCellValue(x - 1, y + 1);
            counter += GetCellValue(x - 1, y - 1);
            counter += GetCellValue(x, y + 1);
            counter += GetCellValue(x, y - 1);
            return counter + borderCells + (8 - borderCells - visibleCells);
        }

        private int GetCellValue(int x, int y)
        {
            if (Game.Board.IsValidCell(x, y) && Game.Board.Cells[x, y] is Cell cell1 && cell1.IsVisible && IsDigitsOnly(cell1.Item))
                return Int32.Parse(cell1.Item);
            return 0;
        }
        private bool IsCellVisible(int x, int y) => Game.Board.IsValidCell(x, y) && Game.Board.Cells[x, y] is Cell cell1 && cell1.IsVisible;

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private IMineSweeper.ActionResult DoRandomMove()
        {
            var rnd = new Random();
            while (true)
            {
                var x = rnd.Next(0, Game.Board.Width);
                var y = rnd.Next(0, Game.Board.Height);
                var cell = Game.Board.Cells[x, y];
                if (cell != null && !cell.IsVisible)
                    return Game.SelectCell(x, y);
            }
        }
    }
}
