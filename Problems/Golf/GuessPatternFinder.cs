using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Golf
{
    public class GuessPatternFinder
    {
        public static readonly GuessPatternFinder FindByMeanSquaredInstance = new GuessPatternFinder { FindBestTest = FindByMeanSquared };
        public static readonly GuessPatternFinder FindByMaxGroupsInstance = new GuessPatternFinder { FindBestTest = FindByMaxGroups };
        public static readonly GuessPatternFinder FindByMinGroupSizeInstance = new GuessPatternFinder { FindBestTest = FindByMinGroupSize };
        public static readonly GuessPatternFinder FindSpacesInstance = new GuessPatternFinder { FindBestTest = FindUsingSpaces, SkipFinalGuess = true };
        public static readonly GuessPatternFinder FindLettersInstance = new GuessPatternFinder { FindBestTest = FindUsingLetterCounts, SkipFinalGuess = true, Terminates = false };

        public Func<IEnumerable<string>, GuessPattern> FindBestTest;
        public bool SkipFinalGuess = true;
        public bool Terminates = true;

        public static StringGuessPattern FindByMeanSquared(IEnumerable<string> candidates)
        {
            return new StringGuessPattern(candidates
                .OrderBy(candidate => -GetScore(candidate, candidates, ScoreMeanSquared))
                .First());
        }

        public static StringGuessPattern FindByMaxGroups(IEnumerable<string> candidates)
        {
            return new StringGuessPattern(FindByMaxGroups(candidates, candidate => candidate, candidates));
        }
        
        public static T FindByMaxGroups<T>(IEnumerable<T> providers, Func<T, string> getCandidate, IEnumerable<string> words)
        {
            var sampleProviders = TakeSome(providers, 1000, 0).ToList();
            var sampleWords = TakeSome(words, 1000, 1).ToList();

            return sampleProviders
                .OrderByDescending(provider => -GetScore(getCandidate(provider), sampleWords, ScoreMaxGroups))
                .First();
        }

        static IEnumerable<T> TakeSome<T>(IEnumerable<T> list, int number, int offset)
        {
            if (number > list.Count())
            {
                return list;
            }
            else
            {
                // Exact number isn't important.
                var multiplier = list.Count() / number;
                return list.Where((item, i) => (i + offset) % multiplier == 0);  
            }
        }

        public static StringGuessPattern FindByMinGroupSize(IEnumerable<string> candidates)
        {
            return
                candidates
                .Select(candidate => new StringGuessPattern(candidate))
                .Select(candidate => Tuple.Create(candidate, Solver.GetDistribution(candidate, candidates)))
                .OrderBy(line => line.Item2.Select(distibution => distibution.Item2).Max())
                .Select(line => line.Item1)
                .First();
        }

        static double ScoreMaxGroups(IEnumerable<Tuple<GuessResult, int>> distribution)
        {
            return distribution.Count();
        }

        static double ScoreMeanSquared(IEnumerable<Tuple<GuessResult, int>> distribution)
        {
            var sum = 0;
            foreach(var t in distribution)
            {
                sum += t.Item2 * t.Item2;
            }
            return -Math.Sqrt(sum / distribution.Count());
        }

        static double ScoreLines(IEnumerable<Tuple<GuessResult, int>> distribution)
        {
            var sum = 0;
            foreach (var count in distribution.Select(t => t.Item2))
            {
                if (count > 1)
                {
                    sum += count * count;
                }
            }
            return -Math.Sqrt(sum / distribution.Count());
        }

        static double GetScore(string test, IEnumerable<string> candidates, Func<IEnumerable<Tuple<GuessResult, int>>, double> scoreFunction)
        {
            return GetScore(new StringGuessPattern(test), candidates, scoreFunction);
        }
        
        static double GetScore(GuessPattern test, IEnumerable<string> candidates, Func<IEnumerable<Tuple<GuessResult, int>>, double> scoreFunction)
        {
            return scoreFunction(Solver.GetDistribution(test, candidates));
        }

        public static GuessPattern FindUsingSpaces(IEnumerable<string> candidates)
        {
            var length = candidates.First().Length;
            var best = candidates.First();
            var bestScore = double.MinValue;

            foreach(var current in candidates)
            {
                var currentScore = GetScore(current, candidates, ScoreLines);
                if (currentScore > bestScore)
                {
                    best = current;
                    bestScore = currentScore;
                }
            }
            
            var changed = true;
            while(changed)
            {
                changed = false;
                for (int i = 0; i < length; i++)
                {
                    var current = best.Substring(0, i) + (best[i] == ' ' ? '.' : ' ') + best.Substring(i + 1, length - i - 1);
                    var currentScore = GetScore(current, candidates, ScoreLines);
                    if (currentScore > bestScore)
                    {
                        changed = true;
                        best = current;
                        bestScore = currentScore;
                        //Console.WriteLine(best + " " + bestScore);
                    }
                }
            }

            return new SpaceGuessPattern(best);
        }

        public static LetterCounter FindUsingLetterCounts(IEnumerable<string> candidates)
        {
            LetterCounter best = null;
            var bestScore = double.MinValue;

            foreach (var current in LetterCounter.GetAny())
            {
                var currentScore = GetScore(current.WordPattern, candidates, ScoreMaxGroups);
                if (currentScore > bestScore)
                {
                    best = current;
                    bestScore = currentScore;
                    //Console.WriteLine("best - " + best + " " + bestScore);
                }
            }

            var changed = true;
            while (changed)
            {
                changed = false;
                foreach (var current in best.AddOneLetter())
                {
                    var currentScore = GetScore(current.WordPattern, candidates, ScoreMaxGroups);
                    //Console.WriteLine("test - " + current + " " + currentScore);

                    if (currentScore > bestScore)
                    {
                        changed = true;
                        best = current;
                        bestScore = currentScore;
                        //Console.WriteLine("best - " + best + " " + bestScore);
                    }
                }
            }

            return best;
        }
    }
}
