using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace Problems.Chapter6
{
    public static class Problem6_9
    {
        public static double GetProfit(double[] prices)
        {
            var profit = 0.0;
            for (int i = 1; i < prices.Length; i++)
            {
                var dayProfit = prices[i] - prices[i - 1];
                if (dayProfit > 0)
                {
                    profit += dayProfit;
                }
            }

            return profit;
        }



        //10, -7, 4, -1, 6
    }
}
