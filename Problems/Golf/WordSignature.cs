using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Golf
{
    public class WordSignature
    {
        public static readonly IReadOnlyDictionary<int, IEnumerable<WordSignature>> AllSignatures;

        public readonly int Letters;
        public readonly IReadOnlyList<int> Counts;
        public readonly string Word;

        static WordSignature()
        {
            AllSignatures = RawData
                .Words
                .Select(word => new WordSignature(word))
                .GroupBy(word => word.Letters)
                .ToDictionary(g1 => g1.Key, g1 => g1.ToList().AsEnumerable());
        }

        public WordSignature(string s)
        {
            Word = s;
            Letters = s.Length;
            Counts = Solver
                .AllLetterCounters
                .Select(counter => counter.CountLetters(s))
                .ToList()
                .AsReadOnly();
        }

        public bool MatchesExactly(IEnumerable<int?> total)
        {
            return total.Select((t, i) => !t.HasValue || t.Value == Counts[i]).All(v => v);
        }

        public bool FitsIn(IEnumerable<int?> total)
        {
            return total.Select((t, i) => !t.HasValue || t.Value >= Counts[i]).All(v => v);
        }

        public IEnumerable<int?> SubtractFrom(IEnumerable<int?> total)
        {
            return total.Select((t, i) => t.HasValue ? t.Value - Counts[i] : (int?)null).ToList().AsReadOnly();
        }

        public override string ToString()
        {
            return Word;
        }
    }
}
