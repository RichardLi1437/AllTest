using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest
{
    class NoReadWriteLock : IReadWriteLock
    {
        public void EnterRead() { }

        public void LeaveRead() { }

        public void EnterWrite() { }

        public void LeaveWrite() { }
    }

    class MonitorReadWriteLock : IReadWriteLock
    {
        private readonly object stateMonitor = new object();
        private Int32 state;
	    //private readonly AutoResetEvent 
	
	    public void EnterRead() {
            Monitor.Enter(stateMonitor);
			while (true) {
				if (state >= 0) {
					state++;
					break;
				} else {
					Monitor.Wait(stateMonitor);
				}
			}
            Monitor.Exit(stateMonitor);
	    }

	    public void LeaveRead() {
		    Monitor.Enter(stateMonitor);
			state--;
			if (state == 0) {
				Monitor.Pulse(stateMonitor);
			}
            Monitor.Exit(stateMonitor);
	    }

	    public void EnterWrite() {
		    Monitor.Enter(stateMonitor);
			while (true) {
				if (state == 0) {
					state = -1;
					break;
				} else {
					Monitor.Wait(stateMonitor);
				}
			}
            Monitor.Exit(stateMonitor);
	    }

	    public void LeaveWrite() {
		    Monitor.Enter(stateMonitor);
            state = 0;
		    Monitor.PulseAll(stateMonitor);
            Monitor.Exit(stateMonitor);
	    }

    }
}
