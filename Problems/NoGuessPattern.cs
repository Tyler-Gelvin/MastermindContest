using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class NoGuessPattern : GuessPattern
    {
        public char Letter { get; private set; }
        
        public override string PhrasePattern
        {
            get
            {
                return "";
            }
        }

        public NoGuessPattern()
        {
            CaresAboutCharacters = false;
            CaresAboutPositions = false;
        }
    }
}
