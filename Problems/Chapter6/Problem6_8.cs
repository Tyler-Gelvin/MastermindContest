using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace Problems.Chapter6
{
    public static class Problem6_8
    {
        public static double GetBatteryRequired(IEnumerable<Tuple<double, double, double>> points)
        {
            var debt = 0.0;
            var current = 0.0;
            
            foreach(var height in points.Select(point => point.Item3))
            {
                current -= height;
                debt = Math.Min(current, debt);
            }

            return -debt;
        }
    }
}
