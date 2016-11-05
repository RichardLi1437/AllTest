using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Algorithm;
using System.Drawing;
using System.Diagnostics;

namespace AlgorithmTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) 
        // {
        // }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestConvert()
        {
            int[] inputs = { -1, 0, 1, 2, 3, 10, 25, 26, 27, 28, 52, 53, 676, 702, 703 };
            string actualResult;
            string[] expectResult = { "err", "err", "a", "b", "c", "j", "y", "z", "aa", "ab", "az", "ba", "yz", "zz", "aaa" };
            for (int i = 0; i < inputs.Length; i++)
            {
                actualResult = Program.convert(inputs[i]);
                Assert.AreEqual<string>(expectResult[i], actualResult);
            }
        }

        [TestMethod]
        public void TestMinHeap()
        {
            int[] input = { 3, 6, 12, 8, 9, 7, 5, 4, 10 };
            MinHeap mh = new MinHeap(input);
            Assert.IsTrue(mh.Check());
            System.Console.WriteLine(mh);
        }

        [TestMethod]
        public void TestMatch()
        {
            StringMatch stringMatch = new StringMatch("32r4ABCojfDE0owejfoweurFoewrr8u", "ABC*DE*F");
            MyMatch myMatch = stringMatch.SimpleMatch("A.C", 0);
            Assert.IsTrue(myMatch.matched);
            Assert.AreEqual(4, myMatch.start);
            Assert.AreEqual(6, myMatch.end);
            myMatch = stringMatch.Match();
            Assert.IsTrue(myMatch.matched);
            Assert.AreEqual(4, myMatch.start);
            Assert.AreEqual(23, myMatch.end);
            stringMatch.Pattern = "ABC*DE*FG";
            myMatch = stringMatch.Match();
            Assert.IsFalse(myMatch.matched);
        }

        [TestMethod]
        public void TestMatch2()
        {
            StringMatch stringmatch = new StringMatch("abcdefghijk", "ab?d*fg*i?*");
            MyMatch mymatch = stringmatch.Match();
            Assert.IsTrue(mymatch.matched);
        }

        [TestMethod]
        public void TestFindKey()
        {
            int[] test0={};
            int[] test1= {5};
            int[] test2= {3,1};
            int[] test3= {1,3,5};
            int[] test4={8,2,4,6};
            int[] test5={8,10,12, 2, 4, 6};
            int[] test6={7,8,11,-2,2,3,4,5};
            int[] test7 = { 8, 10, 12, 14, 16, 18, 2, 4, 6 };
            int[] test8 = {2,4};
            int[] test = null;
            int[] expectMaxIndex = { 0, 2, 0,2,2,5,1};
            int[,] testKey = {
                                 {1,2,3,4,5},//0
                                 {1,3,5,7,9},//1
                                        {1,2,3, -1, 0},//2
                                        {-1, 1, 2, 3, 4},//3
                                        {8,2,4,6,10},//4
                                        {7,10,16, 1, 6},//5
                                        {7,8,11,-2,5},//6
                                        {8,12,2,6,7},//7
                                        {1,3,5,2,4}//8
                                    };
            int[,] expectFindKey ={
                                     {-1,-1,-1,-1,-1},//0
                                     {-1,-1,0,-1,-1},//1
                                     {1,-1,0,-1,-1},//2
                                     {-1,0,-1,1,-1},//3
                                     {0,1,2,3,-1},//4
                                     {-1,1,-1,-1,5},//5
                                     {0,1,2,3,7},//6
                                     {0,2,6,8,-1},//7
                                     {-1,-1,-1,0,1}//8
                                 };
            int actual;
            for (int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0: test = test0; break;
                    case 1: test = test1; break;
                    case 2: test = test2; break;
                    case 3: test = test3; break;
                    case 4: test = test4; break;
                    case 5: test = test5; break;
                    case 6: test = test6; break;
                    case 7: test = test7; break;
                    case 8: test = test8; break;
                }
                if (i >= 2)
                {
                    actual = Program.FindMaxIndex(test);
                    Assert.AreEqual(expectMaxIndex[i - 2], actual);
                }
                for (int j = 0; j<5;j++) {
                    actual = Program.FindKey(test, testKey[i,j]);
                    Assert.AreEqual(expectFindKey[i,j], actual);
                }
            }
        }

        struct Cases {
            public int key, m,n;
            public Cases(int k, int m, int n) {
                key = k;
                this.m = m;
                this.n = n;
            }
        }

        [TestMethod]
        public void TestSearchMatrix()
        {
            int[,] a = { 
                        {1,10,20,30,40},
                        {11,21,31,41,51},
                        {22,32,42,52,62},
                        {33,43,53,63,73},
                        {44,54,64,74,84},
                        {55,65,75,85,95}
                       };
            Cases[] cases = { new Cases(1, 0, 0), new Cases(40, 0, 4), new Cases(55, 5, 0), new Cases(95, 5, 4), new Cases(30, 0, 3), 
                            new Cases(22,2,0), new Cases(65,5,1), new Cases(73,3,4), new Cases(32,2,1), new Cases(64,4,2), 
                            new Cases(54,4,1), new Cases(100,-1,-1), new Cases(-1,-1,-1), new Cases(68,-1,-1), new Cases(26,-1,-1) };
            for (int i = 0; i < cases.Length; i++)
            {
                Pos actual = Program.FindKey(a, cases[i].key);
                Assert.AreEqual(cases[i].m, actual.Row);
                Assert.AreEqual(cases[i].n, actual.Column);
            }
        }

        [TestMethod]
        public void TestReverse()
        {
            int[] input = { 0, 1, 24, 123, -890, 3450, -1, -15, 705 };
            int[] expected = { 0, 1, 42, 321, -98, 543, -1, -51, 507 };
            for (int i = 0; i < input.Length; i++)
            {
                int actual = Program.Reverse(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void TestConstructBTfromInPostOrder()
        {
            Int32[] inOrder = { 2, 4, 1, 3 };
            Int32[] postOrder = { 4, 2, 3, 1 };
            BinaryTree<Int32> bt = BinaryTree<Int32>.Construct(inOrder, postOrder);
            string expected = @"1(2(\,4),3)";
            Assert.AreEqual(expected, bt.ToString());

            Int32[] inOrder1 = { 4,2,1,5,3,6 };
            Int32[] postOrder1 = { 4,2,5,6,3,1 };
            bt = BinaryTree<Int32>.Construct(inOrder1, postOrder1);
            expected = @"1(2(4,\),3(5,6))";
            Assert.AreEqual(expected, bt.ToString());
        }


        [TestMethod]
        public void TestSearhInCircleIncreaseList()
        {
            /*List<int[]> datas = new List<int[]>();
            datas.Add(new int[]{ 1, 2, 3, -2, -1 });
            datas.Add(new int[] { 1 });
            datas.Add(new int[] { 13, 10 });*/
            int[][] datas = {
                                new int[]{1, 2, 3, -2, -1},
                                new int[]{1},
                                new int[]{13, 10},
                                new int[]{1,2},
                                new int[] {1,2,3},
                                new int[] {1,2,0},
                                new int[]{13, 1,2,3}
                            };
            int[] expected = { 3, 0, 1, 0, 0, 2, 1 };
            for (int i = 0; i < datas.Length; i++)
            {
                int[] data = datas[i];
                int min = Program.SearhInCircleIncreaseList(103, data);
                Assert.AreEqual(expected[i], min);

            }
        }

        [TestMethod]
        public void TestDivide()
        {
            #region regular test
            int[] divisors = { 1, 2, 3, 4, 5, 7, 11, 28 };
            for (int divident = 0; divident <= 1000; divident++)
            {
                for (int i = 0; i < divisors.Length; i++) 
                {
                    Assert.AreEqual(divident / divisors[i], Program.Divide(divident, divisors[i]));
                    Assert.AreEqual(divident / -divisors[i], Program.Divide(divident, -divisors[i]));
                    Assert.AreEqual(-divident / divisors[i], Program.Divide(-divident, divisors[i]));
                    Assert.AreEqual(-divident / -divisors[i], Program.Divide(-divident, -divisors[i]));
                }
            }
            #endregion

            #region divide by zero test
            for (int divident = -500; divident <= 500; divident++)
            {
                try
                {
                    Program.Divide(divident, 0);
                    Assert.Fail("No expected excpetion thrown!");
                }
                catch (Exception e) 
                {
                    Assert.IsInstanceOfType(e, typeof(DivideByZeroException));
                }
            }

            #endregion

            #region big number test
            int[] dividents = { 347023, 3098, 309480, -9797324 };
            int[] bigDivisors = {34, -3024, 9873, 9870739, -290347};
            for (int i = 0; i < dividents.Length; i++)
            {
                for (int j = 0; j < bigDivisors.Length; j++)
                {
                    Assert.AreEqual(dividents[i] / bigDivisors[j], Program.Divide(dividents[i], bigDivisors[j]));
                }
            }
            #endregion

            #region run time testing
            const int COUNT = 1000000;
            int q;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int k = 0; k < COUNT; k++)
            {
                for (int i = 0; i < dividents.Length; i++)
                {
                    for (int j = 0; j < bigDivisors.Length; j++)
                    {
                        q = Program.Divide(dividents[i], bigDivisors[j]);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("my run time: {0}ms", sw.ElapsedMilliseconds);

            sw.Restart();
            for (int k = 0; k < COUNT; k++)
            {
                for (int i = 0; i < dividents.Length; i++)
                {
                    for (int j = 0; j < bigDivisors.Length; j++)
                    {
                        q = dividents[i] / bigDivisors[j];
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("system run time: {0}ms", sw.ElapsedMilliseconds);
            #endregion
        }

        [TestMethod]
        public void TestSortN()
        {
            int[][] datas = {
                new int[]{2, 0, 3, 1},
                new int[]{0, 1, 2, 3, 4},
                new int[] { 2, 8, 3, 0, 4, 1, 6, 7, 5}
            };
            for (int j = 0; j < datas.Length; j++)
            {
                int[] data = datas[j];
                //Program.SortN(data);
                Program.SwapSort(data);
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != data[i]) Assert.Fail("sort fail!");
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMaxSum()
        {
            int[] maxs = {1,1,1,5,12};
            int[] sums = {0,1,3,8,39};
            for (int i = 0; i < maxs.Length; i++)
            {
                Console.WriteLine("MaxSum({0}, {1}):", maxs[i], sums[i]);
                foreach (var collection in Program.MaxSum(maxs[i], sums[i]))
                {
                    if (collection.Count <= 0)
                    {
                        Console.WriteLine("{}");
                        continue;
                    }
                    int sum = collection[0];
                    Console.Write("{");
                    Console.Write("{0}", collection[0]);
                    for (int j = 1; j < collection.Count; j++)
                    {
                        sum += collection[j];
                        Console.Write(",{0}", collection[j]);
                    }
                    Console.WriteLine("}");
                    Assert.AreEqual(sums[i], sum);
                }
            }
        }

    }

}
