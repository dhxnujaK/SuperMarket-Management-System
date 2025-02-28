using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public class TreeNode
    {
        public int Key;
        public TreeNode? left;
        public TreeNode? right;

        public TreeNode(int key)
        {
            Key = key;
            left = null;
            right = null;
        }
    }
    public class BinarySearchTree
    {
        private TreeNode? root;

        public void InsertKey(int key)
        {
            root = InsertRecursively(key, root);
        }

       

        public void Delete(int key)
        {
            root = DeleteRecursively(root, key);
        }


        public void PrintTree()
        {
            PrintInOrder(root);
            Console.WriteLine();
        }

        public void PrintPreorderTree()
        {
            PrintPreOrder(root);
            Console.WriteLine();
        }

        public void PrintPostorderTree()
        {
            PrintPostOrder(root);
            Console.WriteLine();
        }

        public BinarySearchTree()
        {
            root = null;
        }


        private TreeNode InsertRecursively(int key, TreeNode? root)
        {
            if (root == null)
            {
                root = new TreeNode(key);
                return root;
            }

            if (key < root.Key)
            {
                root.left = InsertRecursively(key, root.left);
            }
            else if (key > root.Key)
            {
                root.right = InsertRecursively(key, root.right);
            }

            return root;
        }

       

        private TreeNode? DeleteRecursively(TreeNode? root, int key)
        {
            if (root == null)
            {
                return root;
            }

            if (key < root.Key)
            {
                root.left = DeleteRecursively(root.left, key);
            }
            else if (key > root.Key)
            {
                root.right = DeleteRecursively(root.right, key);
            }
            else
            {
                if (root.right == null)
                {
                    return root.left;
                }
                if (root.left == null)
                {
                    return root.right;
                }

                root.Key = FindMinRecursively(root.right);
                //root.Key = FindMinIteratively(root.right);
                root.right = DeleteRecursively(root.right, root.Key);
            }

            return root;
        }

        private int FindMinRecursively(TreeNode? root)
        {
            if (root.left == null)
            {
                return root.Key;
            }

            return FindMinRecursively(root.left);
        }

        private void PrintInOrder(TreeNode? root)
        {
            if (root != null)
            {
                PrintInOrder(root.left);
                Console.Write(root.Key + " ");
                PrintInOrder(root.right);
                //PrintInOrder(root.right);
                //Console.WriteLine(root.Key);
                //PrintInOrder(root.left);
            }

        }

        private void PrintPreOrder(TreeNode? root)
        {
            if (root != null)
            {
                Console.Write(root.Key + " ");
                PrintPreOrder(root.left);
                PrintPreOrder(root.right);
            }
        }

        private void PrintPostOrder(TreeNode? root)
        {
            if (root != null)
            {

                PrintPostOrder(root.left);
                PrintPostOrder(root.right);
                Console.Write(root.Key + " ");
            }
        }
    }
}
