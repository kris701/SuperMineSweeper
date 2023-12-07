using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperMineSweeper.AI.IMineSweeperAI;

namespace SuperMineSweeper.AI
{
    public class RandomAI : IMineSweeperAI
    {
        public event DidActionHandler? DidAction;
        public event DidActionHandler? GameEnded;

        public int MoveInterval { get; }
        public int Moves { get; internal set; }
        public IMineSweeper Game { get; }

        private System.Timers.Timer _timer;

        public RandomAI(int moveInterval, IMineSweeper game)
        {
            MoveInterval = moveInterval;
            Game = game;

            _timer = new System.Timers.Timer();
            _timer.Interval = moveInterval;
            _timer.AutoReset = true;
            _timer.Elapsed += (s, e) => { DoMove(); };
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        public void DoMove()
        {
            var rnd = new Random();
            bool didMove = false;
            while (!didMove)
            {
                var x = rnd.Next(0, Game.Board.Width);
                var y = rnd.Next(0, Game.Board.Height);
                var cell = Game.Board.Cells[x, y];
                if (cell != null && !cell.IsVisible)
                {
                    var result = Game.SelectCell(x, y);
                    if (result == IMineSweeper.ActionResult.Died && GameEnded != null)
                        GameEnded.Invoke();
                    didMove = true;
                }
            }
            if (DidAction != null)
                DidAction.Invoke();
        }
    }
}
