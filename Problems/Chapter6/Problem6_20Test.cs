using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Linq;

namespace Problems.Chapter6
{
    [TestClass]
    public class Problem6_20Test
    {
        public static int[,] GetValidPuzzle()
        {
            return new[,]
            {
                {1,2,3,4,5,6,0,0,0},
                {4,5,6,0,0,0,0,0,0},
                {7,8,9,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0}
            };
        }

        public static int[,] GetInvalidPuzzle()
        {
            return new[,]
            {
                {1,2,3,4,5,6,0,0,1},
                {4,5,6,0,0,0,0,0,0},
                {7,8,9,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0}
            };
        }

        [TestMethod]
        public void HasDuplicatesTrue()
        {
            Assert.IsTrue(Problem6_20.HasDuplicates(new[] { 0, 1, 2, 2 }));
            Assert.IsTrue(Problem6_20.HasDuplicates(new[] { 0, 1, 1, 2 }));
            Assert.IsTrue(Problem6_20.HasDuplicates(new[] { 0, 1, 2, 1 }));
            Assert.IsTrue(Problem6_20.HasDuplicates(new[] { 0, 0, 2, 2 }));
            Assert.IsTrue(Problem6_20.HasDuplicates(new[] { 2, 2, 2, 2 }));
            Assert.IsTrue(Problem6_20.HasDuplicates(new[] { 1, 2, 9, 9 }));
        }

        [TestMethod]
        public void HasDuplicatesFalse()
        {
            Assert.IsFalse(Problem6_20.HasDuplicates(new[] { 0, 1, 2, 3 }));
            Assert.IsFalse(Problem6_20.HasDuplicates(new[] { 0, 0, 1, 2 }));
            Assert.IsFalse(Problem6_20.HasDuplicates(new[] { 0, 0, 0, 0 }));
            Assert.IsFalse(Problem6_20.HasDuplicates(new[] { 2, 3, 4, 9 }));
        }

        [TestMethod]
        public void ValidPuzzleIsValid()
        {
            Assert.IsTrue(Problem6_20.IsValidSudoku(GetValidPuzzle()));
        }

        [TestMethod]
        public void InvalidPuzzleIsValid()
        {
            Assert.IsFalse(Problem6_20.IsValidSudoku(GetInvalidPuzzle()));
        }
    }
}
