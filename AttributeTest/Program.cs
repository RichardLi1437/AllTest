using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AttributeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            f();
            int a = Convert.ToInt32("");
            Console.WriteLine("null=" + a);
            AAA.f();
#if (MSIT)
            Console.WriteLine("MSIT");
#else
            Console.WriteLine("no MSIT");
#endif
        }

        [Conditional("MSIT")]
        static void f()
        {
            Console.WriteLine("hello from f");
        }
    }
}
