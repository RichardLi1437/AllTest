using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a, b = true;
            a = f(ref b);
            System.Console.WriteLine("{0}, {1}", a, b);
        }

        static bool f(ref bool b) 
        {
            b = false;
            return true;
        }
    }
}
