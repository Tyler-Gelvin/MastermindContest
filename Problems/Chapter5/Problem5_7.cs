using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Chapter5
{
    public static class Problem5_7
    {
        public static double Power(double x, int y)
        {
            if (y < 0)
            {
                return Power(1.0 / x, Math.Abs(y));
            }

            return new BitArray(new[] { y })
                .Cast<bool>()
                .Select((bit, i) =>
                    {
                        var part = bit ? x : 1;
                        x *= x;
                        return part;
                    })
                .Aggregate(1.0, (a, b) => a * b);
        }

        public static double Power2(double x, int y)
        {
            if (y < 0)
            {
                return Power2(1.0 / x, Math.Abs(y));
            }

            return new BitArray(new[] { y })
                .Cast<bool>()
                .Aggregate(1.0, (current, bit) =>
                {
                    current *= bit ? x : 1;
                    x *= x;
                    return current;
                });
        }
    }
}
