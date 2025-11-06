using Chess.Model;
using Chess.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chess.ViewModel
{
    public class ChessViewModel
    {
        private Logic _logic;

        internal ChessViewModel(List<Piece> blackPieces, List<Piece> whitePieces, Label clickedPiece)
        {
            _logic = new Logic(blackPieces, whitePieces, clickedPiece);
        }

        public void OnPieceReleased(Label clickedPiece, string pieceName)
        {
            
        }

        public void OnPiecePressed(Label clickedPiece, string pieceName)
        {
            
        }

        public List<String> ReturnPossibleMovement(string color, string pieceLocation, string piece)
        {
            List<String> result = new List<String>();
            result = _logic.GetPossibleMovement(color, pieceLocation, piece);

            return result;
        }

/*        public List<String> ReturnPossibleEatingPieces(string color, string pieceLocation, string piece)
        {
            List<String> result = new List<String>();
            result = _logic.GetPossibleEatingPiece(pieceLocation, color, piece);

            return result;
        }*/

    }
}
