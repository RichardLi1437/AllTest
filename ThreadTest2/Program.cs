using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest2
{
    class Program
    {
        private const int QUEUE_SIZE = 4;
        private const int PRODUCE_COUNT = 20;
        private Thread consumer, producer;
        private Queue<int> queue;

        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            queue = new Queue<int>();
            producer = new Thread(Produce);
            consumer = new Thread(Consume);
            producer.Start();
            consumer.Start();
        }

        void Produce()
        {
            for (int i = 0; i < PRODUCE_COUNT; i++) 
            {
                while (true)
                {
                    lock (queue)
                    {
                        if (queue.Count < QUEUE_SIZE)
                        {
                            queue.Enqueue(i);
                            Console.WriteLine("item " + i + "is put into the queue."); 
                            Monitor.Pulse(queue);
                            break;
                        }
                        else
                        {
                            Monitor.Wait(queue);
                        }
                    }
                }
                //Console.WriteLine("successfully put " + i + " in the queue.");
            }
        }

        void Consume()
        {
            int id;
            for (int i = 0; i < PRODUCE_COUNT; i++)
            {
                while (true)
                {
                    lock (queue)
                    {
                        if (queue.Count > 0)
                        {
                            id = queue.Dequeue();
                            Console.WriteLine("item " + id + " is prcessed"); 
                            Monitor.Pulse(queue);
                            break;
                        }
                        else
                        {
                            Monitor.Wait(queue);
                        }
                    }
                }
                //Console.WriteLine("item " + id + " is prcessed");
            }
        }
    }

}
