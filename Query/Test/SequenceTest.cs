using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using BdsSoft.Query;

namespace Test
{
    /// <summary>
    ///This is a test class for Sequence and is intended
    ///to contain all Sequence Unit Tests
    ///</summary>
    [TestClass()]
    public class SequenceTest
    {
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region 1.16.7 Aggregate

        /// <summary>
        ///A test for Aggregate&lt;,&gt; (IEnumerable&lt;T&gt;, U, Func&lt;U,T,U&gt;)
        ///</summary>
        [TestMethod()]
        public void AggregateTest()
        {
            IEnumerable<string> source = new string[] { "Bart", "Rob", "Scott", "Bill", "Steve" };
            int seed = 2;
            Func<int, string, int> func = ( i, s) => i * s.Length;

            bool exception1 = false;
            try
            {
                Sequence.Aggregate(null, seed, func);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Aggregate(source, seed, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Aggregate<T, U> did not return the expected value (exceptions).");

            int expected = 2400;
            int actual = Sequence.Aggregate(source, seed, func);

            Assert.AreEqual(expected, actual, "Sequence.Aggregate<T, U> did not return the expected value.");
        }

        /// <summary>
        ///A test for Aggregate&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,T,T&gt;)
        ///</summary>
        [TestMethod()]
        public void AggregateTest1()
        {
            AggregateTest_1();
            AggregateTest_2();
        }

        private void AggregateTest_1()
        {
            IEnumerable<string> source = new string[] { "Bart", "Rob", "Scott", "Bill", "Steve" };
            Func<string, string, string> func = ( s1, s2) => s1 + " " + s2;

            bool exception1 = false;
            try
            {
                Sequence.Aggregate(null, func);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Aggregate(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Aggregate<T, U> did not return the expected value (exceptions).");

            string expected = "Bart Rob Scott Bill Steve";
            string actual = Sequence.Aggregate(source, func);

            Assert.IsTrue(expected.Equals(actual), "Sequence.Aggregate<T, U> did not return the expected value (test 1).");
        }

        private void AggregateTest_2()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, string, string> func = ( s1, s2) => s1 + " " + s2;

            bool exception = false;
            try
            {
                string actual = Sequence.Aggregate(source, func);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Aggregate<T, U> did not return the expected value (test 2).");
        }

        #endregion

        #region 1.15.2 All

        /// <summary>
        ///A test for All&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void AllTest()
        {
            AllTest_1();
            AllTest_2();
            AllTest_3();
            AllTest_4();
            AllTest_5();
        }

        private void AllTest_1()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            Func<int, bool> predicate = i => i > 0;

            bool exception1 = false;
            try
            {
                Sequence.All(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.All(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.All<T, U> did not return the expected value (exceptions).");

            bool expected = true;
            bool actual;

            actual = Sequence.All(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.All<T> did not return the expected value (test 1).");
        }

        private void AllTest_2()
        {
            IEnumerable<int> source = new List<int>() { 1, -2, 3 };
            Func<int, bool> predicate = i => i > 0;

            bool expected = false;
            bool actual;

            actual = Sequence.All(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.All<T> did not return the expected value (test 2).");
        }

        private void AllTest_3()
        {
            IEnumerable<int> source = new List<int>() { };
            Func<int, bool> predicate = i => i > 0;

            bool expected = true;
            bool actual;

            actual = Sequence.All(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.All<T> did not return the expected value (test 3).");
        }

        private void AllTest_4()
        {
            IEnumerable<string> source = new List<string>() { "Bart", "Bill" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool expected = true;
            bool actual;

            actual = Sequence.All(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.All<T> did not return the expected value (test 4).");
        }

        private void AllTest_5()
        {
            IEnumerable<string> source = new List<string>() { "Bart", "Bill" };
            Func<string, bool> predicate = s => s.EndsWith("t");

            bool expected = false;
            bool actual;

            actual = Sequence.All(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.All<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.15.1 Any

        /// <summary>
        ///A test for Any&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void AnyTest()
        {
            AnyTest_1();
            AnyTest_2();
        }

        private void AnyTest_1()
        {
            IEnumerable<int> source = new int[] { };

            bool exception1 = false;
            try
            {
                Sequence.Any<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Any<T, U> did not return the expected value (exceptions).");

            bool expected = false;
            bool actual;

            actual = Sequence.Any(source);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 1).");
        }

        private void AnyTest_2()
        {
            IEnumerable<int> source = new int[] { 1 };

            bool expected = true;
            bool actual;

            actual = Sequence.Any(source);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Any&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void AnyTest1()
        {
            AnyTest1_1();
            AnyTest1_2();
            AnyTest1_3();
            AnyTest1_4();
            AnyTest1_5();
        }

        private void AnyTest1_1()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };

            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.Any(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Any(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Any<T, U> did not return the expected value (exceptions).");

            bool expected = true;
            bool actual;

            actual = Sequence.Any(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 1).");
        }

        private void AnyTest1_2()
        {
            IEnumerable<int> source = new List<int> { 1, 3, 5 };

            Func<int, bool> predicate = i => i % 2 == 0;

            bool expected = false;
            bool actual;

            actual = Sequence.Any(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 2).");
        }

        private void AnyTest1_3()
        {
            IEnumerable<int> source = new List<int> { };

            Func<int, bool> predicate = i => i % 2 == 0;

            bool expected = false;
            bool actual;

            actual = Sequence.Any(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 3).");
        }

        private void AnyTest1_4()
        {
            IEnumerable<string> source = new List<string> { "Bart", "Steve" };

            Func<string, bool> predicate = s => s.Length == 4;

            bool expected = true;
            bool actual;

            actual = Sequence.Any(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 4).");
        }

        private void AnyTest1_5()
        {
            IEnumerable<string> source = new List<string> { "Bart", "Steve" };

            Func<string, bool> predicate = s => s.Length < 4;

            bool expected = false;
            bool actual;

            actual = Sequence.Any(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Any<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.16.6 Average

        /// <summary>
        ///A test for Average (IEnumerable&lt;decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest()
        {
            AverageTest_1();
            AverageTest_2();
            AverageTest_3();
        }

        private void AverageTest_1()
        {
            IEnumerable<decimal> source = new decimal[] { 38.0m, 21.0m, -12.3m, 17.4m, 12.34m };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            decimal expected = 0.0m;
            foreach (decimal d in source)
                expected += d;

            expected = expected / 5;
            decimal actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest_2()
        {
            IEnumerable<decimal> source = new decimal[] { decimal.MaxValue - 2.1m, -1.8m, 3.0m, 38.0m };

            bool exception = false;
            try
            {
                decimal actual = Sequence.Average(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest_3()
        {
            IEnumerable<decimal> source = new decimal[] { };

            bool exception = false;
            try
            {
                decimal actual = Sequence.Average(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest1()
        {
            AverageTest1_1();
            AverageTest1_2();
            AverageTest1_3();
            AverageTest1_4();
        }

        private void AverageTest1_1()
        {
            IEnumerable<decimal?> source = new List<decimal?> { 38.0m, null, 21.0m, -12.3m, 17.4m, 12.34m, null };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            decimal expected = 0.0m;
            foreach (decimal? d in source)
                if (d != null)
                    expected += d.Value;

            expected = expected / 5;
            decimal? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest1_2()
        {
            IEnumerable<decimal?> source = new List<decimal?> { decimal.MaxValue - 2.1m, -1.8m, 3.0m, 38.0m };

            bool exception = false;
            try
            {
                decimal? actual = Sequence.Average(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest1_3()
        {
            IEnumerable<decimal?> source = new List<decimal?> { };

            decimal? actual = Sequence.Average(source);

            decimal? expected = null;

            Assert.AreEqual(actual, expected, "Sequence.Average did not return the expected value (test 3).");
        }

        private void AverageTest1_4()
        {
            IEnumerable<decimal?> source = new List<decimal?> { null, null, null };

            decimal? actual = Sequence.Average(source);

            decimal? expected = null;

            Assert.AreEqual(actual, expected, "Sequence.Average did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;double?&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest2()
        {
            AverageTest2_1();
            AverageTest2_2();
            AverageTest2_3();
        }

        private void AverageTest2_1()
        {
            IEnumerable<double?> source = new List<double?> { 38.0, null, 21.0, -12.3, 17.4, 12.34, null };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            double? expected = 0.0;
            foreach (double? d in source)
                if (d != null)
                    expected += d.Value;

            expected = expected / 5;

            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest2_2()
        {
            IEnumerable<double?> source = new List<double?> { };

            double? expected = null;
            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest2_3()
        {
            IEnumerable<double?> source = new List<double?> { null, null, null };

            double? expected = null;
            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;double&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest3()
        {
            AverageTest3_1();
            AverageTest3_2();
        }

        private void AverageTest3_1()
        {
            IEnumerable<double> source = new double[] { 38.0, 21.0, -12.3, 17.4, 12.34 };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<double>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            double expected = 0.0;
            foreach (double d in source)
                expected += d;

            expected = expected / 5;
            double actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest3_2()
        {
            IEnumerable<double> source = new double[] { };

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;int?&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest4()
        {
            AverageTest4_1();
            AverageTest4_2();
            AverageTest4_3();
        }

        private void AverageTest4_1()
        {
            IEnumerable<int?> source = new List<int?> { 38, null, 21, -12, 17, 12, null };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            double? expected = 0.0;
            foreach (int? i in source)
                if (i != null)
                    expected += i.Value;

            expected = expected / 5;

            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest4_2()
        {
            IEnumerable<int?> source = new List<int?> { };

            double? expected = null;
            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest4_3()
        {
            IEnumerable<int?> source = new List<int?> { null, null, null };

            double? expected = null;
            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;int&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest5()
        {
            AverageTest5_1();
            //AverageTest5_2(); //test OverflowException case manually
            AverageTest5_3();
        }

        private void AverageTest5_1()
        {
            IEnumerable<int> source = new int[] { 38, 21, -12, 17, 12 };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<int>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            double expected = 0.0;
            foreach (int d in source)
                expected += d;

            expected = expected / 5;
            double actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest5_2()
        {
            IEnumerable<int> source = new int[] { int.MaxValue - 2, -1, 3, 38 }; //incorrect test code; testing for overflow would require a bunch of int.MaxValue summations

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest5_3()
        {
            IEnumerable<int> source = new int[] { };

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;long?&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest6()
        {
            AverageTest6_1();
            AverageTest6_2();
            AverageTest6_3();
            AverageTest6_4();
        }

        private void AverageTest6_1()
        {
            IEnumerable<long?> source = new List<long?> { 38, null, 21, -12, 17, 12, null };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            double? expected = 0.0;
            foreach (int? i in source)
                if (i != null)
                    expected += i.Value;

            expected = expected / 5;

            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest6_2()
        {
            IEnumerable<long?> source = new List<long?> { };

            double? expected = null;
            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest6_3()
        {
            IEnumerable<long?> source = new List<long?> { null, null, null };

            double? expected = null;
            double? actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 3).");
        }

        private void AverageTest6_4()
        {
            IEnumerable<long?> source = new List<long?> { long.MaxValue - 2, null, -1, 3, 38, null };

            bool exception = false;
            try
            {
                double? actual = Sequence.Average(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Average (IEnumerable&lt;long&gt;)
        ///</summary>
        [TestMethod()]
        public void AverageTest7()
        {
            AverageTest7_1();
            AverageTest7_2();
            AverageTest7_3();
        }

        private void AverageTest7_1()
        {
            IEnumerable<long> source = new long[] { 38, 21, -12, 17, 12 };

            bool exception1 = false;
            try
            {
                Sequence.Average((IEnumerable<long>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Average did not return the expected value (exceptions).");

            double expected = 0.0;
            foreach (long d in source)
                expected += d;

            expected = expected / 5;
            double actual = Sequence.Average(source);

            Assert.AreEqual(expected, actual, "Sequence.Average did not return the expected value (test 1).");
        }

        private void AverageTest7_2()
        {
            IEnumerable<long> source = new long[] { long.MaxValue - 2, -1, 3, 38 };

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 2).");
        }

        private void AverageTest7_3()
        {
            IEnumerable<long> source = new long[] { };

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average did not return the expected value (test 3).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal?&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest8()
        {
            AverageTest8_1();
            AverageTest8_2();
            AverageTest8_3();
            AverageTest8_4();
        }

        private void AverageTest8_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { dN = 38.0m },
                new AverageTest_Helper { dN = 1.23m },
                new AverageTest_Helper { dN = null },
                new AverageTest_Helper { dN = -7.8m }
            };

            Func<AverageTest_Helper, decimal?> selector = s => s.dN;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            decimal? expected = (38.0m + 1.23m - 7.8m) / 3;
            decimal? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest8_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, decimal?> selector = s => s.dN;

            decimal? expected = null;
            decimal? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest8_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { dN = null },
                new AverageTest_Helper { dN = null },
                new AverageTest_Helper { dN = null },
                new AverageTest_Helper { dN = null }
            };

            Func<AverageTest_Helper, decimal?> selector = s => s.dN;

            decimal? expected = null;
            decimal? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        private void AverageTest8_4()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { dN = 38.0m },
                new AverageTest_Helper { dN = decimal.MaxValue },
                new AverageTest_Helper { dN = null },
                new AverageTest_Helper { dN = -7.8m }
            };

            Func<AverageTest_Helper, decimal?> selector = s => s.dN;

            bool exception = false;
            try
            {
                decimal? actual = Sequence.Average(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 4).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest9()
        {
            AverageTest9_1();
            AverageTest9_2();
            AverageTest9_3();
        }

        private void AverageTest9_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { d = 38.0m },
                new AverageTest_Helper { d = 1.23m },
                new AverageTest_Helper { d = -7.8m }
            };

            Func<AverageTest_Helper, decimal> selector = s => s.d;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            decimal expected = (38.0m + 1.23m - 7.8m) / 3;
            decimal actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest9_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, decimal> selector = s => s.d;

            bool exception = false;
            try
            {
                decimal actual = Sequence.Average(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest9_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { d = 38.0m },
                new AverageTest_Helper { d = decimal.MaxValue },
                new AverageTest_Helper { d = -7.8m }
            };

            Func<AverageTest_Helper, decimal> selector = s => s.d;

            bool exception = false;
            try
            {
                decimal actual = Sequence.Average(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double?&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest10()
        {
            AverageTest10_1();
            AverageTest10_2();
            AverageTest10_3();
            AverageTest10_4();
        }

        private void AverageTest10_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { DN = 38.0 },
                new AverageTest_Helper { DN = 1.23 },
                new AverageTest_Helper { DN = null },
                new AverageTest_Helper { DN = -7.8 }
            };

            Func<AverageTest_Helper, double?> selector = s => s.DN;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            double? expected = (38.0 + 1.23 - 7.8) / 3;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest10_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, double?> selector = s => s.DN;

            double? expected = null;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest10_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { DN = null },
                new AverageTest_Helper { DN = null },
                new AverageTest_Helper { DN = null },
                new AverageTest_Helper { DN = null }
            };

            Func<AverageTest_Helper, double?> selector = s => s.DN;

            double? expected = null;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        private void AverageTest10_4()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { DN = 38.0 },
                new AverageTest_Helper { DN = double.MaxValue },
                new AverageTest_Helper { DN = null },
                new AverageTest_Helper { DN = double.MaxValue }
            };

            Func<AverageTest_Helper, double?> selector = s => s.DN;

            double? expected = 0;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(double.IsInfinity(actual.Value), "Sequence.Average<T> did not return the expected value (test 3).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest11()
        {
            AverageTest11_1();
            AverageTest11_2();
            AverageTest11_3();
        }

        private void AverageTest11_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { D = 38.0 },
                new AverageTest_Helper { D = 1.23 },
                new AverageTest_Helper { D = -7.8 }
            };

            Func<AverageTest_Helper, double> selector = s => s.D;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, double>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            double expected = (38.0 + 1.23 - 7.8) / 3;
            double actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest11_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, double> selector = s => s.D;

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest11_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { D = 38.0 },
                new AverageTest_Helper { D = double.MaxValue },
                new AverageTest_Helper { D = -7.8 },
                new AverageTest_Helper { D = double.MaxValue }
            };

            Func<AverageTest_Helper, double> selector = s => s.D;

            double actual = Sequence.Average(source, selector);

            Assert.IsTrue(double.IsInfinity(actual), "Sequence.Average<T> did not return the expected value (test 3).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int?&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest12()
        {
            AverageTest12_1();
            AverageTest12_2();
            AverageTest12_3();
            //AverageTest12_4(); //test OverflowException case manually
        }

        private void AverageTest12_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { IN = 38 },
                new AverageTest_Helper { IN = 123 },
                new AverageTest_Helper { IN = null },
                new AverageTest_Helper { IN = -78 }
            };

            Func<AverageTest_Helper, int?> selector = s => s.IN;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            double? expected = (38 + 123 - 78) / 3.0;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest12_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, int?> selector = s => s.IN;

            double? expected = null;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest12_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { IN = null },
                new AverageTest_Helper { IN = null },
                new AverageTest_Helper { IN = null },
                new AverageTest_Helper { IN = null }
            };

            Func<AverageTest_Helper, int?> selector = s => s.IN;

            double? expected = null;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        private void AverageTest12_4()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { IN = 38 },
                new AverageTest_Helper { IN = int.MaxValue }, //incorrect test code; testing for overflow would require a bunch of int.MaxValue summations
                new AverageTest_Helper { IN = null },
                new AverageTest_Helper { IN = -7 }
            };

            Func<AverageTest_Helper, int?> selector = s => s.IN;

            bool exception = false;
            try
            {
                double? actual = Sequence.Average(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 4).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest13()
        {
            AverageTest13_1();
            AverageTest13_2();
            //AverageTest13_3(); //test OverflowException case manually
        }

        private void AverageTest13_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { I = 380 },
                new AverageTest_Helper { I = 123 },
                new AverageTest_Helper { I = -78 }
            };

            Func<AverageTest_Helper, int> selector = s => s.I;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, int>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            double expected = (380 + 123 - 78) / 3.0;
            double actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest13_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, int> selector = s => s.I;

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest13_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { I = 380 },
                new AverageTest_Helper { I = int.MaxValue }, //incorrect test code; testing for overflow would require a bunch of int.MaxValue summations
                new AverageTest_Helper { I = -78 }
            };

            Func<AverageTest_Helper, int> selector = s => s.I;

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long?&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest14()
        {
            AverageTest14_1();
            AverageTest14_2();
            AverageTest14_3();
            AverageTest14_4();
        }

        private void AverageTest14_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { LN = 380 },
                new AverageTest_Helper { LN = 123 },
                new AverageTest_Helper { LN = null },
                new AverageTest_Helper { LN = -78 }
            };

            Func<AverageTest_Helper, long?> selector = s => s.LN;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            double? expected = (380 + 123 - 78) / 3.0;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest14_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, long?> selector = s => s.LN;

            double? expected = null;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest14_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { LN = null },
                new AverageTest_Helper { LN = null },
                new AverageTest_Helper { LN = null },
                new AverageTest_Helper { LN = null }
            };

            Func<AverageTest_Helper, long?> selector = s => s.LN;

            double? expected = null;
            double? actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        private void AverageTest14_4()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { LN = 380 },
                new AverageTest_Helper { LN = long.MaxValue },
                new AverageTest_Helper { LN = null },
                new AverageTest_Helper { LN = -78 }
            };

            Func<AverageTest_Helper, long?> selector = s => s.LN;

            bool exception = false;
            try
            {
                double? actual = Sequence.Average(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 4).");
        }

        /// <Summary>
        ///A test for Average&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long&gt;)
        ///</Summary>
        [TestMethod()]
        public void AverageTest15()
        {
            AverageTest15_1();
            AverageTest15_2();
            AverageTest15_3();
        }

        private void AverageTest15_1()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { L = 380 },
                new AverageTest_Helper { L = 123 },
                new AverageTest_Helper { L = -78 }
            };

            Func<AverageTest_Helper, long> selector = s => s.L;

            bool exception1 = false;
            try
            {
                Sequence.Average(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Average(source, (Func<AverageTest_Helper, long>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Average<T> did not return the expected value (exceptions).");

            double expected = (380 + 123 - 78) / 3.0;
            double actual = Sequence.Average(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Average<T> did not return the expected value (test 1).");
        }

        private void AverageTest15_2()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> { };

            Func<AverageTest_Helper, long> selector = s => s.L;

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 2).");
        }

        private void AverageTest15_3()
        {
            IEnumerable<AverageTest_Helper> source = new List<AverageTest_Helper> {
                new AverageTest_Helper { L = 380 },
                new AverageTest_Helper { L = long.MaxValue },
                new AverageTest_Helper { L = -78 }
            };

            Func<AverageTest_Helper, long> selector = s => s.L;

            bool exception = false;
            try
            {
                double actual = Sequence.Average(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Average<T> did not return the expected value (test 3).");
        }

        private class AverageTest_Helper
        {
            public decimal d = 0;
            public decimal? dN;
            public double D = 0;
            public double? DN;
            public int I = 0;
            public int? IN;
            public long L = 0;
            public long? LN;
        }

        #endregion

        #region 1.11.7 Cast

        /// <summary>
        ///A test for Cast&lt;&gt; (IEnumerable)
        ///</summary>
        [TestMethod()]
        public void CastTest()
        {
            CastTest_1();
            CastTest_2();
            CastTest_3();
            CastTest_4();
            CastTest_5();
        }

        private void CastTest_1()
        {
            IEnumerable source = new ArrayList(new int[] { 1, 2, 3 });

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Cast<int>(null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Cast<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Cast<int>(source);

            long n = 0;
            foreach (object o in source)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Cast<T> did not return the expected value (test 1).");

            IEnumerator enumerator = source.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.IsTrue((int)enumerator.Current == i, "Sequence.Cast<T> did not return the expected value (test 1).");
            }
        }

        private void CastTest_2()
        {
            IEnumerable source = new ArrayList(new string[] { "Bart", "Bill", "John" });

            IEnumerable<string> actual = Sequence.Cast<string>(source);

            long n = 0;
            foreach (object o in source)
                n++;
            foreach (string s in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Cast<T> did not return the expected value (test 2).");

            IEnumerator enumerator = source.GetEnumerator();
            foreach (string s in actual)
            {
                enumerator.MoveNext();
                Assert.IsTrue(object.ReferenceEquals(enumerator.Current, s), "Sequence.Cast<T> did not return the expected value (test 2).");
            }
        }

        private void CastTest_3()
        {
            IEnumerable source = new ArrayList(new object[] { "Bart", DateTime.Now });

            bool exception = false;
            try
            {
                IEnumerable<string> actual = Sequence.Cast<string>(source);
                foreach (string s in actual)
                    ;
            }
            catch (InvalidCastException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Cast<T> did not return the expected value (test 3).");
        }

        private void CastTest_4()
        {
            IEnumerable source = new object[] { };

            IEnumerable<string> actual = Sequence.Cast<string>(source);
            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Cast<T> did not return the expected value (test 4).");
        }

        private void CastTest_5()
        {
            IEnumerable source = new object[] { };

            IEnumerable<int> actual = Sequence.Cast<int>(source);
            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Cast<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.7.1 Concat

        /// <summary>
        ///A test for Concat&lt;&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void ConcatTest()
        {
            ConcatTest_1();
            ConcatTest_2();
            ConcatTest_3();
            ConcatTest_4();
            ConcatTest_5();
        }

        private void ConcatTest_1()
        {
            IEnumerable<int> first = new int[] { 3, 1, 2 };
            IEnumerable<int> second = new List<int> { 5, -1, 2, 1 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Concat(null, second))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Concat(first, null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Concat<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Concat(first, second);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 7, "Sequence.Concat<T> did not return the expected value (test 1).");

            IEnumerator<int> enumerator = actual.GetEnumerator();

            foreach (int i in first)
            {
                enumerator.MoveNext();
                Assert.IsTrue(i == (int)enumerator.Current, "Sequence.Concat<T> did not return the expected value (test 1).");
            }

            foreach (int i in second)
            {
                enumerator.MoveNext();
                Assert.IsTrue(i == (int)enumerator.Current, "Sequence.Concat<T> did not return the expected value (test 1).");
            }
        }

        private void ConcatTest_2()
        {
            IEnumerable<int> first = new int[] { };
            IEnumerable<int> second = new List<int> { 5, -1, 2, 1 };

            IEnumerable<int> actual = Sequence.Concat(first, second);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.Concat<T> did not return the expected value (test 2).");

            IEnumerator<int> enumerator = actual.GetEnumerator();

            foreach (int i in first)
            {
                enumerator.MoveNext();
                Assert.IsTrue(i == (int)enumerator.Current, "Sequence.Concat<T> did not return the expected value (test 2).");
            }

            foreach (int i in second)
            {
                enumerator.MoveNext();
                Assert.IsTrue(i == (int)enumerator.Current, "Sequence.Concat<T> did not return the expected value (test 2).");
            }
        }

        private void ConcatTest_3()
        {
            IEnumerable<int> first = new int[] { 3, 1, 2 };
            IEnumerable<int> second = new List<int> { };

            IEnumerable<int> actual = Sequence.Concat(first, second);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.Concat<T> did not return the expected value (test 3).");

            IEnumerator<int> enumerator = actual.GetEnumerator();

            foreach (int i in first)
            {
                enumerator.MoveNext();
                Assert.IsTrue(i == (int)enumerator.Current, "Sequence.Concat<T> did not return the expected value (test 3).");
            }

            foreach (int i in second)
            {
                enumerator.MoveNext();
                Assert.IsTrue(i == (int)enumerator.Current, "Sequence.Concat<T> did not return the expected value (test 3).");
            }
        }

        private void ConcatTest_4()
        {
            IEnumerable<string> first = new string[] { "Bart", "Bill", "John" };
            IEnumerable<string> second = new List<string> { "Steve" };

            IEnumerable<string> actual = Sequence.Concat(first, second);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.Concat<T> did not return the expected value (test 4).");

            IEnumerator<string> enumerator = actual.GetEnumerator();

            foreach (string s in first)
            {
                enumerator.MoveNext();
                Assert.IsTrue(object.ReferenceEquals(s, (string)enumerator.Current), "Sequence.Concat<T> did not return the expected value (test 4).");
            }

            foreach (string s in second)
            {
                enumerator.MoveNext();
                Assert.IsTrue(object.ReferenceEquals(s, (string)enumerator.Current), "Sequence.Concat<T> did not return the expected value (test 4).");
            }
        }

        private void ConcatTest_5()
        {
            IEnumerable<object> first = new string[] { };
            IEnumerable<object> second = new object[] { };

            IEnumerable<object> actual = Sequence.Concat(first, second);

            long n = 0;
            foreach (object o in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Concat<T> did not return the expected value (test 4).");
        }

        #endregion

        #region 1.15.3 Contains

        /// <summary>
        ///A test for Contains&lt;&gt; (IEnumerable&lt;T&gt;, T)
        ///</summary>
        [TestMethod()]
        public void ContainsTest()
        {
            ContainsTest_1();
            ContainsTest_2();
            ContainsTest_3();
            ContainsTest_4();
            ContainsTest_5();
            ContainsTest_6();
            ContainsTest_7();
            ContainsTest_8();
        }

        private void ContainsTest_1()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };

            int value = 1;

            bool exception1 = false;
            try
            {
                Sequence.Contains<int>(null, value);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Contains<T> did not return the expected value (exceptions).");

            bool expected = true;
            bool actual;

            actual = Sequence.Contains(source, value);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 1).");
        }

        private void ContainsTest_2()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };

            int value = -1;

            bool expected = false;
            bool actual;

            actual = Sequence.Contains(source, value);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 2).");
        }

        private void ContainsTest_3()
        {
            IEnumerable<int> source = new List<int> { };

            int value = 1;

            bool expected = false;
            bool actual;

            actual = Sequence.Contains(source, value);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 3).");
        }

        private void ContainsTest_4()
        {
            IEnumerable<string> source = new List<string> { "Bart", "John", "Steve" };

            string value = "Bart";

            bool expected = true;
            bool actual;

            actual = Sequence.Contains(source, value);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 4).");
        }

        private void ContainsTest_5()
        {
            IEnumerable<string> source = new List<string> { "Bart", "John", "Steve" };

            string value = "Bill";

            bool expected = false;
            bool actual;

            actual = Sequence.Contains(source, value);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 5).");
        }

        private void ContainsTest_6()
        {
            IEnumerable<string> source = new List<string> { };

            string value = "Bart";

            bool expected = false;
            bool actual;

            actual = Sequence.Contains(source, value);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 6).");
        }

        private void ContainsTest_7()
        {
            ContainsTest_Helper h = new ContainsTest_Helper();

            bool expected = true;
            bool actual = Sequence.Contains(h, 0);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 7).");
        }

        private void ContainsTest_8()
        {
            ContainsTest_Helper h = new ContainsTest_Helper();

            bool expected = false;
            bool actual = Sequence.Contains(h, 3);

            Assert.AreEqual(expected, actual, "Sequence.Contains<T> did not return the expected value (test 8).");
        }

        private class ContainsTest_Helper : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                yield return 0;
                yield return 1;
                yield return 2;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion

        #region 1.16.1 Count

        /// <summary>
        ///A test for Count&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            CountTest_1();
            CountTest_2();
            CountTest_3();

            //Test overflow conditions by code inspection!
            //CountTest_4();
            //CountTest_5();
        }

        private void CountTest_1()
        {
            IEnumerable<int> source = new List<int> { };

            bool exception1 = false;
            try
            {
                Sequence.Count<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Count<T> did not return the expected value (exceptions).");

            int expected = 0;
            int actual;

            actual = Sequence.Count(source);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 1).");
        }

        private void CountTest_2()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };

            int expected = ((ICollection<int>)source).Count;
            int actual;

            actual = Sequence.Count(source);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 2).");
        }

        private void CountTest_3()
        {
            int n = 5;
            IEnumerable<int> source = new CountTest_Helper<int>(n);

            int expected = n;
            int actual;

            actual = Sequence.Count(source);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 3).");
        }

        private void CountTest_4()
        {
            int n = int.MaxValue;
            IEnumerable<int> source = new CountTest_Helper<int>(n);

            int expected = n;
            int actual;

            actual = Sequence.Count(source);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 4).");
        }

        private void CountTest_5()
        {
            long n = (long)int.MaxValue + 1;
            IEnumerable<int> source = new CountTest_Helper<int>(n);

            try
            {
                int actual = Sequence.Count(source);
            }
            catch (OverflowException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false, "Sequence.Count<T> did not return the expected value (test 5).");
            }
        }

        private class CountTest_Helper<T> : IEnumerable<T>
        {
            private long n;

            public CountTest_Helper(long n)
            {
                this.n = n;
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (long i = 0; i < n; i++)
                    yield return default(T);
            }

            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        ///A test for Count&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void CountTest1()
        {
            CountTest1_1();
            CountTest1_2();
            CountTest1_3();
            CountTest1_4();
            CountTest1_5();
        }

        private void CountTest1_1()
        {
            IEnumerable<int> source = new List<int> { 1, 4, 3, 3, 12, 8, -1 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.Count<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Count<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Count<T> did not return the expected value (exceptions).");

            int expected = 3;
            int actual;

            actual = Sequence.Count(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 1).");
        }

        private void CountTest1_2()
        {
            IEnumerable<int> source = new List<int> { 1, 4, 3, 3, 12, 8, -1 };
            Func<int, bool> predicate = i => i == 0;

            int expected = 0;
            int actual;

            actual = Sequence.Count(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 2).");
        }

        private void CountTest1_3()
        {
            IEnumerable<string> source = new List<string> { "Bill", "Bart", "John", "Steve" };
            Func<string, bool> predicate = s => s.Length == 4;

            int expected = 3;
            int actual;

            actual = Sequence.Count(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 3).");
        }

        private void CountTest1_4()
        {
            IEnumerable<string> source = new List<string> { "Bill", "Bart", "John", "Steve" };
            Func<string, bool> predicate = s => s.Length < 4;

            int expected = 0;
            int actual;

            actual = Sequence.Count(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 4).");
        }

        private void CountTest1_5()
        {
            IEnumerable<string> source = new List<string> { };
            Func<string, bool> predicate = s => s.Length == 4;

            int expected = 0;
            int actual;

            actual = Sequence.Count(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Count<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.13.9 DefaultIfEmpty

        /// <summary>
        ///A test for DefaultIfEmpty&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void DefaultIfEmptyTest()
        {
            DefaultIfEmptyTest_1();
            DefaultIfEmptyTest_2();
            DefaultIfEmptyTest_3();
            DefaultIfEmptyTest_4();
        }

        private void DefaultIfEmptyTest_1()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.DefaultIfEmpty((IEnumerable<int>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.DefaultIfEmpty<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.DefaultIfEmpty(source);

            long n = 0;
            foreach (int i in source)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 1).");

            IEnumerator<int> enumSource = source.GetEnumerator();
            IEnumerator<int> enumActual = actual.GetEnumerator();

            while (enumSource.MoveNext() && enumActual.MoveNext())
                Assert.IsTrue(enumSource.Current == enumActual.Current, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 1).");
        }

        private void DefaultIfEmptyTest_2()
        {
            IEnumerable<string> source = new List<string> { "Bart", "Bill", "John" };

            IEnumerable<string> actual = Sequence.DefaultIfEmpty(source);

            long n = 0;
            foreach (string s in source)
                n++;
            foreach (string s in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 2).");

            IEnumerator<string> enumSource = source.GetEnumerator();
            IEnumerator<string> enumActual = actual.GetEnumerator();

            while (enumSource.MoveNext() && enumActual.MoveNext())
                Assert.IsTrue(object.ReferenceEquals(enumSource.Current, enumActual.Current), "Sequence.DefaultIfEmpty<T> did not return the expected value (test 2).");
        }

        private void DefaultIfEmptyTest_3()
        {
            IEnumerable<int> source = new List<int> { };

            IEnumerable<int> actual = Sequence.DefaultIfEmpty(source);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 1, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 3).");

            IEnumerator<int> enumActual = actual.GetEnumerator();
            enumActual.MoveNext();

            Assert.IsTrue(enumActual.Current == default(int), "Sequence.DefaultIfEmpty<T> did not return the expected value (test 3).");
        }

        private void DefaultIfEmptyTest_4()
        {
            IEnumerable<string> source = new List<string> { };

            IEnumerable<string> actual = Sequence.DefaultIfEmpty(source);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 1, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 4).");

            IEnumerator<string> enumActual = actual.GetEnumerator();
            enumActual.MoveNext();

            Assert.IsTrue(enumActual.Current == default(string), "Sequence.DefaultIfEmpty<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for DefaultIfEmpty&lt;&gt; (IEnumerable&lt;T&gt;, T)
        ///</summary>
        [TestMethod()]
        public void DefaultIfEmptyTest1()
        {
            DefaultIfEmptyTest1_1();
            DefaultIfEmptyTest1_2();
            DefaultIfEmptyTest1_3();
            DefaultIfEmptyTest1_4();
        }

        private void DefaultIfEmptyTest1_1()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            int defaultValue = 5;

            IEnumerable<int> actual = Sequence.DefaultIfEmpty(source, defaultValue);

            long n = 0;
            foreach (int i in source)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 1).");

            IEnumerator<int> enumSource = source.GetEnumerator();
            IEnumerator<int> enumActual = actual.GetEnumerator();

            while (enumSource.MoveNext() && enumActual.MoveNext())
                Assert.IsTrue(enumSource.Current == enumActual.Current, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 1).");
        }

        private void DefaultIfEmptyTest1_2()
        {
            IEnumerable<string> source = new List<string> { "Bart", "Bill", "John" };
            string defaultValue = "Foo";

            IEnumerable<string> actual = Sequence.DefaultIfEmpty(source, defaultValue);

            long n = 0;
            foreach (string s in source)
                n++;
            foreach (string s in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 2).");

            IEnumerator<string> enumSource = source.GetEnumerator();
            IEnumerator<string> enumActual = actual.GetEnumerator();

            while (enumSource.MoveNext() && enumActual.MoveNext())
                Assert.IsTrue(object.ReferenceEquals(enumSource.Current, enumActual.Current), "Sequence.DefaultIfEmpty<T> did not return the expected value (test 2).");
        }

        private void DefaultIfEmptyTest1_3()
        {
            IEnumerable<int> source = new List<int> { };
            int defaultValue = 5;

            IEnumerable<int> actual = Sequence.DefaultIfEmpty(source, defaultValue);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 1, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 3).");

            IEnumerator<int> enumActual = actual.GetEnumerator();
            enumActual.MoveNext();

            Assert.IsTrue(enumActual.Current == defaultValue, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 3).");
        }

        private void DefaultIfEmptyTest1_4()
        {
            IEnumerable<string> source = new List<string> { };
            string defaultValue = "Foo";

            IEnumerable<string> actual = Sequence.DefaultIfEmpty(source, defaultValue);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 1, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 4).");

            IEnumerator<string> enumActual = actual.GetEnumerator();
            enumActual.MoveNext();

            Assert.IsTrue(enumActual.Current == defaultValue, "Sequence.DefaultIfEmpty<T> did not return the expected value (test 4).");
        }

        #endregion

        #region 1.10.1 Distinct

        /// <summary>
        ///A test for Distinct&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void DistinctTest()
        {
            DistinctTest_1();
            DistinctTest_2();
            DistinctTest_3();
            DistinctTest_4();
        }

        private void DistinctTest_1()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 8, 2, 4, 9, 1, 7 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Distinct((IEnumerable<int>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Distinct<T> did not return the expected value (exceptions).");

            IEnumerable<int> expected = new List<int> { 1, 2, 8, 4, 9, 7 };
            IEnumerable<int> actual = Sequence.Distinct(source);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Distinct<T> did not return the expected value (test 1).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Distinct<T> did not return the expected value (test 1).");
            }
        }

        private void DistinctTest_2()
        {
            IEnumerable<int> source = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 };

            IEnumerable<int> expected = new List<int> { 1 };
            IEnumerable<int> actual = Sequence.Distinct(source);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Distinct<T> did not return the expected value (test 2).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Distinct<T> did not return the expected value (test 2).");
            }
        }

        private void DistinctTest_3()
        {
            IEnumerable<int> source = new List<int> { };

            IEnumerable<int> actual = Sequence.Distinct(source);

            Assert.IsTrue(!actual.GetEnumerator().MoveNext(), "Sequence.Distinct<T> did not return the expected value (test 3).");
        }

        private void DistinctTest_4()
        {
            DateTime d1 = DateTime.Now;
            DateTime d2 = d1;
            DateTime d3 = new DateTime(1983, 2, 11);
            DateTime d4 = new DateTime(1983, 2, 11);
            DateTime d5 = new DateTime(1948, 10, 11);
            DateTime d6 = d4;

            IEnumerable<DateTime> source = new List<DateTime> { d1, d3, d5, d4, d2, d6 };

            IEnumerable<DateTime> expected = new List<DateTime> { d1, d3, d5 };
            IEnumerable<DateTime> actual = Sequence.Distinct(source);

            long n = 0;
            foreach (DateTime d in expected)
                n++;
            foreach (DateTime d in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Distinct<T> did not return the expected value (test 4).");

            IEnumerator<DateTime> enumerator = actual.GetEnumerator();
            foreach (DateTime d in actual)
            {
                enumerator.MoveNext();
                Assert.IsTrue(d.Equals(enumerator.Current), "Sequence.Distinct<T> did not return the expected value (test 4).");
            }
        }

        #endregion

        #region 1.13.7 ElementAt

        /// <summary>
        ///A test for ElementAt&lt;&gt; (IEnumerable&lt;T&gt;, int)
        ///</summary>
        [TestMethod()]
        public void ElementAtTest()
        {
            ElementAtTest_1();
            ElementAtTest_2();
            ElementAtTest_3();
            ElementAtTest_4();
            ElementAtTest_5();
            ElementAtTest_6();
            ElementAtTest_7();
            ElementAtTest_8();
        }

        private void ElementAtTest_1()
        {
            IEnumerable<int> source = new List<int> { 5, 4, 3, 2, 1 };

            int index = 3;

            bool exception1 = false;
            try
            {
                Sequence.ElementAt<int>(null, index);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.ElementAt<T> did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.ElementAt(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAt<T> did not return the expected value (test 1).");
        }

        private void ElementAtTest_2()
        {
            IEnumerable<int> source = new List<int> { 5, 4, 3, 2, 1 };

            int index = -1;

            bool exception = false;
            try
            {
                int actual = Sequence.ElementAt(source, index);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ElementAt<T> did not return the expected value (test 2).");
        }

        private void ElementAtTest_3()
        {
            IEnumerable<int> source = new List<int> { 5, 4, 3, 2, 1 };

            int index = 5;

            bool exception = false;
            try
            {
                int actual = Sequence.ElementAt(source, index);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ElementAt<T> did not return the expected value (test 3).");
        }

        private void ElementAtTest_4()
        {
            IEnumerable<int> source = new Queue<int>(new int[] { 5, 4, 3, 2, 1 });

            int index = 3;

            int expected = 2;
            int actual = Sequence.ElementAt(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAt<T> did not return the expected value (test 4).");
        }

        private void ElementAtTest_5()
        {
            IEnumerable<int> source = new Queue<int>(new int[] { 5, 4, 3, 2, 1 });

            int index = -1;

            bool exception = false;
            try
            {
                int actual = Sequence.ElementAt(source, index);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ElementAt<T> did not return the expected value (test 5).");
        }

        private void ElementAtTest_6()
        {
            IEnumerable<int> source = new Queue<int>(new int[] { 5, 4, 3, 2, 1 });

            int index = 5;

            bool exception = false;
            try
            {
                int actual = Sequence.ElementAt(source, index);
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ElementAt<T> did not return the expected value (test 6).");
        }

        private void ElementAtTest_7()
        {
            string s = "Steve";
            IEnumerable<string> source = new List<string> { "Bart", "Bill", "John", s, "Rob" };

            int index = 3;

            string expected = s;
            string actual = Sequence.ElementAt(source, index);

            Assert.IsTrue(object.ReferenceEquals(expected, actual), "Sequence.ElementAt<T> did not return the expected value (test 7).");
        }

        private void ElementAtTest_8()
        {
            string s = "Steve";
            IEnumerable<string> source = new Queue<string>(new string[] { "Bart", "Bill", "John", s, "Rob" });

            int index = 3;

            string expected = s;
            string actual = Sequence.ElementAt(source, index);

            Assert.IsTrue(object.ReferenceEquals(expected, actual), "Sequence.ElementAt<T> did not return the expected value (test 8).");
        }

        #endregion

        #region 1.13.8 ElementAtOrDefault

        /// <summary>
        ///A test for ElementAtOrDefault&lt;&gt; (IEnumerable&lt;T&gt;, int)
        ///</summary>
        [TestMethod()]
        public void ElementAtOrDefaultTest()
        {
            ElementAtOrDefaultTest_1();
            ElementAtOrDefaultTest_2();
            ElementAtOrDefaultTest_3();
            ElementAtOrDefaultTest_4();
            ElementAtOrDefaultTest_5();
            ElementAtOrDefaultTest_6();
            ElementAtOrDefaultTest_7();
            ElementAtOrDefaultTest_8();
        }

        private void ElementAtOrDefaultTest_1()
        {
            IEnumerable<int> source = new List<int> { 5, 4, 3, 2, 1 };

            int index = 3;

            bool exception1 = false;
            try
            {
                Sequence.ElementAtOrDefault<int>(null, index);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.ElementAtOrDefault<T> did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.ElementAtOrDefault(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAtOrDefault<T> did not return the expected value (test 1).");
        }

        private void ElementAtOrDefaultTest_2()
        {
            IEnumerable<int> source = new List<int> { 5, 4, 3, 2, 1 };

            int index = -1;

            int expected = default(int);
            int actual = Sequence.ElementAtOrDefault(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAtOrDefault<T> did not return the expected value (test 2).");
        }

        private void ElementAtOrDefaultTest_3()
        {
            IEnumerable<int> source = new List<int> { 5, 4, 3, 2, 1 };

            int index = 5;

            int expected = default(int);
            int actual = Sequence.ElementAtOrDefault(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAtOrDefault<T> did not return the expected value (test 3).");
        }

        private void ElementAtOrDefaultTest_4()
        {
            IEnumerable<int> source = new Queue<int>(new int[] { 5, 4, 3, 2, 1 });

            int index = 3;

            int expected = 2;
            int actual = Sequence.ElementAtOrDefault(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAtOrDefault<T> did not return the expected value (test 4).");
        }

        private void ElementAtOrDefaultTest_5()
        {
            IEnumerable<int> source = new Queue<int>(new int[] { 5, 4, 3, 2, 1 });

            int index = -1;

            int expected = default(int);
            int actual = Sequence.ElementAtOrDefault(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAtOrDefault<T> did not return the expected value (test 5).");
        }

        private void ElementAtOrDefaultTest_6()
        {
            IEnumerable<int> source = new Queue<int>(new int[] { 5, 4, 3, 2, 1 });

            int index = 5;

            int expected = default(int);
            int actual = Sequence.ElementAtOrDefault(source, index);

            Assert.AreEqual(expected, actual, "Sequence.ElementAtOrDefault<T> did not return the expected value (test 6).");
        }

        private void ElementAtOrDefaultTest_7()
        {
            string s = "Steve";
            IEnumerable<string> source = new List<string> { "Bart", "Bill", "John", s, "Rob" };

            int index = 3;

            string expected = s;
            string actual = Sequence.ElementAtOrDefault(source, index);

            Assert.IsTrue(object.ReferenceEquals(expected, actual), "Sequence.ElementAtOrDefault<T> did not return the expected value (test 7).");
        }

        private void ElementAtOrDefaultTest_8()
        {
            string s = "Steve";
            IEnumerable<string> source = new Queue<string>(new string[] { "Bart", "Bill", "John", s, "Rob" });

            int index = 3;

            string expected = s;
            string actual = Sequence.ElementAtOrDefault(source, index);

            Assert.IsTrue(object.ReferenceEquals(expected, actual), "Sequence.ElementAtOrDefault<T> did not return the expected value (test 8).");
        }

        #endregion

        #region 1.14.3 Empty

        /// <summary>
        ///A test for Empty&lt;&gt; ()
        ///</summary>
        [TestMethod()]
        public void EmptyTest()
        {
            EmptyTest_1();
            EmptyTest_2();
        }

        private void EmptyTest_1()
        {
            IEnumerable<int> actual = Sequence.Empty<int>();

            Assert.IsTrue(!actual.GetEnumerator().MoveNext(), "Sequence.Empty<T> did not return the expected value.");
        }

        private void EmptyTest_2()
        {
            IEnumerable<string> actual = Sequence.Empty<string>();

            Assert.IsTrue(!actual.GetEnumerator().MoveNext(), "Sequence.Empty<T> did not return the expected value.");
        }

        #endregion

        #region 1.12.1 EqualAll
        /// <summary>
        ///A test for EqualAll&lt;&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void EqualAllTest()
        {
            EqualAllTest_1();
            EqualAllTest_2();
            EqualAllTest_3();
            EqualAllTest_4();
            EqualAllTest_5();
            EqualAllTest_6();
            EqualAllTest_7();
            EqualAllTest_8();
        }

        private void EqualAllTest_1()
        {
            IEnumerable<int> first = new List<int> { 1, 3, 2 };
            IEnumerable<int> second = new List<int> { 1, 3, 2 };

            bool exception1 = false;
            try
            {
                Sequence.EqualAll<int>(null, second);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.EqualAll<int>(first, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.EqualAll<T> did not return the expected value (exceptions).");

            bool expected = true;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 1).");
        }

        private void EqualAllTest_2()
        {
            IEnumerable<int> first = new List<int> { 1, 3, 2 };
            IEnumerable<int> second = new List<int> { 1, 2, 3 };

            bool expected = false;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 2).");
        }

        private void EqualAllTest_3()
        {
            IEnumerable<int> first = new List<int> { 1, 3, 2 };
            IEnumerable<int> second = new List<int> { 1, 3 };

            bool expected = false;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 3).");
        }

        private void EqualAllTest_4()
        {
            IEnumerable<int> first = new List<int> { 1, 3, 2 };
            IEnumerable<int> second = new List<int> { 7, 8 };

            bool expected = false;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 4).");
        }

        private void EqualAllTest_5()
        {
            IEnumerable<int> first = new List<int> { 1, 3 };
            IEnumerable<int> second = new List<int> { 1, 3, 2 };

            bool expected = false;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 5).");
        }

        private void EqualAllTest_6()
        {
            IEnumerable<int> first = new List<int> { 7, 8 };
            IEnumerable<int> second = new List<int> { 1, 3, 2 };

            bool expected = false;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 6).");
        }

        private void EqualAllTest_7()
        {
            IEnumerable<string> first = new List<string> { "Bart", "Steve", "Bill" };
            IEnumerable<string> second = new List<string> { "Bart", "Steve", "Bill" };

            bool expected = true;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 7).");
        }

        private void EqualAllTest_8()
        {
            IEnumerable<string> first = new List<string> { "Bart", "Steve", "Bill" };
            IEnumerable<string> second = new List<string> { "Bart", "Bill", "Steve" };

            bool expected = false;
            bool actual = Sequence.EqualAll(first, second);

            Assert.AreEqual(expected, actual, "Sequence.EqualAll<T> did not return the expected value (test 8).");
        }

        #endregion

        #region 1.10.4 Except

        /// <summary>
        ///A test for Except&lt;&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void ExceptTest()
        {
            ExceptTest_1();
            ExceptTest_2();
            ExceptTest_3();
            ExceptTest_4();
        }

        private void ExceptTest_1()
        {
            IEnumerable<int> first = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> second = new List<int> { 5, 1, 7 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Except(first, null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Except(null, second))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Except<T> did not return the expected value (exceptions).");

            IEnumerable<int> expected = new List<int> { 2, 9 };
            IEnumerable<int> actual = Sequence.Except(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Except<T> did not return the expected value (test 1).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Except<T> did not return the expected value (test 1).");
            }
        }

        private void ExceptTest_2()
        {
            IEnumerable<int> first = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> second = new List<int> { };

            IEnumerable<int> expected = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> actual = Sequence.Except(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Except<T> did not return the expected value (test 2).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Except<T> did not return the expected value (test 2).");
            }
        }

        private void ExceptTest_3()
        {
            IEnumerable<int> first = new List<int> { };
            IEnumerable<int> second = new List<int> { 3, 4, 5, 1, 7 };

            IEnumerable<int> expected = new List<int> { };
            IEnumerable<int> actual = Sequence.Except(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Except<T> did not return the expected value (test 3).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Except<T> did not return the expected value (test 3).");
            }
        }

        private void ExceptTest_4()
        {
            string j1 = "John";
            IEnumerable<string> first = new List<string> { "Bart", j1, "Bill", "John", "Scott" };
            IEnumerable<string> second = new List<string> { "Steve", "Bart", "Rob", "Steve", j1 };

            IEnumerable<string> expected = new List<string> { "Bill", "Scott" };
            IEnumerable<string> actual = Sequence.Except(first, second);

            long n = 0;
            foreach (string s in expected)
                n++;
            foreach (string s in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Except<T> did not return the expected value (test 4).");

            IEnumerator<string> enumerator = actual.GetEnumerator();
            foreach (string s in actual)
            {
                enumerator.MoveNext();
                Assert.IsTrue(s.Equals(enumerator.Current), "Sequence.Except<T> did not return the expected value (test 4).");
            }
        }

        #endregion

        #region 1.13.1 First

        /// <summary>
        ///A test for First&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void FirstTest()
        {
            FirstTest_1();
            FirstTest_2();
            FirstTest_3();
            FirstTest_4();
        }

        private void FirstTest_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };

            bool exception1 = false;
            try
            {
                Sequence.First<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.First<T> did not return the expected value (exceptions).");

            int expected = 1;
            int actual = Sequence.First(source);

            Assert.AreEqual(expected, actual, "Sequence.First<T> did not return the expected value (test 1).");
        }

        private void FirstTest_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { s, "John", "Bart", "Rob" };

            string expected = s;
            string actual = Sequence.First(source);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.First<T> did not return the expected value (test 2).");
        }

        private void FirstTest_3()
        {
            IEnumerable<int> source = new int[] { };

            bool exception = false;
            try
            {
                int actual = Sequence.First(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.First<T> did not return the expected value (test 3).");
        }

        private void FirstTest_4()
        {
            IEnumerable<string> source = new string[] { };

            bool exception = false;
            try
            {
                string actual = Sequence.First(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.First<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for First&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void FirstTest1()
        {
            FirstTest1_1();
            FirstTest1_2();
            FirstTest1_3();
            FirstTest1_4();
            FirstTest1_5();
            FirstTest1_6();
        }

        private void FirstTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.First<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.First<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.First<T> did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.First(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.First<T> did not return the expected value (test 1).");
        }

        private void FirstTest1_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "John", s, "Rob", "Bill" };
            Func<string, bool> predicate = ss => ss.StartsWith("B");

            string expected = s;
            string actual = Sequence.First(source, predicate);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.First<T> did not return the expected value (test 2).");
        }

        private void FirstTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.First(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.First<T> did not return the expected value (test 3).");
        }

        private void FirstTest1_4()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.First(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.First<T> did not return the expected value (test 4).");
        }

        private void FirstTest1_5()
        {
            IEnumerable<int> source = new int[] { 3, 5, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.First(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.First<T> did not return the expected value (test 5).");
        }

        private void FirstTest1_6()
        {
            IEnumerable<string> source = new string[] { "John", "Steve" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.First(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.First<T> did not return the expected value (test 6).");
        }

        #endregion

        #region 1.13.2 FirstOrDefault

        /// <summary>
        ///A test for FirstOrDefault&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void FirstOrDefaultTest()
        {
            FirstOrDefaultTest_1();
            FirstOrDefaultTest_2();
            FirstOrDefaultTest_3();
            FirstOrDefaultTest_4();
        }

        private void FirstOrDefaultTest_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };

            bool exception1 = false;
            try
            {
                Sequence.FirstOrDefault<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.FirstOrDefault<T> did not return the expected value (exceptions).");

            int expected = 1;
            int actual = Sequence.FirstOrDefault(source);

            Assert.AreEqual(expected, actual, "Sequence.FirstOrDefault<T> did not return the expected value (test 1).");
        }

        private void FirstOrDefaultTest_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { s, "John", "Bart", "Rob" };

            string expected = s;
            string actual = Sequence.FirstOrDefault(source);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.FirstOrDefault<T> did not return the expected value (test 2).");
        }

        private void FirstOrDefaultTest_3()
        {
            IEnumerable<int> source = new int[] { };

            int expected = default(int);
            int actual = Sequence.FirstOrDefault(source);

            Assert.AreEqual(expected, actual, "Sequence.FirstOrDefault<T> did not return the expected value (test 3).");
        }

        private void FirstOrDefaultTest_4()
        {
            IEnumerable<string> source = new string[] { };

            string expected = default(string);
            string actual = Sequence.FirstOrDefault(source);

            Assert.IsTrue(object.Equals(actual, expected), "Sequence.FirstOrDefault<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for FirstOrDefault&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void FirstOrDefaultTest1()
        {
            FirstOrDefaultTest1_1();
            FirstOrDefaultTest1_2();
            FirstOrDefaultTest1_3();
            FirstOrDefaultTest1_4();
            FirstOrDefaultTest1_5();
            FirstOrDefaultTest1_6();
        }

        private void FirstOrDefaultTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.FirstOrDefault<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.FirstOrDefault<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.FirstOrDefault<T> did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.FirstOrDefault(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.FirstOrDefault<T> did not return the expected value (test 1).");
        }

        private void FirstOrDefaultTest1_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "John", s, "Rob", "Bill" };
            Func<string, bool> predicate = ss => ss.StartsWith("B");

            string expected = s;
            string actual = Sequence.FirstOrDefault(source, predicate);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.FirstOrDefault<T> did not return the expected value (test 2).");
        }

        private void FirstOrDefaultTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            int expected = default(int);
            int actual = Sequence.FirstOrDefault(source, predicate);

            Assert.IsTrue(expected == actual, "Sequence.FirstOrDefault<T> did not return the expected value (test 3).");
        }

        private void FirstOrDefaultTest1_4()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, bool> predicate = s => s.StartsWith("B");

            string expected = default(string);
            string actual = Sequence.FirstOrDefault(source, predicate);

            Assert.IsTrue(object.Equals(expected, actual), "Sequence.FirstOrDefault<T> did not return the expected value (test 4).");
        }

        private void FirstOrDefaultTest1_5()
        {
            IEnumerable<int> source = new int[] { 3, 5, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;

            int expected = default(int);
            int actual = Sequence.FirstOrDefault(source, predicate);

            Assert.IsTrue(expected == actual, "Sequence.FirstOrDefault<T> did not return the expected value (test 5).");
        }

        private void FirstOrDefaultTest1_6()
        {
            IEnumerable<string> source = new string[] { "John", "Steve" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            string expected = default(string);
            string actual = Sequence.FirstOrDefault(source, predicate);

            Assert.IsTrue(object.Equals(expected, actual), "Sequence.FirstOrDefault<T> did not return the expected value (test 6).");
        }

        #endregion

        #region 1.9.1 GroupBy

        /// <summary>
        ///A test for GroupBy&lt;,,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, Func&lt;T,E&gt;)
        ///</summary>
        [TestMethod()]
        public void GroupByTest()
        {
            string bart = "Bart";
            string rob = "Rob";
            string scott = "Scott";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            GroupBy_Helper g1 = new GroupBy_Helper { I = 4, S = bart };
            GroupBy_Helper g2 = new GroupBy_Helper { I = 3, S = rob };
            GroupBy_Helper g3 = new GroupBy_Helper { I = 5, S = scott };
            GroupBy_Helper g4 = new GroupBy_Helper { I = 4, S = bill };
            GroupBy_Helper g5 = new GroupBy_Helper { I = 4, S = john };
            GroupBy_Helper g6 = new GroupBy_Helper { I = 5, S = steve };

            IEnumerable<GroupBy_Helper> source = new List<GroupBy_Helper> { g1, g2, g3, g4, g5, g6 };
            Func<GroupBy_Helper, int> keySelector = g => g.I;
            Func<GroupBy_Helper, string> elementSelector = g => g.S;

            bool exception1 = false;
            try
            {
                foreach (var i in Sequence.GroupBy((IEnumerable<GroupBy_Helper>)null, keySelector, elementSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (var i in Sequence.GroupBy(source, (Func<GroupBy_Helper, int>)null, elementSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            bool exception3 = false;
            try
            {
                foreach (var i in Sequence.GroupBy(source, keySelector, (Func<GroupBy_Helper, string>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception3 = true;
            }

            Assert.IsTrue(exception1 && exception2 && exception3, "Sequence.GroupBy<T, K, E> did not return the expected value (exceptions).");

            List<int> expectedKeys = new List<int> { 4, 3, 5 };
            List<string[]> expectedValues = new List<string[]> { new string[] { bart, bill, john }, new string[] { rob }, new string[] { scott, steve } };

            IEnumerable<IGrouping<int, string>> actual = Sequence.GroupBy(source, keySelector, elementSelector);

            long n = 0;
            foreach (IGrouping<int, string> g in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.GroupBy<T, K, E> did not return the expected value.");

            int j = 0;
            foreach (IGrouping<int, string> g in actual)
            {
                Assert.IsTrue(expectedKeys[j] == g.Key, "Sequence.GroupBy<T, K, E> did not return the expected value.");

                int k = 0;
                foreach (string s in g)
                    Assert.IsTrue(object.ReferenceEquals(expectedValues[j][k++], s), "Sequence.GroupBy<T, K, E> did not return the expected value.");

                j++;
            }
        }

        /// <summary>
        ///A test for GroupBy&lt;,,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, Func&lt;T,E&gt;, IEqualityComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void GroupByTest1()
        {
            string bart = "Bart";
            string rob = "Rob";
            string scott = "Scott";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            GroupBy_Helper g1 = new GroupBy_Helper { I = -4, S = bart };
            GroupBy_Helper g2 = new GroupBy_Helper { I = 3, S = rob };
            GroupBy_Helper g3 = new GroupBy_Helper { I = 5, S = scott };
            GroupBy_Helper g4 = new GroupBy_Helper { I = -4, S = bill };
            GroupBy_Helper g5 = new GroupBy_Helper { I = 4, S = john };
            GroupBy_Helper g6 = new GroupBy_Helper { I = -5, S = steve };

            IEnumerable<GroupBy_Helper> source = new List<GroupBy_Helper> { g1, g2, g3, g4, g5, g6 };
            Func<GroupBy_Helper, int> keySelector = g => g.I;
            Func<GroupBy_Helper, string> elementSelector = g => g.S;

            List<int> expectedKeys = new List<int> { -4, 3, 5 };
            List<string[]> expectedValues = new List<string[]> { new string[] { bart, bill, john }, new string[] { rob }, new string[] { scott, steve } };

            IEnumerable<IGrouping<int, string>> actual = Sequence.GroupBy(source, keySelector, elementSelector, new GroupByComparer_Helper<int>());

            long n = 0;
            foreach (IGrouping<int, string> g in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.GroupBy<T, K, E> did not return the expected value.");

            int j = 0;
            foreach (IGrouping<int, string> g in actual)
            {
                Assert.IsTrue(expectedKeys[j] == g.Key, "Sequence.GroupBy<T, K, E> did not return the expected value.");

                int k = 0;
                foreach (string s in g)
                    Assert.IsTrue(object.ReferenceEquals(expectedValues[j][k++], s), "Sequence.GroupBy<T, K, E> did not return the expected value.");

                j++;
            }
        }

        /// <summary>
        ///A test for GroupBy&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void GroupByTest2()
        {
            string bart = "Bart";
            string rob = "Rob";
            string scott = "Scott";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            GroupBy_Helper g1 = new GroupBy_Helper { I = 4, S = bart };
            GroupBy_Helper g2 = new GroupBy_Helper { I = 3, S = rob };
            GroupBy_Helper g3 = new GroupBy_Helper { I = 5, S = scott };
            GroupBy_Helper g4 = new GroupBy_Helper { I = 4, S = bill };
            GroupBy_Helper g5 = new GroupBy_Helper { I = 4, S = john };
            GroupBy_Helper g6 = new GroupBy_Helper { I = 5, S = steve };

            IEnumerable<GroupBy_Helper> source = new List<GroupBy_Helper> { g1, g2, g3, g4, g5, g6 };
            Func<GroupBy_Helper, int> keySelector = g => g.I;

            List<int> expectedKeys = new List<int> { 4, 3, 5 };
            List<GroupBy_Helper[]> expectedValues = new List<GroupBy_Helper[]> { new GroupBy_Helper[] { g1, g4, g5 }, new GroupBy_Helper[] { g2 }, new GroupBy_Helper[] { g3, g6 } };

            IEnumerable<IGrouping<int, GroupBy_Helper>> actual = Sequence.GroupBy(source, keySelector);

            long n = 0;
            foreach (IGrouping<int, GroupBy_Helper> g in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.GroupBy<T, K> did not return the expected value.");

            int j = 0;
            foreach (IGrouping<int, GroupBy_Helper> g in actual)
            {
                Assert.IsTrue(expectedKeys[j] == g.Key, "Sequence.GroupBy<T, K> did not return the expected value.");

                int k = 0;
                foreach (GroupBy_Helper item in g)
                    Assert.IsTrue(object.ReferenceEquals(expectedValues[j][k++], item), "Sequence.GroupBy<T, K> did not return the expected value.");

                j++;
            }
        }

        /// <summary>
        ///A test for GroupBy&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, IEqualityComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void GroupByTest3()
        {
            string bart = "Bart";
            string rob = "Rob";
            string scott = "Scott";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            GroupBy_Helper g1 = new GroupBy_Helper { I = -4, S = bart };
            GroupBy_Helper g2 = new GroupBy_Helper { I = 3, S = rob };
            GroupBy_Helper g3 = new GroupBy_Helper { I = 5, S = scott };
            GroupBy_Helper g4 = new GroupBy_Helper { I = -4, S = bill };
            GroupBy_Helper g5 = new GroupBy_Helper { I = 4, S = john };
            GroupBy_Helper g6 = new GroupBy_Helper { I = -5, S = steve };

            IEnumerable<GroupBy_Helper> source = new List<GroupBy_Helper> { g1, g2, g3, g4, g5, g6 };
            Func<GroupBy_Helper, int> keySelector = g => g.I;

            List<int> expectedKeys = new List<int> { -4, 3, 5 };
            List<GroupBy_Helper[]> expectedValues = new List<GroupBy_Helper[]> { new GroupBy_Helper[] { g1, g4, g5 }, new GroupBy_Helper[] { g2 }, new GroupBy_Helper[] { g3, g6 } };

            IEnumerable<IGrouping<int, GroupBy_Helper>> actual = Sequence.GroupBy(source, keySelector, new GroupByComparer_Helper<int>());

            long n = 0;
            foreach (IGrouping<int, GroupBy_Helper> g in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.GroupBy<T, K> did not return the expected value.");

            int j = 0;
            foreach (IGrouping<int, GroupBy_Helper> g in actual)
            {
                Assert.IsTrue(expectedKeys[j] == g.Key, "Sequence.GroupBy<T, K> did not return the expected value.");

                int k = 0;
                foreach (GroupBy_Helper item in g)
                    Assert.IsTrue(object.ReferenceEquals(expectedValues[j][k++], item), "Sequence.GroupBy<T, K> did not return the expected value.");

                j++;
            }
        }

        private class GroupBy_Helper
        {
            public int I;
            public string S;
        }

        private class GroupByComparer_Helper<T> : IEqualityComparer<T>
        {
            private IEqualityComparer _comparer;

            public GroupByComparer_Helper()
            {
                _comparer = new GroupByComparerNG_Helper();
            }

            public bool Equals(T a, T b)
            {
                return _comparer.Equals(a, b);
            }

            public int GetHashCode(T a)
            {
                return _comparer.GetHashCode(a);
            }
        }

        private class GroupByComparerNG_Helper : IEqualityComparer
        {
            public new bool Equals(object a, object b)
            {
                if (a is int && b is int)
                {
                    int ia = Math.Abs((int)a);
                    int ib = Math.Abs((int)b);

                    return ia == ib;
                }
                else
                    throw new Exception(); // quick-n-dirty
            }

            public int GetHashCode(object a)
            {
                if (a is int)
                    return Math.Abs((int)a).GetHashCode();
                else
                    throw new Exception(); // quick-n-dirty
            }
        }

        #endregion

        #region 1.6.2 GroupJoin

        /// <summary>
        ///A test for GroupJoin&lt;,,,&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;U&gt;, Func&lt;T,K&gt;, Func&lt;U,K&gt;, Func&lt;T,IEnumerable&lt;U&gt;,V&gt;)
        ///</summary>
        [TestMethod()]
        public void GroupJoinTest()
        {
            GroupJoinTest_1();
            GroupJoinTest_2();
        }

        private void GroupJoinTest_1()
        {
            string bart = "Bart";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";
            string rob = "Rob";

            IEnumerable<GroupJoin_Customer> outer = new List<GroupJoin_Customer> {
                new GroupJoin_Customer { CustomerID = 1, Name = bart },
                new GroupJoin_Customer { CustomerID = 2, Name = bill },
                new GroupJoin_Customer { CustomerID = 3, Name = john },
                new GroupJoin_Customer { CustomerID = 4, Name = steve },
                new GroupJoin_Customer { CustomerID = 5, Name = rob }
            };

            IEnumerable<GroupJoin_Order> inner = new List<GroupJoin_Order> {
                new GroupJoin_Order { CustomerID = 1, Total = 12.34m },
                new GroupJoin_Order { CustomerID = 4, Total = 23.45m },
                new GroupJoin_Order { CustomerID = 3, Total = 34.56m },
                new GroupJoin_Order { CustomerID = 3, Total = 45.67m },
                new GroupJoin_Order { CustomerID = 1, Total = 56.78m },
                new GroupJoin_Order { CustomerID = 1, Total = 67.89m },
                new GroupJoin_Order { CustomerID = 2, Total = 78.90m }
            };

            Func<GroupJoin_Customer, int> outerKeySelector = c => c.CustomerID;
            Func<GroupJoin_Order, int> innerKeySelector = o => o.CustomerID;

            Func<GroupJoin_Customer, IEnumerable<GroupJoin_Order>, GroupJoin_CustomerOrder> resultSelector
                = delegate(GroupJoin_Customer c, IEnumerable<GroupJoin_Order> orders)
                {
                    decimal total = 0.0m;

                    foreach (GroupJoin_Order o in orders)
                        total += o.Total;

                    return new GroupJoin_CustomerOrder { CustomerID = c.CustomerID, Name = c.Name, TotalOrders = total };
                };

            bool exception1 = false;
            try
            {
                foreach (var i in Sequence.GroupJoin((IEnumerable<GroupJoin_Customer>)null, inner, outerKeySelector, innerKeySelector, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (var i in Sequence.GroupJoin(outer, (IEnumerable<GroupJoin_Order>)null, outerKeySelector, innerKeySelector, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            bool exception3 = false;
            try
            {
                foreach (var i in Sequence.GroupJoin(outer, inner, (Func<GroupJoin_Customer, int>)null, innerKeySelector, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception3 = true;
            }

            bool exception4 = false;
            try
            {
                foreach (var i in Sequence.GroupJoin(outer, inner, outerKeySelector, (Func<GroupJoin_Order, int>)null, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception4 = true;
            }

            bool exception5 = false;
            try
            {
                foreach (var i in Sequence.GroupJoin(outer, inner, outerKeySelector, innerKeySelector, (Func<GroupJoin_Customer, IEnumerable<GroupJoin_Order>, GroupJoin_CustomerOrder>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception5 = true;
            }

            Assert.IsTrue(exception1 && exception2 && exception3 && exception4 && exception5, "Sequence.GroupJoin<T, U, K, V> did not return the expected value (exceptions).");

            GroupJoin_CustomerOrder[] expected = new GroupJoin_CustomerOrder[] {
                new GroupJoin_CustomerOrder { CustomerID = 1, Name = bart, TotalOrders = 12.34m + 56.78m + 67.89m },
                new GroupJoin_CustomerOrder { CustomerID = 2, Name = bill, TotalOrders = 78.90m },
                new GroupJoin_CustomerOrder { CustomerID = 3, Name = john, TotalOrders = 34.56m + 45.67m },
                new GroupJoin_CustomerOrder { CustomerID = 4, Name = steve, TotalOrders = 23.45m },
                new GroupJoin_CustomerOrder { CustomerID = 5, Name = rob, TotalOrders = 0.0m }
            };

            IEnumerable<GroupJoin_CustomerOrder> actual = Sequence.GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector);

            long n = 0;
            foreach (var co in actual)
                n++;

            Assert.IsTrue(n == expected.Length, "Sequence.GroupJoin<T, U, K, V> did not return the expected value (test 1).");

            int j = 0;
            foreach (GroupJoin_CustomerOrder co in actual)
                Assert.IsTrue(co.Equals(expected[j++]), "Sequence.GroupJoin<T, U, K, V> did not return the expected value (test 1).");
        }

        private class GroupJoin_Customer
        {
            public int CustomerID;
            public string Name;
        }

        private class GroupJoin_Order
        {
            public int CustomerID;
            public decimal Total;
        }

        private class GroupJoin_CustomerOrder
        {
            public int CustomerID;
            public string Name;
            public decimal TotalOrders;

            public override bool Equals(object obj)
            {
                GroupJoin_CustomerOrder co = obj as GroupJoin_CustomerOrder;

                if (co == null)
                    return false;

                return co.CustomerID == CustomerID && co.TotalOrders == TotalOrders && object.ReferenceEquals(co.Name, Name);
            }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode() & Name.GetHashCode() & TotalOrders.GetHashCode();
            }
        }

        private void GroupJoinTest_2()
        {
            string bart = "Bart";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            GroupJoin_CustomerID c1 = new GroupJoin_CustomerID { CustomerID = 1 };
            GroupJoin_CustomerID c2 = new GroupJoin_CustomerID { CustomerID = 2 };
            GroupJoin_CustomerID c3 = new GroupJoin_CustomerID { CustomerID = 3 };
            GroupJoin_CustomerID c4 = new GroupJoin_CustomerID { CustomerID = 4 };

            IEnumerable<GroupJoin_Customer2> outer = new List<GroupJoin_Customer2> {
                new GroupJoin_Customer2 { CustomerID = c1, Name = bart },
                new GroupJoin_Customer2 { CustomerID = c2, Name = bill },
                new GroupJoin_Customer2 { CustomerID = c3, Name = john },
                new GroupJoin_Customer2 { CustomerID = c4, Name = steve }
            };

            IEnumerable<GroupJoin_Order2> inner = new List<GroupJoin_Order2> {
                new GroupJoin_Order2 { CustomerID = c1, Total = 12.34m },
                new GroupJoin_Order2 { CustomerID = c4, Total = 23.45m },
                new GroupJoin_Order2 { CustomerID = c3, Total = 34.56m },
                new GroupJoin_Order2 { CustomerID = c3, Total = 45.67m },
                new GroupJoin_Order2 { CustomerID = c1, Total = 56.78m },
                new GroupJoin_Order2 { CustomerID = c1, Total = 67.89m },
                new GroupJoin_Order2 { CustomerID = c2, Total = 78.90m }
            };

            Func<GroupJoin_Customer2, GroupJoin_CustomerID> outerKeySelector = c => c.CustomerID;
            Func<GroupJoin_Order2, GroupJoin_CustomerID> innerKeySelector = o => o.CustomerID;

            Func<GroupJoin_Customer2, IEnumerable<GroupJoin_Order2>, GroupJoin_CustomerOrder2> resultSelector
                = delegate(GroupJoin_Customer2 c, IEnumerable<GroupJoin_Order2> orders)
                {
                    decimal total = 0.0m;

                    foreach (GroupJoin_Order2 o in orders)
                        total += o.Total;

                    return new GroupJoin_CustomerOrder2 { CustomerID = c.CustomerID, Name = c.Name, TotalOrders = total };
                };

            GroupJoin_CustomerOrder2[] expected = new GroupJoin_CustomerOrder2[] {
                new GroupJoin_CustomerOrder2 { CustomerID = c1, Name = bart, TotalOrders = 12.34m + 56.78m + 67.89m },
                new GroupJoin_CustomerOrder2 { CustomerID = c2, Name = bill, TotalOrders = 78.90m },
                new GroupJoin_CustomerOrder2 { CustomerID = c3, Name = john, TotalOrders = 34.56m + 45.67m },
                new GroupJoin_CustomerOrder2 { CustomerID = c4, Name = steve, TotalOrders = 23.45m }
            };

            IEnumerable<GroupJoin_CustomerOrder2> actual = Sequence.GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector);

            long n = 0;
            foreach (var co in actual)
                n++;

            Assert.IsTrue(n == expected.Length, "Sequence.GroupJoin<T, U, K, V> did not return the expected value (test 2).");

            int j = 0;
            foreach (GroupJoin_CustomerOrder2 co in actual)
                Assert.IsTrue(co.Equals(expected[j++]), "Sequence.GroupJoin<T, U, K, V> did not return the expected value (test 2).");
        }

        private class GroupJoin_Customer2
        {
            public GroupJoin_CustomerID CustomerID;
            public string Name;
        }

        private class GroupJoin_Order2
        {
            public GroupJoin_CustomerID CustomerID;
            public decimal Total;
        }

        private class GroupJoin_CustomerOrder2
        {
            public GroupJoin_CustomerID CustomerID;
            public string Name;
            public decimal TotalOrders;

            public override bool Equals(object obj)
            {
                GroupJoin_CustomerOrder2 co = obj as GroupJoin_CustomerOrder2;

                if (co == null)
                    return false;

                return co.CustomerID == CustomerID && co.TotalOrders == TotalOrders && object.ReferenceEquals(co.Name, Name);
            }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode() & Name.GetHashCode() & TotalOrders.GetHashCode();
            }
        }

        private class GroupJoin_CustomerID
        {
            public int CustomerID;

            public override bool Equals(object obj)
            {
                GroupJoin_CustomerID id = obj as GroupJoin_CustomerID;

                if (id == null)
                    return false;

                return id.CustomerID == CustomerID;
            }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode();
            }
        }

        #endregion

        #region 1.10.3 Intersect

        /// <summary>
        ///A test for Intersect&lt;&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void IntersectTest()
        {
            IntersectTest_1();
            IntersectTest_2();
            IntersectTest_3();
            IntersectTest_4();
        }

        private void IntersectTest_1()
        {
            IEnumerable<int> first = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> second = new List<int> { 3, 4, 5, 1, 7 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Intersect(first, null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Intersect(null, second))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Intersect<T> did not return the expected value (exceptions).");

            IEnumerable<int> expected = new List<int> { 1, 5 };
            IEnumerable<int> actual = Sequence.Intersect(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Intersect<T> did not return the expected value (test 1).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Intersect<T> did not return the expected value (test 1).");
            }
        }

        private void IntersectTest_2()
        {
            IEnumerable<int> first = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> second = new List<int> { };

            IEnumerable<int> expected = new List<int> { };
            IEnumerable<int> actual = Sequence.Intersect(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Intersect<T> did not return the expected value (test 2).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Intersect<T> did not return the expected value (test 2).");
            }
        }

        private void IntersectTest_3()
        {
            IEnumerable<int> first = new List<int> { };
            IEnumerable<int> second = new List<int> { 3, 4, 5, 1, 7 };

            IEnumerable<int> expected = new List<int> { };
            IEnumerable<int> actual = Sequence.Intersect(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Intersect<T> did not return the expected value (test 3).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Intersect<T> did not return the expected value (test 3).");
            }
        }

        private void IntersectTest_4()
        {
            string j1 = "John";
            IEnumerable<string> first = new List<string> { j1, "Bill", "John", "Bart", "Scott" };
            IEnumerable<string> second = new List<string> { "Steve", "Bart", "Rob", "Steve", j1 };

            IEnumerable<string> expected = new List<string> { "John", "Bart" };
            IEnumerable<string> actual = Sequence.Intersect(first, second);

            long n = 0;
            foreach (string s in expected)
                n++;
            foreach (string s in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Intersect<T> did not return the expected value (test 4).");

            IEnumerator<string> enumerator = actual.GetEnumerator();
            foreach (string s in actual)
            {
                enumerator.MoveNext();
                Assert.IsTrue(s.Equals(enumerator.Current), "Sequence.Intersect<T> did not return the expected value (test 4).");
            }
        }

        #endregion

        #region 1.6.1 Join

        /// <summary>
        ///A test for Join&lt;,,,&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;U&gt;, Func&lt;T,K&gt;, Func&lt;U,K&gt;, Func&lt;T,U,V&gt;)
        ///</summary>
        [TestMethod()]
        public void JoinTest()
        {
            JoinTest_1();
            JoinTest_2();
        }

        private void JoinTest_1()
        {
            string bart = "Bart";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            IEnumerable<Join_Customer> outer = new List<Join_Customer> {
                new Join_Customer { CustomerID = 1, Name = bart },
                new Join_Customer { CustomerID = 2, Name = bill },
                new Join_Customer { CustomerID = 3, Name = john },
                new Join_Customer { CustomerID = 4, Name = steve }
            };

            IEnumerable<Join_Order> inner = new List<Join_Order> {
                new Join_Order { CustomerID = 1, Total = 12.34m },
                new Join_Order { CustomerID = 4, Total = 23.45m },
                new Join_Order { CustomerID = 3, Total = 34.56m },
                new Join_Order { CustomerID = 3, Total = 45.67m },
                new Join_Order { CustomerID = 1, Total = 56.78m },
                new Join_Order { CustomerID = 1, Total = 67.89m },
                new Join_Order { CustomerID = 2, Total = 78.90m }
            };

            Func<Join_Customer, int> outerKeySelector = c => c.CustomerID;
            Func<Join_Order, int> innerKeySelector = o => o.CustomerID;

            Func<Join_Customer, Join_Order, Join_CustomerOrder> resultSelector = ( c, o) => new Join_CustomerOrder { CustomerID = c.CustomerID, Name = c.Name, Total = o.Total };

            bool exception1 = false;
            try
            {
                foreach (var i in Sequence.Join((IEnumerable<Join_Customer>)null, inner, outerKeySelector, innerKeySelector, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (var i in Sequence.Join(outer, (IEnumerable<Join_Order>)null, outerKeySelector, innerKeySelector, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            bool exception3 = false;
            try
            {
                foreach (var i in Sequence.Join(outer, inner, (Func<Join_Customer, int>)null, innerKeySelector, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception3 = true;
            }

            bool exception4 = false;
            try
            {
                foreach (var i in Sequence.Join(outer, inner, outerKeySelector, (Func<Join_Order, int>)null, resultSelector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception4 = true;
            }

            bool exception5 = false;
            try
            {
                foreach (var i in Sequence.Join(outer, inner, outerKeySelector, innerKeySelector, (Func<Join_Customer, Join_Order, Join_CustomerOrder>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception5 = true;
            }

            Assert.IsTrue(exception1 && exception2 && exception3 && exception4 && exception5, "Sequence.Join<T, U, K, V> did not return the expected value (exceptions).");

            Join_CustomerOrder[] expected = new Join_CustomerOrder[] {
                new Join_CustomerOrder { CustomerID = 1, Name = bart, Total = 12.34m },
                new Join_CustomerOrder { CustomerID = 1, Name = bart, Total = 56.78m },
                new Join_CustomerOrder { CustomerID = 1, Name = bart, Total = 67.89m },
                new Join_CustomerOrder { CustomerID = 2, Name = bill, Total = 78.90m },
                new Join_CustomerOrder { CustomerID = 3, Name = john, Total = 34.56m },
                new Join_CustomerOrder { CustomerID = 3, Name = john, Total = 45.67m },
                new Join_CustomerOrder { CustomerID = 4, Name = steve, Total = 23.45m }
            };

            IEnumerable<Join_CustomerOrder> actual = Sequence.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector);

            long n = 0;
            foreach (var co in actual)
                n++;

            Assert.IsTrue(n == expected.Length, "Sequence.Join<T, U, K, V> did not return the expected value (test 1).");

            int j = 0;
            foreach (Join_CustomerOrder co in actual)
                Assert.IsTrue(co.Equals(expected[j++]), "Sequence.Join<T, U, K, V> did not return the expected value (test 1).");
        }

        private class Join_Customer
        {
            public int CustomerID;
            public string Name;
        }

        private class Join_Order
        {
            public int CustomerID;
            public decimal Total;
        }

        private class Join_CustomerOrder
        {
            public int CustomerID;
            public string Name;
            public decimal Total;

            public override bool Equals(object obj)
            {
                Join_CustomerOrder co = obj as Join_CustomerOrder;

                if (co == null)
                    return false;

                return co.CustomerID == CustomerID && co.Total == Total && object.ReferenceEquals(co.Name, Name);
            }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode() & Name.GetHashCode() & Total.GetHashCode();
            }
        }

        private void JoinTest_2()
        {
            string bart = "Bart";
            string bill = "Bill";
            string john = "John";
            string steve = "Steve";

            Join_CustomerID c1 = new Join_CustomerID { CustomerID = 1 };
            Join_CustomerID c2 = new Join_CustomerID { CustomerID = 2 };
            Join_CustomerID c3 = new Join_CustomerID { CustomerID = 3 };
            Join_CustomerID c4 = new Join_CustomerID { CustomerID = 4 };

            IEnumerable<Join_Customer2> outer = new List<Join_Customer2> {
                new Join_Customer2 { CustomerID = c1, Name = bart },
                new Join_Customer2 { CustomerID = c2, Name = bill },
                new Join_Customer2 { CustomerID = c3, Name = john },
                new Join_Customer2 { CustomerID = c4, Name = steve }
            };

            IEnumerable<Join_Order2> inner = new List<Join_Order2> {
                new Join_Order2 { CustomerID = c1, Total = 12.34m },
                new Join_Order2 { CustomerID = c4, Total = 23.45m },
                new Join_Order2 { CustomerID = c3, Total = 34.56m },
                new Join_Order2 { CustomerID = c3, Total = 45.67m },
                new Join_Order2 { CustomerID = c1, Total = 56.78m },
                new Join_Order2 { CustomerID = c1, Total = 67.89m },
                new Join_Order2 { CustomerID = c2, Total = 78.90m }
            };

            Func<Join_Customer2, Join_CustomerID> outerKeySelector = c => c.CustomerID;
            Func<Join_Order2, Join_CustomerID> innerKeySelector = o => o.CustomerID;

            Func<Join_Customer2, Join_Order2, Join_CustomerOrder2> resultSelector = ( c, o) => new Join_CustomerOrder2 { CustomerID = c.CustomerID, Name = c.Name, Total = o.Total };

            Join_CustomerOrder2[] expected = new Join_CustomerOrder2[] {
                new Join_CustomerOrder2 { CustomerID = c1, Name = bart, Total = 12.34m },
                new Join_CustomerOrder2 { CustomerID = c1, Name = bart, Total = 56.78m },
                new Join_CustomerOrder2 { CustomerID = c1, Name = bart, Total = 67.89m },
                new Join_CustomerOrder2 { CustomerID = c2, Name = bill, Total = 78.90m },
                new Join_CustomerOrder2 { CustomerID = c3, Name = john, Total = 34.56m },
                new Join_CustomerOrder2 { CustomerID = c3, Name = john, Total = 45.67m },
                new Join_CustomerOrder2 { CustomerID = c4, Name = steve, Total = 23.45m }
            };

            IEnumerable<Join_CustomerOrder2> actual = Sequence.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector);

            long n = 0;
            foreach (var co in actual)
                n++;

            Assert.IsTrue(n == expected.Length, "Sequence.Join<T, U, K, V> did not return the expected value (test 2).");

            int j = 0;
            foreach (Join_CustomerOrder2 co in actual)
                Assert.IsTrue(co.Equals(expected[j++]), "Sequence.Join<T, U, K, V> did not return the expected value (test 2).");
        }

        private class Join_Customer2
        {
            public Join_CustomerID CustomerID;
            public string Name;
        }

        private class Join_Order2
        {
            public Join_CustomerID CustomerID;
            public decimal Total;
        }

        private class Join_CustomerOrder2
        {
            public Join_CustomerID CustomerID;
            public string Name;
            public decimal Total;

            public override bool Equals(object obj)
            {
                Join_CustomerOrder2 co = obj as Join_CustomerOrder2;

                if (co == null)
                    return false;

                return co.CustomerID == CustomerID && co.Total == Total && object.ReferenceEquals(co.Name, Name);
            }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode() & Name.GetHashCode() & Total.GetHashCode();
            }
        }

        private class Join_CustomerID
        {
            public int CustomerID;

            public override bool Equals(object obj)
            {
                Join_CustomerID id = obj as Join_CustomerID;

                if (id == null)
                    return false;

                return id.CustomerID == CustomerID;
            }

            public override int GetHashCode()
            {
                return CustomerID.GetHashCode();
            }
        }

        #endregion

        #region 1.13.3 Last

        /// <summary>
        ///A test for Last&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void LastTest()
        {
            LastTest_1();
            LastTest_2();
            LastTest_3();
            LastTest_4();
        }

        private void LastTest_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };

            bool exception1 = false;
            try
            {
                Sequence.Last<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Last<T> did not return the expected value (exceptions).");

            int expected = 3;
            int actual = Sequence.Last(source);

            Assert.AreEqual(expected, actual, "Sequence.Last<T> did not return the expected value (test 1).");
        }

        private void LastTest_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "John", "Bart", "Rob", s };

            string expected = s;
            string actual = Sequence.Last(source);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.Last<T> did not return the expected value (test 2).");
        }

        private void LastTest_3()
        {
            IEnumerable<int> source = new int[] { };

            bool exception = false;
            try
            {
                int actual = Sequence.Last(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Last<T> did not return the expected value (test 3).");
        }

        private void LastTest_4()
        {
            IEnumerable<string> source = new string[] { };

            bool exception = false;
            try
            {
                string actual = Sequence.Last(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Last<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Last&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void LastTest1()
        {
            LastTest1_1();
            LastTest1_2();
            LastTest1_3();
            LastTest1_4();
            LastTest1_5();
            LastTest1_6();
        }

        private void LastTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.Last<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Last<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Last<T> did not return the expected value (exceptions).");

            int expected = 4;
            int actual = Sequence.Last(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Last<T> did not return the expected value (test 1).");
        }

        private void LastTest1_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "Bill", "John", s, "Rob" };
            Func<string, bool> predicate = ss => ss.StartsWith("B");

            string expected = s;
            string actual = Sequence.Last(source, predicate);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.Last<T> did not return the expected value (test 2).");
        }

        private void LastTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.Last(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Last<T> did not return the expected value (test 3).");
        }

        private void LastTest1_4()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.Last(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Last<T> did not return the expected value (test 4).");
        }

        private void LastTest1_5()
        {
            IEnumerable<int> source = new int[] { 3, 5, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.Last(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Last<T> did not return the expected value (test 5).");
        }

        private void LastTest1_6()
        {
            IEnumerable<string> source = new string[] { "John", "Steve" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.Last(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Last<T> did not return the expected value (test 6).");
        }

        #endregion

        #region 1.13.4 LastOrDefault

        /// <summary>
        ///A test for LastOrDefault&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void LastOrDefaultTest()
        {
            LastOrDefaultTest_1();
            LastOrDefaultTest_2();
            LastOrDefaultTest_3();
            LastOrDefaultTest_4();
        }

        private void LastOrDefaultTest_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };

            bool exception1 = false;
            try
            {
                Sequence.LastOrDefault<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.LastOrDefault<T> did not return the expected value (exceptions).");

            int expected = 3;
            int actual = Sequence.LastOrDefault(source);

            Assert.AreEqual(expected, actual, "Sequence.LastOrDefault<T> did not return the expected value (test 1).");
        }

        private void LastOrDefaultTest_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "John", "Bart", "Rob", s };

            string expected = s;
            string actual = Sequence.LastOrDefault(source);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.LastOrDefault<T> did not return the expected value (test 2).");
        }

        private void LastOrDefaultTest_3()
        {
            IEnumerable<int> source = new int[] { };

            int expected = default(int);
            int actual = Sequence.LastOrDefault(source);

            Assert.AreEqual(expected, actual, "Sequence.LastOrDefault<T> did not return the expected value (test 3).");
        }

        private void LastOrDefaultTest_4()
        {
            IEnumerable<string> source = new string[] { };

            string expected = default(string);
            string actual = Sequence.LastOrDefault(source);

            Assert.IsTrue(object.Equals(actual, expected), "Sequence.LastOrDefault<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for LastOrDefault&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void LastOrDefaultTest1()
        {
            LastOrDefaultTest1_1();
            LastOrDefaultTest1_2();
            LastOrDefaultTest1_3();
            LastOrDefaultTest1_4();
            LastOrDefaultTest1_5();
            LastOrDefaultTest1_6();
        }

        private void LastOrDefaultTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.LastOrDefault<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.LastOrDefault<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.LastOrDefault<T> did not return the expected value (exceptions).");

            int expected = 4;
            int actual = Sequence.LastOrDefault(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.LastOrDefault<T> did not return the expected value (test 1).");
        }

        private void LastOrDefaultTest1_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "Bill", "John", s, "Rob" };
            Func<string, bool> predicate = ss => ss.StartsWith("B");

            string expected = s;
            string actual = Sequence.LastOrDefault(source, predicate);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.LastOrDefault<T> did not return the expected value (test 2).");
        }

        private void LastOrDefaultTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            int expected = default(int);
            int actual = Sequence.LastOrDefault(source, predicate);

            Assert.IsTrue(expected == actual, "Sequence.LastOrDefault<T> did not return the expected value (test 3).");
        }

        private void LastOrDefaultTest1_4()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, bool> predicate = s => s.StartsWith("B");

            string expected = default(string);
            string actual = Sequence.LastOrDefault(source, predicate);

            Assert.IsTrue(object.Equals(expected, actual), "Sequence.LastOrDefault<T> did not return the expected value (test 4).");
        }

        private void LastOrDefaultTest1_5()
        {
            IEnumerable<int> source = new int[] { 3, 5, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;

            int expected = default(int);
            int actual = Sequence.LastOrDefault(source, predicate);

            Assert.IsTrue(expected == actual, "Sequence.LastOrDefault<T> did not return the expected value (test 5).");
        }

        private void LastOrDefaultTest1_6()
        {
            IEnumerable<string> source = new string[] { "John", "Steve" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            string expected = default(string);
            string actual = Sequence.LastOrDefault(source, predicate);

            Assert.IsTrue(object.Equals(expected, actual), "Sequence.LastOrDefault<T> did not return the expected value (test 6).");
        }

        #endregion

        #region 1.16.2 LongCount

        /// <summary>
        ///A test for LongCount&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void LongCountTest()
        {
            LongCountTest_1();
            LongCountTest_2();
            LongCountTest_3();
        }

        private void LongCountTest_1()
        {
            IEnumerable<int> source = new List<int> { };

            bool exception1 = false;
            try
            {
                Sequence.LongCount<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.LongCount<T> did not return the expected value (exceptions).");

            long expected = 0;
            long actual;

            actual = Sequence.LongCount(source);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 1).");
        }

        private void LongCountTest_2()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3 };

            long expected = ((ICollection<int>)source).Count;
            long actual;

            actual = Sequence.LongCount(source);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 2).");
        }

        private void LongCountTest_3()
        {
            long n = 5;
            IEnumerable<int> source = new LongCountTest_Helper<int>(n);

            long expected = n;
            long actual;

            actual = Sequence.LongCount(source);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 3).");
        }

        private class LongCountTest_Helper<T> : IEnumerable<T>
        {
            private long n;

            public LongCountTest_Helper(long n)
            {
                this.n = n;
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (long i = 0; i < n; i++)
                    yield return default(T);
            }

            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        ///A test for LongCount&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void LongCountTest1()
        {
            LongCountTest1_1();
            LongCountTest1_2();
            LongCountTest1_3();
            LongCountTest1_4();
            LongCountTest1_5();
        }

        private void LongCountTest1_1()
        {
            IEnumerable<int> source = new List<int> { 1, 4, 3, 3, 12, 8, -1 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.LongCount<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.LongCount<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.LongCount<T> did not return the expected value (exceptions).");

            long expected = 3;
            long actual;

            actual = Sequence.LongCount(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 1).");
        }

        private void LongCountTest1_2()
        {
            IEnumerable<int> source = new List<int> { 1, 4, 3, 3, 12, 8, -1 };
            Func<int, bool> predicate = i => i == 0;

            long expected = 0;
            long actual;

            actual = Sequence.LongCount(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 2).");
        }

        private void LongCountTest1_3()
        {
            IEnumerable<string> source = new List<string> { "Bill", "Bart", "John", "Steve" };
            Func<string, bool> predicate = s => s.Length == 4;

            long expected = 3;
            long actual;

            actual = Sequence.LongCount(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 3).");
        }

        private void LongCountTest1_4()
        {
            IEnumerable<string> source = new List<string> { "Bill", "Bart", "John", "Steve" };
            Func<string, bool> predicate = s => s.Length < 4;

            long expected = 0;
            long actual;

            actual = Sequence.LongCount(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 4).");
        }

        private void LongCountTest1_5()
        {
            IEnumerable<string> source = new List<string> { };
            Func<string, bool> predicate = s => s.Length == 4;

            long expected = 0;
            long actual;

            actual = Sequence.LongCount(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.LongCount<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.16.5 Max

        /// <summary>
        ///A test for Max (IEnumerable&lt;decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest()
        {
            MaxTest_1();
            MaxTest_2();
            MaxTest_3();
        }

        private void MaxTest_1()
        {
            IEnumerable<decimal?> source = new List<decimal?> { };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            decimal? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest_2()
        {
            IEnumerable<decimal?> source = new List<decimal?> { null, null, null };

            decimal? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 2).");
        }

        private void MaxTest_3()
        {
            IEnumerable<decimal?> source = new List<decimal?> { 30.5m, null, 18.2m, 22.9m, null, 46.3m, 38.0m };

            decimal? expected = 46.3m;
            decimal? actual = Sequence.Max(source);

            Assert.IsTrue(actual == expected, "Sequence.Max did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest1()
        {
            MaxTest1_1();
            MaxTest1_2();
        }

        private void MaxTest1_1()
        {
            IEnumerable<decimal> source = new decimal[] { 30.5m, 18.2m, 22.9m, 46.3m, 38.0m };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            decimal expected = 46.3m;
            decimal actual = Sequence.Max(source);

            Assert.AreEqual(expected, actual, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest1_2()
        {
            IEnumerable<decimal> source = new decimal[] { };

            bool exception = false;
            try
            {
                Sequence.Max(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;double?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest2()
        {
            MaxTest2_1();
            MaxTest2_2();
            MaxTest2_3();
        }

        private void MaxTest2_1()
        {
            IEnumerable<double?> source = new List<double?> { };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            double? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest2_2()
        {
            IEnumerable<double?> source = new List<double?> { null, null, null };

            double? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 2).");
        }

        private void MaxTest2_3()
        {
            IEnumerable<double?> source = new List<double?> { 3.0, null, 1.0, 4.0, 12.0, null, 5.0, 8.0 };

            double? expected = 12.0;
            double? actual = Sequence.Max(source);

            Assert.IsTrue(actual == expected, "Sequence.Max did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;double&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest3()
        {
            MaxTest3_1();
            MaxTest3_2();
        }

        public void MaxTest3_1()
        {
            IEnumerable<double> source = new double[] { 3.0, 1.0, 4.0, 12.0, 5.0, 8.0 };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<double>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            double expected = 12.0;
            double actual = Sequence.Max(source);

            Assert.AreEqual(expected, actual, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest3_2()
        {
            IEnumerable<double> source = new double[] { };

            bool exception = false;
            try
            {
                double actual = Sequence.Max(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;int?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest4()
        {
            MaxTest4_1();
            MaxTest4_2();
            MaxTest4_3();
        }

        private void MaxTest4_1()
        {
            IEnumerable<int?> source = new List<int?> { };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            int? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest4_2()
        {
            IEnumerable<int?> source = new List<int?> { null, null, null };

            int? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 2).");
        }

        private void MaxTest4_3()
        {
            IEnumerable<int?> source = new List<int?> { 3, null, 8, 7, null, 2, 4 };

            int? expected = 8;
            int? actual = Sequence.Max(source);

            Assert.IsTrue(actual == expected, "Sequence.Max did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;int&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest5()
        {
            MaxTest5_1();
            MaxTest5_2();
        }

        public void MaxTest5_1()
        {
            IEnumerable<int> source = new int[] { 3, 8, 7, 2, 4 };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<int>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            int expected = 8;
            int actual = Sequence.Max(source);

            Assert.AreEqual(expected, actual, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest5_2()
        {
            IEnumerable<int> source = new int[] { };

            bool exception = false;
            try
            {
                int actual = Sequence.Max(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;long?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest6()
        {
            MaxTest6_1();
            MaxTest6_2();
            MaxTest6_3();
        }

        private void MaxTest6_1()
        {
            IEnumerable<long?> source = new List<long?> { };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            long? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest6_2()
        {
            IEnumerable<long?> source = new List<long?> { null, null, null };

            long? actual = Sequence.Max(source);

            Assert.IsTrue(actual == null, "Sequence.Max did not return the expected value (test 2).");
        }

        private void MaxTest6_3()
        {
            IEnumerable<long?> source = new List<long?> { 1234, null, 6789, 4567, null, 2345 };

            long? expected = 6789;
            long? actual = Sequence.Max(source);

            Assert.IsTrue(actual == expected, "Sequence.Max did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max (IEnumerable&lt;long&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest7()
        {
            MaxTest7_1();
            MaxTest7_2();
        }

        public void MaxTest7_1()
        {
            IEnumerable<long> source = new long[] { 1234, 6789, 4567, 2345 };

            long expected = 6789;
            long actual = Sequence.Max(source);

            Assert.AreEqual(expected, actual, "Sequence.Max did not return the expected value (test 1).");
        }

        private void MaxTest7_2()
        {
            IEnumerable<long> source = new long[] { };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<long>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max did not return the expected value (exceptions).");

            bool exception = false;
            try
            {
                long actual = Sequence.Max(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,S&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest8()
        {
            MaxTest8_1();
            MaxTest8_2();
            MaxTest8_3();
            MaxTest8_4();
        }

        private void MaxTest8_1()
        {
            string[] source = new string[] { "John", "Bart", "Bill", "Steve", "Rob" };

            Func<string, int> selector = s => s.Length;

            bool exception1 = false;
            try
            {
                Sequence.Max<string, int>(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max<string, int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            int expected = 5;
            int actual = Sequence.Max<string, int>(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest8_2()
        {
            MaxTest9_Helper max = new MaxTest9_Helper(5);

            MaxTest8_Helper[] source = new MaxTest8_Helper[] { 
                new MaxTest8_Helper(new MaxTest9_Helper(2)),
                new MaxTest8_Helper(max),
                new MaxTest8_Helper(new MaxTest9_Helper(3))
            };

            Func<MaxTest8_Helper, MaxTest9_Helper> selector = h => h.H;

            MaxTest9_Helper actual = Sequence.Max<MaxTest8_Helper, MaxTest9_Helper>(source, selector);

            Assert.IsTrue(object.ReferenceEquals(max, actual), "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private void MaxTest8_3()
        {
            MaxTest8_Helper2[] source = new MaxTest8_Helper2[] { 
                new MaxTest8_Helper2(new MaxTest9_Helper2()),
                new MaxTest8_Helper2(new MaxTest9_Helper2())
            };

            Func<MaxTest8_Helper2, MaxTest9_Helper2> selector = h => h.H;

            MaxTest9_Helper2 actual = Sequence.Max<MaxTest8_Helper2, MaxTest9_Helper2>(source, selector);

            Assert.IsTrue(actual == default(MaxTest9_Helper2), "Sequence.Max<T> did not return the expected value (test 3).");
        }

        private void MaxTest8_4()
        {
            string[] source = new string[] { };

            Func<string, int> selector = s => s.Length;

            bool exception = false;
            try
            {
                Sequence.Max<string, int>(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max<T> did not return the expected value (test 4).");
        }

        private class MaxTest8_Helper
        {
            public MaxTest9_Helper H;

            public MaxTest8_Helper(MaxTest9_Helper h)
            {
                H = h;
            }
        }

        private class MaxTest8_Helper2
        {
            public MaxTest9_Helper2 H;

            public MaxTest8_Helper2(MaxTest9_Helper2 h)
            {
                H = h;
            }
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest9()
        {
            MaxTest9_1();
            MaxTest9_2();
            MaxTest9_3();
            MaxTest9_4();
        }

        private void MaxTest9_1()
        {
            string[] source = new string[] { "John", "Bart", "Bill", "Steve", "Rob" };

            bool exception1 = false;
            try
            {
                Sequence.Max((IEnumerable<string>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Max<T> did not return the expected value (exceptions).");

            string expected = source[3];
            string actual = Sequence.Max(source);

            Assert.IsTrue(object.ReferenceEquals(expected, actual), "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest9_2()
        {
            string[] source = new string[] { };

            bool exception = false;
            try
            {
                string actual = Sequence.Max(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private void MaxTest9_3()
        {
            MaxTest9_Helper max = new MaxTest9_Helper(5);
            MaxTest9_Helper[] source = new MaxTest9_Helper[] { new MaxTest9_Helper(3), max, new MaxTest9_Helper(2) };

            MaxTest9_Helper actual = Sequence.Max(source);

            Assert.IsTrue(object.ReferenceEquals(max, actual), "Sequence.Max<T> did not return the expected value (test 3).");
        }

        private void MaxTest9_4()
        {
            MaxTest9_Helper2[] source = new MaxTest9_Helper2[] { new MaxTest9_Helper2(), new MaxTest9_Helper2() };

            MaxTest9_Helper2 actual = Sequence.Max(source);

            Assert.IsTrue(actual == default(MaxTest9_Helper2), "Sequence.Max<T> did not return the expected value (test 4).");
        }

        private class MaxTest9_Helper : IComparable
        {
            public int I;

            public MaxTest9_Helper(int i)
            {
                I = i;
            }

            public int CompareTo(object obj)
            {
                if (obj is MaxTest9_Helper)
                    return I.CompareTo(((MaxTest9_Helper)obj).I);
                else
                    throw new Exception(); // quick-n-dirty
            }
        }

        private class MaxTest9_Helper2
        {
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest10()
        {
            MaxTest10_1();
            MaxTest10_2();
            MaxTest10_3();
        }

        private void MaxTest10_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { dN =  34.56m },
                new MaxTest_Helper { dN =  null   },
                new MaxTest_Helper { dN = -45.67m },
                new MaxTest_Helper { dN =  12.34m },
                new MaxTest_Helper { dN =  56.78m }
            };

            Func<MaxTest_Helper, decimal?> selector = m => m.dN;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            decimal? expected = 56.78m;
            decimal? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest10_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, decimal?> selector = m => m.dN;

            decimal? expected = null;
            decimal? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private void MaxTest10_3()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { dN = null },
                new MaxTest_Helper { dN = null },
                new MaxTest_Helper { dN = null }
            };

            Func<MaxTest_Helper, decimal?> selector = m => m.dN;

            decimal? expected = null;
            decimal? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest11()
        {
            MaxTest11_1();
            MaxTest11_2();
        }

        private void MaxTest11_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { d =  34.56m },
                new MaxTest_Helper { d = -45.67m },
                new MaxTest_Helper { d =  12.34m },
                new MaxTest_Helper { d =  56.78m }
            };

            Func<MaxTest_Helper, decimal> selector = m => m.d;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            decimal expected = 56.78m;
            decimal actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest11_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, decimal> selector = m => m.d;

            bool exception = false;
            try
            {
                decimal actual = Sequence.Max(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest12()
        {
            MaxTest12_1();
            MaxTest12_2();
            MaxTest12_3();
        }

        private void MaxTest12_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { DN =  34.56 },
                new MaxTest_Helper { DN =  null   },
                new MaxTest_Helper { DN = -45.67 },
                new MaxTest_Helper { DN =  12.34 },
                new MaxTest_Helper { DN =  56.78 }
            };

            Func<MaxTest_Helper, double?> selector = m => m.DN;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            double? expected = 56.78;
            double? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest12_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, double?> selector = m => m.DN;

            double? expected = null;
            double? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private void MaxTest12_3()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { DN = null },
                new MaxTest_Helper { DN = null },
                new MaxTest_Helper { DN = null }
            };

            Func<MaxTest_Helper, double?> selector = m => m.DN;

            double? expected = null;
            double? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest13()
        {
            MaxTest13_1();
            MaxTest13_2();
        }

        private void MaxTest13_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { D =  34.56 },
                new MaxTest_Helper { D = -45.67 },
                new MaxTest_Helper { D =  12.34 },
                new MaxTest_Helper { D =  56.78 }
            };

            Func<MaxTest_Helper, double> selector = m => m.D;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, double>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            double expected = 56.78;
            double actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest13_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, double> selector = m => m.D;

            bool exception = false;
            try
            {
                double actual = Sequence.Max(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest14()
        {
            MaxTest14_1();
            MaxTest14_2();
            MaxTest14_3();
        }

        private void MaxTest14_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { IN =  34 },
                new MaxTest_Helper { IN =  null },
                new MaxTest_Helper { IN = -45 },
                new MaxTest_Helper { IN =  12 },
                new MaxTest_Helper { IN =  56 }
            };

            Func<MaxTest_Helper, int?> selector = m => m.IN;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            int? expected = 56;
            int? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest14_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, int?> selector = m => m.IN;

            int? expected = null;
            int? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private void MaxTest14_3()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { IN = null },
                new MaxTest_Helper { IN = null },
                new MaxTest_Helper { IN = null }
            };

            Func<MaxTest_Helper, int?> selector = m => m.IN;

            int? expected = null;
            int? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest15()
        {
            MaxTest15_1();
            MaxTest15_2();
        }

        private void MaxTest15_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { I =  34 },
                new MaxTest_Helper { I = -45 },
                new MaxTest_Helper { I =  12 },
                new MaxTest_Helper { I =  56 }
            };

            Func<MaxTest_Helper, int> selector = m => m.I;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, int>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            int expected = 56;
            int actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest15_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, int> selector = m => m.I;

            bool exception = false;
            try
            {
                int actual = Sequence.Max(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long?&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest16()
        {
            MaxTest16_1();
            MaxTest16_2();
            MaxTest16_3();
        }

        private void MaxTest16_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { LN =  3456 },
                new MaxTest_Helper { LN =  null },
                new MaxTest_Helper { LN = -4567 },
                new MaxTest_Helper { LN =  1234 },
                new MaxTest_Helper { LN =  5678 }
            };

            Func<MaxTest_Helper, long?> selector = m => m.LN;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            long? expected = 5678;
            long? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest16_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, long?> selector = m => m.LN;

            long? expected = null;
            long? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private void MaxTest16_3()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { LN = null },
                new MaxTest_Helper { LN = null },
                new MaxTest_Helper { LN = null }
            };

            Func<MaxTest_Helper, long?> selector = m => m.LN;

            long? expected = null;
            long? actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Max&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long&gt;)
        ///</summary>
        [TestMethod()]
        public void MaxTest17()
        {
            MaxTest17_1();
            MaxTest17_2();
        }

        private void MaxTest17_1()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> {
                new MaxTest_Helper { L =  3456 },
                new MaxTest_Helper { L = -4567 },
                new MaxTest_Helper { L =  1234 },
                new MaxTest_Helper { L =  5678 }
            };

            Func<MaxTest_Helper, long> selector = m => m.L;

            bool exception1 = false;
            try
            {
                Sequence.Max(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Max(source, (Func<MaxTest_Helper, long>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Max<T> did not return the expected value (exceptions).");

            long expected = 5678;
            long actual = Sequence.Max(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Max<T> did not return the expected value (test 1).");
        }

        private void MaxTest17_2()
        {
            IEnumerable<MaxTest_Helper> source = new List<MaxTest_Helper> { };

            Func<MaxTest_Helper, long> selector = m => m.L;

            bool exception = false;
            try
            {
                long actual = Sequence.Max(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Max<T> did not return the expected value (test 2).");
        }

        private class MaxTest_Helper
        {
            public decimal d = 0;
            public decimal? dN;
            public double D = 0;
            public double? DN;
            public int I = 0;
            public int? IN;
            public long L = 0;
            public long? LN;
        }

        #endregion

        #region 1.16.4 Min

        /// <summary>
        ///A test for Min (IEnumerable&lt;decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest()
        {
            MinTest_1();
            MinTest_2();
            MinTest_3();
        }

        private void MinTest_1()
        {
            IEnumerable<decimal?> source = new List<decimal?> { };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            decimal? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest_2()
        {
            IEnumerable<decimal?> source = new List<decimal?> { null, null, null };

            decimal? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 2).");
        }

        private void MinTest_3()
        {
            IEnumerable<decimal?> source = new List<decimal?> { 30.5m, null, 18.2m, 22.9m, null, 46.3m, 38.0m };

            decimal? expected = 18.2m;
            decimal? actual = Sequence.Min(source);

            Assert.IsTrue(actual == expected, "Sequence.Min did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest1()
        {
            MinTest1_1();
            MinTest1_2();
        }

        private void MinTest1_1()
        {
            IEnumerable<decimal> source = new decimal[] { 30.5m, 18.2m, 22.9m, 46.3m, 38.0m };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            decimal expected = 18.2m;
            decimal actual = Sequence.Min(source);

            Assert.AreEqual(expected, actual, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest1_2()
        {
            IEnumerable<decimal> source = new decimal[] { };

            bool exception = false;
            try
            {
                decimal actual = Sequence.Min(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;double?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest2()
        {
            MinTest2_1();
            MinTest2_2();
            MinTest2_3();
        }

        private void MinTest2_1()
        {
            IEnumerable<double?> source = new List<double?> { };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            double? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest2_2()
        {
            IEnumerable<double?> source = new List<double?> { null, null, null };

            double? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 2).");
        }

        private void MinTest2_3()
        {
            IEnumerable<double?> source = new List<double?> { 3.0, null, 1.0, 4.0, 12.0, null, 5.0, 8.0 };

            double? expected = 1.0;
            double? actual = Sequence.Min(source);

            Assert.IsTrue(actual == expected, "Sequence.Min did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;double&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest3()
        {
            MinTest3_1();
            MinTest3_2();
        }

        public void MinTest3_1()
        {
            IEnumerable<double> source = new double[] { 3.0, 1.0, 4.0, 12.0, 5.0, 8.0 };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<double>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            double expected = 1.0;
            double actual = Sequence.Min(source);

            Assert.AreEqual(expected, actual, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest3_2()
        {
            IEnumerable<double> source = new double[] { };

            bool exception = false;
            try
            {
                double actual = Sequence.Min(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;int?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest4()
        {
            MinTest4_1();
            MinTest4_2();
            MinTest4_3();
        }

        private void MinTest4_1()
        {
            IEnumerable<int?> source = new List<int?> { };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            int? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest4_2()
        {
            IEnumerable<int?> source = new List<int?> { null, null, null };

            int? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 2).");
        }

        private void MinTest4_3()
        {
            IEnumerable<int?> source = new List<int?> { 3, null, 8, 7, null, 2, 4 };

            int? expected = 2;
            int? actual = Sequence.Min(source);

            Assert.IsTrue(actual == expected, "Sequence.Min did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;int&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest5()
        {
            MinTest5_1();
            MinTest5_2();
        }

        public void MinTest5_1()
        {
            IEnumerable<int> source = new int[] { 3, 8, 7, 2, 4 };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<int>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.Min(source);

            Assert.AreEqual(expected, actual, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest5_2()
        {
            IEnumerable<int> source = new int[] { };

            bool exception = false;
            try
            {
                int actual = Sequence.Min(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;long?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest6()
        {
            MinTest6_1();
            MinTest6_2();
            MinTest6_3();
        }

        private void MinTest6_1()
        {
            IEnumerable<long?> source = new List<long?> { };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            long? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest6_2()
        {
            IEnumerable<long?> source = new List<long?> { null, null, null };

            long? actual = Sequence.Min(source);

            Assert.IsTrue(actual == null, "Sequence.Min did not return the expected value (test 2).");
        }

        private void MinTest6_3()
        {
            IEnumerable<long?> source = new List<long?> { 6789, null, 1234, 4567, null, 2345 };

            long? expected = 1234;
            long? actual = Sequence.Min(source);

            Assert.IsTrue(actual == expected, "Sequence.Min did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min (IEnumerable&lt;long&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest7()
        {
            MinTest7_1();
            MinTest7_2();
        }

        public void MinTest7_1()
        {
            IEnumerable<long> source = new long[] { 6789, 1234, 4567, 2345 };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<long>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min did not return the expected value (exceptions).");

            long expected = 1234;
            long actual = Sequence.Min(source);

            Assert.AreEqual(expected, actual, "Sequence.Min did not return the expected value (test 1).");
        }

        private void MinTest7_2()
        {
            IEnumerable<long> source = new long[] { };

            bool exception = false;
            try
            {
                long actual = Sequence.Min(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,S&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest8()
        {
            MinTest8_1();
            MinTest8_2();
            MinTest8_3();
            MinTest8_4();
        }

        private void MinTest8_1()
        {
            string[] source = new string[] { "John", "Bart", "Bill", "Steve", "Rob" };

            Func<string, int> selector = s => s.Length;

            bool exception1 = false;
            try
            {
                Sequence.Min<string, int>(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min<string, int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            int expected = 3;
            int actual = Sequence.Min<string, int>(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest8_2()
        {
            MinTest9_Helper min = new MinTest9_Helper(1);

            MinTest8_Helper[] source = new MinTest8_Helper[] { 
                new MinTest8_Helper(new MinTest9_Helper(2)),
                new MinTest8_Helper(min),
                new MinTest8_Helper(new MinTest9_Helper(3))
            };

            Func<MinTest8_Helper, MinTest9_Helper> selector = h => h.H;

            MinTest9_Helper actual = Sequence.Min<MinTest8_Helper, MinTest9_Helper>(source, selector);

            Assert.IsTrue(object.ReferenceEquals(min, actual), "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private void MinTest8_3()
        {
            MinTest8_Helper2[] source = new MinTest8_Helper2[] { 
                new MinTest8_Helper2(new MinTest9_Helper2()),
                new MinTest8_Helper2(new MinTest9_Helper2())
            };

            Func<MinTest8_Helper2, MinTest9_Helper2> selector = h => h.H;

            MinTest9_Helper2 actual = Sequence.Min<MinTest8_Helper2, MinTest9_Helper2>(source, selector);

            Assert.IsTrue(actual == default(MinTest9_Helper2), "Sequence.Min<T> did not return the expected value (test 3).");
        }

        private void MinTest8_4()
        {
            string[] source = new string[] { };

            Func<string, int> selector = s => s.Length;

            bool exception = false;
            try
            {
                Sequence.Min<string, int>(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min<T> did not return the expected value (test 4).");
        }

        private class MinTest8_Helper
        {
            public MinTest9_Helper H;

            public MinTest8_Helper(MinTest9_Helper h)
            {
                H = h;
            }
        }

        private class MinTest8_Helper2
        {
            public MinTest9_Helper2 H;

            public MinTest8_Helper2(MinTest9_Helper2 h)
            {
                H = h;
            }
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest9()
        {
            MinTest9_1();
            MinTest9_2();
            MinTest9_3();
            MinTest9_4();
        }

        private void MinTest9_1()
        {
            string[] source = new string[] { "John", "Bart", "Bill", "Steve", "Rob" };

            bool exception1 = false;
            try
            {
                Sequence.Min((IEnumerable<string>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Min<T> did not return the expected value (exceptions).");

            string expected = source[1];
            string actual = Sequence.Min(source);

            Assert.IsTrue(object.ReferenceEquals(expected, actual), "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest9_2()
        {
            string[] source = new string[] { };

            bool exception = false;
            try
            {
                string actual = Sequence.Min(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private void MinTest9_3()
        {
            MinTest9_Helper min = new MinTest9_Helper(1);
            MinTest9_Helper[] source = new MinTest9_Helper[] { new MinTest9_Helper(3), min, new MinTest9_Helper(2) };

            MinTest9_Helper actual = Sequence.Min(source);

            Assert.IsTrue(object.ReferenceEquals(min, actual), "Sequence.Min<T> did not return the expected value (test 3).");
        }

        private void MinTest9_4()
        {
            MinTest9_Helper2[] source = new MinTest9_Helper2[] { new MinTest9_Helper2(), new MinTest9_Helper2() };

            MinTest9_Helper2 actual = Sequence.Min(source);

            Assert.IsTrue(actual == default(MinTest9_Helper2), "Sequence.Min<T> did not return the expected value (test 4).");
        }

        private class MinTest9_Helper : IComparable
        {
            public int I;

            public MinTest9_Helper(int i)
            {
                I = i;
            }

            public int CompareTo(object obj)
            {
                if (obj is MinTest9_Helper)
                    return I.CompareTo(((MinTest9_Helper)obj).I);
                else
                    throw new Exception(); // quick-n-dirty
            }
        }

        private class MinTest9_Helper2
        {
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest10()
        {
            MinTest10_1();
            MinTest10_2();
            MinTest10_3();
        }

        private void MinTest10_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { dN =  34.56m },
                new MinTest_Helper { dN =  null   },
                new MinTest_Helper { dN = -45.67m },
                new MinTest_Helper { dN =  12.34m },
                new MinTest_Helper { dN =  56.78m }
            };

            Func<MinTest_Helper, decimal?> selector = m => m.dN;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            decimal? expected = -45.67m;
            decimal? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest10_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, decimal?> selector = m => m.dN;

            decimal? expected = null;
            decimal? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private void MinTest10_3()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { dN = null },
                new MinTest_Helper { dN = null },
                new MinTest_Helper { dN = null }
            };

            Func<MinTest_Helper, decimal?> selector = m => m.dN;

            decimal? expected = null;
            decimal? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest11()
        {
            MinTest11_1();
            MinTest11_2();
        }

        private void MinTest11_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { d =  34.56m },
                new MinTest_Helper { d = -45.67m },
                new MinTest_Helper { d =  12.34m },
                new MinTest_Helper { d =  56.78m }
            };

            Func<MinTest_Helper, decimal> selector = m => m.d;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            decimal expected = -45.67m;
            decimal actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest11_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, decimal> selector = m => m.d;

            bool exception = false;
            try
            {
                decimal actual = Sequence.Min(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest12()
        {
            MinTest12_1();
            MinTest12_2();
            MinTest12_3();
        }

        private void MinTest12_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { DN =  34.56 },
                new MinTest_Helper { DN =  null   },
                new MinTest_Helper { DN = -45.67 },
                new MinTest_Helper { DN =  12.34 },
                new MinTest_Helper { DN =  56.78 }
            };

            Func<MinTest_Helper, double?> selector = m => m.DN;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            double? expected = -45.67;
            double? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest12_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, double?> selector = m => m.DN;

            double? expected = null;
            double? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private void MinTest12_3()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { DN = null },
                new MinTest_Helper { DN = null },
                new MinTest_Helper { DN = null }
            };

            Func<MinTest_Helper, double?> selector = m => m.DN;

            double? expected = null;
            double? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest13()
        {
            MinTest13_1();
            MinTest13_2();
        }

        private void MinTest13_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { D =  34.56 },
                new MinTest_Helper { D = -45.67 },
                new MinTest_Helper { D =  12.34 },
                new MinTest_Helper { D =  56.78 }
            };

            Func<MinTest_Helper, double> selector = m => m.D;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, double>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            double expected = -45.67;
            double actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest13_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, double> selector = m => m.D;

            bool exception = false;
            try
            {
                double actual = Sequence.Min(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest14()
        {
            MinTest14_1();
            MinTest14_2();
            MinTest14_3();
        }

        private void MinTest14_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { IN =  34 },
                new MinTest_Helper { IN =  null },
                new MinTest_Helper { IN = -45 },
                new MinTest_Helper { IN =  12 },
                new MinTest_Helper { IN =  56 }
            };

            Func<MinTest_Helper, int?> selector = m => m.IN;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            int? expected = -45;
            int? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest14_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, int?> selector = m => m.IN;

            int? expected = null;
            int? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private void MinTest14_3()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { IN = null },
                new MinTest_Helper { IN = null },
                new MinTest_Helper { IN = null }
            };

            Func<MinTest_Helper, int?> selector = m => m.IN;

            int? expected = null;
            int? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest15()
        {
            MinTest15_1();
            MinTest15_2();
        }

        private void MinTest15_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { I =  34 },
                new MinTest_Helper { I = -45 },
                new MinTest_Helper { I =  12 },
                new MinTest_Helper { I =  56 }
            };

            Func<MinTest_Helper, int> selector = m => m.I;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, int>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            int expected = -45;
            int actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest15_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, int> selector = m => m.I;

            bool exception = false;
            try
            {
                int actual = Sequence.Min(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long?&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest16()
        {
            MinTest16_1();
            MinTest16_2();
            MinTest16_3();
        }

        private void MinTest16_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { LN =  3456 },
                new MinTest_Helper { LN =  null },
                new MinTest_Helper { LN = -4567 },
                new MinTest_Helper { LN =  1234 },
                new MinTest_Helper { LN =  5678 }
            };

            Func<MinTest_Helper, long?> selector = m => m.LN;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            long? expected = -4567;
            long? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest16_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, long?> selector = m => m.LN;

            long? expected = null;
            long? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private void MinTest16_3()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { LN = null },
                new MinTest_Helper { LN = null },
                new MinTest_Helper { LN = null }
            };

            Func<MinTest_Helper, long?> selector = m => m.LN;

            long? expected = null;
            long? actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Min&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long&gt;)
        ///</summary>
        [TestMethod()]
        public void MinTest17()
        {
            MinTest17_1();
            MinTest17_2();
        }

        private void MinTest17_1()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> {
                new MinTest_Helper { L =  3456 },
                new MinTest_Helper { L = -4567 },
                new MinTest_Helper { L =  1234 },
                new MinTest_Helper { L =  5678 }
            };

            Func<MinTest_Helper, long> selector = m => m.L;

            bool exception1 = false;
            try
            {
                Sequence.Min(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Min(source, (Func<MinTest_Helper, long>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Min<T> did not return the expected value (exceptions).");

            long expected = -4567;
            long actual = Sequence.Min(source, selector);

            Assert.AreEqual(expected, actual, "Sequence.Min<T> did not return the expected value (test 1).");
        }

        private void MinTest17_2()
        {
            IEnumerable<MinTest_Helper> source = new List<MinTest_Helper> { };

            Func<MinTest_Helper, long> selector = m => m.L;

            bool exception = false;
            try
            {
                long actual = Sequence.Min(source, selector);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Min<T> did not return the expected value (test 2).");
        }

        private class MinTest_Helper
        {
            public decimal d = 0;
            public decimal? dN;
            public double D = 0;
            public double? DN;
            public int I = 0;
            public int? IN;
            public long L = 0;
            public long? LN;
        }

        #endregion

        #region 1.11.6 OfType

        /// <summary>
        ///A test for OfType&lt;&gt; (IEnumerable)
        ///</summary>
        [TestMethod()]
        public void OfTypeTest()
        {
            string s1 = "Bart";
            string s2 = "Bill";

            IEnumerable source = new object[] { 1, 2.0, s1, DateTime.Now, s2, -1 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.OfType<int>((IEnumerable)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.OfType<T> did not return the expected value (exceptions).");

            string[] expected = new string[] { s1, s2 };
            IEnumerable<string> actual = Sequence.OfType<string>(source);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.OfType<T> did not return the expected value.");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, expected[j++]), "Sequence.OfType<T> did not return the expected value.");
        }

        #endregion

        #region 1.8.1 OrderBy

        /// <summary>
        ///A test for OrderBy&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void OrderByTest()
        {
            OrderBy_Helper o1 = new OrderBy_Helper { K = 3, S = "Bart" };
            OrderBy_Helper o2 = new OrderBy_Helper { K = 4, S = "John" };
            OrderBy_Helper o3 = new OrderBy_Helper { K = 2, S = "Bill" };
            OrderBy_Helper o4 = new OrderBy_Helper { K = 1, S = "Steve" };

            IEnumerable<OrderBy_Helper> source = new List<OrderBy_Helper> { o1, o2, o3, o4 };
            Func<OrderBy_Helper, int> keySelector = o => o.K;

            bool exception1 = false;
            try
            {
                Sequence.OrderBy<OrderBy_Helper, int>(null, keySelector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.OrderBy<OrderBy_Helper, int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.OrderBy<T, K> did not return the expected value (exceptions).");

            OrderBy_Helper[] expected = new OrderBy_Helper[] { o4, o3, o1, o2 };
            OrderedSequence<OrderBy_Helper> actual = Sequence.OrderBy(source, keySelector);

            long n = 0;
            foreach (OrderBy_Helper o in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.OrderBy<T, K> did not return the expected value.");

            int j = 0;
            foreach (OrderBy_Helper o in actual)
                Assert.IsTrue(object.ReferenceEquals(o, expected[j++]), "Sequence.OrderBy<T, K> did not return the expected value.");
        }

        /// <summary>
        ///A test for OrderBy&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, IComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void OrderByTest1()
        {
            OrderBy_Helper o1 = new OrderBy_Helper { K = -3, S = "Bart" };
            OrderBy_Helper o2 = new OrderBy_Helper { K = 4, S = "John" };
            OrderBy_Helper o3 = new OrderBy_Helper { K = -2, S = "Bill" };
            OrderBy_Helper o4 = new OrderBy_Helper { K = 1, S = "Steve" };

            IEnumerable<OrderBy_Helper> source = new List<OrderBy_Helper> { o1, o2, o3, o4 };
            Func<OrderBy_Helper, int> keySelector = o => o.K;

            OrderBy_Helper[] expected = new OrderBy_Helper[] { o4, o3, o1, o2 };
            OrderedSequence<OrderBy_Helper> actual = Sequence.OrderBy<OrderBy_Helper, int>(source, keySelector, new OrderByComparer_Helper<int>());

            long n = 0;
            foreach (OrderBy_Helper o in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.OrderBy<T, K> did not return the expected value.");

            int j = 0;
            foreach (OrderBy_Helper o in actual)
                Assert.IsTrue(object.ReferenceEquals(o, expected[j++]), "Sequence.OrderBy<T, K> did not return the expected value.");
        }

        /// <summary>
        ///A test for OrderByDescending&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void OrderByDescendingTest()
        {
            OrderBy_Helper o1 = new OrderBy_Helper { K = 3, S = "Bart" };
            OrderBy_Helper o2 = new OrderBy_Helper { K = 4, S = "John" };
            OrderBy_Helper o3 = new OrderBy_Helper { K = 2, S = "Bill" };
            OrderBy_Helper o4 = new OrderBy_Helper { K = 1, S = "Steve" };

            IEnumerable<OrderBy_Helper> source = new List<OrderBy_Helper> { o1, o2, o3, o4 };
            Func<OrderBy_Helper, int> keySelector = o => o.K;

            OrderBy_Helper[] expected = new OrderBy_Helper[] { o2, o1, o3, o4 };
            OrderedSequence<OrderBy_Helper> actual = Sequence.OrderByDescending(source, keySelector);

            long n = 0;
            foreach (OrderBy_Helper o in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.OrderBy<T, K> did not return the expected value.");

            int j = 0;
            foreach (OrderBy_Helper o in actual)
                Assert.IsTrue(object.ReferenceEquals(o, expected[j++]), "Sequence.OrderBy<T, K> did not return the expected value.");
        }

        /// <summary>
        ///A test for OrderByDescending&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, IComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void OrderByDescendingTest1()
        {
            OrderBy_Helper o1 = new OrderBy_Helper { K = 3, S = "Bart" };
            OrderBy_Helper o2 = new OrderBy_Helper { K = -4, S = "John" };
            OrderBy_Helper o3 = new OrderBy_Helper { K = 2, S = "Bill" };
            OrderBy_Helper o4 = new OrderBy_Helper { K = -1, S = "Steve" };

            IEnumerable<OrderBy_Helper> source = new List<OrderBy_Helper> { o1, o2, o3, o4 };
            Func<OrderBy_Helper, int> keySelector = o => o.K;

            OrderBy_Helper[] expected = new OrderBy_Helper[] { o2, o1, o3, o4 };
            OrderedSequence<OrderBy_Helper> actual = Sequence.OrderByDescending(source, keySelector, new OrderByComparer_Helper<int>());

            long n = 0;
            foreach (OrderBy_Helper o in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.OrderBy<T, K> did not return the expected value.");

            int j = 0;
            foreach (OrderBy_Helper o in actual)
                Assert.IsTrue(object.ReferenceEquals(o, expected[j++]), "Sequence.OrderBy<T, K> did not return the expected value.");
        }

        private class OrderBy_Helper
        {
            public int K;
            public string S;
        }

        private class OrderByComparer_Helper<T> : IComparer<T>
        {
            private IComparer _comparer;

            public OrderByComparer_Helper()
            {
                _comparer = new OrderByComparerNG_Helper();
            }

            public int Compare(T a, T b)
            {
                return _comparer.Compare(a, b);
            }
        }

        private class OrderByComparerNG_Helper : IComparer
        {
            public int Compare(object a, object b)
            {
                if (a is int && b is int)
                {
                    int ia = Math.Abs((int)a);
                    int ib = Math.Abs((int)b);

                    if (ia < ib)
                        return -1;
                    else if (ia == ib)
                        return 0;
                    else
                        return 1;
                }
                else
                    throw new Exception(); // quick-n-dirty

            }
        }

        #endregion

        #region 1.14.1 Range

        /// <summary>
        ///A test for Range (int, int)
        ///</summary>
        [TestMethod()]
        public void RangeTest()
        {
            RangeTest_1();
            RangeTest_2();
            RangeTest_3();
            RangeTest_4();
        }

        private void RangeTest_1()
        {
            int start = 1234;
            int count = 5;

            IEnumerable<int> actual = Sequence.Range(start, count);

            int n = 0;
            foreach (int i in actual)
                Assert.AreEqual(i, start + n++, "Sequence.Range did not return the expected value (test 1).");

            Assert.IsTrue(n == count, "Sequence.Range did not return the expected value (test 1).");
        }

        private void RangeTest_2()
        {
            int start = 1234;
            int count = -5;

            bool exception = false;
            try
            {
                IEnumerable<int> actual = Sequence.Range(start, count);
                foreach (int i in actual)
                    ;
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Range did not return the expected value (test 2).");
        }

        private void RangeTest_3()
        {
            int start = 1234;
            int count = 0;

            IEnumerable<int> actual = Sequence.Range(start, count);

            int n = 0;
            foreach (int i in actual)
                Assert.AreEqual(i, start + n++, "Sequence.Range did not return the expected value (test 3).");

            Assert.IsTrue(n == count, "Sequence.Range did not return the expected value (test 3).");
        }

        private void RangeTest_4()
        {
            int start = 1234;
            int count = int.MaxValue; // start + count - 1 > int.MaxValue

            bool exception = false;
            try
            {
                IEnumerable<int> actual = Sequence.Range(start, count);
                foreach (int i in actual)
                    ;
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Range did not return the expected value (test 4).");
        }

        #endregion

        #region 1.14.2 Repeat

        /// <summary>
        ///A test for Repeat&lt;&gt; (T, int)
        ///</summary>
        [TestMethod()]
        public void RepeatTest()
        {
            RepeatTest_1();
            RepeatTest_2();
            RepeatTest_3();
            RepeatTest_4();
        }

        private void RepeatTest_1()
        {
            int element = 1234;
            int count = 10;

            IEnumerable<int> actual = Sequence.Repeat(element, count);

            int j = 0;
            foreach (int i in actual)
            {
                Assert.AreEqual(i, element, "Sequence.Repeat<T> did not return the expected value (test 1).");
                j++;
            }

            Assert.AreEqual(j, count, "Sequence.Repeat<T> did not return the expected value (test 1).");
        }

        private void RepeatTest_2()
        {
            string element = "Bart";
            int count = 10;

            IEnumerable<string> actual = Sequence.Repeat(element, count);

            int j = 0;
            foreach (string s in actual)
            {
                Assert.IsTrue(object.ReferenceEquals(s, element), "Sequence.Repeat<T> did not return the expected value (test 2).");
                j++;
            }

            Assert.AreEqual(j, count, "Sequence.Repeat<T> did not return the expected value (test 2).");
        }

        private void RepeatTest_3()
        {
            int element = 1234;
            int count = -10;

            bool exception = false;
            try
            {
                IEnumerable<int> actual = Sequence.Repeat(element, count);
                foreach (int i in actual)
                    ;
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Repeat<T> did not return the expected value (test 3).");
        }

        private void RepeatTest_4()
        {
            DateTime element = DateTime.Now;
            int count = 0;

            IEnumerable<DateTime> actual = Sequence.Repeat(element, count);

            int j = 0;
            foreach (DateTime i in actual)
                j++;

            Assert.AreEqual(j, count, "Sequence.Repeat<T> did not return the expected value (test 4).");
        }

        #endregion

        #region 1.8.2 Reverse

        /// <summary>
        ///A test for Reverse&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void ReverseTest()
        {
            ReverseTest_1();
            ReverseTest_2();
            ReverseTest_3();
        }

        private void ReverseTest_1()
        {
            int[] src = new int[] { 1, 8, -12, 3 };
            IEnumerable<int> source = src;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Reverse((IEnumerable<int>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Reverse<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Reverse(source);

            int n = 0;
            foreach (int i in actual)
                n++;

            Assert.AreEqual(n, src.Length, "Sequence.Reverse<T> did not return the expected value (test 1).");

            int j = src.Length - 1;

            foreach (int i in actual)
                Assert.AreEqual(i, src[j--], "Sequence.Reverse<T> did not return the expected value (test 1).");
        }

        private void ReverseTest_2()
        {
            string[] src = new string[] { "Bart", "Bill", "John", "Steve" };
            IEnumerable<string> source = src;

            IEnumerable<string> actual = Sequence.Reverse(source);

            int n = 0;
            foreach (string s in actual)
                n++;

            Assert.AreEqual(n, src.Length, "Sequence.Reverse<T> did not return the expected value (test 2).");

            int j = src.Length - 1;

            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, src[j--]), "Sequence.Reverse<T> did not return the expected value (test 2).");
        }

        private void ReverseTest_3()
        {
            IEnumerable<DateTime> source = new List<DateTime> { };

            IEnumerable<DateTime> actual = Sequence.Reverse(source);

            Assert.IsTrue(!actual.GetEnumerator().MoveNext(), "Sequence.Reverse<T> did not return the expected value (test 3).");
        }

        #endregion

        #region 1.4.1 Select

        /// <summary>
        ///A test for Select&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int,S&gt;)
        ///</summary>
        [TestMethod()]
        public void SelectTest()
        {
            SelectTest_1();
            SelectTest_2();
        }

        private void SelectTest_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            IEnumerable<SelectTest_Helper> source = new List<SelectTest_Helper> {
                new SelectTest_Helper() { S = s1, I = 1983},
                new SelectTest_Helper() { S = s2, I = 1955},
                new SelectTest_Helper() { S = s3, I = 1948}
            };

            Func<SelectTest_Helper, int, int> selector = ( h, i) => h.I * i;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Select(null, selector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Select(source, (Func<SelectTest_Helper, int, int>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Select<T, S> did not return the expected value (exceptions).");

            int[] expected = new int[] { 0, 1955, 1948 * 2 };
            IEnumerable<int> actual = Sequence.Select(source, selector);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.Select<T, S> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.AreEqual(i, expected[j++], "Sequence.Select<T, S> did not return the expected value (test 1).");
        }

        private void SelectTest_2()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            IEnumerable<SelectTest_Helper> source = new List<SelectTest_Helper> {
                new SelectTest_Helper() { S = s1, I = 1983},
                new SelectTest_Helper() { S = s2, I = 1955},
                new SelectTest_Helper() { S = s3, I = 1948}
            };

            Func<SelectTest_Helper, int, string> selector = ( h, i) => h.S.PadRight(h.S.Length + i, '*');

            string[] expected = new string[] { "Bart", "Bill*", "John**" };
            IEnumerable<string> actual = Sequence.Select(source, selector);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.Select<T, S> did not return the expected value (test 2).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(s.Equals(expected[j++]), "Sequence.Select<T, S> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for Select&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,S&gt;)
        ///</summary>
        [TestMethod()]
        public void SelectTest1()
        {
            SelectTest1_1();
            SelectTest1_2();
        }

        private void SelectTest1_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            IEnumerable<SelectTest_Helper> source = new List<SelectTest_Helper> {
                new SelectTest_Helper() { S = s1, I = 1983},
                new SelectTest_Helper() { S = s2, I = 1955},
                new SelectTest_Helper() { S = s3, I = 1948}
            };

            Func<SelectTest_Helper, int> selector = h => h.I;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Select(null, selector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Select(source, (Func<SelectTest_Helper, int>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Select<T, S> did not return the expected value (exceptions).");

            int[] expected = new int[] { 1983, 1955, 1948 };
            IEnumerable<int> actual = Sequence.Select(source, selector);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.Select<T, S> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.AreEqual(i, expected[j++], "Sequence.Select<T, S> did not return the expected value (test 1).");
        }

        private void SelectTest1_2()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            IEnumerable<SelectTest_Helper> source = new List<SelectTest_Helper> {
                new SelectTest_Helper() { S = s1, I = 1983},
                new SelectTest_Helper() { S = s2, I = 1955},
                new SelectTest_Helper() { S = s3, I = 1948}
            };

            Func<SelectTest_Helper, string> selector = h => h.S;

            string[] expected = new string[] { s1, s2, s3 };
            IEnumerable<string> actual = Sequence.Select(source, selector);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.Select<T, S> did not return the expected value (test 2).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, expected[j++]), "Sequence.Select<T, S> did not return the expected value (test 2).");
        }

        private class SelectTest_Helper
        {
            public string S;
            public int I;
        }

        #endregion

        #region 1.4.2 SelectMany

        /// <summary>
        ///A test for SelectMany&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,IEnumerable&lt;S&gt;&gt;)
        ///</summary>
        [TestMethod()]
        public void SelectManyTest()
        {
            SelectManyTest_1();
            SelectManyTest_2();
        }

        private void SelectManyTest_1()
        {
            IEnumerable<SelectMany_Helper> source = new List<SelectMany_Helper> {
                new SelectMany_Helper() { S = new string[] { "A", "B" }, I = new int[] { 1, 2 } },
                new SelectMany_Helper() { S = new string[] { "C", "D", "E" }, I = new int[] { } },
                new SelectMany_Helper() { S = new string[] { }, I = new int[] { 3, 4, 5 } }
            };

            Func<SelectMany_Helper, IEnumerable<int>> selector = h => h.I;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.SelectMany(null, selector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.SelectMany(source, (Func<SelectMany_Helper, IEnumerable<int>>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.SelectMany<T, S> did not return the expected value (exceptions).");

            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            IEnumerable<int> actual = Sequence.SelectMany(source, selector);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.SelectMany<T, S> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(expected[j++] == i, "Sequence.SelectMany<T, S> did not return the expected value (test 1).");
        }

        private void SelectManyTest_2()
        {
            string a = "A";
            string b = "B";
            string c = "C";
            string d = "D";
            string e = "E";

            IEnumerable<SelectMany_Helper> source = new List<SelectMany_Helper> {
                new SelectMany_Helper() { S = new string[] { a, b }, I = new int[] { 1, 2 } },
                new SelectMany_Helper() { S = new string[] { c, d, e }, I = new int[] { } },
                new SelectMany_Helper() { S = new string[] { }, I = new int[] { 3, 4, 5 } }
            };

            Func<SelectMany_Helper, IEnumerable<string>> selector = h => h.S;

            string[] expected = new string[] { a, b, c, d, e };
            IEnumerable<string> actual = Sequence.SelectMany(source, selector);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.SelectMany<T, S> did not return the expected value (test 2).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(expected[j++], s), "Sequence.SelectMany<T, S> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for SelectMany&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int,IEnumerable&lt;S&gt;&gt;)
        ///</summary>
        [TestMethod()]
        public void SelectManyTest1()
        {
            SelectManyTest1_1();
            SelectManyTest1_2();
        }

        private void SelectManyTest1_1()
        {
            IEnumerable<SelectMany_Helper> source = new List<SelectMany_Helper> {
                new SelectMany_Helper() { S = new string[] { "A", "B" }, I = new int[] { } },
                new SelectMany_Helper() { S = new string[] { "C", "D", "E" }, I = new int[] { 1, 2 } },
                new SelectMany_Helper() { S = new string[] { }, I = new int[] { 3, 4, 5 } }
            };

            Func<SelectMany_Helper, int, IEnumerable<int>> selector = delegate(SelectMany_Helper h, int i) { int[] res = new int[h.I.Length * i]; for (int k = 0; k < i; k++) Array.Copy(h.I, 0, res, k * h.I.Length, h.I.Length); return res; };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.SelectMany(null, selector))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.SelectMany(source, (Func<SelectMany_Helper, int, IEnumerable<int>>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.SelectMany<T, S> did not return the expected value (exceptions).");

            int[] expected = new int[] { 1, 2, 3, 4, 5, 3, 4, 5 };
            IEnumerable<int> actual = Sequence.SelectMany(source, selector);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 8, "Sequence.SelectMany<T, S> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(expected[j++] == i, "Sequence.SelectMany<T, S> did not return the expected value (test 1).");
        }

        private void SelectManyTest1_2()
        {
            string a = "A";
            string b = "B";
            string c = "C";
            string d = "D";
            string e = "E";

            IEnumerable<SelectMany_Helper> source = new List<SelectMany_Helper> {
                new SelectMany_Helper() { S = new string[] { a }, I = new int[] { 1, 2 } },
                new SelectMany_Helper() { S = new string[] { b, c }, I = new int[] { } },
                new SelectMany_Helper() { S = new string[] { d, e }, I = new int[] { 3, 4, 5 } }
            };

            Func<SelectMany_Helper, int, IEnumerable<string>> selector = delegate(SelectMany_Helper h, int i) { string[] res = new string[h.S.Length * i]; for (int k = 0; k < i; k++) Array.Copy(h.S, 0, res, k * h.S.Length, h.S.Length); return res; };

            string[] expected = new string[] { b, c, d, e, d, e };
            IEnumerable<string> actual = Sequence.SelectMany(source, selector);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 6, "Sequence.SelectMany<T, S> did not return the expected value (test 2).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(expected[j++], s), "Sequence.SelectMany<T, S> did not return the expected value (test 2).");
        }

        private class SelectMany_Helper
        {
            public string[] S;
            public int[] I;
        }

        #endregion

        #region 1.13.5 Single

        /// <summary>
        ///A test for Single&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void SingleTest()
        {
            SingleTest_1();
            SingleTest_2();
            SingleTest_3();
            SingleTest_4();
            SingleTest_5();
            SingleTest_6();
        }

        private void SingleTest_1()
        {
            IEnumerable<int> source = new int[] { 1 };

            bool exception1 = false;
            try
            {
                Sequence.Single<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Single<T> did not return the expected value (exceptions).");

            int expected = 1;
            int actual = Sequence.Single(source);

            Assert.AreEqual(expected, actual, "Sequence.Single<T> did not return the expected value (test 1).");
        }

        private void SingleTest_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { s };

            string expected = s;
            string actual = Sequence.Single(source);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.Single<T> did not return the expected value (test 2).");
        }

        private void SingleTest_3()
        {
            IEnumerable<int> source = new int[] { };

            bool exception = false;
            try
            {
                int actual = Sequence.Single(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 3).");
        }

        private void SingleTest_4()
        {
            IEnumerable<string> source = new string[] { };

            bool exception = false;
            try
            {
                string actual = Sequence.Single(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 4).");
        }

        private void SingleTest_5()
        {
            IEnumerable<int> source = new int[] { 1, 2 };

            bool exception = false;
            try
            {
                int actual = Sequence.Single(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 5).");
        }

        private void SingleTest_6()
        {
            IEnumerable<string> source = new string[] { "Bart", "John" };

            bool exception = false;
            try
            {
                string actual = Sequence.Single(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 6).");
        }

        /// <summary>
        ///A test for Single&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void SingleTest1()
        {
            SingleTest1_1();
            SingleTest1_2();
            SingleTest1_3();
            SingleTest1_4();
            SingleTest1_5();
            SingleTest1_6();
            SingleTest1_7();
            SingleTest1_8();
        }

        private void SingleTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 7, 5 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.Single<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Single<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Single<T> did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.Single(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.Single<T> did not return the expected value (test 1).");
        }

        private void SingleTest1_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "Steve", "John", s, "Rob" };
            Func<string, bool> predicate = ss => ss.StartsWith("B");

            string expected = s;
            string actual = Sequence.Single(source, predicate);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.Single<T> did not return the expected value (test 2).");
        }

        private void SingleTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.Single(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 3).");
        }

        private void SingleTest1_4()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.Single(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 4).");
        }

        private void SingleTest1_5()
        {
            IEnumerable<int> source = new int[] { 3, 5, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.Single(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 5).");
        }

        private void SingleTest1_6()
        {
            IEnumerable<string> source = new string[] { "John", "Steve" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.Single(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 6).");
        }

        private void SingleTest1_7()
        {
            IEnumerable<int> source = new int[] { 2, 3, 4 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.Single(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 7).");
        }

        private void SingleTest1_8()
        {
            IEnumerable<string> source = new string[] { "Bill", "John", "Bart" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.Single(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Single<T> did not return the expected value (test 8).");
        }

        #endregion

        #region 1.13.6 SingleOrDefault

        /// <summary>
        ///A test for SingleOrDefault&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void SingleOrDefaultTest()
        {
            SingleOrDefaultTest_1();
            SingleOrDefaultTest_2();
            SingleOrDefaultTest_3();
            SingleOrDefaultTest_4();
            SingleOrDefaultTest_5();
            SingleOrDefaultTest_6();
        }

        private void SingleOrDefaultTest_1()
        {
            IEnumerable<int> source = new int[] { 1 };

            bool exception1 = false;
            try
            {
                Sequence.SingleOrDefault<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.SingleOrDefault<T> did not return the expected value (exceptions).");

            int expected = 1;
            int actual = Sequence.SingleOrDefault(source);

            Assert.AreEqual(expected, actual, "Sequence.SingleOrDefault<T> did not return the expected value (test 1).");
        }

        private void SingleOrDefaultTest_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { s };

            string expected = s;
            string actual = Sequence.SingleOrDefault(source);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.SingleOrDefault<T> did not return the expected value (test 2).");
        }

        private void SingleOrDefaultTest_3()
        {
            IEnumerable<int> source = new int[] { };

            int expected = default(int);
            int actual = Sequence.SingleOrDefault(source);

            Assert.AreEqual(expected, actual, "Sequence.SingleOrDefault<T> did not return the expected value (test 3).");
        }

        private void SingleOrDefaultTest_4()
        {
            IEnumerable<string> source = new string[] { };

            string expected = default(string);
            string actual = Sequence.SingleOrDefault(source);

            Assert.IsTrue(object.Equals(actual, expected), "Sequence.SingleOrDefault<T> did not return the expected value (test 4).");
        }

        private void SingleOrDefaultTest_5()
        {
            IEnumerable<int> source = new int[] { 1, 2 };

            bool exception = false;
            try
            {
                int actual = Sequence.SingleOrDefault(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.SingleOrDefault<T> did not return the expected value (test 5).");
        }

        private void SingleOrDefaultTest_6()
        {
            IEnumerable<string> source = new string[] { "Bart", "John" };

            bool exception = false;
            try
            {
                string actual = Sequence.SingleOrDefault(source);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.SingleOrDefault<T> did not return the expected value (test 6).");
        }

        /// <summary>
        ///A test for SingleOrDefault&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void SingleOrDefaultTest1()
        {
            SingleOrDefaultTest1_1();
            SingleOrDefaultTest1_2();
            SingleOrDefaultTest1_3();
            SingleOrDefaultTest1_4();
            SingleOrDefaultTest1_5();
            SingleOrDefaultTest1_6();
            SingleOrDefaultTest1_7();
            SingleOrDefaultTest1_8();
        }

        private void SingleOrDefaultTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 7, 5 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                Sequence.SingleOrDefault<int>(null, predicate);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.SingleOrDefault<int>(source, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.SingleOrDefault<T> did not return the expected value (exceptions).");

            int expected = 2;
            int actual = Sequence.SingleOrDefault(source, predicate);

            Assert.AreEqual(expected, actual, "Sequence.SingleOrDefault<T> did not return the expected value (test 1).");
        }

        private void SingleOrDefaultTest1_2()
        {
            string s = "Bart";
            IEnumerable<string> source = new string[] { "Steve", "John", s, "Rob" };
            Func<string, bool> predicate = ss => ss.StartsWith("B");

            string expected = s;
            string actual = Sequence.SingleOrDefault(source, predicate);

            Assert.IsTrue(object.ReferenceEquals(actual, expected), "Sequence.SingleOrDefault<T> did not return the expected value (test 2).");
        }

        private void SingleOrDefaultTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            int expected = default(int);
            int actual = Sequence.SingleOrDefault(source, predicate);

            Assert.IsTrue(expected == actual, "Sequence.SingleOrDefault<T> did not return the expected value (test 3).");
        }

        private void SingleOrDefaultTest1_4()
        {
            IEnumerable<string> source = new string[] { };
            Func<string, bool> predicate = s => s.StartsWith("B");

            string expected = default(string);
            string actual = Sequence.SingleOrDefault(source, predicate);

            Assert.IsTrue(object.Equals(expected, actual), "Sequence.SingleOrDefault<T> did not return the expected value (test 4).");
        }

        private void SingleOrDefaultTest1_5()
        {
            IEnumerable<int> source = new int[] { 3, 5, 7 };
            Func<int, bool> predicate = i => i % 2 == 0;

            int expected = default(int);
            int actual = Sequence.SingleOrDefault(source, predicate);

            Assert.IsTrue(expected == actual, "Sequence.SingleOrDefault<T> did not return the expected value (test 5).");
        }

        private void SingleOrDefaultTest1_6()
        {
            IEnumerable<string> source = new string[] { "John", "Steve" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            string expected = default(string);
            string actual = Sequence.SingleOrDefault(source, predicate);

            Assert.IsTrue(object.Equals(expected, actual), "Sequence.SingleOrDefault<T> did not return the expected value (test 6).");
        }

        private void SingleOrDefaultTest1_7()
        {
            IEnumerable<int> source = new int[] { 2, 3, 4 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception = false;
            try
            {
                int actual = Sequence.SingleOrDefault(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.SingleOrDefault<T> did not return the expected value (test 7).");
        }

        private void SingleOrDefaultTest1_8()
        {
            IEnumerable<string> source = new string[] { "Bill", "John", "Bart" };
            Func<string, bool> predicate = s => s.StartsWith("B");

            bool exception = false;
            try
            {
                string actual = Sequence.SingleOrDefault(source, predicate);
            }
            catch (InvalidOperationException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.SingleOrDefault<T> did not return the expected value (test 8).");
        }

        #endregion

        #region 1.5.2 Skip

        /// <summary>
        ///A test for Skip&lt;&gt; (IEnumerable&lt;T&gt;, int)
        ///</summary>
        [TestMethod()]
        public void SkipTest()
        {
            SkipTest_1();
            SkipTest_2();
            SkipTest_3();
            SkipTest_4();
            SkipTest_5();
            SkipTest_6();
        }

        private void SkipTest_1()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 3;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Skip((IEnumerable<int>)null, count))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Skip<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Skip(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.Skip<T> did not return the expected value (test 1).");

            int j = 2;
            foreach (int i in actual)
                Assert.IsTrue(j-- == i, "Sequence.Skip<T> did not return the expected value (test 1).");
        }

        private void SkipTest_2()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = -1;

            IEnumerable<int> actual = Sequence.Skip(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.Skip<T> did not return the expected value (test 2).");

            int j = 5;
            foreach (int i in actual)
                Assert.IsTrue(j-- == i, "Sequence.Skip<T> did not return the expected value (test 2).");
        }

        private void SkipTest_3()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 0;

            IEnumerable<int> actual = Sequence.Skip(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.Skip<T> did not return the expected value (test 3).");

            int j = 5;
            foreach (int i in actual)
                Assert.IsTrue(j-- == i, "Sequence.Skip<T> did not return the expected value (test 3).");
        }

        private void SkipTest_4()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 6;

            IEnumerable<int> actual = Sequence.Skip(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Skip<T> did not return the expected value (test 4).");
        }

        private void SkipTest_5()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 5;

            IEnumerable<int> actual = Sequence.Skip(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Skip<T> did not return the expected value (test 5).");
        }

        private void SkipTest_6()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";
            IEnumerable<string> source = new string[] { s1, s2, s3 };

            int count = 2;

            IEnumerable<string> actual = Sequence.Skip(source, count);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 1, "Sequence.Skip<T> did not return the expected value (test 6).");

            IEnumerator<string> enumerator = actual.GetEnumerator();
            enumerator.MoveNext();

            Assert.IsTrue(object.ReferenceEquals(enumerator.Current, s3), "Sequence.Skip<T> did not return the expected value (test 6).");
        }

        #endregion

        #region 1.5.4 SkipWhile

        /// <summary>
        ///A test for SkipWhile&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void SkipWhileTest()
        {
            SkipWhileTest_1();
            SkipWhileTest_2();
            SkipWhileTest_3();
            SkipWhileTest_4();
            SkipWhileTest_5();
        }

        private void SkipWhileTest_1()
        {
            int[] source = new int[] { 2, 4, 8, 3, 7, 6 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.SkipWhile((IEnumerable<int>)null, predicate))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.SkipWhile(source, (Func<int, bool>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.SkipWhile<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.SkipWhile<T> did not return the expected value (test 1).");

            int j = 3;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.SkipWhile<T> did not return the expected value (test 1).");
        }

        private void SkipWhileTest_2()
        {
            int[] source = new int[] { 1, 2, 4, 8, 3, 7, 6 };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 7, "Sequence.SkipWhile<T> did not return the expected value (test 2).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.SkipWhile<T> did not return the expected value (test 2).");
        }

        private void SkipWhileTest_3()
        {
            int[] source = new int[] { 2, 4, 8, 6 };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.SkipWhile<T> did not return the expected value (test 3).");
        }

        private void SkipWhileTest_4()
        {
            int[] source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.SkipWhile<T> did not return the expected value (test 4).");
        }

        private void SkipWhileTest_5()
        {
            string s1 = "Steve";
            string s2 = "John";
            string[] source = new string[] { "Bart", "Bill", s1, s2 };
            Func<string, bool> predicate = s => s.Length == 4;

            IEnumerable<string> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.SkipWhile<T> did not return the expected value (test 2).");

            int j = 2;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, source[j++]), "Sequence.SkipWhile<T> did not return the expected value (test 5).");
        }

        /// <summary>
        ///A test for SkipWhile&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void SkipWhileTest1()
        {
            SkipWhileTest1_1();
            SkipWhileTest1_2();
            SkipWhileTest1_3();
            SkipWhileTest1_4();
            SkipWhileTest1_5();
        }

        private void SkipWhileTest1_1()
        {
            int[] source = new int[] { 2, 4, 8, 3, 7, 6 };
            Func<int, int, bool> predicate = ( i, k) => i % 2 == 0 && k < 2;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.SkipWhile((IEnumerable<int>)null, predicate))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.SkipWhile(source, (Func<int, int, bool>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.SkipWhile<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.SkipWhile<T> did not return the expected value (test 1).");

            int j = 2;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.SkipWhile<T> did not return the expected value (test 1).");
        }

        private void SkipWhileTest1_2()
        {
            int[] source = new int[] { 1, 2, 4, 8, 3, 7, 6 };
            Func<int, int, bool> predicate = ( i, k) => k < 0;

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 7, "Sequence.SkipWhile<T> did not return the expected value (test 2).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.SkipWhile<T> did not return the expected value (test 2).");
        }

        private void SkipWhileTest1_3()
        {
            int[] source = new int[] { 2, 4, 8, 6 };
            Func<int, int, bool> predicate = ( i, k) => k >= 0;

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.SkipWhile<T> did not return the expected value (test 3).");
        }

        private void SkipWhileTest1_4()
        {
            int[] source = new int[] { };
            Func<int, int, bool> predicate = ( i, k) => k < 0;

            IEnumerable<int> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.SkipWhile<T> did not return the expected value (test 4).");
        }

        private void SkipWhileTest1_5()
        {
            string s1 = "Steve";
            string s2 = "John";
            string[] source = new string[] { "Bart", "Bill", s1, s2 };
            Func<string, int, bool> predicate = ( s, k) => k < 1 && s.Length == 4;

            IEnumerable<string> actual = Sequence.SkipWhile(source, predicate);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.SkipWhile<T> did not return the expected value (test 2).");

            int j = 1;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, source[j++]), "Sequence.SkipWhile<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.16.3 Sum

        /// <summary>
        ///A test for Sum (IEnumerable&lt;decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest()
        {
            SumTest_1();
            SumTest_2();
            SumTest_3();
        }

        private void SumTest_1()
        {
            IEnumerable<decimal> source = new decimal[] { 38.0m, 21.0m, -12.3m, 17.4m, 12.34m };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            decimal expected = 0.0m;
            foreach (decimal d in source)
                expected += d;

            decimal actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest_2()
        {
            IEnumerable<decimal> source = new decimal[] { decimal.MaxValue - 2.1m, -1.8m, 3.0m, 38.0m };

            bool exception = false;
            try
            {
                decimal actual = Sequence.Sum(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest_3()
        {
            IEnumerable<decimal> source = new decimal[] { };

            decimal expected = 0.0m;

            decimal actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest1()
        {
            SumTest1_1();
            SumTest1_2();
            SumTest1_3();
            SumTest1_4();
        }

        private void SumTest1_1()
        {
            IEnumerable<decimal?> source = new List<decimal?> { 38.0m, null, 21.0m, -12.3m, 17.4m, 12.34m, null };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            decimal expected = 0.0m;
            foreach (decimal? d in source)
                if (d != null)
                    expected += d.Value;

            decimal? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest1_2()
        {
            IEnumerable<decimal?> source = new List<decimal?> { decimal.MaxValue - 2.1m, -1.8m, 3.0m, 38.0m };

            bool exception = false;
            try
            {
                decimal? actual = Sequence.Sum(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest1_3()
        {
            IEnumerable<decimal?> source = new List<decimal?> { };

            decimal? actual = Sequence.Sum(source);

            decimal? expected = 0.0m;

            Assert.AreEqual(actual, expected, "Sequence.Sum did not return the expected value (test 3).");
        }

        private void SumTest1_4()
        {
            IEnumerable<decimal?> source = new List<decimal?> { null, null, null };

            decimal? actual = Sequence.Sum(source);

            decimal? expected = 0.0m;

            Assert.AreEqual(actual, expected, "Sequence.Sum did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;double?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest2()
        {
            SumTest2_1();
            SumTest2_2();
            SumTest2_3();
            SumTest2_4();
        }

        private void SumTest2_1()
        {
            IEnumerable<double?> source = new List<double?> { 38.0, null, 21.0, -12.3, 17.4, 12.34, null };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            double? expected = 0.0;
            foreach (double? d in source)
                if (d != null)
                    expected += d.Value;

            double? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest2_2()
        {
            IEnumerable<double?> source = new List<double?> { };

            double? expected = 0;
            double? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest2_3()
        {
            IEnumerable<double?> source = new List<double?> { null, null, null };

            double? expected = 0;
            double? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 3).");
        }

        private void SumTest2_4()
        {
            IEnumerable<double?> source = new List<double?> { double.MaxValue, double.MaxValue, null, null };

            double? actual = Sequence.Sum(source);

            Assert.IsTrue(double.IsInfinity(actual.Value), "Sequence.Sum did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;double&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest3()
        {
            SumTest3_1();
            SumTest3_2();
            SumTest3_3();
        }

        private void SumTest3_1()
        {
            IEnumerable<double> source = new double[] { 38.0, 21.0, -12.3, 17.4, 12.34 };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<double>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            double expected = 0.0;
            foreach (double d in source)
                expected += d;

            double actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest3_2()
        {
            IEnumerable<double> source = new double[] { };

            double expected = 0.0;

            double actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest3_3()
        {
            IEnumerable<double> source = new List<double> { double.MaxValue, double.MaxValue };

            double actual = Sequence.Sum(source);

            Assert.IsTrue(double.IsInfinity(actual), "Sequence.Sum did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;int?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest4()
        {
            SumTest4_1();
            SumTest4_2();
            SumTest4_3();
        }

        private void SumTest4_1()
        {
            IEnumerable<int?> source = new List<int?> { 38, null, 21, -12, 17, 12, null };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            int? expected = 0;
            foreach (int? i in source)
                if (i != null)
                    expected += i.Value;

            int? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest4_2()
        {
            IEnumerable<int?> source = new List<int?> { };

            int? expected = 0;
            int? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest4_3()
        {
            IEnumerable<int?> source = new List<int?> { null, null, null };

            int? expected = 0;
            int? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;int&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest5()
        {
            SumTest5_1();
            //SumTest5_2(); //test OverflowException case manually
            SumTest5_3();
        }

        private void SumTest5_1()
        {
            IEnumerable<int> source = new int[] { 38, 21, -12, 17, 12 };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<int>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            int expected = 0;
            foreach (int d in source)
                expected += d;

            int actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest5_2()
        {
            IEnumerable<int> source = new int[] { int.MaxValue - 2, -1, 3, 38 }; //incorrect test code; testing for overflow would require a bunch of int.MaxValue summations

            bool exception = false;
            try
            {
                int actual = Sequence.Sum(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest5_3()
        {
            IEnumerable<int> source = new int[] { };

            int expected = 0;

            int actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;long?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest6()
        {
            SumTest6_1();
            SumTest6_2();
            SumTest6_3();
            SumTest6_4();
        }

        private void SumTest6_1()
        {
            IEnumerable<long?> source = new List<long?> { 38, null, 21, -12, 17, 12, null };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            long? expected = 0;
            foreach (long? i in source)
                if (i != null)
                    expected += i.Value;

            long? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest6_2()
        {
            IEnumerable<long?> source = new List<long?> { };

            long? expected = 0;
            long? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest6_3()
        {
            IEnumerable<long?> source = new List<long?> { null, null, null };

            long? expected = 0;
            long? actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 3).");
        }

        private void SumTest6_4()
        {
            IEnumerable<long?> source = new List<long?> { long.MaxValue - 2, null, -1, 3, 38, null };

            bool exception = false;
            try
            {
                long? actual = Sequence.Sum(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Sum (IEnumerable&lt;long&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest7()
        {
            SumTest7_1();
            SumTest7_2();
            SumTest7_3();
        }

        private void SumTest7_1()
        {
            IEnumerable<long> source = new long[] { 38, 21, -12, 17, 12 };

            bool exception1 = false;
            try
            {
                Sequence.Sum((IEnumerable<long>)null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Sum did not return the expected value (exceptions).");

            double expected = 0.0;
            foreach (long d in source)
                expected += d;

            double actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 1).");
        }

        private void SumTest7_2()
        {
            IEnumerable<long> source = new long[] { long.MaxValue - 2, -1, 3, 38 };

            bool exception = false;
            try
            {
                long actual = Sequence.Sum(source);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum did not return the expected value (test 2).");
        }

        private void SumTest7_3()
        {
            IEnumerable<long> source = new long[] { };

            long expected = 0;

            long actual = Sequence.Sum(source);

            Assert.AreEqual(expected, actual, "Sequence.Sum did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest8()
        {
            SumTest8_1();
            SumTest8_2();
            SumTest8_3();
            SumTest8_4();
        }

        private void SumTest8_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { dN = 38.0m },
                new SumTest_Helper { dN = 1.23m },
                new SumTest_Helper { dN = null },
                new SumTest_Helper { dN = -7.8m }
            };

            Func<SumTest_Helper, decimal?> selector = s => s.dN;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, decimal?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            decimal? expected = 38.0m + 1.23m - 7.8m;
            decimal? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest8_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, decimal?> selector = s => s.dN;

            decimal? expected = 0;
            decimal? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest8_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { dN = null },
                new SumTest_Helper { dN = null },
                new SumTest_Helper { dN = null },
                new SumTest_Helper { dN = null }
            };

            Func<SumTest_Helper, decimal?> selector = s => s.dN;

            decimal? expected = 0;
            decimal? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        private void SumTest8_4()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { dN = 38.0m },
                new SumTest_Helper { dN = decimal.MaxValue },
                new SumTest_Helper { dN = null },
                new SumTest_Helper { dN = -7.8m }
            };

            Func<SumTest_Helper, decimal?> selector = s => s.dN;

            bool exception = false;
            try
            {
                decimal? actual = Sequence.Sum(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,decimal&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest9()
        {
            SumTest9_1();
            SumTest9_2();
            SumTest9_3();
        }

        private void SumTest9_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { d = 38.0m },
                new SumTest_Helper { d = 1.23m },
                new SumTest_Helper { d = -7.8m }
            };

            Func<SumTest_Helper, decimal> selector = s => s.d;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, decimal>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            decimal expected = 38.0m + 1.23m - 7.8m;
            decimal actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest9_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, decimal> selector = s => s.d;

            decimal expected = 0;
            decimal actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest9_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { d = 38.0m },
                new SumTest_Helper { d = decimal.MaxValue },
                new SumTest_Helper { d = -7.8m }
            };

            Func<SumTest_Helper, decimal> selector = s => s.d;

            bool exception = false;
            try
            {
                decimal actual = Sequence.Sum(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest10()
        {
            SumTest10_1();
            SumTest10_2();
            SumTest10_3();
            SumTest10_4();
        }

        private void SumTest10_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { DN = 38.0 },
                new SumTest_Helper { DN = 1.23 },
                new SumTest_Helper { DN = null },
                new SumTest_Helper { DN = -7.8 }
            };

            Func<SumTest_Helper, double?> selector = s => s.DN;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, double?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            double? expected = 38.0 + 1.23 - 7.8;
            double? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest10_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, double?> selector = s => s.DN;

            double? expected = 0;
            double? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest10_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { DN = null },
                new SumTest_Helper { DN = null },
                new SumTest_Helper { DN = null },
                new SumTest_Helper { DN = null }
            };

            Func<SumTest_Helper, double?> selector = s => s.DN;

            double? expected = 0;
            double? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        private void SumTest10_4()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { DN = 38.0 },
                new SumTest_Helper { DN = double.MaxValue },
                new SumTest_Helper { DN = null },
                new SumTest_Helper { DN = double.MaxValue }
            };

            Func<SumTest_Helper, double?> selector = s => s.DN;

            double? expected = 0;
            double? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(double.IsInfinity(actual.Value), "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,double&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest11()
        {
            SumTest11_1();
            SumTest11_2();
            SumTest11_3();
        }

        private void SumTest11_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { D = 38.0 },
                new SumTest_Helper { D = 1.23 },
                new SumTest_Helper { D = -7.8 }
            };

            Func<SumTest_Helper, double> selector = s => s.D;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, double>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            double expected = 38.0 + 1.23 - 7.8;
            double actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest11_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, double> selector = s => s.D;

            double expected = 0;
            double actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest11_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { D = 38.0 },
                new SumTest_Helper { D = double.MaxValue },
                new SumTest_Helper { D = -7.8 },
                new SumTest_Helper { D = double.MaxValue }
            };

            Func<SumTest_Helper, double> selector = s => s.D;

            double actual = Sequence.Sum(source, selector);

            Assert.IsTrue(double.IsInfinity(actual), "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest12()
        {
            SumTest12_1();
            SumTest12_2();
            SumTest12_3();
            SumTest12_4();
        }

        private void SumTest12_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { IN = 38 },
                new SumTest_Helper { IN = 123 },
                new SumTest_Helper { IN = null },
                new SumTest_Helper { IN = -78 }
            };

            Func<SumTest_Helper, int?> selector = s => s.IN;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, int?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            int? expected = 38 + 123 - 78;
            int? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest12_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, int?> selector = s => s.IN;

            int? expected = 0;
            int? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest12_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { IN = null },
                new SumTest_Helper { IN = null },
                new SumTest_Helper { IN = null },
                new SumTest_Helper { IN = null }
            };

            Func<SumTest_Helper, int?> selector = s => s.IN;

            int? expected = 0;
            int? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        private void SumTest12_4()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { IN = 38 },
                new SumTest_Helper { IN = int.MaxValue },
                new SumTest_Helper { IN = null },
                new SumTest_Helper { IN = -7 }
            };

            Func<SumTest_Helper, int?> selector = s => s.IN;

            bool exception = false;
            try
            {
                int? actual = Sequence.Sum(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest13()
        {
            SumTest13_1();
            SumTest13_2();
            SumTest13_3();
        }

        private void SumTest13_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { I = 380 },
                new SumTest_Helper { I = 123 },
                new SumTest_Helper { I = -78 }
            };

            Func<SumTest_Helper, int> selector = s => s.I;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, int>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            int expected = 380 + 123 - 78;
            int actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest13_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, int> selector = s => s.I;

            int expected = 0;
            int actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest13_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { I = 380 },
                new SumTest_Helper { I = int.MaxValue },
                new SumTest_Helper { I = -78 }
            };

            Func<SumTest_Helper, int> selector = s => s.I;

            bool exception = false;
            try
            {
                int actual = Sequence.Sum(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long?&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest14()
        {
            SumTest14_1();
            SumTest14_2();
            SumTest14_3();
            SumTest14_4();
        }

        private void SumTest14_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { LN = 380 },
                new SumTest_Helper { LN = 123 },
                new SumTest_Helper { LN = null },
                new SumTest_Helper { LN = -78 }
            };

            Func<SumTest_Helper, long?> selector = s => s.LN;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, long?>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            long? expected = 380 + 123 - 78;
            long? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest14_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, long?> selector = s => s.LN;

            long? expected = 0;
            long? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest14_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { LN = null },
                new SumTest_Helper { LN = null },
                new SumTest_Helper { LN = null },
                new SumTest_Helper { LN = null }
            };

            Func<SumTest_Helper, long?> selector = s => s.LN;

            long? expected = 0;
            long? actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        private void SumTest14_4()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { LN = 380 },
                new SumTest_Helper { LN = long.MaxValue },
                new SumTest_Helper { LN = null },
                new SumTest_Helper { LN = -78 }
            };

            Func<SumTest_Helper, long?> selector = s => s.LN;

            bool exception = false;
            try
            {
                long? actual = Sequence.Sum(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum<T> did not return the expected value (test 4).");
        }

        /// <summary>
        ///A test for Sum&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,long&gt;)
        ///</summary>
        [TestMethod()]
        public void SumTest15()
        {
            SumTest15_1();
            SumTest15_2();
            SumTest15_3();
        }

        private void SumTest15_1()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { L = 380 },
                new SumTest_Helper { L = 123 },
                new SumTest_Helper { L = -78 }
            };

            Func<SumTest_Helper, long> selector = s => s.L;

            bool exception1 = false;
            try
            {
                Sequence.Sum(null, selector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.Sum(source, (Func<SumTest_Helper, long>)null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Sum<T> did not return the expected value (exceptions).");

            long expected = 380 + 123 - 78;
            long actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 1).");
        }

        private void SumTest15_2()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> { };

            Func<SumTest_Helper, long> selector = s => s.L;

            long expected = 0;
            long actual = Sequence.Sum(source, selector);

            Assert.IsTrue(expected == actual, "Sequence.Sum<T> did not return the expected value (test 2).");
        }

        private void SumTest15_3()
        {
            IEnumerable<SumTest_Helper> source = new List<SumTest_Helper> {
                new SumTest_Helper { L = 380 },
                new SumTest_Helper { L = long.MaxValue },
                new SumTest_Helper { L = -78 }
            };

            Func<SumTest_Helper, long> selector = s => s.L;

            bool exception = false;
            try
            {
                long actual = Sequence.Sum(source, selector);
            }
            catch (OverflowException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.Sum<T> did not return the expected value (test 3).");
        }

        private class SumTest_Helper
        {
            public decimal d = 0;
            public decimal? dN;
            public double D = 0;
            public double? DN;
            public int I = 0;
            public int? IN;
            public long L = 0;
            public long? LN;
        }

        #endregion

        #region 1.5.1 Take

        /// <summary>
        ///A test for Take&lt;&gt; (IEnumerable&lt;T&gt;, int)
        ///</summary>
        [TestMethod()]
        public void TakeTest()
        {
            TakeTest_1();
            TakeTest_2();
            TakeTest_3();
            TakeTest_4();
            TakeTest_5();
            TakeTest_6();
        }

        private void TakeTest_1()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 3;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Take((IEnumerable<int>)null, count))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.Take<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Take(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == count, "Sequence.Take<T> did not return the expected value (test 1).");

            int j = 5;
            foreach (int i in actual)
                Assert.IsTrue(j-- == i, "Sequence.Take<T> did not return the expected value (test 1).");
        }

        private void TakeTest_2()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = -1;

            IEnumerable<int> actual = Sequence.Take(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Take<T> did not return the expected value (test 2).");
        }

        private void TakeTest_3()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 0;

            IEnumerable<int> actual = Sequence.Take(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Take<T> did not return the expected value (test 3).");
        }

        private void TakeTest_4()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 6;

            IEnumerable<int> actual = Sequence.Take(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.Take<T> did not return the expected value (test 4).");

            int j = 5;
            foreach (int i in actual)
                Assert.IsTrue(j-- == i, "Sequence.Take<T> did not return the expected value (test 4).");
        }

        private void TakeTest_5()
        {
            IEnumerable<int> source = new int[] { 5, 4, 3, 2, 1 };

            int count = 5;

            IEnumerable<int> actual = Sequence.Take(source, count);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.Take<T> did not return the expected value (test 5).");

            int j = 5;
            foreach (int i in actual)
                Assert.IsTrue(j-- == i, "Sequence.Take<T> did not return the expected value (test 5).");
        }

        private void TakeTest_6()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";
            IEnumerable<string> source = new string[] { s1, s2, s3 };

            int count = 1;

            IEnumerable<string> actual = Sequence.Take(source, count);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == count, "Sequence.Take<T> did not return the expected value (test 6).");

            IEnumerator<string> enumerator = actual.GetEnumerator();
            enumerator.MoveNext();

            Assert.IsTrue(object.ReferenceEquals(enumerator.Current, s1), "Sequence.Take<T> did not return the expected value (test 6).");
        }

        #endregion

        #region 1.5.3 TakeWhile

        /// <summary>
        ///A test for TakeWhile&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void TakeWhileTest()
        {
            TakeWhileTest_1();
            TakeWhileTest_2();
            TakeWhileTest_3();
            TakeWhileTest_4();
            TakeWhileTest_5();
        }

        private void TakeWhileTest_1()
        {
            int[] source = new int[] { 2, 4, 8, 3, 7, 6 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.TakeWhile((IEnumerable<int>)null, predicate))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.TakeWhile(source, (Func<int, bool>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.TakeWhile<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.TakeWhile<T> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.TakeWhile<T> did not return the expected value (test 1).");
        }

        private void TakeWhileTest_2()
        {
            int[] source = new int[] { 1, 2, 4, 8, 3, 7, 6 };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.TakeWhile<T> did not return the expected value (test 2).");
        }

        private void TakeWhileTest_3()
        {
            int[] source = new int[] { 2, 4, 8, 6 };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.TakeWhile<T> did not return the expected value (test 3).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.TakeWhile<T> did not return the expected value (test 2).");
        }

        private void TakeWhileTest_4()
        {
            int[] source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.TakeWhile<T> did not return the expected value (test 4).");
        }

        private void TakeWhileTest_5()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string[] source = new string[] { s1, s2, "Steve", "John" };
            Func<string, bool> predicate = s => s.Length == 4;

            IEnumerable<string> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.TakeWhile<T> did not return the expected value (test 2).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, source[j++]), "Sequence.TakeWhile<T> did not return the expected value (test 5).");
        }

        /// <summary>
        ///A test for TakeWhile&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void TakeWhileTest1()
        {
            TakeWhileTest1_1();
            TakeWhileTest1_2();
            TakeWhileTest1_3();
            TakeWhileTest1_4();
            TakeWhileTest1_5();
        }

        private void TakeWhileTest1_1()
        {
            int[] source = new int[] { 2, 4, 8, 3, 7, 6 };
            Func<int, int, bool> predicate = ( i, k) => i % 2 == 0 && k < 2;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.TakeWhile((IEnumerable<int>)null, predicate))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.TakeWhile(source, (Func<int, int, bool>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.TakeWhile<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.TakeWhile<T> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.TakeWhile<T> did not return the expected value (test 1).");
        }

        private void TakeWhileTest1_2()
        {
            int[] source = new int[] { 1, 2, 4, 8, 3, 7, 6 };
            Func<int, int, bool> predicate = ( i, k) => k < 0;

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.TakeWhile<T> did not return the expected value (test 2).");
        }

        private void TakeWhileTest1_3()
        {
            int[] source = new int[] { 2, 4, 8, 6 };
            Func<int, int, bool> predicate = ( i, k) => k <= 3;

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.TakeWhile<T> did not return the expected value (test 3).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == source[j++], "Sequence.TakeWhile<T> did not return the expected value (test 2).");
        }

        private void TakeWhileTest1_4()
        {
            int[] source = new int[] { };
            Func<int, int, bool> predicate = ( i, k) => k >= 0;

            IEnumerable<int> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.TakeWhile<T> did not return the expected value (test 4).");
        }

        private void TakeWhileTest1_5()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string[] source = new string[] { s1, s2, "John", "Steve" };
            Func<string, int, bool> predicate = ( s, k) => s.Length == 4 && k < 2;

            IEnumerable<string> actual = Sequence.TakeWhile(source, predicate);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.TakeWhile<T> did not return the expected value (test 2).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, source[j++]), "Sequence.TakeWhile<T> did not return the expected value (test 5).");
        }

        #endregion

        #region 1.8.1 ThenBy

        /// <summary>
        ///A test for ThenBy&lt;,&gt; (OrderedSequence&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void ThenByTest()
        {
            ThenBy_Helper[] expected1 = new ThenBy_Helper[27];
            ThenBy_Helper[] expected2 = new ThenBy_Helper[27];

            List<ThenBy_Helper> lstA = new List<ThenBy_Helper> {
                expected2[ 3] = expected1[ 3] = new ThenBy_Helper { First = "A", Second = "B", Third = "A" },
                expected2[ 1] = expected1[ 0] = new ThenBy_Helper { First = "A", Second = "A", Third = "B" },
                expected2[ 2] = expected1[ 1] = new ThenBy_Helper { First = "A", Second = "A", Third = "C" },
                expected2[ 8] = expected1[ 6] = new ThenBy_Helper { First = "A", Second = "C", Third = "C" },
                expected2[ 6] = expected1[ 7] = new ThenBy_Helper { First = "A", Second = "C", Third = "A" },
                expected2[ 4] = expected1[ 4] = new ThenBy_Helper { First = "A", Second = "B", Third = "B" },
                expected2[ 5] = expected1[ 5] = new ThenBy_Helper { First = "A", Second = "B", Third = "C" },
                expected2[ 0] = expected1[ 2] = new ThenBy_Helper { First = "A", Second = "A", Third = "A" },
                expected2[ 7] = expected1[ 8] = new ThenBy_Helper { First = "A", Second = "C", Third = "B" }
            };

            List<ThenBy_Helper> lstB = new List<ThenBy_Helper> {
                expected2[16] = expected1[15] = new ThenBy_Helper { First = "B", Second = "C", Third = "B" },
                expected2[10] = expected1[ 9] = new ThenBy_Helper { First = "B", Second = "A", Third = "B" },
                expected2[11] = expected1[10] = new ThenBy_Helper { First = "B", Second = "A", Third = "C" },
                expected2[12] = expected1[12] = new ThenBy_Helper { First = "B", Second = "B", Third = "A" },
                expected2[ 9] = expected1[11] = new ThenBy_Helper { First = "B", Second = "A", Third = "A" },
                expected2[14] = expected1[13] = new ThenBy_Helper { First = "B", Second = "B", Third = "C" },
                expected2[15] = expected1[16] = new ThenBy_Helper { First = "B", Second = "C", Third = "A" },
                expected2[13] = expected1[14] = new ThenBy_Helper { First = "B", Second = "B", Third = "B" },
                expected2[17] = expected1[17] = new ThenBy_Helper { First = "B", Second = "C", Third = "C" }
            };

            List<ThenBy_Helper> lstC = new List<ThenBy_Helper> {
                expected2[21] = expected1[21] = new ThenBy_Helper { First = "C", Second = "B", Third = "A" },
                expected2[22] = expected1[22] = new ThenBy_Helper { First = "C", Second = "B", Third = "B" },
                expected2[20] = expected1[18] = new ThenBy_Helper { First = "C", Second = "A", Third = "C" },
                expected2[25] = expected1[24] = new ThenBy_Helper { First = "C", Second = "C", Third = "B" },
                expected2[19] = expected1[19] = new ThenBy_Helper { First = "C", Second = "A", Third = "B" },
                expected2[24] = expected1[25] = new ThenBy_Helper { First = "C", Second = "C", Third = "A" },
                expected2[18] = expected1[20] = new ThenBy_Helper { First = "C", Second = "A", Third = "A" },
                expected2[23] = expected1[23] = new ThenBy_Helper { First = "C", Second = "B", Third = "C" },
                expected2[26] = expected1[26] = new ThenBy_Helper { First = "C", Second = "C", Third = "C" }
            };

            List<List<ThenBy_Helper>> lst = new List<List<ThenBy_Helper>> { lstA, lstB, lstC };
            List<ThenBy_Helper> source = new List<ThenBy_Helper>();
            source.AddRange(lstA);
            source.AddRange(lstB);
            source.AddRange(lstC);

            Func<ThenBy_Helper, string> keySelector1 = t => t.First;
            Func<ThenBy_Helper, string> keySelector2 = t => t.Second;
            Func<ThenBy_Helper, string> keySelector3 = t => t.Third;

            OrderedSequence<ThenBy_Helper> o = Sequence.OrderBy(source, keySelector1);

            bool exception1 = false;
            try
            {
                Sequence.ThenBy<ThenBy_Helper, string>(null, keySelector2);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.ThenBy<ThenBy_Helper, string>(o, null);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.ThenBy<T, K> did not return the expected value (exceptions).");

            OrderedSequence<ThenBy_Helper> actual1 = Sequence.ThenBy(o, keySelector2);

            int j = 0;
            foreach (ThenBy_Helper h in actual1)
                Assert.IsTrue(object.ReferenceEquals(h, expected1[j++]), "Sequence.ThenBy<T, K> did not return the expected value (order 1).");

            OrderedSequence<ThenBy_Helper> actual2 = Sequence.ThenBy(actual1, keySelector3);

            j = 0;
            foreach (ThenBy_Helper h in actual2)
                Assert.IsTrue(object.ReferenceEquals(h, expected2[j++]), "Sequence.ThenBy<T, K> did not return the expected value (order 2).");
        }

        /// <summary>
        ///A test for ThenBy&lt;,&gt; (OrderedSequence&lt;T&gt;, Func&lt;T,K&gt;, IComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void ThenByTest1()
        {
            ThenBy_Helper[] expected1 = new ThenBy_Helper[27];
            ThenBy_Helper[] expected2 = new ThenBy_Helper[27];

            List<ThenBy_Helper> lstA = new List<ThenBy_Helper> {
                expected2[ 3] = expected1[ 3] = new ThenBy_Helper { First = "A", Second = "b", Third = "A" },
                expected2[ 1] = expected1[ 0] = new ThenBy_Helper { First = "A", Second = "A", Third = "B" },
                expected2[ 2] = expected1[ 1] = new ThenBy_Helper { First = "a", Second = "A", Third = "C" },
                expected2[ 8] = expected1[ 6] = new ThenBy_Helper { First = "A", Second = "c", Third = "C" },
                expected2[ 6] = expected1[ 7] = new ThenBy_Helper { First = "A", Second = "C", Third = "A" },
                expected2[ 4] = expected1[ 4] = new ThenBy_Helper { First = "A", Second = "B", Third = "B" },
                expected2[ 5] = expected1[ 5] = new ThenBy_Helper { First = "a", Second = "B", Third = "C" },
                expected2[ 0] = expected1[ 2] = new ThenBy_Helper { First = "A", Second = "A", Third = "a" },
                expected2[ 7] = expected1[ 8] = new ThenBy_Helper { First = "A", Second = "C", Third = "B" }
            };

            List<ThenBy_Helper> lstB = new List<ThenBy_Helper> {
                expected2[16] = expected1[15] = new ThenBy_Helper { First = "B", Second = "c", Third = "B" },
                expected2[10] = expected1[ 9] = new ThenBy_Helper { First = "B", Second = "A", Third = "B" },
                expected2[11] = expected1[10] = new ThenBy_Helper { First = "b", Second = "A", Third = "c" },
                expected2[12] = expected1[12] = new ThenBy_Helper { First = "B", Second = "B", Third = "A" },
                expected2[ 9] = expected1[11] = new ThenBy_Helper { First = "B", Second = "a", Third = "A" },
                expected2[14] = expected1[13] = new ThenBy_Helper { First = "b", Second = "b", Third = "C" },
                expected2[15] = expected1[16] = new ThenBy_Helper { First = "B", Second = "C", Third = "A" },
                expected2[13] = expected1[14] = new ThenBy_Helper { First = "B", Second = "B", Third = "b" },
                expected2[17] = expected1[17] = new ThenBy_Helper { First = "b", Second = "C", Third = "C" }
            };

            List<ThenBy_Helper> lstC = new List<ThenBy_Helper> {
                expected2[21] = expected1[21] = new ThenBy_Helper { First = "c", Second = "B", Third = "A" },
                expected2[22] = expected1[22] = new ThenBy_Helper { First = "C", Second = "B", Third = "B" },
                expected2[20] = expected1[18] = new ThenBy_Helper { First = "C", Second = "a", Third = "C" },
                expected2[25] = expected1[24] = new ThenBy_Helper { First = "C", Second = "c", Third = "b" },
                expected2[19] = expected1[19] = new ThenBy_Helper { First = "c", Second = "A", Third = "B" },
                expected2[24] = expected1[25] = new ThenBy_Helper { First = "C", Second = "C", Third = "A" },
                expected2[18] = expected1[20] = new ThenBy_Helper { First = "c", Second = "A", Third = "a" },
                expected2[23] = expected1[23] = new ThenBy_Helper { First = "C", Second = "b", Third = "c" },
                expected2[26] = expected1[26] = new ThenBy_Helper { First = "C", Second = "c", Third = "C" }
            };

            List<List<ThenBy_Helper>> lst = new List<List<ThenBy_Helper>> { lstA, lstB, lstC };
            List<ThenBy_Helper> source = new List<ThenBy_Helper>();
            source.AddRange(lstA);
            source.AddRange(lstB);
            source.AddRange(lstC);

            Func<ThenBy_Helper, string> keySelector1 = t => t.First;
            Func<ThenBy_Helper, string> keySelector2 = t => t.Second;
            Func<ThenBy_Helper, string> keySelector3 = t => t.Third;

            OrderedSequence<ThenBy_Helper> o = Sequence.OrderBy(source, keySelector1, new ThenByComparer_Helper<string>());
            OrderedSequence<ThenBy_Helper> actual1 = Sequence.ThenBy(o, keySelector2, new ThenByComparer_Helper<string>());

            int j = 0;
            foreach (ThenBy_Helper h in actual1)
                Assert.IsTrue(object.ReferenceEquals(h, expected1[j++]), "Sequence.ThenBy<T, K> did not return the expected value (order 1).");

            OrderedSequence<ThenBy_Helper> actual2 = Sequence.ThenBy(actual1, keySelector3, new ThenByComparer_Helper<string>());

            j = 0;
            foreach (ThenBy_Helper h in actual2)
                Assert.IsTrue(object.ReferenceEquals(h, expected2[j++]), "Sequence.ThenBy<T, K> did not return the expected value (order 2).");
        }

        /// <summary>
        ///A test for ThenByDescending&lt;,&gt; (OrderedSequence&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void ThenByDescendingTest()
        {
            ThenBy_Helper[] expected1 = new ThenBy_Helper[27];
            ThenBy_Helper[] expected2 = new ThenBy_Helper[27];

            List<ThenBy_Helper> lstA = new List<ThenBy_Helper> {
                expected2[ 5] = expected1[ 3] = new ThenBy_Helper { First = "A", Second = "B", Third = "A" },
                expected2[ 7] = expected1[ 6] = new ThenBy_Helper { First = "A", Second = "A", Third = "B" },
                expected2[ 6] = expected1[ 7] = new ThenBy_Helper { First = "A", Second = "A", Third = "C" },
                expected2[ 0] = expected1[ 0] = new ThenBy_Helper { First = "A", Second = "C", Third = "C" },
                expected2[ 2] = expected1[ 1] = new ThenBy_Helper { First = "A", Second = "C", Third = "A" },
                expected2[ 4] = expected1[ 4] = new ThenBy_Helper { First = "A", Second = "B", Third = "B" },
                expected2[ 3] = expected1[ 5] = new ThenBy_Helper { First = "A", Second = "B", Third = "C" },
                expected2[ 8] = expected1[ 8] = new ThenBy_Helper { First = "A", Second = "A", Third = "A" },
                expected2[ 1] = expected1[ 2] = new ThenBy_Helper { First = "A", Second = "C", Third = "B" }
            };

            List<ThenBy_Helper> lstB = new List<ThenBy_Helper> {
                expected2[10] = expected1[ 9] = new ThenBy_Helper { First = "B", Second = "C", Third = "B" },
                expected2[16] = expected1[15] = new ThenBy_Helper { First = "B", Second = "A", Third = "B" },
                expected2[15] = expected1[16] = new ThenBy_Helper { First = "B", Second = "A", Third = "C" },
                expected2[14] = expected1[12] = new ThenBy_Helper { First = "B", Second = "B", Third = "A" },
                expected2[17] = expected1[17] = new ThenBy_Helper { First = "B", Second = "A", Third = "A" },
                expected2[12] = expected1[13] = new ThenBy_Helper { First = "B", Second = "B", Third = "C" },
                expected2[11] = expected1[10] = new ThenBy_Helper { First = "B", Second = "C", Third = "A" },
                expected2[13] = expected1[14] = new ThenBy_Helper { First = "B", Second = "B", Third = "B" },
                expected2[ 9] = expected1[11] = new ThenBy_Helper { First = "B", Second = "C", Third = "C" }
            };

            List<ThenBy_Helper> lstC = new List<ThenBy_Helper> {
                expected2[23] = expected1[21] = new ThenBy_Helper { First = "C", Second = "B", Third = "A" },
                expected2[22] = expected1[22] = new ThenBy_Helper { First = "C", Second = "B", Third = "B" },
                expected2[24] = expected1[24] = new ThenBy_Helper { First = "C", Second = "A", Third = "C" },
                expected2[19] = expected1[18] = new ThenBy_Helper { First = "C", Second = "C", Third = "B" },
                expected2[25] = expected1[25] = new ThenBy_Helper { First = "C", Second = "A", Third = "B" },
                expected2[20] = expected1[19] = new ThenBy_Helper { First = "C", Second = "C", Third = "A" },
                expected2[26] = expected1[26] = new ThenBy_Helper { First = "C", Second = "A", Third = "A" },
                expected2[21] = expected1[23] = new ThenBy_Helper { First = "C", Second = "B", Third = "C" },
                expected2[18] = expected1[20] = new ThenBy_Helper { First = "C", Second = "C", Third = "C" }
            };

            List<List<ThenBy_Helper>> lst = new List<List<ThenBy_Helper>> { lstA, lstB, lstC };
            List<ThenBy_Helper> source = new List<ThenBy_Helper>();
            source.AddRange(lstA);
            source.AddRange(lstB);
            source.AddRange(lstC);

            Func<ThenBy_Helper, string> keySelector1 = t => t.First;
            Func<ThenBy_Helper, string> keySelector2 = t => t.Second;
            Func<ThenBy_Helper, string> keySelector3 = t => t.Third;

            OrderedSequence<ThenBy_Helper> o = Sequence.OrderBy(source, keySelector1);
            OrderedSequence<ThenBy_Helper> actual1 = Sequence.ThenByDescending(o, keySelector2);

            int j = 0;
            foreach (ThenBy_Helper h in actual1)
                Assert.IsTrue(object.ReferenceEquals(h, expected1[j++]), "Sequence.ThenByDescending<T, K> did not return the expected value (order 1).");

            OrderedSequence<ThenBy_Helper> actual2 = Sequence.ThenByDescending(actual1, keySelector3);

            j = 0;
            foreach (ThenBy_Helper h in actual2)
                Assert.IsTrue(object.ReferenceEquals(h, expected2[j++]), "Sequence.ThenByDescending<T, K> did not return the expected value (order 2).");
        }

        /// <summary>
        ///A test for ThenByDescending&lt;,&gt; (OrderedSequence&lt;T&gt;, Func&lt;T,K&gt;, IComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void ThenByDescendingTest1()
        {
            ThenBy_Helper[] expected1 = new ThenBy_Helper[27];
            ThenBy_Helper[] expected2 = new ThenBy_Helper[27];

            List<ThenBy_Helper> lstA = new List<ThenBy_Helper> {
                expected2[ 5] = expected1[ 3] = new ThenBy_Helper { First = "A", Second = "B", Third = "A" },
                expected2[ 7] = expected1[ 6] = new ThenBy_Helper { First = "A", Second = "A", Third = "B" },
                expected2[ 6] = expected1[ 7] = new ThenBy_Helper { First = "A", Second = "A", Third = "C" },
                expected2[ 0] = expected1[ 0] = new ThenBy_Helper { First = "A", Second = "C", Third = "C" },
                expected2[ 2] = expected1[ 1] = new ThenBy_Helper { First = "A", Second = "C", Third = "A" },
                expected2[ 4] = expected1[ 4] = new ThenBy_Helper { First = "A", Second = "B", Third = "B" },
                expected2[ 3] = expected1[ 5] = new ThenBy_Helper { First = "A", Second = "B", Third = "C" },
                expected2[ 8] = expected1[ 8] = new ThenBy_Helper { First = "A", Second = "A", Third = "A" },
                expected2[ 1] = expected1[ 2] = new ThenBy_Helper { First = "A", Second = "C", Third = "B" }
            };

            List<ThenBy_Helper> lstB = new List<ThenBy_Helper> {
                expected2[10] = expected1[ 9] = new ThenBy_Helper { First = "B", Second = "C", Third = "B" },
                expected2[16] = expected1[15] = new ThenBy_Helper { First = "B", Second = "A", Third = "B" },
                expected2[15] = expected1[16] = new ThenBy_Helper { First = "B", Second = "A", Third = "C" },
                expected2[14] = expected1[12] = new ThenBy_Helper { First = "B", Second = "B", Third = "A" },
                expected2[17] = expected1[17] = new ThenBy_Helper { First = "B", Second = "A", Third = "A" },
                expected2[12] = expected1[13] = new ThenBy_Helper { First = "B", Second = "B", Third = "C" },
                expected2[11] = expected1[10] = new ThenBy_Helper { First = "B", Second = "C", Third = "A" },
                expected2[13] = expected1[14] = new ThenBy_Helper { First = "B", Second = "B", Third = "B" },
                expected2[ 9] = expected1[11] = new ThenBy_Helper { First = "B", Second = "C", Third = "C" }
            };

            List<ThenBy_Helper> lstC = new List<ThenBy_Helper> {
                expected2[23] = expected1[21] = new ThenBy_Helper { First = "C", Second = "B", Third = "A" },
                expected2[22] = expected1[22] = new ThenBy_Helper { First = "C", Second = "B", Third = "B" },
                expected2[24] = expected1[24] = new ThenBy_Helper { First = "C", Second = "A", Third = "C" },
                expected2[19] = expected1[18] = new ThenBy_Helper { First = "C", Second = "C", Third = "B" },
                expected2[25] = expected1[25] = new ThenBy_Helper { First = "C", Second = "A", Third = "B" },
                expected2[20] = expected1[19] = new ThenBy_Helper { First = "C", Second = "C", Third = "A" },
                expected2[26] = expected1[26] = new ThenBy_Helper { First = "C", Second = "A", Third = "A" },
                expected2[21] = expected1[23] = new ThenBy_Helper { First = "C", Second = "B", Third = "C" },
                expected2[18] = expected1[20] = new ThenBy_Helper { First = "C", Second = "C", Third = "C" }
            };

            List<List<ThenBy_Helper>> lst = new List<List<ThenBy_Helper>> { lstA, lstB, lstC };
            List<ThenBy_Helper> source = new List<ThenBy_Helper>();
            source.AddRange(lstA);
            source.AddRange(lstB);
            source.AddRange(lstC);

            Func<ThenBy_Helper, string> keySelector1 = t => t.First;
            Func<ThenBy_Helper, string> keySelector2 = t => t.Second;
            Func<ThenBy_Helper, string> keySelector3 = t => t.Third;

            OrderedSequence<ThenBy_Helper> o = Sequence.OrderBy(source, keySelector1, new ThenByComparer_Helper<string>());
            OrderedSequence<ThenBy_Helper> actual1 = Sequence.ThenByDescending(o, keySelector2, new ThenByComparer_Helper<string>());

            int j = 0;
            foreach (ThenBy_Helper h in actual1)
                Assert.IsTrue(object.ReferenceEquals(h, expected1[j++]), "Sequence.ThenByDescending<T, K> did not return the expected value (order 1).");

            OrderedSequence<ThenBy_Helper> actual2 = Sequence.ThenByDescending(actual1, keySelector3, new ThenByComparer_Helper<string>());

            j = 0;
            foreach (ThenBy_Helper h in actual2)
                Assert.IsTrue(object.ReferenceEquals(h, expected2[j++]), "Sequence.ThenByDescending<T, K> did not return the expected value (order 2).");
        }

        private class ThenBy_Helper
        {
            public string First;
            public string Second;
            public string Third;
        }

        private class ThenByComparer_Helper<T> : IComparer<T>
        {
            private IComparer _comparer;

            public ThenByComparer_Helper()
            {
                _comparer = new ThenByComparerNG_Helper();
            }

            public int Compare(T a, T b)
            {
                return _comparer.Compare(a, b);
            }
        }

        private class ThenByComparerNG_Helper : IComparer
        {
            public int Compare(object a, object b)
            {
                if (a is string && b is string)
                {
                    string sa = ((string)a).ToLower();
                    string sb = ((string)b).ToLower();

                    return sa.CompareTo(sb);
                }
                else
                    throw new Exception(); // quick-n-dirty
            }
        }

        #endregion

        #region 1.11.2 ToArray

        /// <summary>
        ///A test for ToArray&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void ToArrayTest()
        {
            ToArrayTest_1();
            ToArrayTest_2();
        }

        private void ToArrayTest_1()
        {
            IEnumerable<int> source = new List<int> { 1, 3, 2 };

            bool exception1 = false;
            try
            {
                Sequence.ToArray<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.ToArray<T> did not return the expected value (exceptions).");


            int[] expected = new int[] { 1, 3, 2 };
            int[] actual = Sequence.ToArray(source);

            int j = 0;
            foreach (int i in source)
                Assert.IsTrue(i == actual[j++], "Sequence.ToArray<T> did not return the expected value (test 1).");
        }

        private void ToArrayTest_2()
        {
            string s1 = "Bart";

            IEnumerable<string> source = new List<string> { s1, "John", "Bill", s1, "Rob" };

            string[] expected = new string[] { s1, "John", "Bill", s1, "Rob" };
            string[] actual = Sequence.ToArray(source);

            int j = 0;
            foreach (string s in source)
                Assert.IsTrue(object.ReferenceEquals(s, actual[j++]), "Sequence.ToArray<T> did not return the expected value (test 2).");
        }

        #endregion

        #region 1.11.4 ToDictionary

        /// <summary>
        ///A test for ToDictionary&lt;,,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, Func&lt;T,E&gt;)
        ///</summary>
        [TestMethod()]
        public void ToDictionaryTest()
        {
            ToDictionaryTest_1();
            ToDictionaryTest_2();
            ToDictionaryTest_3();
        }

        private void ToDictionaryTest_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = s1 };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = s2 };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 3, S = s3 };

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;
            Func<ToDictionary_Helper, string> elementSelector = d => d.S;

            bool exception1 = false;
            try
            {
                Sequence.ToDictionary<ToDictionary_Helper, int, string>(null, keySelector, elementSelector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.ToDictionary<ToDictionary_Helper, int, string>(source, null, elementSelector);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            bool exception3 = false;
            try
            {
                Sequence.ToDictionary<ToDictionary_Helper, int, string>(source, keySelector, null);
            }
            catch (ArgumentNullException)
            {
                exception3 = true;
            }

            Assert.IsTrue(exception1 && exception2 && exception3, "Sequence.ToDictionary<T, K, E> did not return the expected value (exceptions).");

            Dictionary<int, string> actual = Sequence.ToDictionary(source, keySelector, elementSelector);

            Assert.IsTrue(actual.Keys.Count == 3, "Sequence.ToDictionary<T, K, E> did not return the expected value (test 1).");
            Assert.IsTrue(object.ReferenceEquals(actual[1], s1) && object.ReferenceEquals(actual[2], s2) && object.ReferenceEquals(actual[3], s3), "Sequence.ToDictionary<T, K> did not return the expected value (test 1).");
        }

        private void ToDictionaryTest_2()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = "Bill" };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 1, S = "John" }; // duplicate key

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;
            Func<ToDictionary_Helper, string> elementSelector = d => d.S;

            bool exception = false;
            try
            {
                Dictionary<int, string> actual = Sequence.ToDictionary(source, keySelector, elementSelector);
            }
            catch (ArgumentException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToDictionary<T, K, E> did not return the expected value (test 2).");
        }

        private void ToDictionaryTest_3()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = null }; // null key
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 3, S = "John" };

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, string> keySelector = d => d.S;
            Func<ToDictionary_Helper, int> elementSelector = d => d.I;

            bool exception = false;
            try
            {
                Dictionary<string, int> actual = Sequence.ToDictionary(source, keySelector, elementSelector);
            }
            catch (ArgumentNullException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToDictionary<T, K, E> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for ToDictionary&lt;,,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, Func&lt;T,E&gt;, IEqualityComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void ToDictionaryTest1()
        {
            ToDictionaryTest1_1();
            ToDictionaryTest1_2();
        }

        private void ToDictionaryTest1_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = s1 };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = s2 };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 3, S = s3 };

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;
            Func<ToDictionary_Helper, string> elementSelector = d => d.S;

            Dictionary<int, string> actual = Sequence.ToDictionary(source, keySelector, elementSelector, new ToDirectoryComparer_Helper<int>());

            Assert.IsTrue(actual.Keys.Count == 3, "Sequence.ToDictionary<T, K, E> did not return the expected value (test 1).");
            Assert.IsTrue(object.ReferenceEquals(actual[1], s1) && object.ReferenceEquals(actual[2], s2) && object.ReferenceEquals(actual[3], s3), "Sequence.ToDictionary<T, K> did not return the expected value (test 1).");
        }

        private void ToDictionaryTest1_2()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = "Bill" };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = -1, S = "John" }; // duplicate key

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;
            Func<ToDictionary_Helper, string> elementSelector = d => d.S;

            bool exception = false;
            try
            {
                Dictionary<int, string> actual = Sequence.ToDictionary(source, keySelector, elementSelector, new ToDirectoryComparer_Helper<int>());
            }
            catch (ArgumentException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToDictionary<T, K, E> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for ToDictionary&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void ToDictionaryTest2()
        {
            ToDictionaryTest2_1();
            ToDictionaryTest2_2();
            ToDictionaryTest2_3();
        }

        private void ToDictionaryTest2_1()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = "Bill" };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 3, S = "John" };

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;

            Dictionary<int, ToDictionary_Helper> actual = Sequence.ToDictionary(source, keySelector);

            Assert.IsTrue(actual.Keys.Count == 3, "Sequence.ToDictionary<T, K> did not return the expected value (test 1).");
            Assert.IsTrue(object.ReferenceEquals(actual[1], d1) && object.ReferenceEquals(actual[2], d2) && object.ReferenceEquals(actual[3], d3), "Sequence.ToDictionary<T, K> did not return the expected value (test 1).");
        }

        private void ToDictionaryTest2_2()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = "Bill" };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 1, S = "John" }; // duplicate key

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;

            bool exception = false;
            try
            {
                Dictionary<int, ToDictionary_Helper> actual = Sequence.ToDictionary(source, keySelector);
            }
            catch (ArgumentException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToDictionary<T, K> did not return the expected value (test 2).");
        }

        private void ToDictionaryTest2_3()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = null }; // null key
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 3, S = "John" };

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, string> keySelector = d => d.S;

            bool exception = false;
            try
            {
                Dictionary<string, ToDictionary_Helper> actual = Sequence.ToDictionary(source, keySelector);
            }
            catch (ArgumentNullException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToDictionary<T, K> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for ToDictionary&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, IEqualityComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void ToDictionaryTest3()
        {
            ToDictionaryTest3_1();
            ToDictionaryTest3_2();
        }

        private void ToDictionaryTest3_1()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = "Bill" };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = 3, S = "John" };

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;

            Dictionary<int, ToDictionary_Helper> actual = Sequence.ToDictionary(source, keySelector, new ToDirectoryComparer_Helper<int>());

            Assert.IsTrue(actual.Keys.Count == 3, "Sequence.ToDictionary<T, K> did not return the expected value (test 1).");
            Assert.IsTrue(object.ReferenceEquals(actual[1], d1) && object.ReferenceEquals(actual[2], d2) && object.ReferenceEquals(actual[3], d3), "Sequence.ToDictionary<T, K> did not return the expected value (test 1).");
        }

        private void ToDictionaryTest3_2()
        {
            ToDictionary_Helper d1 = new ToDictionary_Helper { I = 1, S = "Bart" };
            ToDictionary_Helper d2 = new ToDictionary_Helper { I = 2, S = "Bill" };
            ToDictionary_Helper d3 = new ToDictionary_Helper { I = -1, S = "John" }; // duplicate key

            IEnumerable<ToDictionary_Helper> source = new List<ToDictionary_Helper> { d1, d2, d3 };
            Func<ToDictionary_Helper, int> keySelector = d => d.I;

            bool exception = false;
            try
            {
                Dictionary<int, ToDictionary_Helper> actual = Sequence.ToDictionary(source, keySelector, new ToDirectoryComparer_Helper<int>());
            }
            catch (ArgumentException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToDictionary<T, K> did not return the expected value (test 2).");
        }

        private class ToDictionary_Helper
        {
            public int I;
            public string S;
        }

        private class ToDirectoryComparer_Helper<T> : IEqualityComparer<T>
        {
            private IEqualityComparer _comparer;

            public ToDirectoryComparer_Helper()
            {
                _comparer = new ToDirectoryComparerNG_Helper();
            }

            public bool Equals(T a, T b)
            {
                return _comparer.Equals(a, b);
            }

            public int GetHashCode(T a)
            {
                return _comparer.GetHashCode(a);
            }
        }

        private class ToDirectoryComparerNG_Helper : IEqualityComparer
        {
            public new bool Equals(object a, object b)
            {
                if (a is int && b is int)
                {
                    int ia = Math.Abs((int)a);
                    int ib = Math.Abs((int)b);

                    return ia == ib;
                }
                else
                    throw new Exception(); // quick-n-dirty
            }

            public int GetHashCode(object a)
            {
                if (a is int)
                    return Math.Abs((int)a).GetHashCode();
                else
                    throw new Exception(); // quick-n-dirty
            }
        }

        #endregion

        #region 1.11.3 ToList

        /// <summary>
        ///A test for ToList&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void ToListTest()
        {
            ToListTest_1();
            ToListTest_2();
        }

        private void ToListTest_1()
        {
            IEnumerable<int> source = new int[] { 1, 3, 2 };

            bool exception1 = false;
            try
            {
                Sequence.ToList<int>(null);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            Assert.IsTrue(exception1, "Sequence.ToList<T> did not return the expected value (exceptions).");

            List<int> expected = new List<int> { 1, 3, 2 };
            List<int> actual = Sequence.ToList(source);

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in source)
            {
                enumerator.MoveNext();

                Assert.IsTrue(enumerator.Current == i, "Sequence.ToList<T> did not return the expected value (test 1).");
            }
        }

        private void ToListTest_2()
        {
            string s1 = "Bart";

            IEnumerable<string> source = new string[] { s1, "John", "Bill", s1, "Rob" };

            List<string> expected = new List<string> { s1, "John", "Bill", s1, "Rob" };
            List<string> actual = Sequence.ToList(source);

            IEnumerator<string> enumerator = actual.GetEnumerator();
            foreach (string s in source)
            {
                enumerator.MoveNext();

                Assert.IsTrue(object.ReferenceEquals(enumerator.Current, s), "Sequence.ToList<T> did not return the expected value (test 2).");
            }
        }

        #endregion

        #region 1.11.5 ToLookup

        /// <summary>
        ///A test for ToLookup&lt;,,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, Func&lt;T,E&gt;)
        ///</summary>
        [TestMethod()]
        public void ToLookupTest()
        {
            ToLookupTest_1();
            ToLookupTest_2();
            ToLookupTest_3();
        }

        private void ToLookupTest_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = 3, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;
            Func<ToLookup_Helper, string> elementSelector = d => d.S;

            bool exception1 = false;
            try
            {
                Sequence.ToLookup<ToLookup_Helper, int, string>(null, keySelector, elementSelector);
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                Sequence.ToLookup<ToLookup_Helper, int, string>(source, null, elementSelector);
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            bool exception3 = false;
            try
            {
                Sequence.ToLookup<ToLookup_Helper, int, string>(source, keySelector, null);
            }
            catch (ArgumentNullException)
            {
                exception3 = true;
            }

            Assert.IsTrue(exception1 && exception2 && exception3, "Sequence.ToLookup<T, K, E> did not return the expected value (exceptions).");

            Lookup<int, string> actual = Sequence.ToLookup(source, keySelector, elementSelector);

            Assert.IsTrue(actual.Count == 3, "Sequence.ToLookup<T, K> did not return the expected value (test 1).");

            IEnumerator<string> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<string> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();
            IEnumerator<string> enum3 = actual[3].GetEnumerator(); enum3.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, s1) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, s2) && !enum2.MoveNext()
                && object.ReferenceEquals(enum3.Current, s3) && !enum3.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 1).");
        }

        private void ToLookupTest_2()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = 1, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;
            Func<ToLookup_Helper, string> elementSelector = d => d.S;

            Lookup<int, string> actual = Sequence.ToLookup(source, keySelector, elementSelector);

            Assert.IsTrue(actual.Count == 2, "Sequence.ToLookup<T, K> did not return the expected value (test 2).");

            IEnumerator<string> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<string> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, s1) && enum1.MoveNext()
                && object.ReferenceEquals(enum1.Current, s3) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, s2) && !enum2.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 2).");
        }

        private void ToLookupTest_3()
        {
            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = "Bart" };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = null }; // null key
            ToLookup_Helper d3 = new ToLookup_Helper { I = 3, S = "John" };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, string> keySelector = d => d.S;
            Func<ToLookup_Helper, int> elementSelector = d => d.I;

            bool exception = false;
            try
            {
                Lookup<string, int> actual = Sequence.ToLookup(source, keySelector, elementSelector);
            }
            catch (ArgumentNullException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToLookup<T, K> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for ToLookup&lt;,,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, Func&lt;T,E&gt;, IEqualityComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void ToLookupTest1()
        {
            ToLookupTest1_1();
            ToLookupTest1_2();
        }

        private void ToLookupTest1_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = 3, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;
            Func<ToLookup_Helper, string> elementSelector = d => d.S;

            Lookup<int, string> actual = Sequence.ToLookup(source, keySelector, elementSelector, new ToLookupComparer_Helper<int>());

            Assert.IsTrue(actual.Count == 3, "Sequence.ToLookup<T, K> did not return the expected value (test 1).");

            IEnumerator<string> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<string> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();
            IEnumerator<string> enum3 = actual[3].GetEnumerator(); enum3.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, s1) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, s2) && !enum2.MoveNext()
                && object.ReferenceEquals(enum3.Current, s3) && !enum3.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 1).");
        }

        private void ToLookupTest1_2()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = -1, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;
            Func<ToLookup_Helper, string> elementSelector = d => d.S;

            Lookup<int, string> actual = Sequence.ToLookup(source, keySelector, elementSelector, new ToLookupComparer_Helper<int>());

            Assert.IsTrue(actual.Count == 2, "Sequence.ToLookup<T, K> did not return the expected value (test 2).");

            IEnumerator<string> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<string> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, s1) && enum1.MoveNext()
                && object.ReferenceEquals(enum1.Current, s3) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, s2) && !enum2.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 2).");
        }

        /// <summary>
        ///A test for ToLookup&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;)
        ///</summary>
        [TestMethod()]
        public void ToLookupTest2()
        {
            ToLookupTest2_1();
            ToLookupTest2_2();
            ToLookupTest2_3();
        }

        private void ToLookupTest2_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = 3, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;

            Lookup<int, ToLookup_Helper> actual = Sequence.ToLookup(source, keySelector);

            Assert.IsTrue(actual.Count == 3, "Sequence.ToLookup<T, K> did not return the expected value (test 1).");

            IEnumerator<ToLookup_Helper> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<ToLookup_Helper> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();
            IEnumerator<ToLookup_Helper> enum3 = actual[3].GetEnumerator(); enum3.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, d1) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, d2) && !enum2.MoveNext()
                && object.ReferenceEquals(enum3.Current, d3) && !enum3.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 1).");
        }

        private void ToLookupTest2_2()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = 1, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;

            Lookup<int, ToLookup_Helper> actual = Sequence.ToLookup(source, keySelector);

            Assert.IsTrue(actual.Count == 2, "Sequence.ToLookup<T, K> did not return the expected value (test 2).");

            IEnumerator<ToLookup_Helper> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<ToLookup_Helper> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, d1) && enum1.MoveNext()
                && object.ReferenceEquals(enum1.Current, d3) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, d2) && !enum2.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 2).");
        }

        private void ToLookupTest2_3()
        {
            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = "Bart" };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = null }; // null key
            ToLookup_Helper d3 = new ToLookup_Helper { I = 3, S = "John" };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, string> keySelector = d => d.S;

            bool exception = false;
            try
            {
                Lookup<string, ToLookup_Helper> actual = Sequence.ToLookup(source, keySelector);
            }
            catch (ArgumentNullException)
            {
                exception = true;
            }

            Assert.IsTrue(exception, "Sequence.ToLookup<T, K> did not return the expected value (test 3).");
        }

        /// <summary>
        ///A test for ToLookup&lt;,&gt; (IEnumerable&lt;T&gt;, Func&lt;T,K&gt;, IEqualityComparer&lt;K&gt;)
        ///</summary>
        [TestMethod()]
        public void ToLookupTest3()
        {
            ToLookupTest3_1();
            ToLookupTest3_2();
        }

        private void ToLookupTest3_1()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = 3, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;

            Lookup<int, ToLookup_Helper> actual = Sequence.ToLookup(source, keySelector, new ToLookupComparer_Helper<int>());

            Assert.IsTrue(actual.Count == 3, "Sequence.ToLookup<T, K> did not return the expected value (test 1).");

            IEnumerator<ToLookup_Helper> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<ToLookup_Helper> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();
            IEnumerator<ToLookup_Helper> enum3 = actual[3].GetEnumerator(); enum3.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, d1) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, d2) && !enum2.MoveNext()
                && object.ReferenceEquals(enum3.Current, d3) && !enum3.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 1).");
        }

        private void ToLookupTest3_2()
        {
            string s1 = "Bart";
            string s2 = "Bill";
            string s3 = "John";

            ToLookup_Helper d1 = new ToLookup_Helper { I = 1, S = s1 };
            ToLookup_Helper d2 = new ToLookup_Helper { I = 2, S = s2 };
            ToLookup_Helper d3 = new ToLookup_Helper { I = -1, S = s3 };

            IEnumerable<ToLookup_Helper> source = new List<ToLookup_Helper> { d1, d2, d3 };
            Func<ToLookup_Helper, int> keySelector = d => d.I;

            Lookup<int, ToLookup_Helper> actual = Sequence.ToLookup(source, keySelector, new ToLookupComparer_Helper<int>());

            Assert.IsTrue(actual.Count == 2, "Sequence.ToLookup<T, K> did not return the expected value (test 2).");

            IEnumerator<ToLookup_Helper> enum1 = actual[1].GetEnumerator(); enum1.MoveNext();
            IEnumerator<ToLookup_Helper> enum2 = actual[2].GetEnumerator(); enum2.MoveNext();

            Assert.IsTrue(
                object.ReferenceEquals(enum1.Current, d1) && enum1.MoveNext()
                && object.ReferenceEquals(enum1.Current, d3) && !enum1.MoveNext()
                && object.ReferenceEquals(enum2.Current, d2) && !enum2.MoveNext()
                , "Sequence.ToLookup<T, K> did not return the expected value (test 2).");
        }

        private class ToLookup_Helper
        {
            public int I;
            public string S;
        }

        private class ToLookupComparer_Helper<T> : IEqualityComparer<T>
        {
            private IEqualityComparer _comparer;

            public ToLookupComparer_Helper()
            {
                _comparer = new ToLookupComparerNG_Helper();
            }

            public bool Equals(T a, T b)
            {
                return _comparer.Equals(a, b);
            }

            public int GetHashCode(T a)
            {
                return _comparer.GetHashCode(a);
            }
        }

        private class ToLookupComparerNG_Helper : IEqualityComparer
        {
            public new bool Equals(object a, object b)
            {
                if (a is int && b is int)
                {
                    int ia = Math.Abs((int)a);
                    int ib = Math.Abs((int)b);

                    return ia == ib;
                }
                else
                    throw new Exception(); // quick-n-dirty
            }

            public int GetHashCode(object a)
            {
                if (a is int)
                    return Math.Abs((int)a).GetHashCode();
                else
                    throw new Exception(); // quick-n-dirty
            }
        }

        #endregion

        #region 1.11.1 ToSequence

        /// <summary>
        ///A test for ToSequence&lt;&gt; (IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void ToSequenceTest()
        {
            IEnumerable<int> source = new List<int> { 1, 2, 3, 4, 9 };

            IEnumerable<int> actual = Sequence.ToSequence(source);

            Assert.IsTrue(object.ReferenceEquals(actual, source), "Sequence.ToSequence<T> did not return the expected value.");
        }

        #endregion

        #region 1.10.2 Union

        /// <summary>
        ///A test for Union&lt;&gt; (IEnumerable&lt;T&gt;, IEnumerable&lt;T&gt;)
        ///</summary>
        [TestMethod()]
        public void UnionTest()
        {
            UnionTest_1();
            UnionTest_2();
            UnionTest_3();
            UnionTest_4();
        }

        private void UnionTest_1()
        {
            IEnumerable<int> first = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> second = new List<int> { 3, 4, 5, 1, 7 };

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Union(first, null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Union(null, second))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Union<T> did not return the expected value (exceptions).");

            IEnumerable<int> expected = new List<int> { 1, 5, 2, 9, 3, 4, 7 };
            IEnumerable<int> actual = Sequence.Union(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Union<T> did not return the expected value (test 1).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Union<T> did not return the expected value (test 1).");
            }
        }

        private void UnionTest_2()
        {
            IEnumerable<int> first = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> second = new List<int> { };

            IEnumerable<int> expected = new List<int> { 1, 5, 2, 9 };
            IEnumerable<int> actual = Sequence.Union(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Union<T> did not return the expected value (test 2).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Union<T> did not return the expected value (test 2).");
            }
        }

        private void UnionTest_3()
        {
            IEnumerable<int> first = new List<int> { };
            IEnumerable<int> second = new List<int> { 3, 4, 5, 1, 7 };

            IEnumerable<int> expected = new List<int> { 3, 4, 5, 1, 7 };
            IEnumerable<int> actual = Sequence.Union(first, second);

            long n = 0;
            foreach (int i in expected)
                n++;
            foreach (int i in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Union<T> did not return the expected value (test 3).");

            IEnumerator<int> enumerator = actual.GetEnumerator();
            foreach (int i in actual)
            {
                enumerator.MoveNext();
                Assert.AreEqual(enumerator.Current, i, "Sequence.Union<T> did not return the expected value (test 3).");
            }
        }

        private void UnionTest_4()
        {
            string j1 = "John";
            IEnumerable<string> first = new List<string> { "Bart", j1, "Bill", "John", "Scott" };
            IEnumerable<string> second = new List<string> { "Steve", "Bart", "Rob", "Steve", j1 };

            IEnumerable<string> expected = new List<string> { "Bart", "John", "Bill", "Scott", "Steve", "Rob" };
            IEnumerable<string> actual = Sequence.Union(first, second);

            long n = 0;
            foreach (string s in expected)
                n++;
            foreach (string s in actual)
                n--;

            Assert.IsTrue(n == 0, "Sequence.Union<T> did not return the expected value (test 4).");

            IEnumerator<string> enumerator = actual.GetEnumerator();
            foreach (string s in actual)
            {
                enumerator.MoveNext();
                Assert.IsTrue(s.Equals(enumerator.Current), "Sequence.Union<T> did not return the expected value (test 4).");
            }
        }

        #endregion

        #region 1.3.1 Where

        /// <summary>
        ///A test for Where&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void WhereTest()
        {
            WhereTest_1();
            WhereTest_2();
            WhereTest_3();
            WhereTest_4();
            WhereTest_5();
        }

        private void WhereTest_1()
        {
            IEnumerable<int> source = new int[] { 1, 1, 0, 3, 2, 7, 4, 9, 11, 6, 8 };
            Func<int, bool> predicate = i => i % 2 == 0;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Where(null, predicate))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Where(source, (Func<int, bool>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Where<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.Where<T> did not return the expected value (test 1).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == (j++) * 2, "Sequence.Where<T> did not return the expected value (test 1).");
        }

        private void WhereTest_2()
        {
            IEnumerable<int> source = new int[] { 1, 1, 3, 7, 9, 11 };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Where<T> did not return the expected value (test 2).");
        }

        private void WhereTest_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Where<T> did not return the expected value (test 3).");
        }

        private void WhereTest_4()
        {
            IEnumerable<int> source = new int[] { 0, 2, 4, 6, 8 };
            Func<int, bool> predicate = i => i % 2 == 0;

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 5, "Sequence.Where<T> did not return the expected value (test 4).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == (j++) * 2, "Sequence.Where<T> did not return the expected value (test 4).");
        }

        private void WhereTest_5()
        {
            string s1 = "Bart";
            string s2 = "John";
            string s3 = "Bill";
            IEnumerable<string> source = new string[] { s1, s2, "Steve", s1, s3, "Rob" };
            Func<string, bool> predicate = s => s.Length == 4;

            string[] expected = new string[] { s1, s2, s1, s3 };
            IEnumerable<string> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.Where<T> did not return the expected value (test 5).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, expected[j++]), "Sequence.Where<T> did not return the expected value (test 5).");
        }

        /// <summary>
        ///A test for Where&lt;&gt; (IEnumerable&lt;T&gt;, Func&lt;T,int,bool&gt;)
        ///</summary>
        [TestMethod()]
        public void WhereTest1()
        {
            WhereTest1_1();
            WhereTest1_2();
            WhereTest1_3();
            WhereTest1_4();
            WhereTest1_5();
        }

        private void WhereTest1_1()
        {
            IEnumerable<int> source = new int[] { 1, 1, 0, 3, 2, 7, 4, 9, 11, 6, 8 };
            Func<int, int, bool> predicate = ( i, k) => i % 2 == 0 && k > 2;

            bool exception1 = false;
            try
            {
                foreach (int i in Sequence.Where(null, predicate))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception1 = true;
            }

            bool exception2 = false;
            try
            {
                foreach (int i in Sequence.Where(source, (Func<int, int, bool>)null))
                    ;
            }
            catch (ArgumentNullException)
            {
                exception2 = true;
            }

            Assert.IsTrue(exception1 && exception2, "Sequence.Where<T> did not return the expected value (exceptions).");

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 4, "Sequence.Where<T> did not return the expected value (test 1).");

            int j = 1;
            foreach (int i in actual)
                Assert.IsTrue(i == (j++) * 2, "Sequence.Where<T> did not return the expected value (test 1).");
        }

        private void WhereTest1_2()
        {
            IEnumerable<int> source = new int[] { 1, 1, 3, 7, 9, 11 };
            Func<int, int, bool> predicate = ( i, k) => k > 5;

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Where<T> did not return the expected value (test 2).");
        }

        private void WhereTest1_3()
        {
            IEnumerable<int> source = new int[] { };
            Func<int, int, bool> predicate = ( i, k) => k >= 0;

            IEnumerable<int> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 0, "Sequence.Where<T> did not return the expected value (test 3).");
        }

        private void WhereTest1_4()
        {
            IEnumerable<int> source = new int[] { 0, 2, 4, 6, 8 };
            Func<int, int, bool> predicate = ( i, k) => i % 2 == 0 && k % 2 == 1;

            IEnumerable<int> actual = Sequence.Where(source, predicate);
            int[] expected = new int[] { 2, 6 };

            long n = 0;
            foreach (int i in actual)
                n++;

            Assert.IsTrue(n == 2, "Sequence.Where<T> did not return the expected value (test 4).");

            int j = 0;
            foreach (int i in actual)
                Assert.IsTrue(i == expected[j++], "Sequence.Where<T> did not return the expected value (test 4).");
        }

        private void WhereTest1_5()
        {
            string s1 = "Bart";
            string s2 = "John";
            string s3 = "Bill";
            IEnumerable<string> source = new string[] { s1, s2, "Steve", s1, s3, "Rob" };
            Func<string, int, bool> predicate = ( s, k) => s.Length == 4 && k < 4;

            string[] expected = new string[] { s1, s2, s1 };
            IEnumerable<string> actual = Sequence.Where(source, predicate);

            long n = 0;
            foreach (string s in actual)
                n++;

            Assert.IsTrue(n == 3, "Sequence.Where<T> did not return the expected value (test 5).");

            int j = 0;
            foreach (string s in actual)
                Assert.IsTrue(object.ReferenceEquals(s, expected[j++]), "Sequence.Where<T> did not return the expected value (test 5).");
        }

        #endregion
    }
}
