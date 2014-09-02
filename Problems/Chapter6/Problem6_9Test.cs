using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Linq;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem6_9Test
    {
        [TestMethod]
        public void ZeroLengthIsZero()
        {
            Assert.AreEqual(0.0, Problem6_8.GetBatteryRequired(Enumerable.Empty<Tuple<double, double, double>>()));
        }

        [TestMethod]
        public void OneHill()
        {
            Assert.AreEqual(10.0, Problem6_8.GetBatteryRequired(new[] {Tuple.Create(0.0, 0.0, 10.0)}));
        }

        [TestMethod]
        public void TwoHills()
        {
            Assert.AreEqual(10.0, Problem6_8.GetBatteryRequired(new[]
            {
                Tuple.Create(0.0, 0.0, 10.0),
                Tuple.Create(0.0, 0.0, -10.0),
                Tuple.Create(0.0, 0.0, 10.0)
            }));
        }

        [TestMethod]
        public void Mountain()
        {
            Assert.AreEqual(15.0, Problem6_8.GetBatteryRequired(new[]
            {
                Tuple.Create(0.0, 0.0, 10.0),
                Tuple.Create(0.0, 0.0, -5.0),
                Tuple.Create(0.0, 0.0, 10.0),
                Tuple.Create(0.0, 0.0, -50.0)
            }));
        }
    }
}
