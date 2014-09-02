using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem7_6Test
    {
        [TestMethod]
        public void LookTests()
        {
            Assert.AreEqual("1", Problem7_6.GetLookAndSayNumber(0));
            Assert.AreEqual("1", Problem7_6.GetLookAndSayNumber(1));
            Assert.AreEqual("11", Problem7_6.GetLookAndSayNumber(2));
            Assert.AreEqual("21", Problem7_6.GetLookAndSayNumber(3));
            Assert.AreEqual("1211", Problem7_6.GetLookAndSayNumber(4));

            Assert.AreEqual("1113213211", Problem7_6.GetLookAndSayNumber(8));
        }
    }
}
