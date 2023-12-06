using SuperMineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace SuperMineSweeperUI.Windows
{
    public class InfoWindow : Window
    {
        public static int WindowWidth = 15;

        private IMineSweeper _game;
        private Label _widthLabel;
        private Label _heightLabel;
        private Label _bombsLabel;
        private Label _flagsLabel;

        public InfoWindow(IMineSweeper game)
        {
            _game = game;
            Width = WindowWidth;
            Height = game.Board.Height + 2;
            X = game.Board.Width + 2;
            Title = "Info";
            ColorScheme = new ColorScheme();

            _widthLabel = new Label()
            {
                Text = $"Width: {game.Board.Width}",
                Y = 0
            };
            _heightLabel = new Label()
            {
                Text = $"Heigth: {game.Board.Height}",
                Y = 1
            };
            _bombsLabel = new Label()
            {
                Text = $"Bombs: {game.Bombs}",
                Y = 2
            };
            _flagsLabel = new Label()
            {
                Text = $"Flags: {game.Flags}",
                Y = 3
            };
            Add(_widthLabel, _heightLabel, _bombsLabel, _flagsLabel);
        }

        public void Update()
        {
            _flagsLabel.Text = $"Flags: {_game.Flags}";
        }
    }
}
