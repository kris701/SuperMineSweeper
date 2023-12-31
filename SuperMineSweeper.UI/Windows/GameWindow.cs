﻿using SuperMineSweeper;
using SuperMineSweeper.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace SuperMineSweeperUI.Windows
{
    public class GameWindow : FrameView
    {
        private BoardWindow _boardWindow;
        private InfoWindow _infoWindow;

        public GameWindow(IMineSweeper game, IMineSweeperAI ai)
        {
            Title = "Super MineSweeper";
            ColorScheme = new ColorScheme();
            Border.BorderStyle = BorderStyle.Rounded;
            Border.BorderBrush = Color.BrightBlue;
            _boardWindow = new BoardWindow(game, ai);
            _infoWindow = new InfoWindow(game);
            _boardWindow.OnUpdate += _infoWindow.Update;
            _boardWindow.OnGameOver += () =>
            {
                _infoWindow.StopTimer();
                Add(new GameOverWindow(game, "Game Over!"));
            };
            Add(_boardWindow, _infoWindow);
        }
    }
}
