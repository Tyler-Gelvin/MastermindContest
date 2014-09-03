using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class SolverNode
    {
        public IEnumerable<string> Candidates { get; private set; }
        public GuessPatternFinder Strategy { get; private set; }
        GuessPattern _guessPattern;
        bool _makesProgress;
        Dictionary<GuessResult, SolverNode> _nodes;

        public Dictionary<GuessResult, SolverNode> Nodes
        {
            get
            {
                InitNodes();

                return _nodes;
            }
         }

        public SolverNode(IEnumerable<string> candidates, GuessPatternFinder strategy = null, GuessPattern guessPattern = null)
        {
            Candidates = candidates.ToList();
            Strategy = strategy ?? GuessPatternFinder.FindByMaxGroupsInstance;
            if (guessPattern != null)
            {
                GuessPattern = guessPattern;
            }
        }

        public GuessPattern GuessPattern
        {
            get
            {
                return _guessPattern;
            }
            set
            {
                _guessPattern = value;

                //Debug.WriteLine(string.Format("partitioning {0} candidates on '{1}'", Candidates.Count(), Test));

                _nodes = Candidates
                    .Where(candidate => candidate != GuessPattern.PhrasePattern)
                    .Select(candidate => Tuple.Create(candidate, new Mastermind(candidate).Guess(GuessPattern.PhrasePattern)))
                    .GroupBy(t => t.Item2)
                    .ToDictionary(group => group.Key, group => new SolverNode(group.Select(t => t.Item1), Strategy));

                _makesProgress = Nodes.Count() != 1 || Nodes.Single().Value.Candidates.Count() != Candidates.Count();
            }
        }

        void InitNodes()
        {
            if (_nodes == null)
            {
                GuessPattern = FindBestTest();
            }
        }

        public GuessPattern FindBestTest()
        {
            return (Strategy ?? GuessPatternFinder.FindByMaxGroupsInstance).FindBestTest(Candidates);
        }

        public string Guess(IMastermind mastermind)
        {
            InitNodes();
            if (!_makesProgress)
            {
                if (!Strategy.SkipFinalGuess)
                {
                    throw new ApplicationException();
                }
                else if (Strategy.Terminates)
                {
                    return Candidates.Single();
                }
                else
                {
                    return Candidates.First();
                }
            }

            var result = mastermind.Guess(GuessPattern);
            if (result.Solved)
            {
                return GuessPattern.GetPattern(mastermind.IsWholePhrase);
            }
            else
            {
                return Nodes[result].Guess(mastermind);
            }
        }

        public double GetAverageGuesses(bool isWholePhrase = true)
        {
            var masterminds = Candidates.Select(candidate => new Mastermind(candidate, isWholePhrase)).ToList();
            masterminds.ForEach(mastermind => Guess(mastermind));
            return masterminds.Select(mastermind => mastermind.GuessCount).Average();
        }

        public IEnumerable<string> ToStrings(int tabs = 0)
        {
            yield return string.Join("", Enumerable.Repeat("\t", tabs)) + ToString();
            foreach (var s in Nodes.Values.SelectMany(node => node.ToStrings(tabs + 1)))
            {
                yield return s;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", GuessPattern, GetAverageGuesses(false));
        }
    }
}
