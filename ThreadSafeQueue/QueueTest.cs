using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSafeQueue
{
    public abstract class QueueTest
    {
        protected Random r;
        protected Object producerCompleteLocker;
        protected Int32 remainingProducer;
        protected int generateCount;
        private int producerCount, consumerCount;
        private int flops;
        
        public QueueTest(string desc, int producerCount, int consumerCount, int generateCount, int flops)
        {
            Desc = desc;
            this.producerCount = producerCount;
            this.consumerCount = consumerCount;
            this.generateCount = generateCount;
            this.flops = flops;
            r = new Random();
            producerCompleteLocker = new Object();
            remainingProducer = producerCount;
        }

        public string Desc { get; private set; }

        public void Test()
        {
            Thread[] producers = new Thread[producerCount];
            Thread[] consumers = new Thread[consumerCount];

            for (int i = 0; i < producerCount; i++)
            {
                producers[i] = new Thread(Produce);
            }
            for (int i = 0; i < consumerCount; i++)
            {
                consumers[i] = new Thread(Consume);
            }
            for (int i = 0; i < consumerCount; i++)
            {
                consumers[i].Start();
            }
            for (int i = 0; i < producerCount; i++)
            {
                producers[i].Start();
            }

            for (int i = 0; i < consumerCount; i++)
            {
                consumers[i].Join();
            }
        }

        protected void DoDummyWork()
        {
            double f = 1;
            for (int i = 0; i < flops; i++)
            {
                f *= 1.01;
            }
            //System.Console.WriteLine(f);
        }

        protected void Log(char action, DateTime n)
        {
#if PRINT
            System.Console.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}", DateTime.Now.Ticks % 10000000, action, n.Ticks % 10000000, System.AppDomain.GetCurrentThreadId()));
#endif
        }

        abstract protected void Consume();

        abstract protected void Produce();

    }
}
