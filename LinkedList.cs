using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class Node
    {
        public User Data { get; set; }
        public Node? Next { get; set; }

        public Node(User data)
        {
            Data = data;
            Next = null;
        }
    }

    public class sLinkedList
    {
        public Node? Head { get; set; }
        public Node? Tail { get; set; }
        public int Count { get; private set; }

        public sLinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void AddFront(User user)
        {
            Node temp = new Node(user);

            if (Head == null)
            {
                Head = temp;
                Tail = temp;
            }
            else
            {
                temp.Next = Head;
                Head = temp;
            }
            Count++;
        }

        public void AddLast(User user)
        {
            Node temp = new Node(user);

            if (Head == null)
            {
                Head = temp;
                Tail = temp;
            }
            else
            {
                Tail.Next = temp;
                Tail = temp;
            }
            Count++;
        }

        public void AddAt(int index, User user)
        {
            if (index < 0 || index > Count)
            {
                Console.WriteLine("Invalid Index");
                return;
            }

            if (index == 0)
            {
                AddFront(user);
            }
            else if (index == Count)
            {
                AddLast(user);
            }
            else
            {
                Node newNode = new Node(user);
                Node currentNode = Head;

                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;
                Count++;
            }
        }

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
                Count--;
                if (Head == null)
                {
                    Tail = null;
                }
            }
            else
            {
                Node currentNode = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }

                Node nodeToDelete = currentNode.Next;
                currentNode.Next = nodeToDelete.Next;
                Count--;

                if (index == Count)
                {
                    Tail = currentNode;
                }
            }
        }

        public User? SearchById(int userId)
        {
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Data.Id == userId)
                {
                    Console.WriteLine($"User Found: {currentNode.Data.Name} - {currentNode.Data.NIC} - {currentNode.Data.ContactNumber}");
                    return currentNode.Data;
                }
                currentNode = currentNode.Next;
            }
            Console.WriteLine("User not found.");
            return null;
        }

        public void PrintList()
        {
            Node current = Head;

            while (current != null)
            {
                Console.WriteLine($"ID: {current.Data.Id}, Name: {current.Data.Name}, NIC: {current.Data.NIC}, Contact: {current.Data.ContactNumber}");
                current = current.Next;
            }
        }
    }
}
