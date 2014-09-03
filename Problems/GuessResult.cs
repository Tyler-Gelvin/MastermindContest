using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyProject
{
    public class GuessResult : IComparable<GuessResult>
    {
        public int Chars { get; private set; }
        public int Positions { get; private set; }
        public bool Solved { get; private set; }

        public GuessResult(int chars, int positions)
        {
            if (chars < -1)
            {
                throw new ArgumentException("chars");
            }
            else if (positions < 0)
            {
                throw new ArgumentException("positions");
            }

            Chars = chars;
            Positions = positions;
            Solved = chars == -1;
        }

        public bool Equals(GuessResult other)
        {
            return Chars.Equals(other.Chars) && Positions.Equals(other.Positions);
        }

        public override bool Equals(object obj)
        {
            var other = obj as GuessResult;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Chars << 16) ^ Positions;
        }

        int IComparable<GuessResult>.CompareTo(GuessResult other)
        {
            var comparison = Chars.CompareTo(other.Chars);
            return comparison != 0 ? comparison : Positions.CompareTo(other.Positions);
        }

        public override string ToString()
        {
            return Solved ? "Solved" : string.Format("{0}, {1}", Chars, Positions);
        }
    }
}
