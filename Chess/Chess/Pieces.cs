using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess
{
    class Pieces
    {
        string[] pieces = { "King", "Queen", "Bishop", "Rook", "Knight", "Pawn" };
        string[] colors = { "Black", "White" };
        string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };
        List<Piece> whitePieces;
        List<Piece> blackPieces;

        List<string> knightColumns = ["B", "G"];
        List<string> rookColumns = ["A", "H"];
        List<string> bishopColumns = ["C", "F"];
        List<string> kingColumns = ["D", "E"];
        List<string> queenColumns = ["D", "E"];

        public Pieces()
        {
            
            
        }
        

        public void AddPiece()
        {
            blackPieces = new List<Piece>();
            whitePieces = new List<Piece>();

            var kingqueen = 0;

            for (int i = 0; i < colors.Length; i++)
            {
                foreach (var piece in pieces)
                {
                    
                    Piece pieceGet = new Piece
                    {
                        color = colors[i],
                        name = piece,
                        Emoji = piece,
                    };

                    if (piece == "Pawn")
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            pieceGet.Location = $"{alphabet[x]}2";
                            if (i == 0)
                            {
                                blackPieces.Add(pieceGet);
                            }
                            else
                            {
                                whitePieces.Add(pieceGet);
                            }
                        }
                    }
                    else if (piece == "King" || piece == "Queen")
                    {
                        for (int x = 0; x < 1; x++)
                        {
                            if (kingqueen == 0)
                            {
                                pieceGet.Location = $"{kingqueenColumns[x]}1";
                                if (i == 0)
                                {
                                    blackPieces.Add(pieceGet);
                                }
                                else
                                {
                                    whitePieces.Add(pieceGet);
                                }
                            }
                            else
                            {
                                pieceGet.Location = $"{kingqueenColumns[1 - i]}1";
                                if (i == 0)
                                {
                                    blackPieces.Add(pieceGet);
                                }
                                else
                                {
                                    whitePieces.Add(pieceGet);
                                }
                            }
                        }
                    }
                    else if (piece == "King")
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            pieceGet.Location = $"{rookColumns[x]}1";
                            if (i == 0)
                            {
                                blackPieces.Add(pieceGet);
                            }
                            else
                            {
                                whitePieces.Add(pieceGet);
                            }
                        }
                    }
                    else if (piece == "Queen")
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            pieceGet.Location = $"{rookColumns[x]}1";
                            if (i == 0)
                            {
                                blackPieces.Add(pieceGet);
                            }
                            else
                            {
                                whitePieces.Add(pieceGet);
                            }
                        }
                    }
                    else if ( piece == "Rook")
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            pieceGet.Location = $"{rookColumns[x]}1";
                            if (i == 0)
                            {
                                blackPieces.Add(pieceGet);
                            }
                            else
                            {
                                whitePieces.Add(pieceGet);
                            }
                        }
                    }
                    else if (piece == "Knight")
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            pieceGet.Location = $"{knightColumns[x]}1";
                            if (i == 0)
                            {
                                blackPieces.Add(pieceGet);
                            }
                            else
                            {
                                whitePieces.Add(pieceGet);
                            }
                        }
                    }
                    else if (piece == "Bishop")
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            pieceGet.Location = $"{bishopColumns[x]}1";
                            if (i == 0)
                            {
                                blackPieces.Add(pieceGet);
                            }
                            else
                            {
                                whitePieces.Add(pieceGet);
                            }
                        }
                    }
                }
            }
        }
    }
}
