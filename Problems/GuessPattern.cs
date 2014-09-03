using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public abstract class GuessPattern
    {
        public abstract string PhrasePattern
        {
            get;
        }

        public virtual string WordPattern
        {
            get
            {
                return PhrasePattern;
            }
        }

        public string GetPattern(bool wholePhrase)
        {
            return wholePhrase ? PhrasePattern : WordPattern;
        }

        public bool CaresAboutPositions
        {
            get;
            protected set;
        }

        public bool CaresAboutCharacters
        {
            get;
            protected set;
        }
    }
}
