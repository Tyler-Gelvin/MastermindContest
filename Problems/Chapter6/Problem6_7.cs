using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace Problems.Chapter6
{
    public static class Problem6_7
    {
        public static int GetLowestMissing(int[] numbers)
        {
            var present = new BitArray(numbers.Length + 2);

            for(int i = 0; i<numbers.Length; i++)
            {
                var current = numbers[i];
                if (current < present.Length && current > 0)
                {
                    present[current] = true;
                }
            }

            for (int i = 1; i < present.Length; i++)
            {
                if (!present[i])
                {
                    return i;
                }
            }

            throw new ApplicationException();
        }
    }
}
