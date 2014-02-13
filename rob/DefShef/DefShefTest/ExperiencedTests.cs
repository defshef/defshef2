using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefShef.Test
{
    [TestClass]
    public class ExperiencedTests
    {
        [TestMethod]
        public void FlattenTest()
        {
            object[] nestedList = { 1, 2, 3, new object[] { 4, 5, 6, new object[] { 7, 8, 9 }, 10, 11, 12 } };
            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                                           nestedList.Flatten().ToArray());
        }

        [TestMethod]
        public void NestedReplaceTest()
        {
            object[] nestedList = { 1, 2, 3, new object[] { 1, 2, 2, 3 }, new object[] { 1, new object[] { 2 }, 3 } };
            object[] output = nestedList.NestedReplace(2, 5).ToArray();
            object[] expected = {1, 5, 3, new object[] {1, 5, 5, 3}, new object[] {1, new object[] {5}, 3}};
            TestHelpers.AssertNestedListsAreEqual(expected, output);
        }

        [TestMethod]
        public void DeepMapDeepFilterDeepReduceTest()
        {
            object[] actual = {4, 5, new object[] {6, 7, new object[] { 8 }, 9}};
            Assert.AreEqual(155, actual.DeepFilter(typeof(int), i => !((int)i).IsDivisibleBy(2))
                                       .DeepMap(typeof(int), i => (int)i * (int)i)
                                       .DeepReduce());
        }

        [TestMethod]
        public void OddSquareSumWithFlattenTest()
        {
            object[] actual = { 4, 5, new object[] { 6, 7, new object[] { 8 }, 9 } };
            Assert.AreEqual(155, actual.Flatten()
                                       .Where(i => !((int)i).IsDivisibleBy(2))
                                       .Select(i => (int)i * (int)i)
                                       .Sum());
        }
    }
}
