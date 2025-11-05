using Chess.Model;
using Chess.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Piece> blackPieces = new List<Piece>();
        List<Piece> whitePieces = new List<Piece>();

        Point position = new Point();

        public MainWindow()
        {
            InitializeComponent();

            int boardSize = 400;
            int fontSize = boardSize / 10;

            // Luo lauta ja nappulat
            Board board = new Board(ChessBoard, pressedPiece);
            Pieces pieces = new Pieces();
            UIBuilder uIBuilder = new UIBuilder(ChessBoard, pressedPiece);

            uIBuilder.DrawBoard(boardSize);
            whitePieces = pieces.AddWhitePieces();
            blackPieces = pieces.AddBlackPieces();
            uIBuilder.DrawPieces(blackPieces, whitePieces, fontSize);


        }
    }
}