using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace SuperMineSweeperUI.Dialogs
{
    public class HowToPlayDialog : Dialog
    {
        public Button OkButton;
        public HowToPlayDialog()
        {
            Border.BorderStyle = BorderStyle.None;
            Border.Effect3D = false;
            Title = "How to Play";
            Y = 1;
            Width = Dim.Fill();
            Height = Dim.Fill() - 1;
            ButtonAlignment = ButtonAlignments.Center;

            var textView = new TextView()
            {
                Text =  "Super Minesweeper\n\n" +
                        "The basic game is about finding and flagging mines in a minefield." +
                        "You select a tile on the board and it will reveal a number describing how many mines are near it." +
                        "You can flag mines by holding down CTRL while clicking on a tile.\n\n" +
                        "You win by flagging all mines on the field.",
                Y = Pos.Center(),
                X = Pos.Center(),
                Width = Dim.Fill(1),
                Height = Dim.Fill(),
                WordWrap = true,
                Enabled = false,
            };


            OkButton = new Button("Ok")
            {
                AutoSize = false,
                Y = Pos.AnchorEnd(1),
                X = Pos.Center()
            };

            Add(textView, OkButton);
        }
    }
}
