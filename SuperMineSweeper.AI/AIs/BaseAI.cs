using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperMineSweeper.AI.IMineSweeperAI;

namespace SuperMineSweeper.AI.AIs
{
    public abstract class BaseAI : IMineSweeperAI
    {
        public event DidActionHandler? DidAction;
        public event DidActionHandler? GameEnded;

        public int MoveInterval { get; }
        public int Moves { get; internal set; }
        public IMineSweeper Game { get; }

        private System.Timers.Timer _timer;

        public BaseAI(IMineSweeper game)
        {
            MoveInterval = -1;
            Game = game;

            _timer = new System.Timers.Timer();
        }

        public BaseAI(int moveInterval, IMineSweeper game)
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
            var result = PerformAction();
            if (GameEnded != null && (result == IMineSweeper.ActionResult.Died || Game.HaveWon()))
                GameEnded.Invoke();
            if (DidAction != null)
                DidAction.Invoke();
        }

        internal abstract IMineSweeper.ActionResult PerformAction();
    }
}
