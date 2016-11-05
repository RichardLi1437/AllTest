using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest
{
    class ReadWriteTest
    {
        private int readCount, writeCount;
        private int[] data;
        private IReadWriteLock readWriteLock;
        const int READ_TIMES = 1000;
        const int WRITE_TIMES = 2000;

        public ReadWriteTest(int readCount, int writeCount, IReadWriteLock readWriteLock)
        {
            this.readCount = readCount;
            this.writeCount = writeCount;
            this.readWriteLock = readWriteLock;
            data = new int[10];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
        }

        public void Run()
        {
            for (int i = 0; i < writeCount; i++)
            {
                new Thread(Write).Start(i);
            }
            for (int i = 0; i < readCount; i++)
            {
                new Thread(Read).Start(i);
            }
        }

        private void Read(object o)
        {
            int id = (int)o;
            int[] rData = new int[10];
            for (int i = 0; i < READ_TIMES; i++)
            {
                readWriteLock.EnterRead();
                Array.Copy(data, rData, 10);
                readWriteLock.LeaveRead();
                PrintData(string.Format("read {0}: ", id), rData);
            }
        }

        private void Write(object o)
        {
            int id = (int)o;
            int[] wData = new int[10];
            for (int i = 0; i < wData.Length; i++)
            {
                wData[i] = id;
            }
            for (int i = 0; i < WRITE_TIMES; i++)
            {
                readWriteLock.EnterWrite();
                Array.Copy(wData, data, 10);
                readWriteLock.LeaveWrite();
                PrintData(string.Format("write {0}: ", id), wData);
            }
        }

        private void PrintData(string prefix, int[] data)
        {
            System.Console.Write(prefix + ": ");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write("{0}\t", data[i]);
            }
            Console.WriteLine();
        }
    }
}
