using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem6_1Test
    {
        [TestMethod]
        public void Simple()
        {
            Assert.AreEqual("45", Problem6_3.Multiply("9", "5"));
        }

        [TestMethod]
        public void Negative()
        {
            Assert.AreEqual("-55", Problem6_3.Multiply("-11", "5"));
        }

        [TestMethod]
        public void Long()
        {
            Assert.AreEqual("-147573952589676412927", Problem6_3.Multiply("193707721", "-761838257287"));
        }
    }
}
