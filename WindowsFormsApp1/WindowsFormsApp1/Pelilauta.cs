using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Pelilauta : Form
    {
        
        public Label ruutu;

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
        Nappulat Nappulat = new Nappulat();

        //Pieces
        int numberOfPieces;

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
            var label = new Label();

            //Black
            //King
            ChessPiece blackKingPiece = Nappulat.King();

            label = gridlabel[blackKingPiece.X_Location, blackKingPiece.Y_Location];
            label.ForeColor = blackKingPiece.PieceColor;
            label.Text = blackKingPiece.Text;
            label.Font = new Font("Arial", 30, FontStyle.Bold);

            this.Controls.Add(label);

            //Queen
            ChessPiece blackQueenPiece = Nappulat.Queen();

            label = gridlabel[blackQueenPiece.X_Location, blackQueenPiece.Y_Location];
            label.ForeColor = blackQueenPiece.PieceColor;
            label.Text = blackQueenPiece.Text;
            label.Font = new Font("Arial", 30, FontStyle.Bold);

            this.Controls.Add(label);

            //Bishop
            ChessPiece blackBishopPiece = Nappulat.Bishop();

            numberOfPieces = 2;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackBishopPiece.X_Location, numberOfPieces];
                label.ForeColor = blackBishopPiece.PieceColor;
                label.Text = blackBishopPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces = numberOfPieces + 3;
            }

            //Knight
            ChessPiece blackKnightPiece = Nappulat.Knight();

            numberOfPieces = 1;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackKnightPiece.X_Location, numberOfPieces];
                label.ForeColor = blackKnightPiece.PieceColor;
                label.Text = blackKnightPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces = numberOfPieces + 5;
            }

            //Rook
            ChessPiece blackRookPiece = Nappulat.Rook();

            numberOfPieces = 0;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackRookPiece.X_Location, numberOfPieces];
                label.ForeColor = blackRookPiece.PieceColor;
                label.Text = blackRookPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces = numberOfPieces + 7;
            }

            //Pawn
            ChessPiece blackPawnPiece = Nappulat.Pawn();

            numberOfPieces = 0;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackPawnPiece.X_Location, numberOfPieces];
                label.ForeColor = blackPawnPiece.PieceColor;
                label.Text = blackPawnPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces++;
            }

            //White
            //King
            ChessPiece whiteKingPiece = Nappulat.King();

            label = gridlabel[blackKingPiece.X_Location, blackKingPiece.Y_Location];
            label.ForeColor = blackKingPiece.PieceColor;
            label.Text = blackKingPiece.Text;
            label.Font = new Font("Arial", 30, FontStyle.Bold);

            this.Controls.Add(label);

            //Queen
            ChessPiece whiteQueenPiece = Nappulat.Queen();

            label = gridlabel[blackQueenPiece.X_Location, blackQueenPiece.Y_Location];
            label.ForeColor = blackQueenPiece.PieceColor;
            label.Text = blackQueenPiece.Text;
            label.Font = new Font("Arial", 30, FontStyle.Bold);

            this.Controls.Add(label);

            //Bishop
            ChessPiece whiteBishopPiece = Nappulat.Bishop();

            numberOfPieces = 2;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackBishopPiece.X_Location, numberOfPieces];
                label.ForeColor = blackBishopPiece.PieceColor;
                label.Text = blackBishopPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces = numberOfPieces + 3;
            }

            //Knight
            ChessPiece whiteKnightPiece = Nappulat.Knight();

            numberOfPieces = 1;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackKnightPiece.X_Location, numberOfPieces];
                label.ForeColor = blackKnightPiece.PieceColor;
                label.Text = blackKnightPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces = numberOfPieces + 5;
            }

            //Rook
            ChessPiece whiteRookPiece = Nappulat.Rook();

            numberOfPieces = 0;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackRookPiece.X_Location, numberOfPieces];
                label.ForeColor = blackRookPiece.PieceColor;
                label.Text = blackRookPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces = numberOfPieces + 7;
            }

            //Pawn
            ChessPiece whitePawnPiece = Nappulat.Pawn();

            numberOfPieces = 0;

            while (numberOfPieces < 8)
            {
                label = gridlabel[blackPawnPiece.X_Location, numberOfPieces];
                label.ForeColor = blackPawnPiece.PieceColor;
                label.Text = blackPawnPiece.Text;
                label.Font = new Font("Arial", 30, FontStyle.Bold);

                this.Controls.Add(label);
                numberOfPieces++;
            }
        }


    }
}
