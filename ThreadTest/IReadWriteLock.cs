using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest
{
    public interface IReadWriteLock
    {
        void EnterRead();

        void EnterWrite();

        void LeaveRead();

        void LeaveWrite();
    }
}
