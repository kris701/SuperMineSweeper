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
