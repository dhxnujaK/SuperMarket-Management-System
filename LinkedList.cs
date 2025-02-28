using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class Node
    {
        public int Data { get; set; }
        public Node? Next { get; set; }
        public Node? previous { get; set; }

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    public class sLinkedList
    {
        public Node? Head { get; set; }
        public Node? Tail { get; set; }
        public int count { get; set; }

        public sLinkedList()
        {
            Head = null;
            Tail = null;
            count = 0;

        }

        public void AddFront(int val)
        {
            Node temp = new Node(val);

            if (Head == null)
            {
                Head = temp;
                Tail = temp;
                count++;
            }

            else
            {
                temp.Next = Head;
                Head = temp;
                count++;
            }
        }

        public void addLast(int val)
        {
            Node temp = new Node(val);

            if (Head == null) 
            {
                Head = temp;
                Tail = temp;
                count++;
            }
            else
            {
                Tail.Next = temp; 
                Tail = temp; 
                count++;
            }
        }

        public void addAt(int index, int Data)
        {
            Node newnode = new Node(Data);

            if (index < 0 || index > count)
            {
                Console.WriteLine("Invalid Index");
                return;
            }
            if (index == 0)
            {
                AddFront(Data);

            }
            else if (index == count)
            {
                addLast(Data);

            }
            else
            {
                Node currentNode = Head;

                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;

                }


                newnode.Next = currentNode.Next;
                currentNode.Next = newnode;
                count++;
            }
        }
        public void Removeat(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Invalid Index");
                return;
            }

            if (index == 0)
            {
                Head = Head.Next;
                count--;
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

                Node NodetoDelete = currentNode.Next;
                currentNode.Next = NodetoDelete.Next;
                count--;

                if (index == count)
                {
                    Tail = currentNode;
                }


            }
        }

        public Node searchVal(int val)
        {
            Node currentNode = Head;
            int index = 0;
            while (currentNode != null)
            {
                if (currentNode.Data == val)
                {
                    Console.WriteLine("Value Found at index: " + index);
                    Node nodecopy = new Node(currentNode.Data);
                    return nodecopy;
                }
                currentNode = currentNode.Next;
                index++;

            }
            Console.WriteLine("Value not found.");
            return null;
        }
        public void print()
        {
            Node current = Head;

            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;

            }
        }
    }

}
