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
        public MainWindow()
        {
            InitializeComponent();
            Board board = new Board(ChessBoard);
            Pieces pieces = new Pieces();
            board.CreateBoard();
            whitePieces = pieces.AddWhitePieces();
            blackPieces = pieces.AddBlackPieces();
            board.AddPiecesToBoard(blackPieces, whitePieces);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}