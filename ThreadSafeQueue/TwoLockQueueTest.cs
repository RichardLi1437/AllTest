using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSafeQueue
{
    public class TwoLockQueueTest : QueueTest
    {
        private TwoLockQueue<DateTime> q;

        public TwoLockQueueTest(int producerCount, int consumerCount, int generateCount, int flops) :  
            base("TwoLockQueue Test", producerCount, consumerCount, generateCount, flops)
        {
            q = new TwoLockQueue<DateTime>();
        }

        protected override void Consume()
        {
            DateTime n;
            while (true)
            {
                if (q.Dequeue(out n))
                {
                    DoDummyWork();
                    Log('-', n);
                }
                else
                {
                    if (remainingProducer == 0) break;
                    Log('-', DateTime.MinValue);
                    Thread.Sleep(0);
                }
            }
        }

        protected override void Produce()
        {
            for (int i = 0; i < generateCount; i++)
            {
                DoDummyWork();
                DateTime n = DateTime.Now;
                q.Enqueue(n);
                Log('+', n);
            }
            lock (producerCompleteLocker)
            {
                remainingProducer--;
            }
        }
    }
}
