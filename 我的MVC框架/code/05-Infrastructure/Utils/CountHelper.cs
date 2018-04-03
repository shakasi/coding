using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Shaka.Utils
{
    public class CountHelper
    {
        long elapsedCount = 0;
        long startCount = 0;

        public void Start()
        {
            startCount = 0;
            QueryPerformanceCounter(ref startCount);
        }

        public void Stop()
        {
            long stopCount = 0;
            QueryPerformanceCounter(ref stopCount);

            //elapsedCount += (stopCount - startCount);
            elapsedCount = (stopCount - startCount);
        }

        public float Seconds
        {
            get
            {
                long freq = 0;
                QueryPerformanceFrequency(ref freq);
                return ((float)elapsedCount / (float)freq);
            }
        }

        public override string ToString()
        {
            return String.Format("{0} seconds", Seconds);
        }

        static long Frequency
        {
            get
            {
                long freq = 0;
                QueryPerformanceFrequency(ref freq);
                return freq;
            }
        }
        static long Value
        {
            get
            {
                long count = 0;
                QueryPerformanceCounter(ref count);
                return count;
            }
        }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency); 
    }
}
