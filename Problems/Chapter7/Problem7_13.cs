using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Chapter6
{
    public static class Problem7_13
    {
        public static IEnumerable<string> JustifyLines(int targetLength, IEnumerable<string> words)
        {
            var candidateLength = 0;
            var currentWords = new List<string>();

            foreach(var word in words)
            {
                if (currentWords.Count() == 0)
                {
                    currentWords.Add(word);
                    candidateLength = word.Length;
                }
                else
                {
                    candidateLength += word.Length + 1;
                    if (candidateLength > targetLength)
                    {
                        yield return GetJustifiedLine(targetLength, currentWords);
                        currentWords.Clear();
                        currentWords.Add(word);
                        candidateLength = word.Length;
                    }
                }
            }

            if (currentWords.Count() > 0)
            {
                yield return string.Join(" ", currentWords.ToArray());
            }
        }

        public static string GetJustifiedLine(int targetLength, IEnumerable<string> words)
        {
            double padding = targetLength - words.Select(word => word.Length).Sum();
            var paddingPlaces = words.Count() - 1;

            var builder = new StringBuilder();

            foreach(var word in words)
            {
                builder.Append(word);
                if (paddingPlaces > 0)
                {
                    var spaces = (int)Math.Ceiling((double)padding / paddingPlaces);
                    builder.Append(' ', spaces);
                    padding -= spaces;
                    paddingPlaces--;
                }
            }

            return builder.ToString();
        }
    }
}
