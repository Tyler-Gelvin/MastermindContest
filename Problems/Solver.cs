using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static readonly char BonusLetter;

        Mastermind Mastermind;
        public int Length { get; private set; }
        public int BonusLetterCount { get; private set; }
        public IEnumerable<int?> LetterCounts { get; private set; }
        public IReadOnlyList<int> WordLengths { get; private set; }
        public IReadOnlyList<string> PossiblePhrases { get; private set; }

        public TimeSpan TimeForSteps1And2;
        public TimeSpan TimeForPassPhrases;
        public TimeSpan TimeForStep3;

        static Solver()
        {
            var max = RawData.MaxWordCharacters;
            var builder = new StringBuilder();
            BonusLetter = 'e';
            builder.Append(BonusLetter, RawData.MaxPassCharacters);
            foreach (var letter in RawData.Letters)
            {
                builder.Append(letter, RawData.MaxPassLetters);
            }
            builder.Append(' ', 2);
            LengthTester = new StringGuessPattern(builder.ToString());

            BaseLetterCounters = LetterCounterFinder.GetLetterCounters().ToList().AsReadOnly();
            AllLetterCounters = new[]{new LetterCounter(BonusLetter)}
                .Concat(BaseLetterCounters.SelectMany(counter => new[]{counter, counter.ExtendedLetterCounter, counter.ReducedLetterCounter}))
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
            List<Solver> finishedSolvers = new List<Solver>();

            foreach(var mastermind in masterminds)
            {
                var solver = Solver.Solve(mastermind);
                finishedSolvers.Add(solver);
            }
        }

        public static Solver Solve(Mastermind mastermind)
        {
            var solver = new Solver(mastermind);
            solver.Solve();
            solver.Report();
            return solver;
        }

        void Report()
        {
            Console.WriteLine(Mastermind.GuessCount + ", " + Mastermind.Guesses.Last().ToString() + ", " + TimeForSteps1And2 + ", " + TimeForPassPhrases + ", " + TimeForStep3);
        }

        void Solve()
        {
            var timer = new Stopwatch();
            
            timer.Start();
            SolvePhaseOne();
            SolvePhaseTwo();
            timer.Stop();
            TimeForSteps1And2 = timer.Elapsed;
            
            timer.Restart();
            SetPossiblePhrases();
            timer.Stop();
            TimeForPassPhrases = timer.Elapsed;

            timer.Restart();
            SolvePhaseThree();
            timer.Stop();
            TimeForStep3 = timer.Elapsed;
        }

        void SolvePhaseOne()
        {
            var result = Mastermind.Guess(LengthTester);
            Length = result.Chars + result.Positions;
            BonusLetterCount = result.Positions;
        }

        void SolvePhaseTwo()
        {
            var proxy = new ProxyMastermind(Mastermind, BaseLetterCounters);
            WordLengths = GetWordLengths(proxy, Length).ToList();

            var extra = Math.Max(BaseLetterCounters.Count() - proxy.LetterCounts.Count(), 0);
            for (int i = 0; i < extra; i++)
            {
                proxy.Guess(new NoGuessPattern());
            }

            LetterCounts = AllLetterCounters
                .Select(counter => GetLetterCount(counter, proxy))
                .ToList()
                .AsReadOnly();
        }

        void SolvePhaseThree()
        {
            var node = new SolverNode(PossiblePhrases);
            node.Guess(Mastermind);
        }

        int? GetLetterCount(LetterCounter counter, ProxyMastermind proxy)
        {
            int value;
            if (counter.Letters == BonusLetter.ToString())
            {
                return BonusLetterCount;
            }
            else
            {
                return proxy.LetterCounts.TryGetValue(counter, out value) ? value : (int?)null;
            }
        }

        void SetPossiblePhrases()
        {
            PossiblePhrases = GetPossiblePhrases().ToList().AsReadOnly();
        }

        public static IEnumerable<string> GetPossiblePhrases(Mastermind mastermind)
        {
            var solver = new Solver(mastermind);
            solver.SolvePhaseOne();
            solver.SolvePhaseTwo();
            return solver.GetPossiblePhrases();
        }

        public static int GetPossiblePhraseCount(Mastermind mastermind)
        {
            var solver = new Solver(mastermind);
            solver.SolvePhaseOne();
            solver.SolvePhaseTwo();
            return solver.GetPossiblePhrases(true).Count();
        }
            
        public IEnumerable<string> GetPossiblePhrases(bool countOnly = false)
        {
            var firstAllocation = LetterCounts;
            var firstList = WordSignature.AllSignatures[WordLengths[0]].Where(word => word.FitsIn(firstAllocation));

            foreach(var first in firstList)
            {
                var secondAllocation = first.SubtractFrom(firstAllocation);
                var secondList = WordSignature.AllSignatures[WordLengths[1]].Where(word => word.FitsIn(secondAllocation));

                foreach(var second in secondList)
                {
                    var thirdAllocation = second.SubtractFrom(secondAllocation);
                    var thirdList = WordSignature.AllSignatures[WordLengths[2]].Where(word => word.MatchesExactly(thirdAllocation));

                    foreach(var third in thirdList)
                    {
                        yield return countOnly ? "" : string.Format("{0} {1} {2}", first, second, third);
                    }
                }
            }
        }

        public static IReadOnlyList<int> GetLetterCounts(Mastermind mastermind)
        {
            return LetterCounterFinder
                .GetLetterCounters()
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
