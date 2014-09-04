using OnlyProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public static class LetterCounterFinder
    {
        static Random _r = new Random();

        public static IEnumerable<LetterCounter> GetLetterCounters()
        {
            /*
            2m
            yield return new LetterCounter("a");
            yield return new LetterCounter("e");
            yield return new LetterCounter("i");
            yield return new LetterCounter("o");
            yield return new LetterCounter("u");
            */


            /*
             1.5m
yield return new LetterCounter("as");
yield return new LetterCounter("ib");
yield return new LetterCounter("dm");
yield return new LetterCounter("gp");
yield return new LetterCounter("et");
 */

            /*
             1.1m
yield return new LetterCounter("aes");
yield return new LetterCounter("iseb");
yield return new LetterCounter("dm");
yield return new LetterCounter("gop");
yield return new LetterCounter("h");
  */

            /*
//.76m
yield return new LetterCounter("auto");
yield return new LetterCounter("bels");
yield return new LetterCounter("twin");
yield return new LetterCounter("gupy");
yield return new LetterCounter("hrzf");
 */

                      /*
            // 3.1m
            yield return new LetterCounter("abcd");
            yield return new LetterCounter("efghi");
            yield return new LetterCounter("jklmno");
            yield return new LetterCounter("pqrst");
            yield return new LetterCounter("uvwxyz");
             */

            /*
            //.202m
            yield return new LetterCounter("auto");
            yield return new LetterCounter("bels");
            yield return new LetterCounter("twin");
            yield return new LetterCounter("gupy");
            yield return new LetterCounter("hrzf");
            yield return new LetterCounter("eqzm");
            */

            /*
            // 15k
            yield return new LetterCounter("auto");
            yield return new LetterCounter("bels");
            yield return new LetterCounter("twin");
            yield return new LetterCounter("gupy");
            yield return new LetterCounter("hrzf");
            yield return new LetterCounter("nectd");
            yield return new LetterCounter("iseb");
            */

            /*
            // 34k
            yield return new LetterCounter("qjxzw");
            yield return new LetterCounter("kvfy");
            yield return new LetterCounter("mu");
            yield return new LetterCounter("dc");
            yield return new LetterCounter("sc");
            yield return new LetterCounter("er");
            */
             
            /*
            // 1.2m
            yield return new LetterCounter("qjxz");
            yield return new LetterCounter("wkv");
            yield return new LetterCounter("n");
            yield return new LetterCounter("s");
            yield return new LetterCounter("ar");
            yield return new LetterCounter("ei");
             */
            
            /*
            // 469k
            yield return new LetterCounter("mu");
            yield return new LetterCounter("cl");
            yield return new LetterCounter("or");
            yield return new LetterCounter("at");
            yield return new LetterCounter("in");
            yield return new LetterCounter("es");
             */

            yield return new LetterCounter("rstinacopy");
            yield return new LetterCounter("sldumrbvqaw");
            yield return new LetterCounter("ndiglfmtujvy");
            yield return new LetterCounter("rthdofwcujkbx");
            yield return new LetterCounter("stuhmpolyz");
        }

        public static Tuple<string, double> EvaluateByMeanSquared(LetterCounter letterCounter)
        {
            var length = 50000;
            var list = GetRandomPassPhrases()
                .Take(length)
                .Select(pass => letterCounter.CountLetters(pass))
                .GroupBy(x => x)
                .Select(group => group.Count())
                .ToList();

            var score = -Math.Log10((MeanSquared(list) / ((double)length * (double)length)));
            return Tuple.Create(letterCounter.Letters, score);
        }

        public static Tuple<string, double> EvaluateByMeanSquared2(LetterCounter letterCounter)
        {
            var score = EvaluateBySumSquared(
                new LetterCounter("e"),
                new LetterCounter("rstinacopy"),
                new LetterCounter("sldumrbvqaw"),
                new LetterCounter("ndiglfmtujvy"),
                new LetterCounter("rthdofwcujkbx"),
                letterCounter,
                new LetterCounter("e"));

            return Tuple.Create(letterCounter.Letters, score);
        }

        public static double EvaluateBySumSquared(
            LetterCounter lc1,
            LetterCounter lc2,
            LetterCounter lc3,
            LetterCounter lc4,
            LetterCounter lc5,
            LetterCounter lc6,
            LetterCounter lc7)
        {
            var length = 100000;
            var list = GetRandomPassPhrasesFixedLength()
                .Take(length)
                .Select(pass => Tuple.Create(
                    lc1.CountLetters(pass),
                    lc2.CountLetters(pass),
                    lc3.CountLetters(pass),
                    lc4.CountLetters(pass),
                    lc5.CountLetters(pass),
                    lc6.CountLetters(pass),
                    lc7.CountLetters(pass)
                    ))
                .GroupBy(x => x)
                .Select(group => group.Count())
                .ToList();

            var score = Math.Log10(length) * 2 - Math.Log10(SumSquared(list));
            return score;
        }

        public static Tuple<string, double> EvaluateByMaxEliminations(LetterCounter letterCounter)
        {
            var passes = GetRandomPassPhrases().Take(10000).ToList();
            //var words = Enumerable.Range(0, 10000).Select(i => GetRandomWord()).ToList();
            //var words = RawData.Words;

            var eliminations = GetRandomPassPhrases()
                .Take(1000)
                .Select(pass => letterCounter.CountLetters(pass))
                .GroupBy(x => x)
                .Select(group => passes.Where(word => letterCounter.CountLetters(word) > group.Key).Count() * group.Count())
                .Sum();

            return Tuple.Create(letterCounter.Letters, (double)-eliminations);
        }

        static double MeanSquared(IEnumerable<int> list)
        {
            var sum = 0;
            foreach (var count in list)
            {
                sum += count * count;
            }
            return Math.Sqrt(sum / list.Count());
        }

        static double SumSquared(IEnumerable<int> list)
        {
            var sum = 0;
            foreach (var count in list)
            {
                sum += count * count;
            }
            return sum;
        }

        static List<IGrouping<int, int>> GetCounts(LetterCounter counter)
        {
            return GetRandomPassPhrases().Take(10000).Select(pass => counter.CountLetters(pass)).GroupBy(x => x).ToList();
        }

        public static IEnumerable<string> GetRandomPassPhrases()
        {
            for (; ; )
            {
                yield return string.Format(
                    "{0} {1} {2}",
                    GetRandomWord(),
                    GetRandomWord(),
                    GetRandomWord());
            }
        }

        public static IEnumerable<string> GetRandomPassPhrasesFixedLength()
        {
            for (; ; )
            {
                yield return string.Format(
                    "{0} {1} {2}",
                    WordSignature.GetRandomWord(6),
                    WordSignature.GetRandomWord(7),
                    WordSignature.GetRandomWord(8));
            }
        }

        public static string GetRandomWord()
        {
            return RawData.Words[_r.Next(RawData.Words.Count)];
        }

        public static LetterCounter FindUsingLetterCounts(bool debug = false)
        {
            LetterCounter best = null;
            var bestScore = double.MinValue;

            foreach (var current in LetterCounter.GetAny())
            {
                var currentScore = EvaluateByMeanSquared2(current).Item2;
                if (currentScore > bestScore)
                {
                    best = current;
                    bestScore = currentScore;
                    if (debug)
                    {
                        Console.WriteLine("best - " + best + " " + bestScore);
                    }
                }
            }

            var changed = true;
            while (changed)
            {
                changed = false;
                foreach (var current in best.AddOneLetter())
                {
                    var currentScore = EvaluateByMeanSquared2(current).Item2;
                    //Console.WriteLine("test - " + current + " " + currentScore);

                    if (currentScore > bestScore)
                    {
                        changed = true;
                        best = current;
                        bestScore = currentScore;
                        if (debug)
                        {
                            Console.WriteLine("best - " + best + " " + bestScore);
                        }
                    }
                }
            }

            return best;
        }
    }
}
