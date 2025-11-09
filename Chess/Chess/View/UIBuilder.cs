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
                    if (piece.name == "King")
                    {
                        label.Name = $"{piece.color}_{piece.name}_{piece.Location}_HasNotMoved";

                        _chessBoard.RegisterName(label.Name, label);
                    }
                    else
                    {
                        label.Name = $"{piece.color}_{piece.name}_{piece.Location}";

                        _chessBoard.RegisterName(label.Name, label);
                    }

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
                    if (piece.name == "King")
                    {
                        label.Name = $"{piece.color}_{piece.name}_{piece.Location}_HasNotMoved";

                        _chessBoard.RegisterName(label.Name, label);
                    }
                    else
                    {
                        label.Name = $"{piece.color}_{piece.name}_{piece.Location}";

                        _chessBoard.RegisterName(label.Name, label);
                    }

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
            var hasMoved = "";
            mouseButtonDown = false;
            var ableToMoves = new List<string>();
            var ableToEat = new List<string>();
            var ableToSwitch = new List<string>();
            bool pawnAtFinish = false;

            unClickCheck = clickedPiece.Split('_')[3];
            
            if (unClickCheck == "HasNotMoved")
            {
                hasMoved = clickedPiece.Split('_')[3];
                unClickCheck = clickedPiece.Split('_')[4];
            }

            if (unClickCheck == "true")
            {
                mouseButtonDown = true;
            }
            else
            {
                mouseButtonDown = false;
            }

            _chessViewModel = new(blackPiecesInfo, whitePiecesInfo, pressedPiece);
            _logic = new(pressedPiece);

            _chessBoard.MouseMove += ChessBoard_MouseMove;

            // Vaihtaa nappulan läpinäkyvyyttä kun sitä painetaan
            ChangePieceOpacity(mouseButtonDown, pressedPiece);

            // Asetetaan hiiren mukana liikkuvan nappulan näkö samaksi kuin painettu
            SetDraggedPieceVisual(pressedPiece, draggedPiece, mouseButtonDown);

            // Katsoo mitä nappulaa painettiin ja palauttaa listan mahdollisista liikkumis paikoista
            ableToMoves = _chessViewModel.ReturnPossibleMovement(color, pieceLocation, piece, whitePiecesInfo, blackPiecesInfo, hasMoved);
            ableToEat = _chessViewModel.ReturnPossibleEatingPieces();
            ableToSwitch = _chessViewModel.ReturnPossibleSwitchingKing();

            // Katsoo jos pawn on laudan päädyssä ja voi muuttua
            GetPawnChange(ableToMoves, pawnAtFinish);
            
            if (!pawnAtFinish)
            {
                // Luo lableit jotka näyttävät mihin nappula voi liikkua
                CreateMovementPatterns(color, ableToMoves, ableToEat, mouseButtonDown, ableToSwitch);

                // Asetetaan Nappula siirretylle paikalle
                IfCursorOnPossibleMove(ableToMoves, ableToEat, pieceLocation, ableToSwitch);
            }
            else
            {
                SetPawnChange();
            }
        }

        private bool GetPawnChange(List<string> ableToMoves, bool pawnAtFinish)
        {
            if (ableToMoves.Count == 1)
            {
                foreach (var item in ableToMoves)
                {
                    if (item == "pawnChange")
                    {
                        pawnAtFinish = true;
                        return pawnAtFinish;
                    }
                }
            }

            return pawnAtFinish;
        }

        private void SetPawnChange(/*string fromLocation*/)
        {
            /*Border oldBorder = _chessBoard.FindName(fromLocation) as Border;

            if (oldBorder?.Child is Label pieceLabel)
            {
                pieceLabel
            }*/
        }

        private void SetDraggedPieceVisual(Label pressedPiece, Label draggedLabel, bool mouseButtonDown)
        {
            if (mouseButtonDown)
            {
                draggedLabel.Foreground = pressedPiece.Foreground;
                draggedLabel.Effect = pressedPiece.Effect;
                draggedLabel.Content = pressedPiece.Content;
            }
            else
            {
                draggedPiece.Content = null;
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

        public void CreateMovementPatterns(string color, List<string> ableToMoves, List<string> ableToEat, bool mouseButtonDown, List<string> ableToSwitch)
        {
            foreach (var move in ableToMoves)
            {
                // katsoo ruudun paikan johon laitetaan label
                Border border = _chessBoard.FindName(move) as Border;

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

                    if (border1.Child is Label existingLabel)
                    {
                        if (!mouseButtonDown)
                        {
                            if (existingLabel.Name.StartsWith("White"))
                                existingLabel.Foreground = Brushes.White;
                            else
                                existingLabel.Foreground = Brushes.Black;
                        }
                        else
                        {
                            existingLabel.Foreground = Brushes.Red;
                        }
                    }
                }
            }

            if (ableToSwitch != null)
            {
                foreach (var piece in ableToSwitch)
                {
                    Border border1 = _chessBoard.FindName(piece) as Border;
                    if (color == "Black")
                    {
                        if (border1.Child is Label existingLabel)
                        {
                            if (!mouseButtonDown) // poistetaan liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.Black;  // Vaihtaa tekstin värin mustaksi
                            }
                            else // lisätään liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.Blue;  // Vaihtaa tekstin värin Siniseksi
                            }
                        }
                    }
                    else
                    {
                        if (border1.Child is Label existingLabel)
                        {
                            if (!mouseButtonDown) // poistetaan liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.White;  // Vaihtaa tekstin värin mustaksi
                            }
                            else // lisätään liikkumis mahdollisuus pallo
                            {
                                existingLabel.Foreground = Brushes.Blue;  // Vaihtaa tekstin värin Siniseksi
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

            // mahdollistaa nappulan päästämisen ruudun ulkopuolella
            clickedPiece.CaptureMouse();

            chessViewModel.OnPiecePressed(clickedPiece, name);
            PieceHandling(clickedPiece, name);
        }

        public void Piece_UnClick(object sender, MouseButtonEventArgs e)
        {
            Label clickedPiece = sender as Label;
            string name = $"{((Label)sender).Name}_false";


            position = e.GetPosition(_chessBoard);

            // mahdollistaa nappulan päästämisen ruudun ulkopuolella
            clickedPiece.ReleaseMouseCapture();

            _chessViewModel.OnPieceReleased(clickedPiece, name);
            PieceHandling(clickedPiece, name);
        }

        public void IfCursorOnPossibleMove(List<string> ableToMoves, List<string> ableToEat, string pieceLocation, List<string> ableToSwitch)
        {
            if (!mouseButtonDown)
            {
                foreach (var move in ableToMoves)
                {
                    // katsoo ruudun paikan johon laitetaan label
                    Border border = _chessBoard.FindName(move) as Border;

                    Point pos = border.TransformToAncestor(_chessBoard).Transform(new Point(0, 0));

                    if (position.X >= pos.X && position.X < pos.X + 50 && position.Y >= pos.Y && position.Y < pos.Y + 50)
                    {
                        MovePiece(pieceLocation, move);
                    }
                }

                foreach (var move in ableToEat)
                {
                    // katsoo ruudun paikan johon laitetaan label
                    Border border = _chessBoard.FindName(move) as Border;

                    Point pos = border.TransformToAncestor(_chessBoard).Transform(new Point(0, 0));

                    if (position.X >= pos.X && position.X < pos.X + 40 && position.Y >= pos.Y && position.Y < pos.Y + 40)
                    {
                        MovePiece(pieceLocation, move);
                    }
                }

                if (ableToSwitch != null)
                {
                    foreach (var move in ableToSwitch)
                    {
                        // katsoo ruudun paikan johon laitetaan label
                        Border border = _chessBoard.FindName(move) as Border;

                        Point pos = border.TransformToAncestor(_chessBoard).Transform(new Point(0, 0));

                        if (position.X >= pos.X && position.X < pos.X + 40 && position.Y >= pos.Y && position.Y < pos.Y + 40)
                        {
                            SwitchPlaces(pieceLocation, move);
                        }
                    }
                }
            }
        }

        public void SwitchPlaces(string fromLocation, string toLocation)
        {
            // valkoisen kuninkaan lyhyt tornitus 
            if (toLocation == "H1")
            {
                MovePieceOnBoard(fromLocation, "G1"); // kuningas
                MovePieceOnBoard("H1", "F1");         // torni
            }
            // valkoisen kuninkaan pitkä tornitus 
            else if (toLocation == "A1")
            {
                MovePieceOnBoard(fromLocation, "C1"); // kuningas
                MovePieceOnBoard("A1", "D1");         // torni
            }
            // mustan kuninkaan lyhyt tornitus 
            else if (toLocation == "H8")
            {
                MovePieceOnBoard(fromLocation, "G8"); // kuningas
                MovePieceOnBoard("H8", "F8");         // torni
            }
            // mustan kuninkaan pitkä tornitus 
            else if (toLocation == "A8")
            {
                MovePieceOnBoard(fromLocation, "C8"); // kuningas
                MovePieceOnBoard("A8", "D8");         // torni
            }
        }

        private void MovePieceOnBoard(string fromLocation, string toLocation)
        {
            Border oldBorder = _chessBoard.FindName(fromLocation) as Border;
            Border newBorder = _chessBoard.FindName(toLocation) as Border;

            if (oldBorder?.Child is not Label pieceLabel || newBorder == null)
                return;

            // Poistetaan vanhasta ruudusta
            oldBorder.Child = null;

            // Yritetään poistaa nimi rekisteristä jos se on olemassa
            try { _chessBoard.UnregisterName(pieceLabel.Name); } catch { }

            // Päivitetään nimi ja sijainti
            string[] parts = pieceLabel.Name.Split('_');
            pieceLabel.Name = $"{parts[0]}_{parts[1]}_{toLocation}";

            try { _chessBoard.RegisterName(pieceLabel.Name, pieceLabel); } catch { }

            // Asetetaan uusi ruutu
            newBorder.Child = pieceLabel;

            // Päivitetään nappulan sijainti listassa
            UpdatePieceLocation(fromLocation, toLocation);
        }

        private void UpdatePieceLocation(string fromLocation, string toLocation)
        {
            foreach (var item in whitePiecesInfo)
            {
                if (item.Location == fromLocation)
                {
                    item.Location = toLocation;
                }
            }

            foreach (var item in blackPiecesInfo)
            {
                if (item.Location == fromLocation)
                {
                    item.Location = toLocation;
                }
            }
        }

        public void MovePiece(string fromLocation, string toLocation)
        {
            Border oldBorder = _chessBoard.FindName(fromLocation) as Border;
            Border newBorder = _chessBoard.FindName(toLocation) as Border;


            if (oldBorder?.Child is Label pieceLabel)
            {
                // Poista vanhasta ruudusta
                oldBorder.Child = null;

                try
                {
                    // poistetaan vanha nimi rekisteristä
                    _chessBoard.UnregisterName(pieceLabel.Name);
                }
                catch { /* nimi ei ehkä ollut rekisteröity */ }

                // Päivitä nimi ja sijainti
                string[] parts = pieceLabel.Name.Split('_');
                pieceLabel.Name = $"{parts[0]}_{parts[1]}_{toLocation}";

                try
                {
                    // lisätään nimi rekisteriin
                    _chessBoard.RegisterName(pieceLabel.Name, pieceLabel);
                }
                catch { /* nimi oli ehkä rekisteröity jo */ }

                // Laita uuteen ruutuun
                newBorder.Child = pieceLabel;

                foreach (var item in whitePiecesInfo)
                {
                    if (item.Location == fromLocation)
                    {
                        item.Location = toLocation;
                    }
                }

                foreach (var item in blackPiecesInfo)
                {
                    if (item.Location == fromLocation)
                    {
                        item.Location = toLocation;
                    }
                }
            }
        }
    }
}
