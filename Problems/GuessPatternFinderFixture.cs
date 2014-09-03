using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OnlyProject
{
    [TestClass]
    public class GuessPatternFinderFixture
    {
        [TestMethod]
        public void FindBestLength()
        {
            var lines = Solver.GetPossiblePatterns(
                RawData.MinWordCharacters,
                RawData.MaxWordCharacters,
                (int)Math.Round(RawData.AverageWordCharacters * 3))
                .ToList();

            var best = GuessPatternFinder.FindSpacesInstance.FindBestTest(lines);
            
            Console.WriteLine(best);
        }

        [TestMethod]
        public void FindBestLetterCounter()
        {
            var best = GuessPatternFinder.FindUsingLetterCounts(RawData.Words);

            Console.WriteLine(best);
        }
    }
}
