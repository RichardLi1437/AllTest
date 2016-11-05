using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSafeQueue
{
    public class SyncQueueTest : QueueTest
    {
        private Queue q;

        public SyncQueueTest(int producerCount, int consumerCount, int generateCount, int flops) :  
            base("SyncQueue Test", producerCount, consumerCount, generateCount, flops)
        {
            q = Queue.Synchronized(new Queue());
        }

        protected override void Consume()
        {
            DateTime n;
            while (true)
            {
                try
                {
                    n = (DateTime)q.Dequeue();
                    DoDummyWork();
                    Log('-', n);
                }
                catch (InvalidOperationException)
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
