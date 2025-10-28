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

        Logic logic;

        public MainWindow()
        {
            InitializeComponent();

            // Luo lauta ja nappulat
            Board board = new Board(ChessBoard);
            Pieces pieces = new Pieces();
            logic = new Logic(ChessBoard, blackPieces, whitePieces);

            board.CreateBoard();
            whitePieces = pieces.AddWhitePieces();
            blackPieces = pieces.AddBlackPieces();
            board.AddPiecesToBoard(blackPieces, whitePieces);


        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            // Haetaan hiiren sijainti päägridin koordinaateissa
            Point position = e.GetPosition(ChessBoard);
            /*
            // Päivitetään ellipsin paikka keskitetysti hiiren ympärille
            ok.Margin = new Thickness(
                position.X - ok.Width / 2,
                position.Y - ok.Height / 2,
                0, 0);*/
            if (logic == null) return;

            logic.position = position;
            logic.UpdateDraggedPiecePosition();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}