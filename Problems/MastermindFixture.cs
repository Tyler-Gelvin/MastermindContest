using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OnlyProject
{
    [TestClass]
    public class MastermindFixture
    {
        [TestMethod]
        public void Test1()
        {
            var guess = new Mastermind("the big cat").Guess("tiger baby mauling");

            Assert.AreEqual(7, guess.Chars);
            Assert.AreEqual(1, guess.Positions);
            Assert.IsFalse(guess.Solved);
        }

        [TestMethod]
        public void GuessIsCounted()
        {
            var mastermind = new Mastermind("the big cat");
            mastermind.Guess("tiger baby mauling");

            Assert.AreEqual(1, mastermind.GuessCount);
        }

        [TestMethod]
        public void DotDoesNotCount()
        {
            var mastermind = new Mastermind("a.b");
            var result = mastermind.Guess("a.c");

            Assert.AreEqual(1, result.Positions);
        }

        [TestMethod]
        public void SolvedWithDot()
        {
            var mastermind = new Mastermind("a.b");
            var result = mastermind.Guess("a.b");

            Assert.IsTrue(result.Solved);
        }
    }
}
