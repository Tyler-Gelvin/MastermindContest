using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OnlyProject
{
    [TestClass]
    public class LetterCounterFixture
    {
        [TestMethod]
        public void FindBest()
        {
            var best = LetterCounter.FindBest(RawData.Words);
            Assert.AreEqual("aes", best.Letters);
            //Console.WriteLine(best);
        }

        [TestMethod]
        public void FindBestTree()
        {
            var node = new SolverNode(RawData.Words, GuessPatternFinder.FindLettersInstance);
            node.GuessPattern = new LetterCounter("aes");

            foreach (var line in node.ToStrings().Take(100))
            {
                Console.WriteLine(line);
            }
        }
    }
}
