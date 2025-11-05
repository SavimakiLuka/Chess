using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Chess.Model
{
    class Board
    {
        private Grid chessBoard;
        private Label pressedPiece;
        List<Piece> blackPiecesInfo = new();
        List<Piece> whitePiecesInfo = new();

        public Board(Grid grid, Label pressedpiece)
        {
            chessBoard = grid;
            pressedPiece = pressedpiece;
        }

        /*public void CreateBoard()
        {
            string[] boardAlphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };
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
                    square.Background = (row + col) % 2 == 0
                        ? (SolidColorBrush)new BrushConverter().ConvertFrom("#DEDEDE")  // vaalea ruutu, lähes valkoinen
                        : (SolidColorBrush)new BrushConverter().ConvertFrom("#141414");
                    square.Name = $"{boardAlphabet[col]}{rows - row}";
                    Panel.SetZIndex(square, 0);

                    chessBoard.RegisterName(square.Name, square);

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);
                    chessBoard.Children.Add(square);
                }
            }
        }

        public void AddPiecesToBoard(List<Piece> blackPieces, List<Piece> whitePieces)
        {
            Border a1Border;
            foreach (var piece in whitePieces)
            {
                a1Border = chessBoard.FindName(piece.Location) as Border;

                whitePiecesInfo.Add(piece);

                if (a1Border != null)
                {
                    Label label = new Label();
                    label.Name = $"{piece.color}_{piece.name}_{piece.Location}";

                    chessBoard.RegisterName(label.Name, label);

                    label.FontSize = 40;
                    label.Content = piece.Emoji;
                    label.Foreground = Brushes.White;
                    label.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.Black,      // rajausväri
                        BlurRadius = 1,            // terävä reuna
                        ShadowDepth = 1,           // suoraan emojiin ympärille
                        Opacity = 1
                    };
                    Panel.SetZIndex(label, 2);

                    label.MouseLeftButtonDown += Piece_Click;
                    label.MouseLeftButtonUp += Piece_UnClick;


                    if (a1Border.Child == null)
                    {
                        a1Border.Child = label;
                    }
                    else
                    {

                    }
                }
            }

            foreach (var piece in blackPieces)
            {
                a1Border = chessBoard.FindName(piece.Location) as Border;

                blackPiecesInfo.Add(piece);

                if (a1Border != null)
                {
                    Label label = new Label();
                    label.Name = $"{piece.color}_{piece.name}_{piece.Location}";

                    chessBoard.RegisterName(label.Name, label);

                    label.FontSize = 40;
                    label.Content = piece.Emoji;
                    label.Foreground = Brushes.Black;
                    label.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.White,      // rajausväri
                        BlurRadius = 1,            // terävä reuna
                        ShadowDepth = 1,           // suoraan emojiin ympärille
                        Opacity = 1
                    };
                    Panel.SetZIndex(label, 2);

                    label.MouseLeftButtonDown += Piece_Click;
                    label.MouseLeftButtonUp += Piece_UnClick;


                    if (a1Border.Child == null)
                    {
                        a1Border.Child = label;
                    }
                    else
                    {

                    }
                }
            }
        }*/

        public void Piece_Click(object sender, MouseButtonEventArgs e)
        {
            Logic logic = new Logic(chessBoard, blackPiecesInfo, whitePiecesInfo, pressedPiece);
            string name = $"{((Label)sender).Name}_false";
            Label clickedLabel = sender as Label;

            Point pos = e.GetPosition(chessBoard);
            logic.clickedPosition = pos;

            logic.Piece_Clicked(name, clickedLabel);
        }

        public void Piece_UnClick(object sender, MouseButtonEventArgs e)
        {
            Logic logic = new Logic(chessBoard, blackPiecesInfo, whitePiecesInfo, pressedPiece);
            string name = $"{((Label)sender).Name}_true";
            Label clickedLabel = sender as Label;

            logic.Piece_Clicked(name, clickedLabel);
        }
    }
}
