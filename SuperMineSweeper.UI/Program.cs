using System;
using SuperMineSweeper.AI;
using SuperMineSweeperUI.Dialogs;
using SuperMineSweeperUI.GameStyles;
using SuperMineSweeperUI.Windows;
using Terminal.Gui;

namespace SuperMineSweeperUI
{
    internal class Program
    {
        private static bool _stop = false;
        private static string _currentStyle = GameStyleBuilder.GetStyleNames()[0];
        private static string _currentAI = AIBuilder.GetAINames()[0];
        static void Main(string[] args)
        {
            Console.Title = "Super MineSweeper";
            var styles = new List<MenuItem>();
            foreach(var style in GameStyleBuilder.GetStyleNames())
            {
                styles.Add(new MenuItem($"_{style}", "", () => {
                    _currentStyle = style;
                    Application.RequestStop();
                }));
            }
            var ais = new List<MenuItem>();
            foreach (var ai in AIBuilder.GetAINames())
            {
                ais.Add(new MenuItem($"_{ai}", "", () => {
                    _currentAI = ai;
                    Application.RequestStop();
                }));
            }

            while (!_stop)
            {
                Application.Init();
                var menu = new MenuBar(new MenuBarItem[] {
                    new MenuBarItem ("_Game", new MenuItem [] {
                        new MenuItem ("_Quit", "", () => {
                            _stop = true;
                            Application.RequestStop ();
                        }),
                        new MenuItem ("_Restart", "", () => {
                            Application.RequestStop ();
                        }),
                        new MenuItem ("_How To Play", "", () => {
                            var dialog = new HowToPlayDialog();
                            dialog.OkButton.Clicked += () => {
                                Application.Top.Remove(dialog);
                            };
                            Application.Top.Add(dialog);
                            dialog.EnsureFocus();
                        })
                    }),
                    new MenuBarItem ("_Styles", styles.ToArray()),
                    new MenuBarItem ("_AIs", ais.ToArray()),
                });

                var game = GameStyleBuilder.GetStyle(_currentStyle);
                var ai = AIBuilder.GetAI(_currentAI, game);
                var win = new GameWindow(game, ai)
                {
                    X = 0,
                    Y = 1,
                    Width = Dim.Fill(),
                    Height = Dim.Fill() - 1
                };

                // Add both menu and win in a single call
                Application.Top.Add(menu, win);
                Application.Top.Width = game.Board.Width + 2 + InfoWindow.WindowWidth + 2;
                Application.Top.Height = 2 + game.Board.Height + 2 + GameOverWindow.WindowHeight + 2;
                Application.Run();
                Application.Shutdown();
            }
        }
    }
}