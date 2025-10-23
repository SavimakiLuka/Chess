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
        string[] pieces = { "King", "Queen", "Pawn", "Bishop", "Rook", "Knight" };
        string[] colors = { "Black"/*, "White" */};
        List<Piece> allPieces;

        public Pieces()
        {
            
            
        }
        

        public void AddPiece()
        {
            allPieces = new List<Piece>();
            Piece pieceGet = new Piece();
            
            
            foreach (var color in colors)
            {
                pieceGet.color = color;
                foreach (var piece in pieces)
                {
                    pieceGet.name = piece;
                    allPieces.Add();
                }
            }
            foreach (var piece in allPieces)
            {
                MessageBox.Show($"{piece.Key} : {piece.Value}");
            }
        }
    }
}
