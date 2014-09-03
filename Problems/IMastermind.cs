using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public interface IMastermind
    {
        bool IsWholePhrase { get; }
        GuessResult Guess(GuessPattern guess);
    }
}
