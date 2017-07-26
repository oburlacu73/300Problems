using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Problems.Chapter10
{
    class Chapter10
    {
        private struct BalancedStatusWithHeight
        {
            public BalancedStatusWithHeight(bool b, int h)
            {
                balanced = b;
                height = h;
            }

            public bool balanced;
            public int height;
        }

        //10.1 TEST IF A BINARY TREE IS HEIGHT-BALANCED
        // A binary tree is said to be height-balanced if for each node in the tree, the difference in height
        // of it's left and right subtrees is at most one
        // Write a program that takes as input the root of a binary tree and checks if wheter the tre is height-balanced.

        //HINT: Think of a classic binary tree problem.
        public static bool IsBalanced<T>(BinaryTreeNode<T> root)
        {

            return checkBalanced(root).balanced;
        }

        private static BalancedStatusWithHeight checkBalanced<T>(BinaryTreeNode<T> root)
        {
            if(root == null)
            {
                return new BalancedStatusWithHeight(true,1);
            }

            var leftResult = checkBalanced(root.Left);
            if(!leftResult.balanced)
            {
                return new BalancedStatusWithHeight(false, 0);
            }

            var rightResult = checkBalanced(root.Right);
            if (!rightResult.balanced)
            {
                return new BalancedStatusWithHeight(false, 0);
            }

            bool isBalanced = Math.Abs(leftResult.height - rightResult.height) <= 1;
            int height = Math.Max(leftResult.height, rightResult.height) + 1;

            return new BalancedStatusWithHeight(isBalanced, height);
        }

        //10.2 TEST IF A BINARY TREE IS SYMMETRIC
        // A binary tree is symmetric if you can draw a vertical line through the root and then the left tree
        // is the mirror image of th right tree.
        // Write a program that checks if a tree is symmetric.

        //HINT: The definition of symmetry is recursive
        private static bool areMirror<T>(BinaryTreeNode<T> tree1, BinaryTreeNode<T> tree2)
        {
            if (tree1 == null && tree2 == null)
            {
                return true;
            }
            else if (tree1 != null && tree2 != null)
            {
                return (tree1.Data.Equals(tree2.Data)
                    && areMirror(tree1.Left, tree2.Right)
                    && areMirror(tree1.Right, tree2.Left));
            }

            // One tree is empty the other is not
            return false;
        }

        //10.4 COMPUTE LCA WHEN NODES HAVE PARENT POINTER
        // Given 2 nodes in a binary tree, design an algorithm that computes their LCA.
        // Assume that each node has a parent pointer

        //HINT: The problem is easy if both nodes are same distance from the root.
        private static int getDepth<T>(BinaryTreeNode<T> node)
        {
            int depth = -1;
            while( node != null )
            {
                depth++;
                node = node.Parent;
            }

            return depth;
        }

        public static BinaryTreeNode<T> LCA<T>(BinaryTreeNode<T> node0, BinaryTreeNode<T> node1)
        {
            var iter0 = node0;
            var iter1 = node1;
            int depth0 = getDepth(node0), depth1 = getDepth(node1);

            // Make iter0 the deeper node to simplify the code
            if(depth0 < depth1)
            {
                var temp = iter0;
                iter0 = iter1;
                iter1 = iter0;
            }

            // Ascend from the deeper node
            int depthDiff = Math.Abs(depth0 - depth1);
            while(depthDiff-- > 0)
            {
                iter0 = iter0.Parent;
            }

            //Now ascend both nodes until they meet
            while(iter0 != iter1)
            {
                iter0 = iter0.Parent;
                iter1 = iter1.Parent;
            }

            return iter0;
        }

        //10.12 RECONSTRUCT BINARY TREE FROM TRAVERSAL DATA
        // Given an inorder and a preorder traversal of a binary tree, write a program to recontruct the tree.
        // Assume each node has a unique key.

        //HINT: Focus on the root
        private static BinaryTreeNode<int> BinaryTreeFromPreorderInorderHelper(int[] preorder, int preorderStart, int preorderEnd, int inorderStart, int inorderEnd, Dictionary<int,int> nodeToInorderIdx)
        {
            if (preorderEnd <= preorderStart || inorderEnd <= inorderStart)
                return null;

            int rootInorderIndex = nodeToInorderIdx[preorder[preorderStart]];
            int leftSubtreeSize = rootInorderIndex - inorderStart;

            BinaryTreeNode<int> root = new BinaryTreeNode<int>() { Data = preorder[preorderStart] };
            root.Left = BinaryTreeFromPreorderInorderHelper(preorder, preorderStart + 1, preorderStart + leftSubtreeSize + 1, inorderStart, rootInorderIndex, nodeToInorderIdx);
            root.Right = BinaryTreeFromPreorderInorderHelper(preorder, preorderStart + 1+leftSubtreeSize, preorderEnd, rootInorderIndex+1, inorderEnd, nodeToInorderIdx);

            return root;
        }

        public static BinaryTreeNode<int> BinaryTreeFromPreorderInorder(int[] preorder, int[] inorder)
        {
            Dictionary<int, int> nodeToInorderIdx = new Dictionary<int, int>();

            for(int i = 0; i < inorder.Length; i++)
            {
                nodeToInorderIdx.Add(inorder[i], i);
            }

            return BinaryTreeFromPreorderInorderHelper(preorder, 0, preorder.Length, 0, inorder.Length, nodeToInorderIdx);
        }
    }
}
