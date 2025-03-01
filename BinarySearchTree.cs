using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class TreeNode
    {
        public User Data;
        public TreeNode? Left;
        public TreeNode? Right;

        public TreeNode(User data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    public class BinarySearchTree
    {
        private TreeNode? root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void InsertUser(User user)
        {
            root = InsertRecursively(user, root);
        }

        private TreeNode InsertRecursively(User user, TreeNode? root)
        {
            if (root == null)
            {
                return new TreeNode(user);
            }

            if (user.Id < root.Data.Id)
            {
                root.Left = InsertRecursively(user, root.Left);
            }
            else if (user.Id > root.Data.Id)
            {
                root.Right = InsertRecursively(user, root.Right);
            }

            return root;
        }

        public void DeleteUser(int userId)
        {
            root = DeleteRecursively(root, userId);
        }

        private TreeNode? DeleteRecursively(TreeNode? root, int userId)
        {
            if (root == null)
            {
                return root;
            }

            if (userId < root.Data.Id)
            {
                root.Left = DeleteRecursively(root.Left, userId);
            }
            else if (userId > root.Data.Id)
            {
                root.Right = DeleteRecursively(root.Right, userId);
            }
            else
            {
                if (root.Right == null)
                {
                    return root.Left;
                }
                if (root.Left == null)
                {
                    return root.Right;
                }

                root.Data = FindMinUser(root.Right);
                root.Right = DeleteRecursively(root.Right, root.Data.Id);
            }

            return root;
        }

        private User FindMinUser(TreeNode? root)
        {
            while (root?.Left != null)
            {
                root = root.Left;
            }

            return root!.Data;
        }

        public User? SearchUser(int userId)
        {
            return SearchRecursively(root, userId);
        }

        private User? SearchRecursively(TreeNode? root, int userId)
        {
            if (root == null)
            {
                return null;
            }

            if (userId == root.Data.Id)
            {
                return root.Data;
            }

            if (userId < root.Data.Id)
            {
                return SearchRecursively(root.Left, userId);
            }
            else
            {
                return SearchRecursively(root.Right, userId);
            }
        }

        public void PrintInOrder()
        {
            PrintInOrder(root);
            Console.WriteLine();
        }

        private void PrintInOrder(TreeNode? root)
        {
            if (root != null)
            {
                PrintInOrder(root.Left);
                Console.WriteLine($"ID: {root.Data.Id}, Name: {root.Data.Name}, NIC: {root.Data.NIC}, Contact: {root.Data.ContactNumber}");
                PrintInOrder(root.Right);
            }
        }

        public void PrintPreOrder()
        {
            PrintPreOrder(root);
            Console.WriteLine();
        }

        private void PrintPreOrder(TreeNode? root)
        {
            if (root != null)
            {
                Console.WriteLine($"ID: {root.Data.Id}, Name: {root.Data.Name}, NIC: {root.Data.NIC}, Contact: {root.Data.ContactNumber}");
                PrintPreOrder(root.Left);
                PrintPreOrder(root.Right);
            }
        }

        public void PrintPostOrder()
        {
            PrintPostOrder(root);
            Console.WriteLine();
        }

        private void PrintPostOrder(TreeNode? root)
        {
            if (root != null)
            {
                PrintPostOrder(root.Left);
                PrintPostOrder(root.Right);
                Console.WriteLine($"ID: {root.Data.Id}, Name: {root.Data.Name}, NIC: {root.Data.NIC}, Contact: {root.Data.ContactNumber}");
            }
        }
    }
}
