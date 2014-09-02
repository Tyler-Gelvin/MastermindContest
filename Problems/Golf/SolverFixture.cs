using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Problems.Golf
{
    [TestClass]
    public class SolverFixture
    {
        [TestMethod]
        public void CountWords()
        {
            var test = "abstruseness";
            for (var i = 0; i < 100; i++)
            {
                var distribution = Solver.GetDistribution(new StringGuessPattern(test), RawData.Words);
            }
        }

        //[TestMethod]
        public void GetAllDistributions1()
        {
            var list = RawData
                .Words
                .Take(10000)
                .Select(word => Tuple.Create(word, Solver.GetDistribution(new StringGuessPattern(word), RawData.Words).Count()))
                .OrderBy(line => line.Item2)
                .ToList();

            		// "interrelationships"

            // 15
        }

        [TestMethod]
        public void CheckLength()
        {
            Assert.AreEqual(0, Solver.GetLength(new Mastermind("")));
            Assert.AreEqual(5, Solver.GetLength(new Mastermind("aaaaa")));
            Assert.AreEqual(5, Solver.GetLength(new Mastermind("aaabb")));
            Assert.AreEqual(5, Solver.GetLength(new Mastermind("aa bb")));
        }

        [TestMethod]
        public void CheckGetPossiblePatternsOne()
        {
            Assert.AreEqual(".. .. ..", Solver.GetPossiblePatterns(2, 2, 2 * 3 + 2).Single());
        }

        [TestMethod]
        public void CheckGetPossiblePatternsCount()
        {
            Assert.AreEqual(3, Solver.GetPossiblePatterns(2, 3, 2 + 2 + 3 + 2).Count());
        }

        [TestMethod]
        public void CheckGetSpaces()
        {
            var spaces = Solver.GetWordLengths(new Mastermind("aaa bb cccc"), 11).ToList();
            Assert.AreEqual(3, spaces[0]);
            Assert.AreEqual(2, spaces[1]);
            Assert.AreEqual(4, spaces[2]);
        }

        [TestMethod]
        public void CheckPossiblePhrases()
        {
            var phrase = RawData.TestPhrase;
            var list = Solver.GetPossiblePhrases(new Mastermind(phrase))
                //.Take(1000)
                .ToList();

            Console.WriteLine(list.Count());
            foreach (var line in list)
            {

                //Console.WriteLine(line);
            }
        }

        [TestMethod]
        public void CheckPossiblePhrasesTree()
        {
            var phrase = RawData.TestPhrase;
            var list = Solver
                .GetPossiblePhrases(new Mastermind(phrase))
                //.Take(1000)
                .ToList();

            var node = new SolverNode(list);
            node.GuessPattern = node.FindBestTest();

            foreach (var line in node.ToStrings().Take(1000))
            {
                Console.WriteLine(line);
            }
        }

        [TestMethod]
        public void SolveCount()
        {
            var phrase = RawData.TestPhrase;
            var mastermind = new Mastermind(phrase);
            Solver.Solve(mastermind);

            Console.WriteLine(phrase);
            Console.WriteLine(mastermind.GuessCount);
            foreach (var line in mastermind.Guesses)
            {
                Console.WriteLine(line);
            }
        }

        [TestMethod]
        public void SolveCount100()
        {
            foreach (var phrase in RawData.PassPhrases.Take(100))
            {
                var mastermind = new Mastermind(phrase);
                Solver.Solve(mastermind);

                Console.WriteLine(phrase + " in " + mastermind.GuessCount);
                Console.WriteLine(mastermind.GuessCount);
                foreach (var line in mastermind.Guesses)
                {
                    Console.WriteLine(line);
                }
            }
        }

        [TestMethod]
        public void SolveCreeps()
        {
            var phrase = "sublime hawaiian creeps";
            var mastermind = new Mastermind(phrase);
            Solver.Solve(mastermind);

            Console.WriteLine(phrase + " in " + mastermind.GuessCount);
            Console.WriteLine(mastermind.GuessCount);
            foreach (var line in mastermind.Guesses)
            {
                Console.WriteLine(line);
            }
        }

        [TestMethod]
        public void SolveParallel5()
        {
            var masterminds = RawData
                .PassPhrases
                .Take(5)
                .Select(pass => new Mastermind(pass))
                .ToList();

            Solver.SolveParallel(masterminds);
            var counts = masterminds.Select(mastermind => mastermind.GuessCount);

            Console.WriteLine(string.Format("{0} {1} {2}", counts.Min(), counts.Max(), counts.Average()));
        }

        [TestMethod]
        public void SolveSerial5()
        {
            var masterminds = RawData
                .PassPhrases
                .Take(5)
                .Select(pass => new Mastermind(pass))
                .ToList();

            Solver.SolveSerial(masterminds);
            var counts = masterminds.Select(mastermind => mastermind.GuessCount);

            Console.WriteLine(string.Format("{0} {1} {2}", counts.Min(), counts.Max(), counts.Average()));
        }
    }
}
