using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest
{
    public class SpinLock
    {
        private int isLock = 0;

        public void Lock()
        {
            while (true) {
                if (Interlocked.Exchange(ref isLock, 1) == 0)
                {
                    return;
                }
            }
        }

        public void Unlock()
        {
            Thread.VolatileWrite(ref isLock, 0);
        }
    }
}
