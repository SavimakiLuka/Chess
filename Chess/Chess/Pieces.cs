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
        List<Piece> whitePieces;
        List<Piece> blackPieces;

        public Pieces()
        {
            
            
        }
        

        public void AddPiece()
        {
            blackPieces = new List<Piece>();
            whitePieces = new List<Piece>();

            for (int i = 0; i < colors.Length; i++)
            {
                foreach (var piece in pieces)
                {
                    Piece pieceGet = new Piece
                    {
                        color = colors[i],
                        name = piece
                    };

                    if (piece == "Pawn")
                    {
                        for (int x = 0; x < 8; x++)
                        {
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
                    else
                    {
                        for (int x = 0; x < 2; x++)
                        {
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
