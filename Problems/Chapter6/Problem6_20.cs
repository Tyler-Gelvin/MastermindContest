using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace Problems.Chapter6
{
    public static class Problem6_20
    {
        public static bool IsValidSudoku(int[,] puzzle)
        {
            if (puzzle.GetLength(0) != 9 || puzzle.GetLength(1) != 9)
            {
                throw new ApplicationException("Invalid puzzle size.");
            }

            var columns = Enumerable.Range(0, 9).Select(column => Enumerable.Range(0, 9).Select(row => puzzle[column, row]));
            var rows = Enumerable.Range(0, 9).Select(row => Enumerable.Range(0, 9).Select(column => puzzle[column, row]));
            var squares = Enumerable.Range(0, 9).Select(square => GetSquare(puzzle, square));

            return !(columns.Any(HasDuplicates) || rows.Any(HasDuplicates) || squares.Any(HasDuplicates));
        }

        public static IEnumerable<int> GetSquare(int[,] puzzle, int square)
        {
            var column = square % 3;
            var row = square / 3;

            return Enumerable.Range(0,9).Select(cell => puzzle[column + cell % 3, row + cell / 3]);
        }

        public static bool HasDuplicates(IEnumerable<int> list)
        {
            var found = new BitArray(10);

            foreach(var number in list)
            {
                if (number < 0 || number > 9)
                {
                    throw new ApplicationException("Number out of range.");
                }
                else if (number != 0)
                {
                    if (found[number])
                    {
                        return true;
                    }
                    else
                    {
                        found[number] = true;
                    }
                }
            }

            return false;
        }
    }
}
