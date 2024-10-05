using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ChessPiece
    {
        public string Text { get; set; }
        public Color PieceColor { get; set; }
        public int X_Location { get; set; }
        public int Y_Location { get; set; }

        public ChessPiece(string text, Color pieceColor, int x_Location, int y_Location)
        {
            Text = text;
            PieceColor = pieceColor;
            X_Location = x_Location;
            Y_Location = y_Location;
        }
    }
}
