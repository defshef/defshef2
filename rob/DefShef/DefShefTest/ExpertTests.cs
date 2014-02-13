using DefShef.TicTacToe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefShef.Test
{
    [TestClass]
    public class ExpertTests
    {
        [TestMethod]
        public void TicTacToeTestA()
        {
            Board board = new Board("o, ,x,o,x, ,x, , ");
            Assert.AreEqual(Side.Cross, board.DeclareWinner());
        }

        [TestMethod]
        public void TicTacToeTestB()
        {
            Board board = new Board("x,o, , ,o, ,x,o, ");
            Assert.AreEqual(Side.Naught, board.DeclareWinner());
        }

        [TestMethod]
        public void DSLEvaluatorAsStaticsTest()
        {
            // ReSharper disable InvokeAsExtensionMethod
            CollectionAssert.AreEquivalent(new[] { 1, 3, 3, 7 },
                Extensions.Geek(Extensions.Burrito(Extensions.Meatspace(Extensions.Burrito(1, Extensions.Tap(4))),
                                1),
                                Extensions.Sheffield(Extensions.Burrito(Extensions.Steel(Extensions.Cowering(2)),
                                                                        2),
                                                     Extensions.Steel(Extensions.Cowering(Extensions.Cowering(Extensions.Tap(1)))))));
            // ReSharper restore InvokeAsExtensionMethod
        }

        [TestMethod]
        public void DSLEvaluatorAsExtensionsTest()
        {
            CollectionAssert.AreEquivalent(new[] { 1, 3, 3, 7 }, 1.Burrito(4.Tap()).Meatspace().Burrito(1).Geek(2.Cowering().Steel().Burrito(2).Sheffield(1.Tap().Cowering().Cowering().Steel())));
        }
    }
}
