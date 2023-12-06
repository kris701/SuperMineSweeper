using System;
using SuperMineSweeperUI.GameStyles;
using SuperMineSweeperUI.Windows;
using Terminal.Gui;

namespace SuperMineSweeperUI
{
    internal class Program
    {
        private static bool _stop = false;
        private static string _currentStyle = "Square, Easy";
        static void Main(string[] args)
        {
            var styles = new List<MenuItem>();
            foreach(var style in GameStyleBuilder.GetStyleNames())
            {
                styles.Add(new MenuItem($"_{style}", "", () => {
                    _currentStyle = style;
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
                        })
                    }),
                    new MenuBarItem ("_Styles", styles.ToArray()),
                });

                var game = GameStyleBuilder.GetStyle(_currentStyle);
                var win = new GameWindow(game)
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