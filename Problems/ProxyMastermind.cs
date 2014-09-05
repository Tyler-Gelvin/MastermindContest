using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class ProxyMastermind : IMastermind
    {
        public static int RequiredCounters = 5;
        public Mastermind RealMastermind;
        public IDictionary<LetterCounter, int> LetterCounts = new Dictionary<LetterCounter, int>();
        Queue<LetterCounter> GuessQueue = new Queue<LetterCounter>();

        public ProxyMastermind(Mastermind realMastermind, IEnumerable<LetterCounter> extraGuesses)
        {
            RealMastermind = realMastermind;

            foreach (var guess in extraGuesses)
            {
                GuessQueue.Enqueue(guess);
            }
        }

        public bool IsWholePhrase
        {
            get { return true; }
        }

        public GuessResult Guess(GuessPattern positionGuesser)
        {
            if (GuessQueue.Count() == 0)
            {
                return RealMastermind.Guess(positionGuesser);
            }

            var characterGuesser = GuessQueue.Dequeue();

            if (positionGuesser.CaresAboutCharacters || characterGuesser.CaresAboutPositions)
            {
                throw new ApplicationException();
            }

            if (positionGuesser.CaresAboutPositions)
            {
                return DoNormalGuess(positionGuesser, characterGuesser);
            }
            else
            {
                return DoExtendedGuess(characterGuesser);
            }
        }

        GuessResult DoNormalGuess(GuessPattern positionGuesser, LetterCounter characterGuesser)
        {
            var positionPattern = positionGuesser.PhrasePattern;
            var characterPattern = characterGuesser.PhrasePattern;

            var pattern = new StringGuessPattern(positionPattern + characterPattern.Substring(positionPattern.Length));

            var result = RealMastermind.Guess(pattern);

            LetterCounts.Add(characterGuesser, result.Positions + result.Chars - 2);

            if (result.Positions == 2)
            {
                if (Mastermind.CountSpaces(positionPattern) == 2)
                {
                    return new GuessResult(-1, 2);
                }
                else
                {
                    return new GuessResult(0, 2);
                }
            }
            else
            {
                return new GuessResult(2 - result.Positions, result.Positions);
            }
        }

        GuessResult DoExtendedGuess(LetterCounter characterGuesser)
        {
            var pattern = new StringGuessPattern(characterGuesser.ExtendedPattern);
            var result = RealMastermind.Guess(pattern);

            LetterCounts.Add(characterGuesser.ReducedLetterCounter, result.Chars);
            LetterCounts.Add(characterGuesser.ExtendedLetterCounter, result.Positions);

            // Nobody should actually care.
            return null;
        }
    }
}
