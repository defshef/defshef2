using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefShef.Test
{
    [TestClass]
    public class WarmupTests
    {
        [TestMethod]
        public void DoublingTest()
        {
            CollectionAssert.AreEquivalent(new[] { 2, 4, 6, 8 },
                                           1.UpTo(4)
                                            .Select(i => i * 2)
                                            .ToArray());
        }

        [TestMethod]
        public void EvensTest()
        {
            CollectionAssert.AreEquivalent(new[] { 1, 3, 5, 7 },
                                           1.UpTo(7)
                                            .Where(i => !i.IsDivisibleBy(2))
                                            .ToArray());
        }

        [TestMethod]
        public void DotProductTest()
        {
            Assert.AreEqual(120, 1.UpTo(5).DotProduct());
        }

        [TestMethod]
        public void ReverseTest()
        {
            CollectionAssert.AreEquivalent(new[] { 5, 4, 3, 2, 1 },
                                                  1.UpTo(5)
                                                   .Reverse()
                                                   .ToArray());
        }

        [TestMethod]
        public void GetNthItemTest()
        {
            Assert.AreEqual(7, 5.UpTo(8).GetNthItem(3));
        }
    }
}
