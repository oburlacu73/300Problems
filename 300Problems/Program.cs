using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 2, 4, 6, 7, 9, 16, 15, 18, 21, 27, 24 };

            //EvenOdd(arr);

            /*arr = new int[] { 9, 9, 9 };
            arr = Chapter6.PlusOne(arr);

            arr = new int[] { 1, 2, 7 };
            Chapter6.PlusOne(arr);

            arr = new int[] { 1, 2, 9 };
            Chapter6.PlusOne(arr);

            double[] prices = new double[] { 310, 315, 275, 295, 260, 270, 290, 230, 255, 250 };
            double maxProfit = Chapter6.BuyAndSellStockOnce(prices);

            prices = new double[] { 310, 305, 300, 295, 290, 270, 260, 230, 225, 205 };
            maxProfit = Chapter6.BuyAndSellStockOnce(prices);

            arr = new int[]{3, 7, 5, 11};

            Chapter6.RandomSampling(arr, 3);

            int[,] squareMatrix = new int[4, 4] { { 1, 2, 3, 4 }, { 12, 13, 14, 5 }, { 11, 16, 15, 6 }, { 10, 9, 8, 7 } };

            int[] spiralOrdering = Chapter6.MatrixInSpiralOrdering(squareMatrix);

            squareMatrix = new int[5, 5] {{1, 2, 3, 4, 5},
{16, 17, 18, 19, 6},
{15, 24, 25, 20, 7},
{14, 23, 22, 21, 8},
{13, 12, 11, 10, 9}};

            spiralOrdering = Chapter6.MatrixInSpiralOrdering(squareMatrix);

            int x = 110;
            string s = Chapter7.IntToString(x);

            x = 145;
            s = Chapter7.IntToString(x);

            x = -1145;
            s = Chapter7.IntToString(x);

            string s = "110";
            int x = Chapter7.StringToInt(s);

            s = "0";
            x = Chapter7.StringToInt(s);

            s = "-796";
            x = Chapter7.StringToInt(s);

            string s = "16";
            string c = Chapter7.ConvertBase(s, 10, 2);

            s = "10";
            c = Chapter7.ConvertBase(s, 16, 10);

            string s = "A man, a plan, a canal, Panama.";
            bool isPalyndrome = Chapter7.IsPalindrome(s);

            s = "Ray a Ray";
            isPalyndrome = Chapter7.IsPalindrome(s);

            s = "Able was I, ere I saw Elba!";
            isPalyndrome = Chapter7.IsPalindrome(s);

            string s = "Alice likes Bob";
            string reverseS = Chapter7.ReverseWords(s);

            //CHAPTER 8
            Node<int> l1 = new Node<int>
            {
                Item = 2,
                Next = new Node<int>()
                {
                    Item = 5,
                    Next = new Node<int>()
                    {
                        Item = 7
                    }
                }
            };

            Node<int> l2 = new Node<int>()
            {
                Item = 3,
                Next = new Node<int>()
                {
                    Item = 11
                }
            };

            Node<int> merged = Chapter8.MergeTwoSortedLists(l1, l2);

            Node<int> a = Chapter8.ReverseSublist(merged, 2, 4);
            Node<int> b = Chapter8.ReverseSublist(merged, 1, 5);

            int a = 16;
            int r_bit = a & -a;

            Node<int> node1 = new Node<int>() { Item = 1 };
            Node<int> node2 = new Node<int>() { Item = 2 };
            Node<int> node3 = new Node<int>() { Item = 3 };
            Node<int> node4 = new Node<int>() { Item = 4 };
            Node<int> node5 = new Node<int>() { Item = 5 };
            Node<int> node6 = new Node<int>() { Item = 6 };

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = node5;
            node6.Next = node3; //TO MAKE THIS WORK REMOVE THE ToString in Node class!!!!!!

            //Node<int> cycleStart = Chapter8.HasCycle<int>(node1);

            //Node<int> overlapStart = Chapter8.OverlappingNoCycleLists<int>(node1, node6);

            Node<int> removed = Chapter8.RemoveKthLastNode<int>(node1, 3);

            string s = "edifieda";
            bool bPalindrome = Chapter13.CanFormPalindrome(s);*/
            Node<int> node5 = new Node<int>() { Item = 5 };
            Node<int> node4 = new Node<int>() { Item = 4, Next = node5 };
            Node<int> node3 = new Node<int>() { Item = 3, Next = node4 };
            Node<int> node2 = new Node<int>() { Item = 2, Next = node3 };
            Node<int> node1 = new Node<int>() { Item = 1, Next = node2 };

            Node<int> a = Chapter8.ReverseSublist(node1, 2, 4);
        }

        static void EvenOdd(int[] array)
        {
            int nextEven = 0, nextOdd = array.Length - 1;

            while(nextEven < nextOdd)
            {
                if(array[nextEven]%2 == 0)
                {
                    nextEven++;
                }
                else
                {
                    int backup = array[nextEven];
                    array[nextEven] = array[nextOdd];
                    array[nextOdd--] = backup;
                }
            }
        }
    }
}
