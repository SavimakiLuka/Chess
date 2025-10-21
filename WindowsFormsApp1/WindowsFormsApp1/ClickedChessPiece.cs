using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ClickedChessPiece
    {
        public string clickedPiece { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ClickedChessPiece(string clickedpiece, int x, int y)
        {
            clickedPiece = clickedpiece;
            X = x;
            Y = y;
        }
    }
}
