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
    }
}
