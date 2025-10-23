using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Chess
{
    class Board
    {
        private Grid chessBoard;

        public Board(Grid grid)
        {
            chessBoard = grid;
        }

        public void CreateBoard()
        {
            string[] boardAlphabet = {"A", "B", "C", "D", "E", "F", "H", "I"};
            int rows = 8;
            int cols = 8;

            for (int r = 0; r < rows; r++)
            {
                chessBoard.RowDefinitions.Add(new RowDefinition());
            }

            for (int c = 0; c < cols; c++)
            {
                chessBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

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
                    square.Name = $"{boardAlphabet[row]}{col + 1}";

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);
                    chessBoard.Children.Add(square);
                }
            }
            Pieces pieces = new Pieces();
            pieces.AddPiece();
        }
    }
}
