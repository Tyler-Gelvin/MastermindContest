using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class StringGuessPattern : GuessPattern
    {
        string _pattern;

        public override string PhrasePattern
        {
            get
            {
                return _pattern;
            }
        }

        public StringGuessPattern(string s)
        {
            CaresAboutCharacters = true;
            CaresAboutPositions = false;
            _pattern = s;
        }

        public override string ToString()
        {
            return _pattern;
        }
    }
}
