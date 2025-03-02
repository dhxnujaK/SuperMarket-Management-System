using System;

namespace DSA_SuperMarket_Management_System
{
    public class DArray<T>
    {
        private T[] data;
        private int count;
        private int capacity;

        public DArray()
        {
            capacity = 4;
            data = new T[capacity];
            count = 0;
        }

        public void PrintData()
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.WriteLine();
        }

        public void Add(T item)
        {
            if (capacity == count)
            {
                ExpandArray();
            }

            data[count] = item;
            count++;
        }

        private void ExpandArray()
        {
            capacity *= 2;
            T[] newArray = new T[capacity];
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
                Shrink();
            }
        }

        private void Shrink()
        {
            capacity = capacity / 2;
            T[] newArray = new T[capacity];
            Array.Copy(data, newArray, count);
            data = newArray;
        }
    }
}
