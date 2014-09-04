using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    [TestClass]
    public class LetterCounterFinderFixture
    {
        [TestMethod]
        public void EvaluateCurrentLetterCounters()
        {
            EvaluateLetterCounters(Solver.AllLetterCounters);
        }

        public void EvaluateLetterCounters(IEnumerable<LetterCounter> letterCounters)
        {
            var list = letterCounters
                .Select(counter => LetterCounterFinder.EvaluateByMeanSquared(counter))
                .OrderBy(t => t.Item2)
                .ToList();

            list.ForEach(t => Console.WriteLine(string.Format("{0} {1}", t.Item1, t.Item2)));
        }

        [TestMethod]
        public void EvaluateTwoLetterCounters()
        {
            //var letters = "abdefghijkmnopqrstuvwxyz";;
            var letters = "bdfghjkmpquvwxyz".ToList();
            var counters = LetterCounter.GetAny(2, letters);

            EvaluateLetterCounters(counters);
        }

        [TestMethod]
        public void FindUsingLetterCounts()
        {
            var counter = LetterCounterFinder.FindUsingLetterCounts(true);
        }

        [TestMethod]
        public void Evaluate1()
        {
            //	3.65669068485286

            var score = 
                LetterCounterFinder.EvaluateBySumSquared(
                new LetterCounter("qjxzw"),
            new LetterCounter("kvfy"),
            new LetterCounter("mu"),
            new LetterCounter("dc"),
            new LetterCounter("sc"),
            new LetterCounter("er"),
            new LetterCounter("er"));

            Console.WriteLine(score);
        }

        [TestMethod]
        public void Evaluate2()
        {
            // 4.87751543393508
            var score =
                LetterCounterFinder.EvaluateBySumSquared(
                new LetterCounter("e"),
                new LetterCounter("rstinacopy"),
                new LetterCounter("sldumrbvqaw"),
                new LetterCounter("ndiglfmtujvy"),
                new LetterCounter("rthdofwcujkbx"),
                new LetterCounter("iseb"),
                new LetterCounter("iseb"));

            Console.WriteLine(score);
        }

        [TestMethod]
        public void Evaluate3()
        {
            // 4.90085824586256
            var score =
                LetterCounterFinder.EvaluateBySumSquared(
                new LetterCounter("auto"),
                new LetterCounter("bels"),
                new LetterCounter("twin"),
                new LetterCounter("gupy"),
                new LetterCounter("hrzf"),
                new LetterCounter("nectd"),
                new LetterCounter("iseb"));

            Console.WriteLine(score);
        }

        [TestMethod]
        public void Evaluate4()
        {
            // 4.87734513264595

            var score =
                LetterCounterFinder.EvaluateBySumSquared(
                new LetterCounter("e"),
                new LetterCounter("rstinacopy"),
                new LetterCounter("sldumrbvqaw"),
                new LetterCounter("ndiglfmtujvy"),
                new LetterCounter("rthdofwcujkbx"),
                new LetterCounter("iseb"),
                new LetterCounter("i"));

            Console.WriteLine(score);
        }

        [TestMethod]
        public void Evaluate5()
        {
            // 4.91748031251767

            var score =
                LetterCounterFinder.EvaluateBySumSquared(
                new LetterCounter("e"),
                new LetterCounter("rstinacopy"),
                new LetterCounter("sldumrbvqaw"),
                new LetterCounter("ndiglfmtujvy"),
                new LetterCounter("rthdofwcujkbx"),
                new LetterCounter("stuhmpolyz"),
                new LetterCounter("e"));

            Console.WriteLine(score);
        }
    }
}
