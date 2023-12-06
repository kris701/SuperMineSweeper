using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMineSweeper
{
    public interface IMineSweeper
    {
        public enum ActionResult { None, Success, Died }

        public int Bombs { get; set; }
        public int Flags { get; set; }
        public IBoard Board { get; }
        public ActionResult SelectCell(int x, int y);
        public void FlagCell(int x, int y);
        public bool HaveWon();
    }
}
