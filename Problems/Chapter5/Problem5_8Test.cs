using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter5
{
    [TestClass]
    public class Problem5_8Test
    {
        [TestMethod]
        public void PowersOfTwo()
        {
            Assert.AreEqual("0", Problem5_8.ChangeBase(10, "0", 2));
            Assert.AreEqual("1", Problem5_8.ChangeBase(10, "1", 2));
            Assert.AreEqual("10", Problem5_8.ChangeBase(10, "2", 2));
            Assert.AreEqual("100", Problem5_8.ChangeBase(10, "4", 2));
            Assert.AreEqual("1000", Problem5_8.ChangeBase(10, "8", 2));
            Assert.AreEqual("10000", Problem5_8.ChangeBase(10, "16", 2));
            Assert.AreEqual("100000", Problem5_8.ChangeBase(10, "32", 2));
        }

        [TestMethod]
        public void Hex()
        {
            Assert.AreEqual("15", Problem5_8.ChangeBase(16, "F", 10));
            Assert.AreEqual("255", Problem5_8.ChangeBase(16, "FF", 10));
        }
    }
}
