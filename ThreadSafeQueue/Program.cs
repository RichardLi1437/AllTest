using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace ThreadSafeQueue
{
    class Program
    {
        const int PRODUCER_COUNT = 100, CONSUMER_COUNT = 100, GENERATE_COUNT = 1000, FLOPS = 0;

        static void Main(string[] args)
        {
            #region init
            QueueTest[] queueTesters = new QueueTest[4];
            queueTesters[0] = new SyncQueueTest(PRODUCER_COUNT, CONSUMER_COUNT, GENERATE_COUNT, FLOPS);
            queueTesters[1] = new ConcurrentQueueTest(PRODUCER_COUNT, CONSUMER_COUNT, GENERATE_COUNT, FLOPS);
            queueTesters[2] = new SingleLockQueueTest(PRODUCER_COUNT, CONSUMER_COUNT, GENERATE_COUNT, FLOPS);
            queueTesters[3] = new TwoLockQueueTest(PRODUCER_COUNT, CONSUMER_COUNT, GENERATE_COUNT, FLOPS);
            #endregion

            #region testing
            DateTime start, end;
            TimeSpan ts;
            System.Console.WriteLine("time\t+/-\tn\tThread#");
            for (int i = 0; i < queueTesters.Length; i++)
            {
                //if (i == 1) continue;
                start = DateTime.Now;
                queueTesters[i].Test();
                end = DateTime.Now;
                ts = end - start;
                System.Console.WriteLine(queueTesters[i].Desc + ": " + ts.TotalMilliseconds);
            }
            #endregion
        }
    }

}
