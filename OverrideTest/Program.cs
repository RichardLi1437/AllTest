using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverrideTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*System.Console.WriteLine(1 << 20);
            Base bd = new Derived();
            bd.f();
            bd.g();*/
            MyClass mc = new MyClass(3);
            Console.WriteLine(mc.ToString());
            mc = new MyClass("pebble");
            Console.WriteLine(mc);
        }
    }

    class Base
    {
        public void f()
        {
            System.Console.WriteLine("Base::f()");
        }

        public virtual void g() 
        {
            System.Console.WriteLine("Base::g()");
        }
    }

    class Derived : Base
    {
        public void f()
        {
            System.Console.WriteLine("Derived::f()");
        }

        public override void g()
        {
            System.Console.WriteLine("Derived::g()");
        }
    }

    class MyClass
    {
        private int a;
        private double b;
        private string c;

        public MyClass(int a, double b, string c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public MyClass(int a): this(a, 0.0, "Richard")
        {
        }

        public MyClass(string c): this(0, 0.0, c)
        {
        }

        override public string ToString()
        {
            return a +"," + b +"," + c;
        }
    }
}
