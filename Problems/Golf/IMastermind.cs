using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Golf
{
    public interface IMastermind
    {
        bool IsWholePhrase { get; }
        GuessResult Guess(GuessPattern guess);
    }
}
