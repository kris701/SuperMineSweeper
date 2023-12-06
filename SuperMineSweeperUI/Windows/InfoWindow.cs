using SuperMineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Terminal.Gui;

namespace SuperMineSweeperUI.Windows
{
    public class InfoWindow : FrameView
    {
        public static int WindowWidth = 15;

        private IMineSweeper _game;
        private Label _widthLabel;
        private Label _heightLabel;
        private Label _bombsLabel;
        private Label _flagsLabel;
        private Label _timerLabel;
        private System.Timers.Timer _timer;
        private int _ellapsed = 0;

        public InfoWindow(IMineSweeper game)
        {
            _game = game;
            Width = WindowWidth;
            Height = game.Board.Height + 2;
            X = game.Board.Width + 2;
            Title = "Info";
            Border.BorderStyle = BorderStyle.Rounded;
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
            _timerLabel = new Label()
            {
                Text = $"Time: 0s",
                Y = 4
            };
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.AutoReset = true;
            _timer.Elapsed += (s, e) => {
                _ellapsed++;
                _timerLabel.Text = $"Time: {_ellapsed}s";
                Application.Refresh();
            };
            _timer.Start();

            Add(_widthLabel, _heightLabel, _bombsLabel, _flagsLabel, _timerLabel);
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void Update()
        {
            _flagsLabel.Text = $"Flags: {_game.Flags}";
        }
    }
}
