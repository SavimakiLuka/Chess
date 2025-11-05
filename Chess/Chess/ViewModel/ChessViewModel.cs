using Chess.Model;
using Chess.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chess.ViewModel
{
    public class ChessViewModel
    {
        private Logic _logic;

        internal ChessViewModel(Grid _chessBoard, List<Piece> blackPieces, List<Piece> whitePieces, Label clickedPiece)
        {
            _logic = new Logic(_chessBoard, blackPieces, whitePieces, clickedPiece);
        }

        public void OnPieceReleased(Label clickedPiece, string pieceName)
        {
            _logic.Piece_Clicked(clickedPiece, pieceName);
        }

        public void OnPiecePressed(Label clickedPiece, string pieceName)
        {
            _logic.Piece_Clicked(clickedPiece, pieceName);
        }
    }
}
