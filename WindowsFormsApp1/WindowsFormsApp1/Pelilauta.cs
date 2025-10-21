using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Pelilauta : Form
    {
        
        public Label ruutu;
        public Button piece;

        //Timer
        int cellSize = 50;
        int timerSize = 80;
        int timerHight = 20;
        int timerWidht = 110;
        int remainingTime;
        
        //Gameboard
        int riviMaara = 8;
        int torniMaara = 8;
        public double time = 0;
        Peli peli = new Peli();
        Nappulat nappulat = new Nappulat();

        //Pieces
        int numberOfPieces;
        private int[] clickedPiece;
        static string gamepiece;

        private static Label[,] gridlabel;

        public static Label[,] gridLabel
        {
            get { return gridlabel; }
            set { gridlabel = value; }
        }

        

        public Pelilauta()
        {
            InitializeComponent();
            KentanLuonti();
        }

        private void KentanLuonti()
        {
            Ruudukko(this);
            Timer_CreateTimer_ShowTheTimer();
            PlaceThePieces();
        }
        public void Ruudukko(Form form)
        {
            int luku = 0;
            int yOffset = 35;

            gridlabel = new Label[riviMaara, torniMaara];

            for (int i = 0; i < riviMaara; i++)
            {
                for (int j = 0; j < torniMaara; j++)
                {
                    var label = new Label();
                    label.Size = new Size(cellSize, cellSize);
                    label.Location = new Point(j * cellSize, i * cellSize + yOffset);
                    label.Tag = new Point(i, j); //jotta pystyy painamaan
                    label.BorderStyle = BorderStyle.FixedSingle;

                    label.Text = luku.ToString();
                    label.Font = new Font("Arial", 1, FontStyle.Bold);
                    int intLabel = Convert.ToInt32(label.Text);

                    //Color placement
                    if (intLabel >= 8)
                    {
                        if (intLabel >= 16)
                        {
                            luku = 0;
                            intLabel = 0;
                        }
                        else
                        {
                            if (intLabel % 2 == 1)
                            {
                                label.BackColor = Color.White;
                                label.ForeColor = Color.White;
                            }
                            else
                            {
                                label.BackColor = Color.Black;
                                label.ForeColor = Color.Black;
                            }
                        }
                    }
                    else if (intLabel < 8)
                    {
                        if (intLabel % 2 == 0)
                        {
                            label.BackColor = Color.White;
                            label.ForeColor = Color.White;
                        }
                        else
                        {
                            label.BackColor = Color.Black;
                            label.ForeColor = Color.Black;
                        }
                    }

                    label.Click += new EventHandler(nappulat.Label_Click);

                    gridlabel[i, j] = label;
                    form.Controls.Add(label);
                    luku++;
                }
            }
        }
        private void Timer_CreateTimer_ShowTheTimer()
        {
                int leveys = 10;
                int korkeus = 10;
                ruutu = new Label();
                ruutu.Size = new Size(timerSize, timerHight);
                ruutu.Location = new Point(timerWidht, korkeus);
                ruutu.Font = new Font("Arial", 14, FontStyle.Bold);
                ruutu.TextAlign = ContentAlignment.MiddleCenter;
                ruutu.BackColor = Color.White;
                this.Controls.Add(ruutu);


            int remainingtime = Peli.remainingTime;

            ruutu.Text = Convert.ToString(remainingtime);

            timerWidht += 100;
            
        }
        public void Timer_TimerMovement(Timer timer)
        {

        }

        public void PlaceThePieces()
        {
            AddPieceToLabel(Nappulat.King());
            AddPieceToLabel(Nappulat.Queen());
            AddPieceToLabel(Nappulat.Bishop(), isSymmetric: true);
            AddPieceToLabel(Nappulat.Knight(), isSymmetric: true);
            AddPieceToLabel(Nappulat.Rook(), isSymmetric: true);
            AddPieceToLabel(Nappulat.Pawn(), isRow: true);
        }

        private void AddPieceToLabel(ChessPiece piece, bool isSymmetric = false, bool isRow = false)
        {
            if (isRow)
            {
                // Sotilaat riviin
                for (int i = 0; i < 8; i++)
                {
                    ChessPiece pawnPiece = Nappulat.Pawn();
                    pawnPiece.Y_Location = i;
                    UpdateLabelWithPiece(pawnPiece);
                }
            }
            else if (isSymmetric)
            {
                // Vasemmat
                UpdateLabelWithPiece(piece);
                piece.Y_Location = 7 - piece.Y_Location; // Symmetrinen sijainti oikealle puolelle
                // Oikeat
                UpdateLabelWithPiece(piece);
            }
            else
            {
                // Kuningas ja kuningatar
                UpdateLabelWithPiece(piece);
            }
        }

        private void UpdateLabelWithPiece(ChessPiece piece)
        {
            Label label = gridlabel[piece.X_Location, piece.Y_Location];
            label.Text = piece.Text;
            label.ForeColor = piece.PieceColor;
            label.Font = new Font("Arial", 28, FontStyle.Bold);
        }

        
    }
}
