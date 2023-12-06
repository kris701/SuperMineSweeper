using SuperMineSweeper;
using SuperMineSweeper.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeperUI.GameStyles
{
    public static class GameStyleBuilder
    {
        private static Dictionary<string, Func<IMineSweeper>> _styles = new Dictionary<string, Func<IMineSweeper>>()
        {
            { "Square, Easy", () => { return new MineSweeper(new SquareBoard(10, 10, 5), 5); } },
            { "Square, Medium", () => { return new MineSweeper(new SquareBoard(15, 15, 20), 20); } },
            { "Square, Hard", () => { return new MineSweeper(new SquareBoard(20, 20, 50), 50); } },
            { "Random, Easy", () => { return new MineSweeper(new RandomBoard(10, 10, 5), 5); } },
            { "Random, Medium", () => { return new MineSweeper(new RandomBoard(15, 15, 20), 20); } },
            { "Random, Hard", () => { return new MineSweeper(new RandomBoard(20, 20, 50), 50); } },
        };

        public static IMineSweeper GetStyle(string name) => _styles[name]();
        public static List<string> GetStyleNames() => _styles.Keys.ToList();
    }
}
