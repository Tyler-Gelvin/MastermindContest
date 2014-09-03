using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OnlyProject
{
    [TestClass]
    public class SolverNodeFixture
    {
        [TestMethod]
        public void TestInterrelationships()
        {
            var node = new SolverNode(RawData.Words.Take(100));
            node.GuessPattern = new StringGuessPattern("interrelationships");
        }

        [TestMethod]
        public void TestAccreditations()
        {
            var node = new SolverNode(RawData.Words.Take(100));
            Assert.AreEqual("accreditations", node.FindBestTest().PhrasePattern);
        }

        [TestMethod]
        public void PrintAllMaxGroups()
        {
            var node = new SolverNode(RawData.Words.Take(10000), GuessPatternFinder.FindByMaxGroupsInstance);
            node.GuessPattern = node.FindBestTest(); // "accreditations";

            // 1 4.4603
            // 2 4.48
            foreach(var line in node.ToStrings())
            {
                Console.WriteLine(line);
            }

            //Assert.AreEqual("accreditations", node.FindBestTest());
        }

        [TestMethod]
        public void PrintAllMeanSquared()
        {
            var node = new SolverNode(RawData.Words.Take(10000), GuessPatternFinder.FindByMeanSquaredInstance);
            node.GuessPattern = node.FindBestTest();

            // 1 4.459
            foreach (var line in node.ToStrings().Take(100))
            {
                Console.WriteLine(line);
            }
        }

        [TestMethod]
        public void PrintLengthFinders()
        {
            var lines = Solver.GetPossiblePatterns(
                RawData.MinWordCharacters,
                RawData.MaxWordCharacters,
                (int)Math.Round(RawData.AverageWordCharacters * 3))
                .ToList();

            var node = new SolverNode(lines, GuessPatternFinder.FindSpacesInstance);
            node.GuessPattern = node.FindBestTest();

            // 1 5.81
            // 3 5.82
            foreach (var line in node.ToStrings())
            {
                Console.WriteLine(line);
            }
        }
    }
}
