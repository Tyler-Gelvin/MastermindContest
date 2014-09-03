using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OnlyProject
{
    [TestClass]
    public class RawDataFixture
    {
        [TestMethod]
        public void CountWords()
        {
            Assert.AreEqual(10000, RawData.Words.Count());
        }

        [TestMethod]
        public void Min()
        {
            Assert.AreEqual(1, RawData.MinWordCharacters);
        }

        [TestMethod]
        public void Max()
        {
            Assert.AreEqual(21, RawData.MaxWordCharacters);
        }

        [TestMethod]
        public void CountPassPhrases()
        {
            Assert.AreEqual(1000, RawData.PassPhrases.Count());
        }

        [TestMethod]
        public void CheckFirst()
        {
            Assert.AreEqual("aaas", RawData.Words.First());
        }

        [TestMethod]
        public void CheckSecond()
        {
            Assert.AreEqual("aaron", RawData.Words.Skip(1).First());
        }

        [TestMethod]
        public void CheckCounts()
        {
            var list = RawData
                .Words
                .GroupBy(word => word.Length)
                .Select(group => Tuple.Create(group.Key, group.Count()))
                .OrderBy(t => t.Item1)
                .ToList();

            list.ForEach(t => Console.WriteLine(t));
        }
    }
}
