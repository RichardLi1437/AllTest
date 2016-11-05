using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ThreadTest
{
    class NewThread : MultiThreadTest
    {
        public NewThread(int taskCount, int taskComplexity) : base(taskCount, taskComplexity) { }

        protected override void StartOne(object state)
        {
            Thread t = new Thread(Sum);
            t.Start((int)state);
        }
    }

    class WithThreadPool : MultiThreadTest
    {
        public WithThreadPool(int taskCount, int taskComplexity) : base(taskCount, taskComplexity) { }
        
        protected override void StartOne(object state)
        {
            ThreadPool.QueueUserWorkItem(Sum, state);
        }
    }

    abstract class MultiThreadTest
    {
        private int completeCount = 0;
        private int taskCount, taskComplexity;

        private SpinLock completeLock = new SpinLock();

        public MultiThreadTest(int taskCount, int taskComplexity)
        {
            this.taskComplexity = taskComplexity;
            this.taskCount = taskCount;
        }

        protected void Sum(object o)
        {
            int n = (int)o;
            int s = 0;
            for (int i = 1; i <= n; i++) s += i;
            completeLock.Lock();
            completeCount++;
            completeLock.Unlock();
            //return s;
        }

        public void Start()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < taskCount; i++)
            {
                StartOne(taskComplexity);
            }
            while (true)
            {
                completeLock.Lock();
                if (completeCount >= taskCount)
                {
                    completeLock.Unlock();
                    break;
                }
                else
                {
                    completeLock.Unlock();
                    Thread.Sleep(1);
                }
            }
            completeLock.Unlock();
            timer.Stop();
            System.Console.WriteLine(timer.ElapsedMilliseconds);
        }

        protected abstract void StartOne(object state);

    }

    class NewTask : MultiThreadTest
    {
        public NewTask(int count, int complexity) : base(count, complexity) { }

        protected override void StartOne(object state)
        {
            Task t = new Task(Sum, state);
            t.Start();
        }
    }

    class SingleThread : MultiThreadTest
    {
        public SingleThread(int count, int complexity) : base(count, complexity) { }

        protected override void StartOne(object state)
        {
            Sum(state);
        }
    }

    class Program
    {

        //private static int interval = 10000;

        static void Main(string[] args)
        {
            TestReadWriteLock();
            //WorkerThread worker0 = new WorkerThread(10000, 0, ThreadPriority.Highest);
            //WorkerThread worker1 = new WorkerThread(10000, 1, ThreadPriority.BelowNormal);
            //WorkerThread worker2 = new WorkerThread(10000, 2, ThreadPriority.Normal);

                //Thread workerThread = new Thread(DisplayNumbers);
                //workerThread.Name = "worker" + i;
                //workerThread.Start(1000);
        }

        static void TestReadWriteLock()
        {
            ReadWriteTest rwTest;
            //rwTest = new ReadWriteTest(100, 20, new MonitorReadWriteLock());
            //rwTest.Run();
            rwTest = new ReadWriteTest(100, 20, new NoReadWriteLock());
            rwTest.Run();
        }

        static void TestCreateThread()
        {
            int count = 100000, complexity = 100000;
            NewThread newThread = new NewThread(count, complexity);
            newThread.Start();
            WithThreadPool withPool = new WithThreadPool(count, complexity);
            withPool.Start();
            NewTask newTask = new NewTask(count, complexity);
            newTask.Start();
            SingleThread single = new SingleThread(count, complexity);
            single.Start();
        }

        static void Sum(int n)
        {
            int s = 0;
            for (int i = 1; i <= n; i++) s += i;
        }

        static void DisplayNumbers(object _interval)
        {
            int interval = (int)_interval;
            Thread thisThread = Thread.CurrentThread;
            string name = thisThread.Name;
            Console.WriteLine(name + " starting");
            for (int i = 1; i <= 8 * interval; i++)
            {
                if (i % interval == 0)
                    Console.WriteLine(name + ": count has reached " + i);
            }
            Console.WriteLine(name + " has finished");
        }
    }

    class WorkerThread
    {
        private int interval;
        private Thread worker;

        public WorkerThread(int interval, int id, ThreadPriority priority)
        {
            worker = new Thread(DisplayNumbers);
            worker.Name = "worker " + id;
            worker.Priority = priority;
            this.interval = interval;
            worker.Start();
        }

        void DisplayNumbers()
        {
            Console.WriteLine(worker.Name + " starting");
            for (int i = 1; i <= 8 * interval; i++)
            {
                if (i % interval == 0)
                    Console.WriteLine(worker.Name + ": count has reached " + i);
            }
            Console.WriteLine(worker.Name + " has finished");
        }
    }
}
