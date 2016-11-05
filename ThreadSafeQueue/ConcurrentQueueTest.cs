using System;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace ThreadSafeQueue
{
    public class ConcurrentQueueTest : QueueTest
    {
        private ConcurrentQueue<DateTime> q;

        public ConcurrentQueueTest(int producerCount, int consumerCount, int generateCount, int flops) :  
            base("ConcurrentQueue Test", producerCount, consumerCount, generateCount, flops)
        {
            q = new ConcurrentQueue<DateTime>();
        }

        protected override void Consume()
        {
            DateTime n;
            while (true)
            {                
                if (q.TryDequeue(out n))
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
