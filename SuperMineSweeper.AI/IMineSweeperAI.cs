using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.AI
{
    public interface IMineSweeperAI
    {
        public delegate void DidActionHandler();
        public event DidActionHandler? DidAction;
        public event DidActionHandler? GameEnded;

        public int MoveInterval { get; }
        public int Moves { get; }
        public IMineSweeper Game { get; }
        
        public void Start();
        public void Stop();

        public void DoMove();
    }
}
