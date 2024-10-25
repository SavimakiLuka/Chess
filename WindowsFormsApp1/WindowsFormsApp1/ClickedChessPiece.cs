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

        public ClickedChessPiece(string clickedpiece)
        {
            clickedPiece = clickedpiece;
        }
    }
}
