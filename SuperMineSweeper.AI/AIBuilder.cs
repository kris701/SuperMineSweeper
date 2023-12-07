using SuperMineSweeper.AI.AIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper.AI
{
    public static class AIBuilder
    {
        private static Dictionary<string, Func<IMineSweeper, IMineSweeperAI>> _ais = new Dictionary<string, Func<IMineSweeper, IMineSweeperAI>>()
        {
            { "User", (g) => { return new User(g); } },
            { "Random, Slow", (g) => { return new RandomAI(1000, g); } },
        };

        public static IMineSweeperAI GetAI(string name, IMineSweeper game) => _ais[name](game);
        public static List<string> GetAINames() => _ais.Keys.ToList();
    }
}
