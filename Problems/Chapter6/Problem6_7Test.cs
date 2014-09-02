using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem6_7Test
    {
        [TestMethod]
        public void ZeroLengthIsOne()
        {
            Assert.AreEqual(1, Problem6_7.GetLowestMissing(new int[] {}));
        }

        [TestMethod]
        public void Long2()
        {
            Assert.AreEqual(2, Problem6_7.GetLowestMissing(new int[] {1,1,1,3 }));
        }

        [TestMethod]
        public void Long1()
        {
            Assert.AreEqual(1, Problem6_7.GetLowestMissing(new int[] { 3, 4, 5, 3 }));
        }

        [TestMethod]
        public void NegativeNumbers()
        {
            Assert.AreEqual(2, Problem6_7.GetLowestMissing(new int[] { -4, 0, 1, 3 }));
        }
    }
}
