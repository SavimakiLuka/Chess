using Chess.Model;
using Chess.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chess.View
{
    internal class UIBuilder
    {
        Grid chessBoard;
        Label pressedPiece;
        List<Piece> blackPiecesInfo;
        List<Piece> whitePiecesInfo;

        public UIBuilder(Grid board, Label _pressedPiece)
        {
            chessBoard = board;
            pressedPiece = _pressedPiece;
        }

        public void DrawBoard(int boardSize)
        {
            _chessBoard.Children.Clear();

            string[] boardAlphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };

            int rows = 8;
            int cols = 8;

            for (int r = 0; r < rows; r++)
            {
                _chessBoard.RowDefinitions.Add(new RowDefinition());
            }

            for (int c = 0; c < cols; c++)
            {
                _chessBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Laittaa shakin ruudukon leveyden ja korkeuden ja myös taustavärin
            _chessBoard.Background = Brushes.White;
            _chessBoard.Width = boardSize;
            _chessBoard.Height = boardSize;

            // Luo shakin ruudukon
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var square = new Border();

                    square.Background = (row + col) % 2 == 0
                        ? (SolidColorBrush)new BrushConverter().ConvertFrom("#DEDEDE")  // vaalea ruutu, lähes valkoinen
                        : (SolidColorBrush)new BrushConverter().ConvertFrom("#141414"); // tumma ruutu, lähes musta
                    square.Name = $"{boardAlphabet[col]}{rows - row}";
                    Panel.SetZIndex(square, 0);

                    _chessBoard.RegisterName(square.Name, square);

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);
                    _chessBoard.Children.Add(square);
                }
            }
        }

        public void DrawPieces(List<Piece> blackPieces, List<Piece> whitePieces, int fontSize)
        {
            blackPiecesInfo = new();
            whitePiecesInfo = new();

            Border squareBorder;

            // Asettaa valkoiset nappulat laudalle
            foreach (var piece in whitePieces)
            {
                squareBorder = _chessBoard.FindName(piece.Location) as Border;

                whitePiecesInfo.Add(piece);

                if (squareBorder != null && squareBorder.Child == null)
                {
                    Label label = new Label();
                    label.Name = $"{piece.color}_{piece.name}_{piece.Location}";

                    _chessBoard.RegisterName(label.Name, label);

                    label.FontSize = fontSize;
                    label.Content = piece.Emoji;
                    label.Foreground = Brushes.White;
                    label.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.Black,      // rajausväri
                        BlurRadius = 1,            // terävä reuna
                        ShadowDepth = 1,           // suoraan emojiin ympärille
                        Opacity = 1
                    };
                    Panel.SetZIndex(label, 1);

                    /*label.MouseLeftButtonDown += Piece_Click;
                    label.MouseLeftButtonUp += Piece_UnClick;*/

                    squareBorder.Child = label;
                }
            }

            // Asettaa mustat nappulat laudalle
            foreach (var piece in blackPieces)
            {
                squareBorder = _chessBoard.FindName(piece.Location) as Border;

                blackPiecesInfo.Add(piece);

                if (squareBorder != null && squareBorder.Child == null)
                {
                    Label label = new Label();
                    label.Name = $"{piece.color}_{piece.name}_{piece.Location}";

                    _chessBoard.RegisterName(label.Name, label);

                    label.FontSize = fontSize;
                    label.Content = piece.Emoji;
                    label.Foreground = Brushes.Black;
                    label.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.White,      // rajausväri
                        BlurRadius = 1,            // terävä reuna
                        ShadowDepth = 1,           // suoraan emojiin ympärille
                        Opacity = 1
                    };
                    Panel.SetZIndex(label, 1);

                    /*label.MouseLeftButtonDown += Piece_Click;
                    label.MouseLeftButtonUp += Piece_UnClick;*/

                    squareBorder.Child = label;
                }
            }
        }

        public void PieceHandling(Label clickedLabel, string clickedPiece)
        {
            var pressedPiece = clickedLabel;
            var color = clickedPiece.Split('_')[0];
            var piece = clickedPiece.Split('_')[1];
            var pieceLocation = clickedPiece.Split('_')[2];
            var unClickCheck = "";
            var mouseButtonDown = false;
            var ableToMoves = new List<string>();
            var ableToEat = new List<string>();

            unClickCheck = clickedPiece.Split('_')[3];

            if (unClickCheck == "true")
            {
                mouseButtonDown = true;
            }
            else
            {
                mouseButtonDown = false;
            }

            chessBoard.MouseMove += ChessBoard_MouseMove;

            // Vaihtaa nappulan läpinäkyvyyttä kun sitä painetaan
            ChangePieceOpacity(mouseButtonDown);

            // Asetetaan hiiren mukana liikkuvan nappulan näkö samaksi kuin painettu
            SetDraggedPieceVisual(pressedLabel, draggedLabel, mouseButtonDown);

            // Katsoo mitä nappulaa painettiin ja palauttaa listan mahdollisista liikkumis paikoista
            ableToMoves = GetPressedPieceMovement(color, pieceLocation, piece);

            // Jotain
            MoveChangeVisualCreate(color);
        }

        private void SetDraggedPieceVisual(Label pressedPiece, Label draggedLabel, bool mouseButtonDown)
        {
            pressedPiece.MouseLeftButtonUp += Piece_UnClick;
            if (mouseButtonDown)
            {
                draggedLabel.Foreground = pressedPiece.Foreground;
                draggedLabel.Effect = pressedPiece.Effect;
                draggedLabel.Content = pressedPiece.Content;
            }
            else
            {
                draggedLabel.Content = "";
            }
        }

        private void ChangePieceOpacity(bool mouseButtonDown, Label pressedPiece)
        {

            if (mouseButtonDown)
            {
                pressedPiece.Opacity = 1;
            }
            else
            {
                pressedPiece.Opacity = 0.3;
            }
        }

        public void Piece_Click(object sender, MouseButtonEventArgs e)
        {
            Label clickedPiece = sender as Label;
            string name = $"{((Label)sender).Name}_false";

            ChessViewModel chessViewModel = new ChessViewModel(_chessBoard, blackPiecesInfo, whitePiecesInfo, clickedPiece);

            Point pos = e.GetPosition(_chessBoard);
            /*logic.clickedPosition = pos; siirrettävä */

            chessViewModel.OnPiecePressed(clickedPiece, name);
        }

        public void Piece_UnClick(object sender, MouseButtonEventArgs e)
        {
            Label clickedPiece = sender as Label;
            string name = $"{((Label)sender).Name}_false";

            ChessViewModel chessViewModel = new ChessViewModel(_chessBoard, blackPiecesInfo, whitePiecesInfo, clickedPiece);

            Point pos = e.GetPosition(_chessBoard);
            /*logic.clickedPosition = pos; siirrettävä */

            chessViewModel.OnPieceReleased(clickedPiece, name);
        }
    }
}
