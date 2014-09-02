using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Problems.Chapter6
{
    public static class Problem6_3
    {
        public static string Multiply(string oneAsString, string twoAsString)
        {
            return (BigInteger.Parse(oneAsString) * BigInteger.Parse(twoAsString)).ToString();
        }
    }
}
