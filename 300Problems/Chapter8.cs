using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems
{
    class Chapter8
    {
        //8.1 MERGE TWO SORTED LINKED LISTS
        // COnsider 2 sorted linked lists. Write an algorithm that merge the 2 lists
        // and the result is sorted.

        //HINT: Two sorted arrays can be merged using 2 indices. For lists, take care when one iterator reaches the end
        public static Node<int> MergeTwoSortedLists(Node<int> L1, Node<int> L2)
        {
            //Create a placeholder for the result
            Node<int> dummyHead = new Node<int>();
            Node<int> tail = dummyHead;
            while (L1 != null && L2 != null)
            {
                if (L1.Item <= L2.Item)
                {
                    tail.Next = L1;
                    tail = L1;
                    L1 = L1.Next;
                }
                else
                {
                    tail.Next = L2;
                    tail = L2;
                    L2 = L2.Next;
                }
            }

            //Appends the remaining nodes of the non empty list left
            tail.Next = L1 ??  L2;

            return dummyHead.Next;
        }


        //8.2 REVERSE A SINGLE SUBLIST
        // Write an algorithm which takes a singly linked list L and two integers s and f
        // as arguments, and reverse the order of the nodes from sth node to fth node, inclusive.
        // The numbering begins at 1, i.e., the head node is the first node.
        // Do not allocate additional nodes.

        //HINT: focus on the succesor fields which needs to be updated.
        public static Node<int> ReverseSublist(Node<int> L, int start, int finish)
        {
            if (start >= finish)
                return L;
            var dummy_head = new Node<int>()
            {
                Item = 0,
                Next = L.Clone()
            };

            var sublistHead = dummy_head;
            int k = 1;

            while(k++ < start)
            {
                sublistHead = sublistHead.Next;
            }

            var sublistIter = sublistHead.Next;
            while(start++ < finish)
            {
                var temp = sublistIter.Next;
                sublistIter.Next = temp.Next;
                temp.Next = sublistHead.Next;
                sublistHead.Next = temp;
            }

            return dummy_head.Next;
        }

        //8.3 TEST FOR CYCLICITY
        // Writes a program that takes the head of a singly linked list and returns null if there not exist a cycle
        // and the node at the start of the cycle, if a cycle is present (You do not know the length of the list in advance).

        //HINT: Consider using 2 iterators, one fast one slow
        public static Node<T> HasCycle<T>(Node<T> head)
        {
            Node<T> fast = head, slow = head;

            while(fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if(slow == fast)
                {

                    slow = head;
                    // Both iterators adanve in tandem now
                    while( slow != fast)
                    {
                        slow = slow.Next;
                        fast = fast.Next;
                    }

                    return slow; // iter is the start of the cycle.
                }
            }

            return null; // No cycle.
        }

        //8.4 TEST FOR OVERLAPPING LISTS - LISTS ARE CYCLE-FREE
        // Write a program that takes two cycle-free linked lists, and determines if there
        // exists a node that is common to both lists

        //HINT: Solve the simple cases first.
        static int Length<T>(Node<T> l)
        {
            int length = 0;
            while(l != null)
            {
                length++;
                l = l.Next;
            }

            return length;
        }

        //Advances L by k steps
        static void AdvanceListByK<T>(int k, ref Node<T> L)
        {
            while(k-- > 0)
            {
                L = L.Next;
            }
        }

        public static Node<T> OverlappingNoCycleLists<T>(Node<T> L1, Node<T> L2)
        {
            int l1Len = Length(L1);
            int l2Len = Length(L2);

            Node<T> listToAdvance = l1Len > l2Len ? L1 : L2;
            Node<T> otherList = listToAdvance == L1 ? L2 : L1;
            AdvanceListByK(Math.Abs(l1Len - l2Len), ref listToAdvance);

            while( listToAdvance != null && otherList != null && listToAdvance != otherList)
            {
                listToAdvance = listToAdvance.Next;
                otherList = otherList.Next;
            }

            return listToAdvance; //if L1 == null, then there is no overlap between L1 and L2;
        }

        //8.7 REMOVE THE kTH LAST ELEMENT FROM A LIST
        // Given a singly linked list and an integer k, write a program to remove the kTH last
        // element from the list. Need to use constant space.

        //HINT: if you know the length of the list, can you find the kTH last element using 2 iterators?

        //We assume the list has at least k nodes. We delete the k-th last node in the list
        public static Node<T> RemoveKthLastNode<T>(Node<T> L, int k)
        {
            var dummyHead = new Node<T> { Next = L };
            var first = dummyHead.Next;
            while(k-- > 0)
            {
                first = first.Next;
            }

            var second = dummyHead;

            while(first != null)
            {
                second = second.Next;
                first = first.Next;
            }

            // second points to (k+1)-th last node. Delete it's succesor;
            second.Next = second.Next.Next;

            return dummyHead.Next;
        }
    }
}
