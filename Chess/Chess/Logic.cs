using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chess
{
    internal class Logic
    {
        private Grid chessBoard;
        private Label pressedPiece;
        string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };
        List<Piece> blackPiecesInfo = new();
        List<Piece> whitePiecesInfo = new();
        List<string> ableToMoves;
        List<string> ableToMove;
        List<string> ableToEat;

        Label draggedLabel = null;
        Point position = new Point();
        bool followMouse = false;
        public Point clickedPosition { get; set; }

        public Logic(Grid grid, List<Piece> blackPieces, List<Piece> whitePieces, Label pressedpiece)
        {
            chessBoard = grid;
            blackPiecesInfo = blackPieces;
            whitePiecesInfo = whitePieces;
            pressedPiece = pressedpiece;
        }

        public void Piece_Clicked(string clickedPiece, Label clickedLabel)
        {
            draggedLabel = clickedLabel;
            string color = clickedPiece.Split('_')[0];
            string piece = clickedPiece.Split('_')[1];
            string pieceLocation = clickedPiece.Split('_')[2];
            string unClickCheck = "";
            bool unClick = false;
            ableToMoves = new List<string>();
            ableToEat = new List<string>();

            unClickCheck = clickedPiece.Split('_')[3];

            if (unClickCheck == "true")
            {
                unClick = true;
            }
            else
            {
                unClick = false;
            }

            chessBoard.MouseMove += ChessBoard_MouseMove;

            PieceOpacity(unClick);
            MouseMoveVisual(unClick, clickedLabel);
            PressedPiece(color, pieceLocation, piece);
            MoveChangeVisualCreate(color, unClick);
            IfCursorOnLabel(unClick);
        }

        public List<string> Pawn_Movement(string color, string pieceLocation)
        {
            int possibleLocations = 0;


            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            chessBoard.MouseMove += ChessBoard_MouseMove;

            if (color == "Black")
            {
                if (num == 7)
                {
                    var con = true;
                    for (int i = 1; i < 3 && con; i++)
                    {
                        string move = ($"{alphabet[alphabetNum]}{num - i}");

                        con = MoveChangeListAdding(move, color, con);
                    }
                }
                else if (num == 1)
                {
                    MessageBox.Show("Pääsit päätyyn onneksi olkoon!");
                }
                else
                {
                    var con = true;
                    for (int i = 1; i < 2 && con; i++)
                    {
                        string move = ($"{alphabet[alphabetNum]}{num - i}");

                        con = MoveChangeListAdding(move, color, con);
                    }

                }
            }
            else
            {
                if (num == 2)
                {
                    var con = true;
                    for (int i = 1; i < 3 && con; i++)
                    {
                        string move = ($"{alphabet[alphabetNum]}{num + i}");

                        con = MoveChangeListAdding(move, color, con);
                    }
                }
                else if (num == 8)
                {
                    MessageBox.Show("Pääsit päätyyn onneksi olkoon!");
                }
                else
                {
                    var con = true;
                    for (int i = 1; i < 2 && con; i++)
                    {
                        string move = ($"{alphabet[alphabetNum]}{num + i}");

                        con = MoveChangeListAdding(move, color, con);
                    }
                }
            }

            return ableToMove;
        }

        public List<string> Bishop_Movement(string color, string pieceLocation)
        {
            int possibleLocations = 0;


            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);


            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum - i]}{num - i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum + i]}{num - i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum - i]}{num + i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum + i]}{num + i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }

            return ableToMove;
        }

        public List<string> Rook_Movement(string color, string pieceLocation)
        {
            int possibleLocations = 0;

            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            for (int i = 1; i < num + 1 & con; i++)
            {
                if (num - i <= 8 && num - i >= 1)
                {
                    string move = ($"{alphabet[alphabetNum]}{num - i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - num + 1 & con; i++)
            {
                if (num + i <= 8 && num + i >= 1)
                {
                    string move = ($"{alphabet[alphabetNum]}{num + i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 & con; i++)
            {
                if (alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum - i]}{num}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum & con; i++)
            {
                if (alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum + i]}{num}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }

            return ableToMove;
        }

        public List<string> Knight_Movement(string color, string pieceLocation) 
        {
            int possibleLocations = 0;


            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 1]}{num - 2}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 1]}{num - 2}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 1]}{num + 2}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 1]}{num + 2}");

                con = MoveChangeListAdding(move, color, con);
            }
            //sivuille
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 2]}{num - 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 2]}{num + 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 2]}{num - 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 2]}{num + 1}");

                con = MoveChangeListAdding(move, color, con);
            }

            return ableToMove;
        }

        public List<string> King_Movement(string color, string pieceLocation)
        {
            int possibleLocations = 0;


            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 1]}{num + 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 1]}{num - 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 1]}{num + 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 1]}{num - 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1)
            {
                string move = ($"{alphabet[alphabetNum]}{num + 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1)
            {
                string move = ($"{alphabet[alphabetNum]}{num - 1}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum + 1]}{num}");

                con = MoveChangeListAdding(move, color, con);
            }
            con = true;
            if (alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = ($"{alphabet[alphabetNum - 1]}{num}");

                con = MoveChangeListAdding(move, color, con);
            }

            return ableToMove;
        }

        public List<string> Queen_Movement(string color, string pieceLocation)
        {
            int possibleLocations = 0;

            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum - i]}{num - i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum + i]}{num - i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum - i]}{num + i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum + i]}{num + i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < num + 1 & con; i++)
            {
                if (num - i <= 8 && num - i >= 1)
                {
                    string move = ($"{alphabet[alphabetNum]}{num - i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - num + 1 & con; i++)
            {
                if (num + i <= 8 && num + i >= 1)
                {
                    string move = ($"{alphabet[alphabetNum]}{num + i}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 & con; i++)
            {
                if (alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum - i]}{num}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum & con; i++)
            {
                if (alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = ($"{alphabet[alphabetNum + i]}{num}");

                    con = MoveChangeListAdding(move, color, con);
                }
            }

            return ableToMove;
        }

        public List<string> PressedPiece(string color, string pieceLocation, string piece)
        {
            // Katsotaan mikä nappula oli painettu juuri olike se sotilas vai kuningas vai mikä
            if (piece == "Pawn")
            {
                ableToMoves = Pawn_Movement(color, pieceLocation);
            }
            else if (piece == "Bishop")
            {
                ableToMoves = Bishop_Movement(color, pieceLocation);
            }
            else if (piece == "Rook")
            {
                ableToMoves = Rook_Movement(color, pieceLocation);
            }
            else if (piece == "Knight")
            {
                ableToMoves = Knight_Movement(color, pieceLocation);
            }
            else if (piece == "King")
            {
                ableToMoves = King_Movement(color, pieceLocation);
            }
            else if (piece == "Queen")
            {
                ableToMoves = Queen_Movement(color, pieceLocation);
            }
            return ableToMoves;
        }

        public void MoveChangeVisualCreate(string color, bool unClick)
        {
            foreach (var move in ableToMoves)
            {
                // katsoo ruudun paikan johon laitetaan label
                Border border = chessBoard.FindName(move) as Border;

                // katsoo onko ruudun paikalla valmiiksi jo joku nappula
                bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
                bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                Label label = new Label();

                if (unClick) // poistetaan liikkumis mahdollisuus pallo
                {
                    label.Content = "";

                    border.Child = label;
                }
                else // lisätään liikkumis mahdollisuus pallo
                {
                    label.FontSize = 30;
                    label.Content = "●";
                    label.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f7f7f"));
                    label.Opacity = 0.5;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.VerticalAlignment = VerticalAlignment.Center;

                    border.Child = label;
                }
            }

            foreach (var piece in ableToEat)
            {
                Border border1 = chessBoard.FindName(piece) as Border;

                if (color == "White")
                {
                    if (border1.Child is Label existingLabel)
                    {
                        if (unClick) // poistetaan liikkumis mahdollisuus pallo
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
                        if (unClick) // poistetaan liikkumis mahdollisuus pallo
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
    
        public bool MoveChangeListAdding(string move, string color, bool con)
        {
            bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
            bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

            if (!blackMoveChanceHit && !whiteMoveChanceHit)
            {
                ableToMove.Add(move);
            }
            else
            {
                if ((color == "White" && blackMoveChanceHit) || (color == "Black" && whiteMoveChanceHit))
                {
                    ableToEat.Add($"{move}");
                }

                con = false;
            }

            return con;
        }

        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedLabel != null && followMouse)
            {
                Point position = e.GetPosition(chessBoard);

                Canvas.SetLeft(pressedPiece, position.X - 25);
                Canvas.SetTop(pressedPiece, position.Y - 30);
            }
        }

        private void PieceOpacity(bool unClick)
        {

            if (unClick) 
            {
                draggedLabel.Opacity = 1;
                followMouse = false;
            }
            else 
            {
                draggedLabel.Opacity = 0.3;
                followMouse = true;
            }
        }

        private void MouseMoveVisual(bool unClick, Label clickedLabel)
        {
            clickedLabel.MouseLeftButtonUp += Piece_UnClick;
            if (!unClick)
            {
                pressedPiece.Foreground = clickedLabel.Foreground;
                pressedPiece.Effect = clickedLabel.Effect;
                pressedPiece.Content = clickedLabel.Content;
            }
            else
            {
                pressedPiece.Content = "";
            }         
        }

        public void IfCursorOnLabel(bool unClick)
        {
            if (unClick)
            {
                foreach (var move in ableToMoves)
                {
                    // katsoo ruudun paikan johon laitetaan label
                    Border border = chessBoard.FindName(move) as Border;

                    Point pos = border.TransformToAncestor(chessBoard).Transform(new Point(0, 0));

                    // katsoo onko ruudun paikalla valmiiksi jo joku nappula
                    bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
                    bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                    Label label = new Label();

                    label.FontSize = pressedPiece.FontSize;
                    label.Content = pressedPiece.Content;
                    label.Foreground = pressedPiece.Foreground;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.VerticalAlignment = VerticalAlignment.Center;

                    border.Child = label;
                }
            }
        }

        private void CursorChange(string cursorType)
        {
            if (cursorType == "Hand")
            {
                
            }
        }

        public void Piece_UnClick(object sender, MouseButtonEventArgs e)
        {
            string name = ((Label)sender).Name;
            Label clickedLabel = sender as Label;
            /*IfCursorOnLabel(unClick);*/
        }
    }
}
