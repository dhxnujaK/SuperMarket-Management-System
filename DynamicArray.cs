using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class DArray
    {
        private int[] data;
        private int count;
        private int capacity;

        public DArray()
        {
            capacity = 4;
            data = new int[capacity];
            count = 0;
        }

        public void printData()
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(data[i] + " ");

            }
            Console.WriteLine();
        }

        public void Add(int item)
        {
            if (capacity == count)
            {
                ExpandArray();
            }

            data[count] = item;
            count++;

        }

        public void ExpandArray()
        {
            capacity *= 2;
            int[] newArray = new int[capacity];
            Array.Copy(data, newArray, count);
            data = newArray;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Invalid index");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                data[i] = data[i + 1];
            }

            count--;

            // Check if shrinking is needed (when the array is 25% full)
            if (count > 0 && count <= capacity / 4)
            {
                shrink();
            }
        }

        public void shrink()
        {
            capacity = capacity / 2;
            int[] newArray = new int[capacity];
            Array.Copy(data, newArray, count);
            data = newArray;
        }
    }
}
