using SuperMineSweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace SuperMineSweeperUI.Windows
{
    public class BoardWindow : FrameView
    {
        public delegate void UpdatedHandler();
        public event UpdatedHandler? OnUpdate;
        public event UpdatedHandler? OnGameOver;

        private IMineSweeper _game;
        private List<Label> _cells;
        private bool _isHoldingCtrl = false;

        public BoardWindow(IMineSweeper game)
        {
            _cells = new List<Label>();
            _game = game;
            Width = game.Board.Width + 2;
            Height = game.Board.Height + 2;
            Title = "Board";
            ColorScheme = new ColorScheme();
            Border.BorderStyle = BorderStyle.Rounded;
            Application.Top.KeyDown += (a) =>
            {
                if (a.KeyEvent.IsCtrl)
                    _isHoldingCtrl = true;
            };
            Application.Top.KeyUp += (a) =>
            {
                if (a.KeyEvent.IsCtrl)
                    _isHoldingCtrl = false;
            };

            for (int x = 0; x < _game.Board.Width; x++)
            {
                for (int y = 0; y < _game.Board.Height; y++)
                {
                    var cell = _game.Board.Cells[x, y];
                    if (cell != null)
                    {
                        var newButton = new Label();
                        newButton.Clicked += () =>
                        {
                            if (newButton.Data is Cell cell)
                                HandleCellClick(cell);
                        };
                        newButton.Data = cell;
                        newButton.X = x;
                        newButton.Y = y;
                        newButton.Text = "=";
                        _cells.Add(newButton);
                        Add(newButton);
                    }
                }
            }
        }

        private void HandleCellClick(Cell cell)
        {
            if (cell.IsVisible)
                return;
            if (_isHoldingCtrl)
            {
                _game.FlagCell(cell.X, cell.Y);
                if (_game.HaveWon())
                    GameEnd();
                else
                    UpdateField();
            }
            else
            {
                var result = _game.SelectCell(cell.X, cell.Y);
                if (result == IMineSweeper.ActionResult.Died)
                    GameEnd();
                else
                    UpdateField();
            }
        }

        private void UpdateField()
        {
            foreach (var uiCell in _cells)
            {
                if (uiCell.Data is Cell cell)
                {
                    if (cell.IsVisible)
                        uiCell.Text = cell.Item;
                    else if (cell.IsFlagged)
                        uiCell.Text = "F";
                    else
                        uiCell.Text = "=";
                    uiCell.ColorScheme = GetColorSchemeForCell(cell);
                }
            }
            if (OnUpdate != null)
                OnUpdate.Invoke();
        }

        private void GameEnd()
        {
            foreach (var uiCell in _cells)
            {
                if (uiCell.Data is Cell cell)
                {
                    cell.IsVisible = true;
                    uiCell.ColorScheme = GetColorSchemeForCell(cell);
                }
                uiCell.Enabled = false;
            }
            UpdateField();
            if (OnGameOver != null)
                OnGameOver.Invoke();
        }

        private ColorScheme GetColorSchemeForCell(Cell cell)
        {
            var newScheme = new ColorScheme()
            {
                Normal = new Terminal.Gui.Attribute(Color.Gray, Color.Black),
                Disabled = new Terminal.Gui.Attribute(Color.DarkGray, Color.Black)
            };

            if (cell.IsVisible && cell.HasBomb && !cell.IsFlagged)
            {
                newScheme.Disabled = new Terminal.Gui.Attribute(Color.Red, Color.Black);
            }
            else if (cell.IsVisible && cell.HasBomb && cell.IsFlagged)
            {
                newScheme.Disabled = new Terminal.Gui.Attribute(Color.Green, Color.Black);
            }
            else if (!cell.IsVisible && cell.IsFlagged)
            {
                newScheme.Normal = new Terminal.Gui.Attribute(Color.Cyan, Color.Black);
            }
            else if (!cell.HasBomb && cell.IsVisible && cell.Item != " ")
            {
                if (cell.Item == "1")
                    newScheme.Normal = new Terminal.Gui.Attribute(Color.BrightGreen, Color.Black);
                else if (cell.Item == "2")
                    newScheme.Normal = new Terminal.Gui.Attribute(Color.BrightBlue, Color.Black);
                else if (cell.Item == "3")
                    newScheme.Normal = new Terminal.Gui.Attribute(Color.BrightYellow, Color.Black);
                else
                    newScheme.Normal = new Terminal.Gui.Attribute(Color.BrightRed, Color.Black);
            }
            return newScheme;
        }
    }
}
