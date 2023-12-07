using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.AI.AIs
{
    public class RandomAI : BaseAI
    {
        public RandomAI(int moveInterval, IMineSweeper game) : base(moveInterval, game)
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
