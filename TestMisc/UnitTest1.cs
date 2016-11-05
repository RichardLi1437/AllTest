using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiscTest;

namespace TestMisc
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountOne()
        {
            int[] ns = { -1, 0, 1, 2, 9, 10, 11, 12, 20, 38, 109, 110, 118, 238, 313, 1024, 32768024 };
            //int[] expected = { -1, 0, 1, 1, 1, 2, 4, 5, 12 };
            int actual, expected;
            for (int i = 0; i < ns.Length; i++)
            {
                actual = Program.CountOne(ns[i]);
                expected = Program.CountOne2(ns[i]); 
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void TestCountOneRadix()
        {
            int[] ns = { -1, 0, 2, 12, 3258, 32459, 32768024 };
            int[] r = { 2, 5, 10, 16, 137 };
            //int[] expected = { -1, 0, 1, 7 };
            int actual, expected;
            for (int i = 0; i < ns.Length; i++)
            {
                for (int j = 0; j < r.Length; j++) 
                {
                    actual = Program.CountOneRadix(ns[i], r[j]);
                    expected = Program.CountOne2Radix(ns[i], r[j]);
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void TestCountDigitRadix()
        {
            int[] ns = { -1, 0, 2, 12, 3256929 };
            //int[] expected = { -1, 0, 1, 7 }; //radix=3, digit = 2
            int actual, expected;
            for (int i = 0; i < ns.Length; i++)
            {
               actual = Program.CountDigitRadix(ns[i], 0, 7);
               expected = Program.CountDigitRadix2(ns[i], 0, 7);
               //Assert.AreEqual(expected, actual);
            }
        }

    }
}
