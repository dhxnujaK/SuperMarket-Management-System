using System;
using System.Collections.Generic;

namespace DSA_SuperMarket_Management_System
{
    public class Node<T> // Generic Node class
    {
        public T Data { get; set; }
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
        public Node<T>? Head { get; private set; }
        public Node<T>? Tail { get; private set; }
        public int Count { get; private set; }

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
            }
            else
            {
                temp.Next = Head;
                Head.Previous = temp;
                Head = temp;
            }

            Count++;
        }

        // Add a new node at the end of the list
        public void AddLast(T val)
        {
            Node<T> temp = new Node<T>(val);

            if (Head == null)
            {
                Head = temp;
                Tail = temp;
            }
            else
            {
                Tail.Next = temp;
                temp.Previous = Tail;
                Tail = temp;
            }

            Count++;
        }

        // Add a node at a specific index
        public void AddAt(int index, T data)
        {
            if (index < 0 || index > Count)
            {
                Console.WriteLine("Invalid Index");
                return;
            }

            if (index == 0)
            {
                AddFront(data);
                return;
            }
            if (index == Count)
            {
                AddLast(data);
                return;
            }

            Node<T> newNode = new Node<T>(data);
            Node<T> currentNode = Head;

            for (int i = 0; i < index - 1; i++)
            {
                currentNode = currentNode.Next;
            }

            newNode.Next = currentNode.Next;
            if (currentNode.Next != null)
                currentNode.Next.Previous = newNode;

            currentNode.Next = newNode;
            newNode.Previous = currentNode;
            Count++;
        }

        // Remove a node at a specific index
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count || Head == null)
            {
                Console.WriteLine("Invalid Index");
                return;
            }

            if (index == 0)
            {
                Head = Head.Next;
                if (Head != null)
                    Head.Previous = null;
                else
                    Tail = null;  // List is now empty
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
                    nodeToDelete.Next.Previous = currentNode;
                else
                    Tail = currentNode;  // If last node was removed, update Tail
            }

            Count--;
        }

        // Search for a value in the list
        public Node<T>? SearchVal(T val)
        {
            Node<T>? currentNode = Head;
            while (currentNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Data, val))
                    return currentNode;
                currentNode = currentNode.Next;
            }
            return null;
        }


        // Find item in Linked List (returns the actual data instead of Node)
        public T? Find(Func<T, bool> predicate)
        {
            Node<T>? currentNode = Head;
            while (currentNode != null)
            {
                if (predicate(currentNode.Data))
                {
                    return currentNode.Data;
                }
                currentNode = currentNode.Next;
            }
            return default;
        }

        // Update an item by its unique property (assume T has an "ItemCode" property)
        public bool Update(string itemCode, T newItem)
        {
            Node<T>? currentNode = Head;
            while (currentNode != null)
            {
                dynamic item = currentNode.Data;  // Treat as dynamic to access properties
                if (item.ItemCode == itemCode)
                {
                    currentNode.Data = newItem; // Replace with new item
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }
        public bool Remove(T item)
        {
            Node<T>? currentNode = Head;
            while (currentNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Data, item))
                {
                    if (currentNode.Previous != null)
                    {
                        currentNode.Previous.Next = currentNode.Next;
                    }
                    else
                    {
                        Head = currentNode.Next; // Removing Head
                    }

                    if (currentNode.Next != null)
                    {
                        currentNode.Next.Previous = currentNode.Previous;
                    }
                    else
                    {
                        Tail = currentNode.Previous; // Removing Tail
                    }

                    Count--;
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        // Print all elements of the linked list
        public void Print()
        {
            Node<T>? current = Head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}
