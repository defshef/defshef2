using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefShef.Test
{
    [TestClass]
    public class BeginnerTests
    {
        [TestMethod]
        public void OddSquareSumTest()
        {
            Assert.AreEqual(155,
                            4.UpTo(9)
                             .Where(i => !i.IsDivisibleBy(2))
                             .Select(i => i * i)
                             .Sum());
        }

        [TestMethod]
        public void SliceTest()
        {
            CollectionAssert.AreEquivalent(new[] { 5, 6, 7, 8 },
                                           3.UpTo(9)
                                            .Slice(2, 4)
                                            .ToArray());
        }

        [TestMethod]
        public void FibonacciTest()
        {
            CollectionAssert.AreEquivalent(new[] { 1, 1, 2, 3, 5, 8, 13 },
                                           1.UpTo(7)
                                            .Select(i => i.Fibonacci())
                                            .ToArray());
        }

        [TestMethod]
        public void MemoizedFibonacciTest()
        {
            Func<int, int> fibonacciFunc = Extensions.Fibonacci;
            fibonacciFunc = fibonacciFunc.Memoize();
            CollectionAssert.AreEquivalent(new[] { 1, 1, 2, 3, 5, 8, 13 },
                                           1.UpTo(7)
                                            .Select(i => fibonacciFunc(i))
                                            .ToArray());
        }
    }
}