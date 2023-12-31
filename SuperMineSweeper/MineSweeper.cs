﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperMineSweeper.IMineSweeper;

namespace SuperMineSweeper
{
    public class MineSweeper : IMineSweeper
    {
        public int Bombs { get; set; }
        public int Flags { get; set; }
        public IBoard Board { get; }

        public MineSweeper(IBoard board, int bombs)
        {
            Bombs = bombs;
            Flags = bombs;
            Board = board;
        }

        public ActionResult SelectCell(int x, int y)
        {
            if (Board.IsValidCell(x, y))
            {
                var cell = Board.Cells[x, y];
                if (cell != null)
                {
                    if (cell.HasBomb)
                    {
                        cell.IsVisible = true;
                        return ActionResult.Died;
                    }
                    else
                    {
                        ClearAdjacentCells(cell);
                        cell.IsVisible = true;
                        return ActionResult.Success;
                    }
                }
            }
            return ActionResult.None;
        }

        private void ClearAdjacentCells(Cell cell)
        {
            if (cell.IsVisible || cell.HasBomb)
                return;
            cell.IsVisible = true;
            if (cell.Item != " ")
                return;
            if (Board.IsValidCell(cell.X + 1, cell.Y) && Board.Cells[cell.X + 1, cell.Y] is Cell other1) ClearAdjacentCells(other1);
            if (Board.IsValidCell(cell.X + 1, cell.Y + 1) && Board.Cells[cell.X + 1, cell.Y + 1] is Cell other2) ClearAdjacentCells(other2);
            if (Board.IsValidCell(cell.X + 1, cell.Y - 1) && Board.Cells[cell.X + 1, cell.Y - 1] is Cell other3) ClearAdjacentCells(other3);
            if (Board.IsValidCell(cell.X - 1, cell.Y) && Board.Cells[cell.X - 1, cell.Y] is Cell other4) ClearAdjacentCells(other4);
            if (Board.IsValidCell(cell.X - 1, cell.Y + 1) && Board.Cells[cell.X - 1, cell.Y + 1] is Cell other5) ClearAdjacentCells(other5);
            if (Board.IsValidCell(cell.X - 1, cell.Y - 1) && Board.Cells[cell.X - 1, cell.Y - 1] is Cell other6) ClearAdjacentCells(other6);
            if (Board.IsValidCell(cell.X, cell.Y + 1) && Board.Cells[cell.X, cell.Y + 1] is Cell other7) ClearAdjacentCells(other7);
            if (Board.IsValidCell(cell.X, cell.Y - 1) && Board.Cells[cell.X, cell.Y - 1] is Cell other8) ClearAdjacentCells(other8);
        }

        public void FlagCell(int x, int y)
        {
            if (Board.IsValidCell(x,y))
            {
                var cell = Board.Cells[x, y];
                if (cell != null)
                {
                    if (cell.IsFlagged)
                    {
                        cell.IsFlagged = false;
                        Flags++;
                    }
                    else if (!cell.IsFlagged && Flags > 0)
                    {
                        cell.IsFlagged = true;
                        Flags--;
                    }
                }
            }
        }

        public bool HaveWon()
        {
            bool haveWon = true;
            foreach (var cell in Board.Cells) 
            {
                if (cell != null && cell.HasBomb && !cell.IsFlagged)
                {
                    haveWon = false;
                    break;
                }
            }

            return haveWon;
        }
    }
}
