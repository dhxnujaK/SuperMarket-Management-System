using System;

namespace DSA_SuperMarket_Management_System
{
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Data;
        public TreeNode<T>? left;
        public TreeNode<T>? right;

        public TreeNode(T data)
        {
            Data = data;
            left = null;
            right = null;
        }
    }

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private TreeNode<T>? root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void InsertKey(T data)
        {
            root = InsertRecursively(data, root);
        }

        public void Delete(T data)
        {
            root = DeleteRecursively(root, data);
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

        // Recursively insert the node into the tree
        private TreeNode<T> InsertRecursively(T data, TreeNode<T>? node)
        {
            if (node == null)
            {
                node = new TreeNode<T>(data);
                return node;
            }

            if (data.CompareTo(node.Data) < 0)
            {
                node.left = InsertRecursively(data, node.left);
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                node.right = InsertRecursively(data, node.right);
            }

            return node;
        }

        // Recursively delete the node from the tree
        private TreeNode<T>? DeleteRecursively(TreeNode<T>? node, T data)
        {
            if (node == null)
            {
                return node;
            }

            if (data.CompareTo(node.Data) < 0)
            {
                node.left = DeleteRecursively(node.left, data);
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                node.right = DeleteRecursively(node.right, data);
            }
            else
            {
                // Node to delete found

                // Case 1: Node has no children
                if (node.left == null && node.right == null)
                {
                    return null;
                }
                // Case 2: Node has only one child
                else if (node.left == null)
                {
                    return node.right;
                }
                else if (node.right == null)
                {
                    return node.left;
                }
                // Case 3: Node has two children
                else
                {
                    // Find the minimum value in the right subtree
                    node.Data = FindMinRecursively(node.right);
                    // Delete the minimum node
                    node.right = DeleteRecursively(node.right, node.Data);
                }
            }

            return node;
        }

        // Find the minimum value in a subtree
        private T FindMinRecursively(TreeNode<T>? node)
        {
            if (node.left == null)
            {
                return node.Data;
            }
            return FindMinRecursively(node.left);
        }
        public TreeNode<T>? Search(string itemCode)
        {
            return SearchRecursively(root, itemCode);
        }

        private TreeNode<T>? SearchRecursively(TreeNode<T>? node, string itemCode)
        {
            if (node == null) return null;

            if (node.Data is Item item)
            {
                int comparison = string.Compare(item.ItemCode, itemCode, StringComparison.OrdinalIgnoreCase);
                if (comparison == 0)
                    return node;
                else if (comparison > 0)
                    return SearchRecursively(node.left, itemCode);
                else
                    return SearchRecursively(node.right, itemCode);
            }

            return null; // Return null if T is not an Item type
        }

        

        private void PrintInOrder(TreeNode<T>? node)
        {
            if (node != null)
            {
                PrintInOrder(node.left);
                Console.WriteLine(node.Data);  // Print as a new line instead of space
                PrintInOrder(node.right);
            }
        }


        // Pre-order traversal
        private void PrintPreOrder(TreeNode<T>? node)
        {
            if (node != null)
            {
                Console.Write(node.Data + " ");
                PrintPreOrder(node.left);
                PrintPreOrder(node.right);
            }
        }

        // Post-order traversal
        private void PrintPostOrder(TreeNode<T>? node)
        {
            if (node != null)
            {
                PrintPostOrder(node.left);
                PrintPostOrder(node.right);
                Console.Write(node.Data + " ");
            }
        }
    }
}
