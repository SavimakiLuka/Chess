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
        Grid _chessBoard;
        Label pressedPiece; // nappula jota painettiin
        Label draggedPiece; // nappula joka liikkuu hiiren mukana

        List<Piece> blackPiecesInfo;
        List<Piece> whitePiecesInfo;

        Point position = new Point();

        bool mouseButtonDown;

        ChessViewModel _chessViewModel;
        Logic _logic;

        public UIBuilder(Grid board, Label _pressedPiece)
        {
            _chessBoard = board;
            pressedPiece = _pressedPiece;
            draggedPiece = _pressedPiece;
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

                    label.MouseLeftButtonDown += Piece_Click;
                    label.MouseLeftButtonUp += Piece_UnClick;

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

                    label.MouseLeftButtonDown += Piece_Click;
                    label.MouseLeftButtonUp += Piece_UnClick;

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
            mouseButtonDown = false;
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

            _chessViewModel = new(blackPiecesInfo, whitePiecesInfo, pressedPiece);
            _logic = new(blackPiecesInfo, whitePiecesInfo, pressedPiece);

            _chessBoard.MouseMove += ChessBoard_MouseMove;

            // Vaihtaa nappulan läpinäkyvyyttä kun sitä painetaan
            ChangePieceOpacity(mouseButtonDown, pressedPiece);

            // Asetetaan hiiren mukana liikkuvan nappulan näkö samaksi kuin painettu
            SetDraggedPieceVisual(pressedPiece, draggedPiece, mouseButtonDown);

            // Katsoo mitä nappulaa painettiin ja palauttaa listan mahdollisista liikkumis paikoista
            ableToMoves = _chessViewModel.ReturnPossibleMovement(color, pieceLocation, piece);

            // Katsoo mitä nappulaa painettiin ja palauttaa listan mahdollisista syömis paikoista
            /*ableToEat = _chessViewModel.ReturnPossibleEatingPieces(color, pieceLocation, piece);*/
            ableToEat = _logic.ableToEat;

            // Luo lableit jotka näyttävät mihin nappula voi liikkua
            CreateMovementPatterns(color, ableToMoves, ableToEat, mouseButtonDown);
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

            if (!mouseButtonDown)
            {
                pressedPiece.Opacity = 1;
            }
            else
            {
                pressedPiece.Opacity = 0.3;
            }
        }

        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (pressedPiece != null && mouseButtonDown)
            {
                position = e.GetPosition(_chessBoard);

                SetDraggedPiecePosition();
            }
        }

        private void SetDraggedPiecePosition()
        {
            Canvas.SetLeft(draggedPiece, position.X - 25);
            Canvas.SetTop(draggedPiece, position.Y - 30);
        }

        public void CreateMovementPatterns(string color, List<string> ableToMoves, List<string> ableToEat, bool mouseButtonDown)
        {
            foreach (var move in ableToMoves)
            {
                // katsoo ruudun paikan johon laitetaan label
                Border border = _chessBoard.FindName(move) as Border;

                // katsoo onko ruudun paikalla valmiiksi jo joku nappula
                bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
                bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                Label label = new Label();

                if (!mouseButtonDown) // poistetaan liikkumis mahdollisuus pallo
                {
                    label.Content = "";

                    border.Child = label;
                }
                else // lisätään liikkumis mahdollisuus pallo
                {
                    label.FontSize = 30;
                    label.Content = "●";
                    label.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#7f7f7f");
                    label.Opacity = 0.5;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.VerticalAlignment = VerticalAlignment.Center;

                    border.Child = label;
                }
            }

            if (ableToEat != null)
            {
                foreach (var piece in ableToEat)
                {
                    Border border1 = _chessBoard.FindName(piece) as Border;

                    if (color == "White")
                    {
                        if (border1.Child is Label existingLabel)
                        {
                            if (mouseButtonDown) // poistetaan liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.Black;  // Vaihtaa tekstin värin mustaksi
                            }
                            else // lisätään liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.Red;  // Vaihtaa tekstin värin punaiseksi
                            }
                        }
                    }
                    else
                    {
                        if (border1.Child is Label existingLabel)
                        {
                            if (mouseButtonDown) // poistetaan liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.White;  // Vaihtaa tekstin värin mustaksi
                            }
                            else // lisätään liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.Red;  // Vaihtaa tekstin värin punaiseksi
                            }
                        }
                    }
                }
            }
        }

        public void Piece_Click(object sender, MouseButtonEventArgs e)
        {
            Label clickedPiece = sender as Label;
            string name = $"{((Label)sender).Name}_true";

            ChessViewModel chessViewModel = new ChessViewModel(blackPiecesInfo, whitePiecesInfo, clickedPiece);

            position = e.GetPosition(_chessBoard);
            SetDraggedPiecePosition();

            chessViewModel.OnPiecePressed(clickedPiece, name);
            PieceHandling(clickedPiece, name);
        }

        public void Piece_UnClick(object sender, MouseButtonEventArgs e)
        {
            Label clickedPiece = sender as Label;
            string name = $"{((Label)sender).Name}_false";


            position = e.GetPosition(_chessBoard);

            _chessViewModel.OnPieceReleased(clickedPiece, name);
            PieceHandling(clickedPiece, name);
        }

        /* public void IfCursorOnLabel()
        {
            if (mouseButtonDown)
            {
                foreach (var move in ableToMoves)
                {
                    // katsoo ruudun paikan johon laitetaan label
                    Border border = chessBoard.FindName(move) as Border;

                    Point pos = border.TransformToAncestor(chessBoard).Transform(new Point(0, 0));

                    // katsoo onko ruudun paikalla valmiiksi jo joku nappula
                    bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
                    bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                    if (position.X == pos.X)
                    {
                        Label label = new Label();

                        label.FontSize = draggedLabel.FontSize;
                        label.Content = draggedLabel.Content;
                        label.Foreground = draggedLabel.Foreground;
                        label.HorizontalAlignment = HorizontalAlignment.Center;
                        label.VerticalAlignment = VerticalAlignment.Center;

                        border.Child = label;
                    }
                }
            }
        }*/
    }
}
