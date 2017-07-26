using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    public class Chapter6
    {
        // quicksort the subarray a[lo .. hi] using 3-way partitioning
        private static void sort(int[] a, int lo, int hi)
        {
            if (hi <= lo) return;
            int lt = lo, gt = hi;
            int v = a[lo];
            int i = lo;
            while (i <= gt)
            {
                if (a[i] < v) exch(a, lt++, i++);
                else if (a[i] > v) exch(a, i, gt--);
                else i++;
            }

            // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
            sort(a, lo, lt - 1);
            sort(a, gt + 1, hi);
        }

        // exchange a[i] and a[j]
        private static void exch(int[] a, int i, int j)
        {
            int swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }
        
        //6.1 Dutch national flag or 3 way partitioning
        public static void DutchFlagPartition(int[] A, int pivotIndex, int lo, int hi)
        {
            if (hi <= lo) return;
            int pivot = A[pivotIndex];
            int smaller = lo;
            int equal = lo;
            int larger = hi;

            while(equal < larger)
            {
                if(A[equal] < pivot)
                {
                    exch(A, smaller++, equal++);
                }
                else if(A[equal] > pivot)
                {
                    exch(A, equal, larger--);
                }
                else
                {
                    equal++;
                }
            }
        }

        //6.2 Increment an arbitrary-precision integer
        // Write a program which takes as inout an array of digits encoding a decimal number D
        // and updates the array to represent the number D + 1. For example, if the input is 
        // {1, 2, 9} then you should update the array to {1, 3, 0}. Your alghorithm should work
        // even if it's implemnted in a language that has fine-precision arithmetic.
        // HINT: Experiment with concrete examples
        public static int[] PlusOne(int[] A)
        {
            // incement the last digit in the array.
            A[A.Length - 1]++;

            // Going from the back of the array deal with values that have carry point
            for(int i = A.Length-1; A[i]==10 && i>0; i--)
            {
                A[i] = 0;
                A[i - 1] += 1;
            }

            if(A[0] == 10)
            {
                // Need aditional element in array
                A[0] = 0;
                int[] B = new int[A.Length + 1];
                B[0] = 1;
                Array.Copy(A, 0, B, 1, A.Length);
                A = B;
            }

            return A;
        }

        //6.6 BUY AND SELL A STOCK ONCE
        //  Given and array with the stock price over 40 days, design an algorithm that determines
        // the maximum profit that could have been made by buying and selling a single share with
        // the constraint that the buying and selling have to take place at the start of the day

        // EXAMPLE: consider the following sequence {310, 315, 275, 295, 260, 270, 290, 230, 255, 250}. 
        // The max profit that can be made is 30 - buying at 260 and selling at 290. 
        // Note that 260 is not the lowest price nor 290 th highest.

        // HINT: Identifying the minimum and maximum is not enough since minimum can occur after maximum
        public static double BuyAndSellStockOnce(double[] prices)
        {
            //The max profit that can be made each day is the difference between opening price and the minimum seen so far
            double minPrice = double.MaxValue;
            double maxProfit = 0;

            foreach(int price in prices)
            {
                double maxProfitToday = price - minPrice;
                maxProfit = Math.Max(maxProfit, maxProfitToday);
                minPrice = Math.Min(minPrice, price);
            }

            return maxProfit;
        }

        //6.11 SAMPLE OFFLINE DATA
        // Implement and algorithm that takes an input array of distinct elements and a size
        // and returns a subset of the given size of the array elements. All subsets should be equal likely.
        // Return result in the input array itself.

        //HINT: How would you construct a random subset of size k+1 given a random subset of size k?
        public static void RandomSampling(int[] A, int k)
        {
            Random r = new Random();

            for(int i = 0; i < k; i++)
            {
                int randomIndex = r.Next(i, A.Length - 1);
                exch(A, i, randomIndex);
            }
        }

        //6.16 Sudoku checker problem
        // Sudoku is a popular logic-based combinatorial number placemnt puzzle.
        // the objective is to fill a 9x9 grid with digits subjet to the constraint that each column, each row,
        // and each nine of the 3x3 subgrids that compose the grid contain unique integers
        
        // Design an algorithm to check wheter a 9x9 2d aray represnting a partially completed Sudoku is valid.
        // Specifically, check that no row, column, or 3x3 2d subarrays contains duplicates. A 0-value in the 2d arrays
        // indicates that the entry is blank; every other entry is in [1,9].

        //HINT: directly test the coinstraints. Use an array to encode sets.

        /// <summary>
        /// Check if a partially filled matrix has any conflicts
        /// </summary>
        /// <param name="partialAssignment"></param>
        /// <returns></returns>
        bool IsValidSudoku(int[,] partialAssignment)
        {
            int rowCount = partialAssignment.GetLength(0);
            int columnCount = partialAssignment.GetLength(1);

            // Check row constraints
            for(int i = 0; i < rowCount; i++)
            {
                if (HasDuplicate(partialAssignment, i, i + 1, 0, rowCount))
                {
                    return false;
                }
            }

            // Check column constraints
            for(int j = 0; j < columnCount; j++)
            {
                if (HasDuplicate(partialAssignment, 0, columnCount, j, j + 1))
                {
                    return false;
                }
            }

            // Check the region constraints
            int regionSize = (int)Math.Sqrt(rowCount);
            for(int I = 0; I < regionSize; I++)
            {
                for(int J = 0; J < regionSize; J++)
                {
                    if(HasDuplicate(partialAssignment, regionSize*I, regionSize*(I+1), regionSize*J, regionSize*(J+1)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        bool HasDuplicate(int[,] partialAssignment, int startRow, int endRow, int startCol, int endCol)
        {
            bool[] isPresent = new bool[partialAssignment.GetLength(0) + 1];

            for(int i = startRow; i <endRow; i++)
            {
                for(int j = startCol; j < endCol; j++)
                {
                    if(partialAssignment[i,j] != 0 && isPresent[partialAssignment[i, j]])
                    {
                        return true;
                    }

                    isPresent[partialAssignment[i, j]] = true;
                }
            }

            return false;
        }

        //6.17 COMPUTE THE SPIRAL ORDERING OF A 2D ARRAY
        // Write a program which takes a 2D array and returns the spiral ordering of the array.

        //HINT: Use case analysis and divide-and-conquer.

        //SOLUTION 1: 4 iterations
        public static int[] MatrixInSpiralOrdering(int[,] squareMatrix)
        {
            List<int> spiralOrdering = new List<int>();
            for (int offset = 0; offset < (int)Math.Ceiling(0.5 * squareMatrix.GetLength(0)); offset++)
            {
                MatrixLayerInClockwise(squareMatrix, offset, spiralOrdering);
            }

            return spiralOrdering.ToArray();
        }

        static void MatrixLayerInClockwise(int[,] squareMatrix, int offset, List<int> spiralOrdering)
        {
            int matrixSize = squareMatrix.GetLength(0);
            if(offset == matrixSize-offset-1)
            {
                // matrix has odd dimension and we are at the center of it
                spiralOrdering.Add(squareMatrix[offset, offset]);
                return;
            }

            // Add top row
            for(int j = offset; j < matrixSize - offset -1; j++)
            {
                spiralOrdering.Add(squareMatrix[offset, j]);
            }

            // Add right column
            for(int i = offset; i < matrixSize-offset-1; i++)
            {
                spiralOrdering.Add(squareMatrix[i, matrixSize-offset-1]);
            }

            // Add bottom row in reverse order
            for(int j = matrixSize -offset-1; j > offset; j--)
            {
                spiralOrdering.Add(squareMatrix[matrixSize-offset-1, j]);
            }

            // Add left column in reverse order
            for (int i = matrixSize-offset-1; i > offset; i--)
            {
                spiralOrdering.Add(squareMatrix[i, offset]);
            }
        }
    }
}
