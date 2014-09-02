using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem7_13Test
    {
        [TestMethod]
        public void JustifyLineTests()
        {
            Assert.AreEqual("xxx bbb", Problem7_13.GetJustifiedLine(7, new[] { "xxx", "bbb" }));
            Assert.AreEqual("xxx  bbb", Problem7_13.GetJustifiedLine(8, new[] { "xxx", "bbb" }));
            Assert.AreEqual("xxx   bbb", Problem7_13.GetJustifiedLine(9, new[] { "xxx", "bbb" }));
            Assert.AreEqual("xxx bbb xxx", Problem7_13.GetJustifiedLine(11, new[] { "xxx", "bbb", "xxx" }));
            Assert.AreEqual("xxx  bbb xxx", Problem7_13.GetJustifiedLine(12, new[] { "xxx", "bbb", "xxx" }));
            Assert.AreEqual("xxx  bbb  xxx", Problem7_13.GetJustifiedLine(13, new[] { "xxx", "bbb", "xxx" }));
        }
    }
}
