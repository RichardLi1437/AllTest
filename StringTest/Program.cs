using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StringTest
{
    class Program
    {
        private const int N = 1000000;
        private static Stopwatch watch = new Stopwatch();
        static void Main(string[] args)
        {

            string source = "abcdefghijklmnopqrstuvwxyz0123456789C#"
                          + "ToLower.Contains or IndexOf(OrdinalIgnoreCase)?.uonun";
            string target = "Abc";
            Console.WriteLine("target in beginning:");
            TestContains(source, target);
            target = "abc";
            TestIndexOf(source, target);
            Console.WriteLine();

            Console.WriteLine("target in the middle:");
            target = "tol";
            TestContains(source, target);
            TestIndexOf(source, target);
            Console.WriteLine();

            Console.WriteLine("target in the end:");
            target = "nuN";
            TestContains(source, target);
            target = "nun";
            TestIndexOf(source, target);

            Console.WriteLine("Complete, press any key to return...");
            Console.ReadKey();

        }
        private static void TestIndexOf(string source, string target)
        {
            watch.Reset();
            watch.Start();
            for (int i = 0; i < N; i++)
            {
                //source.IndexOf(target, StringComparison.OrdinalIgnoreCase);
                source.IndexOf(target, StringComparison.Ordinal);
            }
            watch.Stop();
            Console.WriteLine("IndexOf: " + watch.ElapsedMilliseconds.ToString() + "ms");
            return;
        }

        private static void TestContains(string source, string target)
        {
            watch.Reset();
            watch.Start();
            for (int i = 0; i < N; i++)
            {
                //source.ToLower().Contains(target);
                source.Contains(target);
            }
            watch.Stop();
            Console.WriteLine("Contains: " + watch.ElapsedMilliseconds.ToString() + "ms");
            return;
        }
    }
}
