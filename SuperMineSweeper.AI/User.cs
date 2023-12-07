using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperMineSweeper.AI.IMineSweeperAI;

namespace SuperMineSweeper.AI
{
    public class User : IMineSweeperAI
    {
        public event DidActionHandler? DidAction;
        public event DidActionHandler? GameEnded;

        public int MoveInterval { get; }
        public int Moves { get; internal set; }
        public IMineSweeper Game { get; }

        public User(IMineSweeper game)
        {
            MoveInterval = -1;
            Game = game;
        }

        public void Start() { }
        public void Stop() { }

        public void DoMove() { }
    }
}
