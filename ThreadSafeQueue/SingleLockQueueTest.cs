using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSafeQueue
{
    public class SingleLockQueueTest : QueueTest
    {
        private Queue<DateTime> q;

        public SingleLockQueueTest(int producerCount, int consumerCount, int generateCount, int flops) :  
            base("SingleLockQueue Test", producerCount, consumerCount, generateCount, flops)
        {
            q = new Queue<DateTime>();
        }

        protected override void Consume()
        {
            DateTime n;
            while (true)
            {
                try 
                {
                    lock(q) 
                    {
                        n = q.Dequeue();
                    }
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
                lock (q)
                {
                    q.Enqueue(n);
                }
                Log('+', n);
            }
            lock (producerCompleteLocker)
            {
                remainingProducer--;
            }
        }
    }
}
