using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MessageQueueUtils
{
    public class CountHelper
    {
        // 开始时间
        private long _startTime = 0;
        // 结束花费时间
        private long _elapsedCount = 0;

        public void Start()
        {
            _startTime = 0;
            QueryPerformanceCounter(ref _startTime);
        }

        public void Stop()
        {
            long stopTime = 0;
            QueryPerformanceCounter(ref stopTime);
            _elapsedCount = (stopTime - _startTime);
        }

        /// <summary>
        /// 开始到结束，花费的时间，单位秒
        /// </summary>
        public float Seconds
        {
            get
            {
                long freq = 0;
                QueryPerformanceFrequency(ref freq);
                return ((float)_elapsedCount / (float)freq);
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