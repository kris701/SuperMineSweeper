using SuperMineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace SuperMineSweeperUI.Windows
{
    public class GameOverWindow : Window
    {
        public static int WindowHeight = 5;

        public GameOverWindow(IMineSweeper game, string text)
        {
            Width = game.Board.Width + 2 + InfoWindow.WindowWidth;
            Height = WindowHeight;
            Y = game.Board.Height + 2;
            ColorScheme = new ColorScheme();
            Border.BorderStyle = BorderStyle.Rounded;
            var label = new Label()
            {
                Text = text,
                X = Pos.Percent(50) - text.Length / 2,
                Y = Pos.Percent(50)
            };
            Add(label);
        }
    }
}
