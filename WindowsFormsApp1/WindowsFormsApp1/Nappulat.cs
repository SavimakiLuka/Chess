using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Nappulat : Form
    {
        Label[,] gridlabel = Pelilauta.gridLabel;

        public static ChessPiece King()
        {
            string text = "♚";
            Color color = Color.Blue;
            int x_Location = 7;
            int y_Location = 3;

            return new ChessPiece(text, color, x_Location, y_Location); 
        }

        public static ChessPiece Knight()
        {
            string text = "♞";
            Color color = Color.Blue;
            int x_Location = 7;
            int y_Location = 1;

            return new ChessPiece(text, color, x_Location, y_Location);
        }

        public static ChessPiece Rook()
        {
            string text = "♜";
            Color color = Color.Blue;
            int x_Location = 7;
            int y_Location = 0;

            return new ChessPiece(text, color, x_Location, y_Location);
        }

        public static ChessPiece Bishop()
        {
            string text = "♝";
            Color color = Color.Blue;
            int x_Location = 7;
            int y_Location = 2;

            return new ChessPiece(text, color, x_Location, y_Location);
        }

        public static ChessPiece Queen()
        {
            string text = "♛";
            Color color = Color.Blue;
            int x_Location = 7;
            int y_Location = 4;

            return new ChessPiece(text, color, x_Location, y_Location);
        }

        public static ChessPiece Pawn()
        {
            string text = "♟";
            Color color = Color.Blue;
            int x_Location = 6;
            int y_Location = 0;

            return new ChessPiece(text, color, x_Location, y_Location);
        }
    }
}
