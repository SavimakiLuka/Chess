using Chess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chess.View
{
    public class Logic
    {
        private Label draggedLabel; 

        Point position = new Point();

        List<Piece> blackPiecesInfo = new();
        List<Piece> whitePiecesInfo = new();

        List<string> ableToMoves;
        List<string> ableToMove;
        List<string> ableToSwitch;

        public List<string> ableToEat {  get; set; }

        string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };

        bool mouseButtonDown = false;

        public Logic(Label pressedpiece)
        {
        }

        public List<string> KingRook_Movementint(int alphabetNum, int num, string color)
        {
            ableToSwitch = new List<string>();
            List<string> moves = new List<string>();

            if (color == "Black")
            {
                moves.Add($"{alphabet[alphabetNum + 3]}{num}");
                moves.Add($"{alphabet[alphabetNum - 4]}{num}");

                foreach (string move in moves)
                {
                    var blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move && l.name == "Rook");

                    if (blackMoveChanceHit)
                    {
                        ableToSwitch.Add(move);
                    }
                }
            }
            else
            {
                moves.Add($"{alphabet[alphabetNum + 3]}{num}");
                moves.Add($"{alphabet[alphabetNum - 4]}{num}");

                foreach (string move in moves)
                {
                    var whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move && l.name == "Rook");

                    if (whiteMoveChanceHit)
                    {
                        ableToSwitch.Add(move);
                    }
                }
            }

            return ableToSwitch;
        }

        public void Pawn_Eating(int alphabetNum, int num, string color)
        {
            if (color == "Black")
            {
                var con = true;
                for (int i = 1; i < 2 && con; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        string move = $"{alphabet[alphabetNum - 1]}{num - i}";

                        bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                        if (whiteMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }
                }

                con = true;
                for (int i = 1; i < 2 && con; i++)
                {
                    if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        string move = $"{alphabet[alphabetNum + 1]}{num - i}";

                        bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                        if (whiteMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }
                }
            }
            else
            {
                var con = true;
                for (int i = 1; i < 2 && con; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                    {
                        string move = $"{alphabet[alphabetNum - 1]}{num + i}";

                        bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);

                        if (blackMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }
                }

                con = true;
                for (int i = 1; i < 2 && con; i++)
                {
                    if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                    {
                        string move = $"{alphabet[alphabetNum + 1]}{num + i}";

                        bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);

                        if (blackMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }
                }
            }
        }

        public List<string> Pawn_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = int.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            if (color == "Black")
            {
                if (num == 7)
                {
                    var con = true;
                    for (int i = 1; i < 3 && con; i++)
                    {
                        string move = $"{alphabet[alphabetNum]}{num - i}";

                        bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                        if (!whiteMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }

                    Pawn_Eating(alphabetNum, num, color);
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
                        string move = $"{alphabet[alphabetNum]}{num - i}";

                        bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

                        if (!whiteMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }

                    Pawn_Eating(alphabetNum, num, color);
                    }
            }
            else
            {
                if (num == 2)
                {
                    var con = true;
                    for (int i = 1; i < 3 && con; i++)
                    {
                        string move = $"{alphabet[alphabetNum]}{num + i}";

                        bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);

                        if (!blackMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                        else
                        {
                            con = false;
                        }
                    }

                    Pawn_Eating(alphabetNum, num, color);
                }
                else if (num == 8)
                {
                    ableToMove.Clear();
                    ableToMove.Add("PawnChange");

                    return ableToMove;
                }
                else
                {
                    var con = true;
                    for (int i = 1; i < 2 && con; i++)
                    {
                        string move = $"{alphabet[alphabetNum]}{num + i}";

                        bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);

                        if (!blackMoveChanceHit)
                        {
                            con = GetPossibleEatingPiece(move, color, con);
                        }
                    }

                    Pawn_Eating(alphabetNum, num, color);
                }
            }

            return ableToMove;
        }

        public List<string> Bishop_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = int.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);


            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = $"{alphabet[alphabetNum - i]}{num - i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = $"{alphabet[alphabetNum + i]}{num - i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = $"{alphabet[alphabetNum - i]}{num + i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = $"{alphabet[alphabetNum + i]}{num + i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }

            return ableToMove;
        }

        public List<string> Rook_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = int.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            for (int i = 1; i < num + 1 & con; i++)
            {
                if (num - i <= 8 && num - i >= 1)
                {
                    string move = $"{alphabet[alphabetNum]}{num - i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - num + 1 & con; i++)
            {
                if (num + i <= 8 && num + i >= 1)
                {
                    string move = $"{alphabet[alphabetNum]}{num + i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 & con; i++)
            {
                if (alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = $"{alphabet[alphabetNum - i]}{num}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum & con; i++)
            {
                if (alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = $"{alphabet[alphabetNum + i]}{num}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }

            return ableToMove;
        }

        public List<string> Knight_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = int.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 1]}{num - 2}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num - 2 <= 8 && num - 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 1]}{num - 2}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 1]}{num + 2}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num + 2 <= 8 && num + 2 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 1]}{num + 2}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            //sivuille
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 2]}{num - 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 2 >= 0 && alphabetNum - 2 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 2]}{num + 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 2]}{num - 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 2 >= 0 && alphabetNum + 2 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 2]}{num + 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }

            return ableToMove;
        }

        public List<string> King_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = int.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 1]}{num + 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 1]}{num - 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 1]}{num + 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1 && alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 1]}{num - 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num + 1 <= 8 && num + 1 >= 1)
            {
                string move = $"{alphabet[alphabetNum]}{num + 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (num - 1 <= 8 && num - 1 >= 1)
            {
                string move = $"{alphabet[alphabetNum]}{num - 1}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (alphabetNum + 1 >= 0 && alphabetNum + 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum + 1]}{num}";

                con = GetPossibleEatingPiece(move, color, con);
            }
            con = true;
            if (alphabetNum - 1 >= 0 && alphabetNum - 1 <= 7)
            {
                string move = $"{alphabet[alphabetNum - 1]}{num}";

                con = GetPossibleEatingPiece(move, color, con);
            }

            ableToSwitch = KingRook_Movementint(alphabetNum, num, color);

            return ableToMove;
        }

        public List<string> Queen_Movement(string color, string pieceLocation)
        {
            int index = pieceLocation.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string chars = pieceLocation.Substring(0, index);
            int num = int.Parse(pieceLocation.Substring(index));

            int alphabetNum = Array.IndexOf(alphabet, chars);

            ableToMove = new List<string>();
            ableToEat = new List<string>();

            var con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = $"{alphabet[alphabetNum - i]}{num - i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num - i <= 8 && num - i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = $"{alphabet[alphabetNum + i]}{num - i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = $"{alphabet[alphabetNum - i]}{num + i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum && con; i++)
            {
                if (num + i <= 8 && num + i >= 1 && alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = $"{alphabet[alphabetNum + i]}{num + i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < num + 1 & con; i++)
            {
                if (num - i <= 8 && num - i >= 1)
                {
                    string move = $"{alphabet[alphabetNum]}{num - i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - num + 1 & con; i++)
            {
                if (num + i <= 8 && num + i >= 1)
                {
                    string move = $"{alphabet[alphabetNum]}{num + i}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < alphabetNum + 1 & con; i++)
            {
                if (alphabetNum - i >= 0 && alphabetNum - i <= 7)
                {
                    string move = $"{alphabet[alphabetNum - i]}{num}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }
            con = true;
            for (int i = 1; i < 8 - alphabetNum & con; i++)
            {
                if (alphabetNum + i >= 0 && alphabetNum + i <= 7)
                {
                    string move = $"{alphabet[alphabetNum + i]}{num}";

                    con = GetPossibleEatingPiece(move, color, con);
                }
            }

            return ableToMove;
        }

        public List<string> GetPossibleMovement(string color, string pieceLocation, string piece, List<Piece> whitePieces, List<Piece> blackPieces)
        {
            blackPiecesInfo = blackPieces;
            whitePiecesInfo = whitePieces;

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

        public List<string> GetPossibleEatingPiece()
        {
            return ableToEat;
        }

        public List<string> GetPossibleSwitchingKing()
        {
            return ableToSwitch;
        }

        public bool GetPossibleEatingPiece(string move, string color, bool con)
        {
            bool blackMoveChanceHit = blackPiecesInfo.Any(l => l.Location == move);
            bool whiteMoveChanceHit = whitePiecesInfo.Any(l => l.Location == move);

            if (!blackMoveChanceHit && !whiteMoveChanceHit)
            {
                ableToMove.Add(move);
            }
            else
            {
                if (color == "White" && blackMoveChanceHit || color == "Black" && whiteMoveChanceHit)
                {
                    ableToEat.Add($"{move}");
                }

                con = false;
            }

            return con;
        }
    }
}
