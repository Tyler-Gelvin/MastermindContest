using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem6_3Test
    {
        [TestMethod]
        public void KeepsArray()
        {
            var arr = new[] { 1, 2, 3 };

            Problem6_1.ArrangeAroundIndex(arr, 1);
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(2, arr[1]);
            Assert.AreEqual(3, arr[2]);
        }

        [TestMethod]
        public void ReversesArray()
        {
            var arr = new[] { 3, 2, 1 };

            Problem6_1.ArrangeAroundIndex(arr, 1);
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(2, arr[1]);
            Assert.AreEqual(3, arr[2]);
        }

        [TestMethod]
        public void SortsArray()
        {
            var arr = new[] { 3,3, 1, 1, 2,2 };

            Problem6_1.ArrangeAroundIndex(arr, 5);
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(1, arr[1]);
            Assert.AreEqual(2, arr[2]);
            Assert.AreEqual(2, arr[3]);
            Assert.AreEqual(3, arr[4]);
            Assert.AreEqual(3, arr[5]);
        }

        [TestMethod]
        public void MixedUp()
        {
            var arr = new[] { 1, 3, 2, 3, 1, 2 };

            Problem6_1.ArrangeAroundIndex(arr, 2);
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(1, arr[1]);
            Assert.AreEqual(2, arr[2]);
            Assert.AreEqual(2, arr[3]);
            Assert.AreEqual(3, arr[4]);
            Assert.AreEqual(3, arr[5]);
        }
    }
}
