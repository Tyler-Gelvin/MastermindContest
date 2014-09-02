using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Chapter6
{
    public static class Problem7_6
    {
        public static string GetLookAndSayNumber(int n)
        {
            var current = "1";
            for (int i = 1; i<n; i++)
            {
                current = GetNextLookAndSayNumber(current);
            }

            return current;
        }

        public static string GetNextLookAndSayNumber(string s)
        {
            var builder = new StringBuilder();
            var count = 0;
            var current = ' ';
            foreach (var digit in s)
            {
                if (digit == current)
                {
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        builder.Append(count);
                        builder.Append(current);
                    }

                    current = digit;
                    count = 1;
                }
            }

            if (count > 0)
            {
                builder.Append(count);
                builder.Append(current);
            }

            return builder.ToString();
        }
    }
}
