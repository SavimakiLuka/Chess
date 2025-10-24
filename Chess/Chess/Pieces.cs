using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Chess
{
    class Pieces
    {
        string[] pieces = { "King", "Queen", "Bishop", "Rook", "Knight", "Pawn" };
        string[] colors = { "Black", "White" };
        string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H" };
        List<Piece> blackPieces = new List<Piece>();
        List<Piece> whitePieces = new List<Piece>();

        List<string> knightColumns = ["B", "G"];
        List<string> rookColumns = ["A", "H"];
        List<string> bishopColumns = ["C", "F"];
        List<string> kingColumns = ["D", "E"];
        List<string> queenColumns = ["D", "E"];

        public Pieces()
        {
            
            
        }
        

        public List<Piece> AddWhitePieces()
        {
            foreach (var piece in pieces)
            {
                    
                Piece pieceGet = new Piece
                {
                    color = colors[1],
                    name = piece,
                    Emoji = piece,
                };

                if (piece == "Pawn")
                {
                    for (int x = 0; x < 8; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[1],
                            name = piece,
                            Emoji = piece,
                            Location = $"{alphabet[x]}2"
                        };
                        whitePieces.Add(pieceGet);
                    }
                }
                else if (piece == "King")
                {
                    for (int x = 0; x < 1; x++)
                    {
                        pieceGet.Location = $"{kingColumns[0]}1";
                        whitePieces.Add(pieceGet);
                    }
                }
                else if (piece == "Queen")
                {
                    for (int x = 0; x < 1; x++)
                    {
                        pieceGet.Location = $"{queenColumns[1]}1";
                        whitePieces.Add(pieceGet);
                    }
                }
                else if ( piece == "Rook")
                {
                    for (int x = 0; x < 2; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[1],
                            name = piece,
                            Emoji = piece,
                            Location = $"{rookColumns[x]}1"
                        };
                        whitePieces.Add(pieceGet);
                    }
                }
                else if (piece == "Knight")
                {
                    for (int x = 0; x < 2; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[1],
                            name = piece,
                            Emoji = piece,
                            Location = $"{knightColumns[x]}1"
                        };
                        whitePieces.Add(pieceGet);
                    }
                }
                else if (piece == "Bishop")
                {
                    for (int x = 0; x < 2; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[1],
                            name = piece,
                            Emoji = piece,
                            Location = $"{bishopColumns[x]}1"
                        };
                        whitePieces.Add(pieceGet);
                    }
                }
            }
            return whitePieces;
        }

        public List<Piece> AddBlackPieces()
        {
            foreach (var piece in pieces)
            {

                Piece pieceGet = new Piece
                {
                    color = colors[0],
                    name = piece,
                    Emoji = piece,
                };

                if (piece == "Pawn")
                {
                    for (int x = 0; x < 8; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[0],
                            name = piece,
                            Emoji = piece,
                            Location = $"{alphabet[x]}7"
                        };
                        blackPieces.Add(pieceGet);
                    }
                }
                else if (piece == "King")
                {
                    for (int x = 0; x < 1; x++)
                    {
                        pieceGet.Location = $"{kingColumns[1]}8";
                        blackPieces.Add(pieceGet);
                    }
                }
                else if (piece == "Queen")
                {
                    for (int x = 0; x < 1; x++)
                    {
                         pieceGet.Location = $"{queenColumns[0]}8";
                        blackPieces.Add(pieceGet);
                    }
                }
                else if (piece == "Rook")
                {
                    for (int x = 0; x < 2; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[0],
                            name = piece,
                            Emoji = piece,
                            Location = $"{rookColumns[x]}8"
                        };
                        blackPieces.Add(pieceGet);
                    }
                }
                else if (piece == "Knight")
                {
                    for (int x = 0; x < 2; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[0],
                            name = piece,
                            Emoji = piece,
                            Location = $"{knightColumns[x]}8"
                        };
                        blackPieces.Add(pieceGet);
                    }
                }
                else if (piece == "Bishop")
                {
                    for (int x = 0; x < 2; x++)
                    {
                        pieceGet = new Piece
                        {
                            color = colors[0],
                            name = piece,
                            Emoji = piece,
                            Location = $"{bishopColumns[x]}8"
                        };
                        blackPieces.Add(pieceGet);
                    }
                }
            }
            return blackPieces;
        }
    }
}
