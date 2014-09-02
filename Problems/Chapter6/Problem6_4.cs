using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Problems.Chapter6
{
    public static class Problem6_4
    {
        public static bool CanWin(int[] moves)
        {
            var needed = 0;
            for (int i = moves.Length - 1; i >= 0; i--)
            {
                needed++;
                if (moves[i] >= needed)
                {
                    needed = 0;
                }
            }

            return needed == 0;
        }
    }
}
