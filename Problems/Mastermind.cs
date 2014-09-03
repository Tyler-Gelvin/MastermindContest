using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class Mastermind : IMastermind
    {
        // Should probably be another class.
        static IDictionary<string, int[]> CharCounts = new ConcurrentDictionary<string, int[]>();

        List<string> _guesses = new List<string>();

        string Password { get; set; }
        public bool IsWholePhrase {get; private set;}

        public IEnumerable<string> Guesses
        {
            get
            {
                return _guesses.AsReadOnly();
            }
        }

        public int GuessCount
        {
            get
            {
                return _guesses.Count();
            }
        }

        public Mastermind(string password, bool wholePhrase = true)
        {
            Password = password;
            IsWholePhrase = wholePhrase;
        }

        public GuessResult Guess(GuessPattern guess)
        {
            if (guess == null)
            {
                throw new ArgumentNullException();
            }

            return Guess(IsWholePhrase ? guess.PhrasePattern : guess.WordPattern);
        }

        public GuessResult Guess(string guess)
        {
            if (guess == null)
            {
                throw new ArgumentNullException();
            }

            _guesses.Add(guess);

            return DoGuess2(guess);
        }

        int[] DoGuess(String s)
        {
            int chars=0, positions=0;
            var pw = Password;
            for(int i=0;i<s.Length && i<pw.Length;i++)
            {
                if(s[i]==pw[i])
                {
                    positions++;
                }
            }

            if(positions == pw.Length && pw.Length==s.Length)
            {
                return new int[]{-1,positions};
            }

            for(int i=0; i<s.Length; i++)
            {
                String c = s[i].ToString();
                if(pw.Contains(c))
                {
                    pw = pw.Remove(pw.IndexOf(c), 1);
                    chars++;
                }
            }
            chars -= positions;
            return new int[]{chars,positions};
        }

        GuessResult DoGuess2(String s)
        {
            var positions = 0;
            var possiblePositions = 0;
            var pw = Password;
            for (int i = 0; i < s.Length && i < pw.Length; i++)
            {
                if (s[i] != '.')
                {
                    possiblePositions++;
                    if (s[i] == pw[i])
                    {
                        positions++;
                    }
                }
            }

            if (positions == possiblePositions && pw.Length == s.Length)
            {
                return new GuessResult(-1, positions);
            }

            var chars = GetMinCharCounts(GetCharCounts(Password), GetCharCounts(s));

            return new GuessResult(chars - positions, positions);
        }

        static int[] GetCharCounts(string s)
        {
            int[] counts;
            if (!CharCounts.TryGetValue(s, out counts))
            {
                counts = CreateCharCounts(s);
                CharCounts.Add(s, counts);
            }

            return counts;
        }

        static int[] CreateCharCounts(string s)
        {
            var counts = new int[27];
            foreach(var c in s)
            {
                if (c == ' ')
                {
                    counts[26] += 1;
                }
                else if (c >= 'a' && c <= 'z')
                {
                    counts[ c - 'a'] += 1;
                }
            }
            return counts;
        }

        static int GetMinCharCounts(int[] one, int[] two)
        {
            var count = 0;
            for (int i = 0; i<27; i++)
            {
                count += Math.Min(one[i], two[i]);
            }
            return count;
        }

        public static int CountSpaces(string s)
        {
            return GetCharCounts(s)[26];
        }

        public static int GetMaxCharCount(IEnumerable<string> words)
        {
            return words.Select(word => GetCharCounts(word).Max()).Max();
        }
    }
}
