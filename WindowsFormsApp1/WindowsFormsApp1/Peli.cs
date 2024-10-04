using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Peli
    {
        public double Timer(double time)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            time = timer.Interval = 100000;
            timer.Enabled = true;
            return time;
        }
    }
}
