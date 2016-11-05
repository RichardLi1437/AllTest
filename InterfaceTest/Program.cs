using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ITest t1 = new Test();
            System.Console.WriteLine(t1.f1());
            System.Console.WriteLine(t1.f2(3));
        }
    }

    interface ITest
    {
        int f1();
        bool f2(int p);
    }

    class Test : ITest
    {
        public int f1()
        {
            return 2;
        }

        public bool f2(int p)
        {
            return p >= 0;
        }
    }
}
