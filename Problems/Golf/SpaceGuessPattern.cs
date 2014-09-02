using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Golf
{
    public class SpaceGuessPattern : GuessPattern
    {
        readonly string _pattern;

        public override string PhrasePattern
        {
            get
            {
                return _pattern;
            }
        }

        public SpaceGuessPattern(string pattern)
        {
            _pattern = pattern;
            CaresAboutCharacters = false;
            CaresAboutPositions = true;
        }

        public override string ToString()
        {
            return _pattern;
        }
    }
}
