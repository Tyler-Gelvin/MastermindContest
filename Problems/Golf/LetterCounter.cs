using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Golf
{
    public class LetterCounter : GuessPattern
    {
        public string Letters { get; private set; }
        string _bigPattern;
        string _smallPattern;
        LetterCounter _extendedLetterCounter;
        LetterCounter _reducedLetterCounter;
        
        public override string PhrasePattern
        {
            get
            {
                return _bigPattern = _bigPattern ?? CreatePattern(true);
            }
        }

        public override string WordPattern
        {
            get
            {
                return _smallPattern = _smallPattern ?? CreatePattern(false);
            }
        }

        LetterCounter(string letters)
        {
            Letters = letters;
            CaresAboutCharacters = true;
            CaresAboutPositions = false;
        }

        public LetterCounter(params char[] characters)
            : this(new string(characters))
        {
            Letters = new string(characters);
        }

        public LetterCounter(IEnumerable<char> characters)
        {
            Letters = new string(characters.ToArray());
        }

        public char ExtendedCharacter
        {
            get
            {
                return Letters[0];
            }
        }

        public string ExtendedPattern
        {
            get
            {
                return new string(ExtendedCharacter, RawData.MaxPassCharacters) + PhrasePattern.Substring(RawData.MaxPassCharacters);
            }
        }

        public LetterCounter ExtendedLetterCounter
        {
            get
            {
                return _extendedLetterCounter = _extendedLetterCounter ?? new LetterCounter(ExtendedCharacter);
            }
        }

        public LetterCounter ReducedLetterCounter
        {
            get
            {
                return _reducedLetterCounter = _reducedLetterCounter ?? new LetterCounter(Letters.Substring(1));
            }
        }

        string CreatePattern(bool big)
        {
            var builder = new StringBuilder();
            builder.Append('.', big ? RawData.MaxPassCharacters : RawData.MaxWordCharacters);
            foreach (var c in Letters)
            {
                builder.Append(c, big ? RawData.MaxPassLetterCount : RawData.MaxWordLetterCount);
            }

            return builder.ToString();
        }

        public int CountLetters(string s)
        {
            return Letters.Select(letter => s.Where(l2 => letter == l2).Count()).Sum();
        }

        public static IEnumerable<LetterCounter> GetAny()
        {
            foreach(var i in RawData.Letters)
            {
                yield return new LetterCounter(i);
            }
        }

        public static IEnumerable<LetterCounter> GetAny(int maxLength)
        {
            if (maxLength< 1)
            {
                throw new ArgumentException();
            }
            else if (maxLength == 1)
            {
                return GetAny();
            }
            else
            {
                return GetAny(maxLength - 1).SelectMany(counter => counter.AddOneLetter());
            }
        }

        public static LetterCounter FindBest(IEnumerable<string> words)
        {
            var counters = GetAny(3).ToList();
            return GuessPatternFinder.FindByMaxGroups(counters, counter => counter.WordPattern, words);
        }

        public IEnumerable<LetterCounter> AddOneLetter()
        {
            yield return this;
            foreach (var i in RawData.Letters)
            {
                if (!Letters.Contains(i))

                yield return new LetterCounter(Letters + i.ToString());
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as LetterCounter;
            return other != null && other.Letters == Letters;
        }

        public override int GetHashCode()
        {
            return Letters.GetHashCode();
        }

        public override string ToString()
        {
            return new string(Letters.ToArray());
        }
    }
}
