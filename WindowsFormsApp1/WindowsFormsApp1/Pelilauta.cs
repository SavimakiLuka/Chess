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
        public string[,] ruudut;
        public int leveys = 10;
        public int korkeus = 60;
        public int rivit;
        public int sarakkeet;
        int ruudukon_varit = 0;
        int size = 50;
        int cellSize = 50;
        int vali = 10;
        int timerSize = 80;
        int timerHight = 20;
        int timerWidht = 110;
        bool a = false;
        int b = 0;
        int riviMaara = 8;
        int torniMaara = 8;
        public Label[,] gridLabel;
        public double time = 0;
        Peli peli = new Peli();
        public Pelilauta()
        {
            InitializeComponent();
            KentanLuonti();
        }

        private void KentanLuonti()
        {
            Ruudukko(this);
            Timer_CreateTimer_ShowTheTimer();
        }
        public void Ruudukko(Form form)
        {
            int luku = 0;
            int yOffset = 35;

            gridLabel = new Label[riviMaara, torniMaara];

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

                    gridLabel[i, j] = label;
                    form.Controls.Add(label);
                    luku++;
                }
            }
        }
        private void Timer_CreateTimer_ShowTheTimer()
        {
            for (int i = 0; i < 2; i++)
            {
                Timer Clock = new Timer();
                Clock.Interval = 100000;
                Clock.Start();
                Clock.Tick += new EventHandler(Timer_Tick);
                leveys = 10;
                korkeus = 10;
                ruutu = new Label();
                ruutu.Size = new Size(timerSize, timerHight);
                ruutu.Location = new Point(timerWidht, korkeus);
                ruutu.Font = new Font("Arial", 14, FontStyle.Bold);
                ruutu.TextAlign = ContentAlignment.MiddleCenter;
                ruutu.Text = Convert.ToString(Clock);
                ruutu.BackColor = Color.White;
                this.Controls.Add(ruutu);

                timerWidht += 100;
            }
        }
    }
}
