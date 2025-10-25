using System;
using System.Collections.Generic;
using System.Linq;
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
        string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };
        List<Piece> blackPiecesInfo = new();
        List<Piece> whitePiecesInfo = new();

        public Logic(Grid grid, List<Piece> blackPieces, List<Piece> whitePieces)
        {
            chessBoard = grid;
            blackPiecesInfo = blackPieces;
            whitePiecesInfo = whitePieces;
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

            List<string> ableToMoves = new List<string>();
            string ableToMove = "";

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

            foreach (var move in ableToMoves)
            {
                // katsoo ruudun paikan johon laitetaan label
                Border border = chessBoard.FindName(move) as Border;

                // katsoo onko ruudun paikalla valmiiksi jo joku nappula
                bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
                bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                Label label = new Label();

                // Jos ruudussa ei ole nappulaa niin luodaan liikkumis mahdollisuus pallo laudalle
                if (blackMoveChanceHit == false && whiteMoveChanceHit == false)
                {
                    

                    if (remove == "poisto") // poistetaan liikkumis mahdollisuus pallo
                    {
                        label.Content = "";

                        border.Child = label;
                    }
                    else // lisätään liikkumis mahdollisuus pallo
                    {
                        label.FontSize = 30;
                        label.Content = "●";
                        label.Foreground = Brushes.Green;
                        label.HorizontalAlignment = HorizontalAlignment.Center;
                        label.VerticalAlignment = VerticalAlignment.Center;

                        border.Child = label;
                    }
                }
                else
                {
                    label.FontSize = 30;
                    label.Content = "●";
                    label.Foreground = Brushes.Red;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.VerticalAlignment = VerticalAlignment.Center;

                    if (color == "Black" && blackMoveChanceHit == false) // katsoo jos painetun nappulan väri on musta ja ettei ruudulla ole mustaa nappulaa
                    {
                        border.Child = label;
                    }
                    else if (color == "White" && whiteMoveChanceHit == false) // katsoo jos painetun nappulan väri on valkoinen ja ettei ruudulla ole valkoista nappulaa
                    {
                        border.Child = label;
                    }
                }
            }
        }

        public List<string> Pawn_Movement(string color, string pieceLocation)
        {
            int possibleLocations = 0;


            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = Int32.Parse(pieceLocation.Substring(index));

            List<string> ableToMove = new List<string>();

            if (color == "Black")
            {
                if (num == 7)
                {
                    possibleLocations = 2;
                    ableToMove.Add($"{chars}{num - 2}");
                    ableToMove.Add($"{chars}{num - 1}");

                }
                else if (num == 1)
                {
                    MessageBox.Show("Pääsit päätyyn onneksi olkoon!");
                }
                else
                {
                    possibleLocations = 1;
                    ableToMove.Add($"{chars}{num - 1}");
                }
            }
            else
            {
                if (num == 2)
                {
                    possibleLocations = 2; // vois koittaa ehkä laskee jotenkin
                    ableToMove.Add($"{chars}{num + 2}");
                    ableToMove.Add($"{chars}{num + 1}");
                }
                else if (num == 8)
                {
                    MessageBox.Show("Pääsit päätyyn onneksi olkoon!");
                }
                else
                {
                    ableToMove.Add($"{chars}{num + 1}");
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

            List<string> ableToMove = new List<string>();

            if (color == "Black")
            {
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num - i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num + i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num + i}");
                    }
                }
            }
            else
            {
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num - i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num + i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num + i}");
                    }
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

            List<string> ableToMove = new List<string>();

            if (color == "Black")
            {
                for (int i = 1; i < num + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - num + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num + i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num}");
                    }
                }
            }
            else
            {
                for (int i = 1; i < num + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - num + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num + i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num}");
                    }
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

            List<string> ableToMove = new List<string>();

            if (color == "Black")
            {
                if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num - 2}");
                }

                if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num - 2}");
                }

                if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num + 2}");
                }

                if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num + 2}");
                }
                //sivuille
                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 2]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 2]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 2]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 2]}{num + 1}");
                }
            }
            else
            {
                if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num - 2}");
                }

                if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num - 2}");
                }

                if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num + 2}");
                }

                if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num + 2}");
                }
                //sivuille
                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 2]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 2]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 2]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 2]}{num + 1}");
                }
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

            List<string> ableToMove = new List<string>();

            if (color == "Black")
            {
                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1)
                {
                    ableToMove.Add($"{alphabet[alphabetNum]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1)
                {
                    ableToMove.Add($"{alphabet[alphabetNum]}{num - 1}");
                }

                if (alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num}");
                }

                if (alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num}");
                }
            }
            else
            {
                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num - 1}");
                }

                if (num + 1 <= 8 && num + 1 >= 1)
                {
                    ableToMove.Add($"{alphabet[alphabetNum]}{num + 1}");
                }

                if (num - 1 <= 8 && num - 1 >= 1)
                {
                    ableToMove.Add($"{alphabet[alphabetNum]}{num - 1}");
                }

                if (alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum + 1]}{num}");
                }

                if (alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                {
                    ableToMove.Add($"{alphabet[alphabetNum - 1]}{num }");
                }
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

            List<string> ableToMove = new List<string>();

            if (color == "Black")
            {
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num - i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num + i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num + i}");
                    }
                }
                for (int i = 1; i < num + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - num + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num + i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num}");
                    }
                }
            }
            else
            {
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num - i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num + i}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num + i}");
                    }
                }
                for (int i = 1; i < num + 1; i++)
                {
                    if (num - i <= 8 && num - i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num - i}");
                    }
                }
                for (int i = 1; i < 8 - num + 1; i++)
                {
                    if (num + i <= 8 && num + i >= 1)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum]}{num + i}");
                    }
                }
                for (int i = 1; i < alphabetNum + 1; i++)
                {
                    if (alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum - i]}{num}");
                    }
                }
                for (int i = 1; i < 8 - alphabetNum; i++)
                {
                    if (alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
                    {
                        ableToMove.Add($"{alphabet[alphabetNum + i]}{num}");
                    }
                }
            }

            return ableToMove;
        }

        /*private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            // Hae hiiren sijainti suhteessa lautaan
            System.Windows.Point pos = e.GetPosition(chessBoard);

            // Siirrä labelin paikkaa hiiren mukaan
            Canvas.SetLeft(followLabel, pos.X - followLabel.ActualWidth / 2);
            Canvas.SetTop(followLabel, pos.Y - followLabel.ActualHeight / 2);
        }*/
    }
}
