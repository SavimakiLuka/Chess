using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp1
{
    public class Peli : Form
    {
        int seconds = 0;
        private static int remainingtime = 60;
        bool updateTimer = false;

        public static int remainingTime
        {
            get { return remainingtime; }
            set { remainingTime = value; }
        }

        public void UpdateTimer()
        {
            if (updateTimer)
            {
                while (updateTimer)
                {
                    Thread.Sleep(1000);
                    remainingtime--;
                }
            }
        }

        public void Turns()
        {
            // if (mustatlista muuttuu ja mustatsta listatsta ei lähde mitään)
        }


        public static ClickedChessPiece Piecelocations()
        {
            return new ClickedChessPiece("King", 6, 0);
        }

        List<ClickedChessPiece> PieceLocations = new List<ClickedChessPiece>();

        public void PieceLocationsUpdate()
        {
            if (PieceLocations.Any())
            {
                Console.WriteLine("PieceLocations sisältää ainakin yhden nappulan.");
            }
            else
            {
                Console.WriteLine("PieceLocations on tyhjä.");
            }
        }

        public void AddPiece()
        {
            PieceLocations.Add(new ClickedChessPiece("King", 6, 0));
        }

    }
}
