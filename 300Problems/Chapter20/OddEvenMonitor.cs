using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    public class OddEvenMonitor
    {
        private bool isOdd = true;
        public System.Threading.ManualResetEvent mre = new System.Threading.ManualResetEvent(false);
        private object a = new object();

        private void WaitTurn(bool isOdd)
        {
            while(this.isOdd != isOdd)
            {
                mre.WaitOne(100);
            }
        }

        private void ToggleTurn()
        {
            lock(a)
            {
                this.isOdd = !this.isOdd;
                mre.Set();
            }
        }

        public static void OddThread(object o)
        {
            OddEvenMonitor oem = o as OddEvenMonitor;
            if (oem != null)
            {
                for (int i = 1; i <= 100; i += 2)
                {
                    oem.WaitTurn(true);
                    Console.WriteLine(i);
                    oem.ToggleTurn();
                }
            }
        }

        public static void EvenThread(object o)
        {
            OddEvenMonitor oem = o as OddEvenMonitor;
            if (oem != null)
            {
                for (int i = 2; i <= 100; i += 2)
                {
                    oem.WaitTurn(false);
                    Console.WriteLine(i);
                    oem.ToggleTurn();
                }
            }
        }
    }
}
