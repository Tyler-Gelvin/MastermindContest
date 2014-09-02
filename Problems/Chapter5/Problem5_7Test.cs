using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter5
{
    [TestClass]
    public class Problem5_7Test
    {
        [TestMethod]
        public void PowersOfTwo()
        {
            Assert.AreEqual(1.0, Problem5_7.Power(2.0, 0));
            Assert.AreEqual(2.0, Problem5_7.Power(2.0, 1));
            Assert.AreEqual(4.0, Problem5_7.Power(2.0, 2));
            Assert.AreEqual(8.0, Problem5_7.Power(2.0, 3));
            Assert.AreEqual(16.0, Problem5_7.Power(2.0, 4));
            Assert.AreEqual(32.0, Problem5_7.Power(2.0, 5));
        }

        [TestMethod]
        public void PowersOfThree()
        {
            Assert.AreEqual(1.0, Problem5_7.Power(3.0, 0));
            Assert.AreEqual(3.0, Problem5_7.Power(3.0, 1));
            Assert.AreEqual(9.0, Problem5_7.Power(3.0, 2));
            Assert.AreEqual(27.0, Problem5_7.Power(3.0, 3));
            Assert.AreEqual(81.0, Problem5_7.Power(3.0, 4));
            Assert.AreEqual(243.0, Problem5_7.Power(3.0, 5));
        }

        [TestMethod]
        public void NegativePowersOfTen()
        {
            Assert.AreEqual(1.0, Problem5_7.Power(10.0, -0), 0.000001);
            Assert.AreEqual(0.1, Problem5_7.Power(10.0, -1), 0.000001);
            Assert.AreEqual(0.01, Problem5_7.Power(10.0, -2), 0.000001);
            Assert.AreEqual(0.001, Problem5_7.Power(10.0, -3), 0.000001);
            Assert.AreEqual(0.0001, Problem5_7.Power(10.0, -4), 0.000001);
            Assert.AreEqual(0.00001, Problem5_7.Power(10.0, -5), 0.000001);
        }
    }
}
