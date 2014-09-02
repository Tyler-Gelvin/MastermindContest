using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Chapter5
{
    public static class Problem5_8
    {
        static char ToChar(int i)
        {
            if (i >= 0 && i <= 9)
            {
                return (char)(i + '0');
            }
            else
            {
                return (char)(i + 'A' - 10);
            }
        }

        static int ToInt(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            else
            {
                return c - 'A' + 10;
            }
        }

        static IEnumerable<char> ToBase(int i, int b)
        {
            while (i > 0)
            {
                var c = i % b;
                yield return ToChar(c);
                i /= b;
            }
        }

        public static string ChangeBase(int base1, string s, int base2)
        {
            if (s.First() == '-')
            {
                return "-" + ChangeBase(base1, s.Substring(1), base2);
            }

            var r = s
                .Reverse()
                .Select((c, i) => ToInt(c) * (int)Math.Pow(base1, i))
                .Sum();

            return r == 0 ? "0" : new string(ToBase(r, base2).Reverse().ToArray());
        }
    }
}
