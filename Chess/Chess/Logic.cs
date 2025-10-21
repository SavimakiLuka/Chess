using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{
    internal class Logic
    {
        private Grid chessBoard;

        public Logic(Grid grid)
        {
            chessBoard = grid;
        }
        public void CreateBoard()
        {
            int rows = 8;
            int cols = 8;

            // Laittaa shakin ruudukon leveyden ja korkeuden ja myös taustavärin
            chessBoard.Background = Brushes.White;
            chessBoard.Width = 400;
            chessBoard.Height = chessBoard.Width;

            // Luo shakin ruudukon
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var square = new Border();
                    square.Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.Black;

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);
                    chessBoard.Children.Add(square);
                }
            }
        }
    }
}
