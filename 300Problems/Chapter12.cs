using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    class Chapter12
    {
        //12.1 SEARCH A SORTED ARRAY FOR THE FIRST OCCURENCE OF k
        // Write a method that takes a sorted array and a key and returns the index of the first occurence
        // of that key in the array.

        //HINT: What happens when every key is equal to k? Don't stop when you first see k.
        public static int SearchOfFirstK(int[] A, int k)
        {
            int left = 0, right = A.Length - 1, result = -1;

            // [left:right] is the candidate set
            while(left < right)
            {
                int mid = left + ((right - left) / 2);

                if(A[mid] > k)
                {
                    right = mid - 1;
                }
                else if(A[mid]==k)
                {
                    result = mid;

                    // Nothing to the right of mid can be FIRST occurence of k
                    right = mid - 1;
                }
                else // A[mid] < k
                {
                    left = mid + 1;
                }
            }

            return result;
        }

        //12.3 SEARCH A CYCLICALLY SORTED ARRAY
        // An array is said to be cyclically sorted if it's possible to cyclically shift its entries
        // so that it becomes sorted.
        // Design an O(log n) algorithm to find the position of the smalles element in a cyclically
        // sorted array. Assume all elemnts are distinct

        public static int SearchSmallest(int[] A)
        {
            int left = 0, right = A.Length - 1;

            while(left < right)
            {
                int mid = left + ((right - left) / 2);
                if( A[mid] > A[right])
                {
                    // Minimum must be in [mid+1:right]
                    left = mid + 1;
                }
                else // A[mid] < A[right]
                {
                    // minimum cannot be in [mid+1:right] so it must be in [left:mid]
                    right = mid;
                }
            }

            // Loop ends when left == right
            return left;
        }

        //12.4 Compute the integer square root
        // Write a program which takes a nonnegative integer and return the largest integer
        // whose square is less than or equal to the give integer.
        //HINT: Brute force for MAX_INT64 takes 500 years with 1 nanosecond per number
        public static int SquareRoot(int k)
        {
            int left = 0, right = k;

            // Candidate interval [left:right] where everything before left has square <= k,
            // everything after right has square > k.
            while( left <= right)
            {
                long mid = left + ((right - left) / 2);
                long mid_squared = mid * mid;

                if(mid_squared <= k)
                {
                    left = (int)mid + 1;
                }
                else
                {
                    right = (int)mid - 1;
                }
            }

            return left - 1;
        }

        //12.8 FIND THE kTH LARGEST ELEMENT
        // Design an algorithm for computing the k-th largest element in an array. 
        // Assume entries are distinct.
        //HINT: use the quicksort partitioning in conjuction with randomization.
        private static void exch(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        private static int partition(int[] a, int lo, int hi)
        {
            int i = lo, j = hi + 1;

            while (true)
            {
                while (a[++i] < a[lo]) //find item on left to swap
                    if (i == hi) break;

                while (a[lo] < a[--j])
                    if (j == lo) break;

                if (i >= j) break; //check if indexes crossed

                exch(a, i, j); //swap
            }

            exch(a, lo, j); // swap the pivot in place

            return j; // return the index of the item now known to be in place
        }

        public static int FindKth(int[] A, int k)
        {
            int lo = 0, hi = A.Length - 1;
            Random rand = new Random();
            while (hi > lo)
            {
                int pivotIndex = rand.Next(lo, hi);
                exch(A, lo, pivotIndex);

                int j = partition(A, lo, hi);

                if (j < k) lo = j + 1;
                else if (j > k) hi = j - 1;
                else return A[k];
            }

            return A[k];
        }

        //12.9 FIND THE MISSING IP ADDRESS
        // Suppose you were give a file containing roughly 1 billion ip addresses, each of which is a 32-bit quantity.
        // How would you programatically find an IP address that is not in the file?
        // Assume you have unlimited disk space but only a few MB RAM memory

        //HINT: Can you be sure there is an address which is not in the file?
        int FindMissingElement(Stream file)
        {
            int missingIP = 0;
            int kNumBuckets = 1 << 16;
            int[] counter = new int[kNumBuckets];

            uint x;

            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    x = UInt32.Parse(line);
                    int upperPartX = (int)(x >> 16);
                    ++counter[upperPartX];
                }
            }

            // Look for a bucket that contains less than (1 << 16) elements
            int bucketCapacity = 1 << 16;
            int candidateBucket = 0;
            for (int i = 0; i < kNumBuckets; i++)
            {
                if (counter[i] < bucketCapacity)
                {
                    candidateBucket = i;
                    break;
                }
            }

            //Find all IP addresses in the stream whose first 16 bits
            // are equal to candidateBucket

            BitArray bitVector = new BitArray(bucketCapacity);
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    x = UInt32.Parse(line);
                    int upperPartX = (int)(x >> 16);

                    if(upperPartX == candidateBucket)
                    {
                        // Record the presence of 16 LSB of x
                        int lowerPartX = (int)(((1 << 16) - 1) & x);
                        bitVector[lowerPartX] = true;
                    }
                }
            }

            //At least one of the LSB combinations is absent, let's find it
            for(int i = 0; i < bucketCapacity; i++)
            {
                if(bitVector[i] == false)
                {
                    missingIP = (int)((candidateBucket << 16) | i);
                    break;
                }
            }

            return missingIP;
        }
    }
}
