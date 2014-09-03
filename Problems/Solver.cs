using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class Solver
    {
        public static readonly GuessPattern LengthTester;
        public static readonly IReadOnlyList<LetterCounter> BaseLetterCounters;
        public static readonly IReadOnlyList<LetterCounter> AllLetterCounters;

        Mastermind Mastermind;
        public int Length { get; private set; }
        public IEnumerable<int?> LetterCounts { get; private set; }
        public IReadOnlyList<int> WordLengths { get; private set; }
        public IReadOnlyList<string> PossiblePhrases { get; private set; }

        static Solver()
        {
            var max = RawData.MaxWordCharacters;
            var builder = new StringBuilder();
            foreach (var letter in RawData.Letters)
            {
                builder.Append(letter, RawData.MaxPassLetters);
            }
            builder.Append(' ', 2);
            LengthTester = new StringGuessPattern(builder.ToString());

            BaseLetterCounters = GetLetterCounters().ToList().AsReadOnly();
            AllLetterCounters = BaseLetterCounters
                .SelectMany(counter => new[]{counter, counter.ExtendedLetterCounter, counter.ReducedLetterCounter})
                .ToList()
                .AsReadOnly();
        }

        Solver(Mastermind mastermind)
        {
            Mastermind = mastermind;
        }

        public static void SolveParallel(IEnumerable<Mastermind> masterminds)
        {
            masterminds
                .AsParallel()
                .Select(mastermind => Solve(mastermind))
                .ToList();
        }

        public static void SolveSerial(IEnumerable<Mastermind> masterminds)
        {
            masterminds
                .Select(mastermind => Solve(mastermind))
                .ToList();
        }

        public static Mastermind Solve(Mastermind mastermind)
        {
            new Solver(mastermind).Solve();
            return mastermind;
        }

        void Solve()
        {
            Length = GetLength(Mastermind);
            var proxy = new ProxyMastermind(Mastermind, BaseLetterCounters);
            WordLengths = GetWordLengths(proxy, Length).ToList();

            var extra = Math.Max(BaseLetterCounters.Count() - proxy.LetterCounts.Count(), 0);
            for (int i = 0; i < extra; i++)
            {
                proxy.Guess(new NoGuessPattern());
            }

            int value;
            LetterCounts = AllLetterCounters
                .Select(counter => proxy.LetterCounts.TryGetValue(counter, out value) ? value : (int?)null)
                .ToList()
                .AsReadOnly();

            PossiblePhrases = GetPossiblePhrases().ToList().AsReadOnly();

            var node = new SolverNode(PossiblePhrases);
            node.Guess(Mastermind);
        }

        public static IEnumerable<string> GetPossiblePhrases(Mastermind mastermind)
        {
            var solver = new Solver(mastermind);
            solver.Solve();
            return solver.PossiblePhrases;
        }
            
        public IEnumerable<string> GetPossiblePhrases()
        {
            var firstList = WordSignature.AllSignatures[WordLengths[0]];

            foreach(var first in firstList)
            {
                var secondAllocation = first.SubtractFrom(LetterCounts);
                var secondList = WordSignature.AllSignatures[WordLengths[1]].Where(word => word.FitsIn(secondAllocation));

                foreach(var second in secondList)
                {
                    var thirdAllocation = second.SubtractFrom(secondAllocation);
                    var thirdList = WordSignature.AllSignatures[WordLengths[2]].Where(word => word.MatchesExactly(thirdAllocation));

                    foreach(var third in thirdList)
                    {
                        //yield return "dummy";
                        yield return string.Format("{0} {1} {2}", first, second, third);
                    }
                }
            }
        }

        static IEnumerable<LetterCounter> GetLetterCounters()
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

            // 32k
            yield return new LetterCounter("auto");
            yield return new LetterCounter("bels");
            yield return new LetterCounter("twin");
            yield return new LetterCounter("gupy");
            yield return new LetterCounter("hrzf");
            yield return new LetterCounter("nectd");
            yield return new LetterCounter("iseb");
        }

        public static IReadOnlyList<int> GetLetterCounts(Mastermind mastermind)
        {
            return GetLetterCounters()
                .Select(counter => mastermind.Guess(counter).Chars)
                .ToList()
                .AsReadOnly();
        }

        public static IEnumerable<int> GetWordLengths(IMastermind mastermind, int length)
        {
            var node = new SolverNode(GetPossiblePatterns(RawData.MinWordCharacters, RawData.MaxWordCharacters, length), GuessPatternFinder.FindSpacesInstance);
            node.GuessPattern = node.FindBestTest();
            var pattern = node.Guess(mastermind);
            
            var first = pattern.IndexOf(' ');
            var second = pattern.IndexOf(' ', first + 1);
            
            return new[]{first, second - first - 1, length - second - 1};
        }

        public static IEnumerable<Tuple<GuessResult, int>> GetDistribution(GuessPattern test, IEnumerable<string> candidates)
        {
            return candidates
                .Select(word => new Mastermind(word).Guess(test))
                .GroupBy(result => result)
                .Select(group => Tuple.Create(group.Key, group.Count()))
                .OrderBy(pair => pair.Item1)
                .ToList();
        }

        public static int GetLength(Mastermind masterMind)
        {
            var result = masterMind.Guess(LengthTester);
            return result.Chars + result.Positions;
        }

        public static IEnumerable<string> GetPossiblePatterns(int min, int max, int length)
        {
            for (var i = min; i <= max; i++)
            {
                for (var j = min; j <= max; j++)
                {
                    var left = length - (i + j + 2);
                    if (left >= min && left <= max)
                    {
                        var builder = new StringBuilder();
                        builder.Append('.', i);
                        builder.Append(' ');
                        builder.Append('.', j);
                        builder.Append(' ');
                        builder.Append('.', left);
                        yield return builder.ToString();
                    }
                }
            }
        }
    }
}
