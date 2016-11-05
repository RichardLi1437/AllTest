#define LCAIT

using System;
using System.Diagnostics;

namespace AttributeTest
{
    public class AAA
    {
        [Conditional ("MSIT")]
        public static void f()
        {
            Console.WriteLine("aaa");
        }
    }
}