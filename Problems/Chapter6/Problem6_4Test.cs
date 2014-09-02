using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem6_4Test
    {
        [TestMethod]
        public void YesWin()
        {
            Assert.IsTrue(Problem6_4.CanWin(new[] { 3, 3, 1, 0, 2, 0, 1 }));
        }

        [TestMethod]
        public void NoWin()
        {
            Assert.IsFalse(Problem6_4.CanWin(new[] { 3,2,0,0,2,0,1}));
        }
    }
}
