using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    class Chapter13
    {
        //13.1 TEST FOR PALINDROMIC PERMUTATIONS
        // Write a program to test wheter the letters forming a string can be permuted
        // to form a palindrome. E.g.: "edified" can be permuted to form "deified"
        //HINT: Find a simple characterization of string that can be permuted to form a palindrome.
        public static bool CanFormPalindrome(string s)
        {
            Dictionary<char, int> charFrequencies = new Dictionary<char, int>();
            foreach(char c in s)
            {
                if (charFrequencies.ContainsKey(c))
                {
                    charFrequencies[c] += 1;
                }
                else
                {
                    charFrequencies.Add(c, 1);
                }
            }

            // A string can be premuted as a palindrome if and only if the number of chars whose 
            // frequencies is odd is at most 1.
            int oddFrequencyCount = 0;

            foreach(var pair in charFrequencies)
            {
                if(pair.Value % 2 == 1)
                {
                    oddFrequencyCount++;
                }
            }

            return oddFrequencyCount <= 1;
        }

        //13.2 IS AN ANONYMOUS LETTER CONSTRUCTIBLE?
        // Write a program which takes a letter and a magazine and determines if it's
        // possible to wirte the letter using the letters from the magazine

        //HINT: Count the number of distinct characters appearing in the letter
        public static bool IsLetterConstructibleFromMagazine(string letterText, string magazineText)
        {
            Dictionary<char, int> letterCharFrequency = new Dictionary<char, int>();

            foreach(char c in letterText)
            {
                if(!letterCharFrequency.ContainsKey(c))
                {
                    letterCharFrequency.Add(c, 1);
                }
                else
                {
                    letterCharFrequency[c] += 1;
                }
            }

            //Check if the letters in magazineText can cover the characters in the dictionary
            foreach(char c in magazineText)
            {
                if(letterCharFrequency.ContainsKey(c))
                {
                    letterCharFrequency[c] -= 1;

                    if(letterCharFrequency[c] == 0)
                    {
                        letterCharFrequency.Remove(c);
                    }
                }

                if (letterCharFrequency.Count == 0)
                    break; // All characters are matched we can write the letter.
            }

            return letterCharFrequency.Count == 0;
        }

    }
}
