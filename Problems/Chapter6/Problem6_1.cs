using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problems.Chapter6
{
    public static class Problem6_1
    {
        public static void ArrangeAroundIndex<T>(T[] arr, int pivotIndex)
            where T : IComparable<T>
        {
            var less = 0;
            var equal = 0;
            var more = arr.Length - 1;
            var pivot = arr[pivotIndex];

            for (var current = 0; current <= more;)
            {
                var comparison = arr[current].CompareTo(pivot);
                if (comparison>0)
                {
                    var swap = arr[current];
                    arr[current] = arr[more];
                    arr[more] = swap;
                    more--;
                }
                else if (comparison == 0)
                {
                    var swap = arr[current];
                    arr[current] = arr[equal];
                    arr[equal] = swap;
                    equal++;
                    current++;
                }
                else
                {
                    var swap = arr[current];
                    arr[current] = arr[less];
                    arr[less] = swap;
                    equal++;
                    less++;
                    current++;
                }
            }
        }
    }
}
