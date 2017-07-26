using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    /// <summary>
    /// STRINGS
    /// </summary>
    class Chapter7
    {
        //7.1 INTERCONVERT STRINGS AND INTEGERS
        public static string IntToString(int x)
        {
            bool isNegative = x < 0;

            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Insert(0, (char)('0' + Math.Abs(x % 10)));
                x = x / 10;
            } while (x != 0);

            if (isNegative) sb.Insert(0, '-');

            return sb.ToString();
        }

        public static int StringToInt(string s)
        {
            int result = 0;

            for (int i = s[0]=='-' ? 1:0; i < s.Length; i++)
            {
                int digit = s[i] - '0';

                result = result * 10 + digit;
            }

            return s[0] == '-' ? -result : result;
        }

        //7.2 BASE CONVERSION
        // Write a program that perform base conversion. The input is a string, a integer b1 and another integer b2.
        // The string represents an integer in base b1. The output should be a string representing the integer in base b2

        //HINT: What base can you easily convert to and from?
        public static string ConvertBase(string s, int b1, int b2)
        {
            bool isNegative = s[0] == '-';

            int numAsInt = 0;
            for (int i = s[0] == '-' ? 1 : 0; i < s.Length; i++)
            {
                numAsInt *= b1;
                numAsInt += Char.IsDigit(s[i]) ? s[i] - '0' : s[i] - 'A' + 10; 
            }

            return (isNegative ? "-" : "") + (numAsInt == 0 ? "0" : ConstructFromBase(numAsInt, b2));
        }

            static string ConstructFromBase(int numAsInt, int b)
        {
            return numAsInt == 0 ? "" : ConstructFromBase(numAsInt / b, b) +
                    (char)(numAsInt % b >= 10 ? 'A' + numAsInt % b - 10 : '0' + numAsInt % b);
        }

        //7.4 REPLACE AND REMOVE
        // Considering the following 2 rules that are to be applied to an array of characters:
        //  - Replace each 'a' by 2 'd'
        //  - Delete each entry containing 'b'

        // Write a program which takes as input an array of characters and removes each 'b' and
        // replaces each 'a' with 2 'd's. Along with the array you are provided with an integer 
        // value that represents the number of entires in the array that the operation will be applied to
        // You do not have to worry about preserving subsequent entries. For example, if the array is
        // {a,b,c,a, } and the size is 4 then you can return {d,d,d,d,c}. You can assume there is
        // enough space in the array to hold the final result.

        //HINT: Consider performing multiple passes on s.
        public static int ReplaceAndRemove(char[] s, int size)
        {
            int finalSize = 0;
            // Forward iteration: remove 'b's and count 'a's.
            int writeIndex = 0, aCount = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i] != 'b')
                {
                    s[writeIndex++] = s[i];
                }

                if (s[i] == 'a')
                    aCount++;                       
            }

            // Backward iteration: replace 'a's with 'dd's starting from the end.
            int curIndex = writeIndex - 1;
            writeIndex = writeIndex + aCount - 1;
            finalSize = writeIndex + 1;

            while(curIndex >= 0)
            {
                if(s[curIndex] == 'a')
                {
                    s[writeIndex--] = 'd';
                    s[writeIndex--] = 'd';
                }
                else
                {
                    s[writeIndex--] = s[curIndex];
                }

                curIndex--;
            }

            return finalSize;
        }

        //7.5 TEST PALINDROMICITY
        // A string is palindromic if, when removing all nonalphanumeric characters
        // it reads the same front to back ignoring case
        // E.g.:    - "A man, a plan, a canal, Panama." - is palyndrom
        //          - "Ray a Ray" - is not a palyndrom

        //HINT: use 2 indices
        public static bool IsPalindrome(string s)
        {
            string lowerS = s.ToLowerInvariant();
            // i moves froward, j moves backward.
            int i = 0, j = s.Length - 1;
            while ( i < j)
            {
                while (!Char.IsLetterOrDigit(lowerS[i]) && i < j)
                    i++;

                while (!Char.IsLetterOrDigit(lowerS[j]) && i < j)
                    j--;

                if (lowerS[i++] != lowerS[j--])
                    return false;
            }

            return true;
        }

        //7.6 REVERSE ALL THE WORDS IN A SENTENCE
        // Given a string containing a set of words separated by whitespace 
        // we would like to transform it into a string in which the words
        // appear in reverse order
        // E.g.: "Alice likes Bob" => "Bob likes Alice"
        
        // HINT: It's difficult to do it in one pass
        static void reverse(char[] s, int lo, int hi)
        {
            int i = lo, j = hi;
            while(i < j)
            {
                char temp = s[i];
                s[i++] = s[j];
                s[j--] = temp;
            }
        }

        public static string ReverseWords(string s)
        {
            //Reverses the whole string first;
            char[] c = s.ToArray();

            reverse(c, 0, c.Length-1);

            int start = 0, end = 0;

            while (end < c.Length)
            {
                for (end = start; end < c.Length && c[end] != ' '; end++) ;
                reverse(c, start, end-1);
                start = end+1;
            }
            return new string(c);
        }
    }
}
