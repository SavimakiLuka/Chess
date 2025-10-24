using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{
    internal class Logic
    {
        private Grid chessBoard;
        string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };

        public Logic(Grid grid)
        {
            chessBoard = grid;
        }

        public void Piece_Clicked(string clickedPiece)
        {
            string color = clickedPiece.Split('_')[0];
            string piece = clickedPiece.Split('_')[1];
            string pieceLocation = clickedPiece.Split('_')[2];
            string remove = "";
            try
            {
                remove = clickedPiece.Split('_')[3];
            }
            catch
            {

            }

            string ableToMove = "";


            if (piece == "Pawn")
            {
                ableToMove = Pawn_Movement(color, pieceLocation);
            }
            else
            {
                ableToMove = Pawn_Movement(color, pieceLocation);
            }


            Border border = chessBoard.FindName(ableToMove) as Border;

            Label label = new Label();

            if (remove == "poisto")
            {
                label.FontSize = 20;
                label.Content = "";
                label.Foreground = Brushes.Green;

                border.Child = label;
            }
            else
            {
                label.FontSize = 20;
                label.Content = "o";
                label.Foreground = Brushes.Green;

                border.Child = label;
            }
        }

        public string Pawn_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            string ableToMove = "";

            if (color == "Black")
            {
                if (num == 7)
                {
                    ableToMove = $"{chars}{num - 2}";
                }
                else if (num == 1) 
                {
                    MessageBox.Show("Pääsit päätyyn onneksi olkoon!");
                }
                else
                {
                    ableToMove = $"{chars}{num - 1}";
                }
            }
            else
            {
                if (num == 2)
                {
                    ableToMove = $"{chars}{num + 2}";
                }
                else if (num == 8)
                {
                    MessageBox.Show("Pääsit päätyyn onneksi olkoon!");
                }
                else
                {
                    ableToMove = $"{chars}{num + 1}";
                }
            }
            return ableToMove;
        }
    }
}
