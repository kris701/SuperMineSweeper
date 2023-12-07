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
            { "Random, Slow", (g) => { return new RandomAI(500, g); } },
            { "Safe, Slow", (g) => { return new SafeAI(500, g); } },
        };

        public static IMineSweeperAI GetAI(string name, IMineSweeper game) => _ais[name](game);
        public static List<string> GetAINames() => _ais.Keys.ToList();
    }
}
