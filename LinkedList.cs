using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class Node<T> // Generic Node class
    {
        public T Data { get; set; }  // Change data type to T
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }

    public class sLinkedList<T> // Generic Linked List class
    {
        public Node<T>? Head { get; set; }
        public Node<T>? Tail { get; set; }
        public int Count { get; set; }

        public sLinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        // Add a new node at the front of the list
        public void AddFront(T val)
        {
            Node<T> temp = new Node<T>(val);

            if (Head == null)
            {
                Head = temp;
                Tail = temp;
                Count++;
            }
            else
            {
                temp.Next = Head;
                Head.Previous = temp; // Set previous link
                Head = temp;
                Count++;
            }
        }

        // Add a new node at the end of the list
        public void AddLast(T val) // Method name should start with uppercase "AddLast"
        {
            Node<T> temp = new Node<T>(val);

            if (Head == null)
            {
                Head = temp;
                Tail = temp;
                Count++;
            }
            else
            {
                Tail.Next = temp;
                temp.Previous = Tail; // Set previous link
                Tail = temp;
                Count++;
            }
        }

        // Add a node at a specific index
        public void AddAt(int index, T data)
        {
            if (index < 0 || index > Count)
            {
                Console.WriteLine("Invalid Index");
                return;
            }

            Node<T> newNode = new Node<T>(data);

            if (index == 0)
            {
                AddFront(data);
            }
            else if (index == Count)
            {
                AddLast(data);
            }
            else
            {
                Node<T> currentNode = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }

                newNode.Next = currentNode.Next;
                if (currentNode.Next != null)
                    currentNode.Next.Previous = newNode; // Set previous of next node
                currentNode.Next = newNode;
                newNode.Previous = currentNode; // Set previous of new node

                Count++;
            }
        }

        // Remove a node at a specific index
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                Console.WriteLine("Invalid Index");
                return;
            }

            if (index == 0)
            {
                Head = Head.Next;
                if (Head != null)
                    Head.Previous = null; // If there is a head, update its previous pointer
                Count--;
                if (Head == null)
                {
                    Tail = null;
                }
            }
            else
            {
                Node<T> currentNode = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }

                Node<T> nodeToDelete = currentNode.Next;
                currentNode.Next = nodeToDelete.Next;
                if (nodeToDelete.Next != null)
                    nodeToDelete.Next.Previous = currentNode; // Update next node's previous pointer

                Count--;
                if (currentNode.Next == null)
                {
                    Tail = currentNode; // If removed node is the last, update the tail
                }
            }
        }

        // Search for a value in the list
        public Node<T>? SearchVal(T val)
        {
            Node<T>? currentNode = Head;
            int index = 0;

            while (currentNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Data, val))
                {
                    Console.WriteLine("Value Found at index: " + index);
                    return currentNode;
                }
                currentNode = currentNode.Next;
                index++;
            }

            Console.WriteLine("Value not found.");
            return null;
        }

        // Print all elements of the linked list
        public void Print()
        {
            Node<T> current = Head;

            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}
