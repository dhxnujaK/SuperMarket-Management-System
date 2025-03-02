using System;
using System.Linq;

namespace DSA_SuperMarket_Management_System
{
    public class DArray<T>
    {
        private T[] data;
        private int count;
        private int capacity;

        public int Count => count;
        public int Capacity => capacity;

        public DArray()
        {
            capacity = 4;
            data = new T[capacity];
            count = 0;
        }

        // Print all items in the array
        public void PrintData()
        {
            if (count == 0)
            {
                Console.WriteLine("Array is empty.");
                return;
            }
            Console.WriteLine("Array Elements: " + string.Join(", ", data.Take(count)));
        }

        // Add a new item
        public void Add(T item)
        {
            if (capacity == count)
            {
                ExpandArray();
            }

            data[count] = item;
            count++;
        }

        // Expand array when full
        private void ExpandArray()
        {
            capacity *= 2;
            T[] newArray = new T[capacity];
            Array.Copy(data, newArray, count);
            data = newArray;
        }

        // Remove an item at a specific index
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

        // Shrink the array to optimize memory usage
        private void Shrink()
        {
            capacity = Math.Max(4, capacity / 2); // Prevent capacity from going below 4
            T[] newArray = new T[capacity];
            Array.Copy(data, newArray, count);
            data = newArray;
        }

        // Get item at a specific index
        public T GetAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Invalid index");
            }
            return data[index];
        }

        // Find an item (returns index, -1 if not found)
        // Find the index of an item based on a condition (predicate)
        public int Find(Func<T, bool> predicate)
        {
            for (int i = 0; i < count; i++)
            {
                if (predicate(data[i]))
                {
                    return i;  // ✅ Return the index if found
                }
            }
            return -1;  
        }

       

        // Update an existing item at a given index
        public bool Update(int index, T newItem)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Invalid index");
                return false;
            }
            data[index] = newItem;
            return true;
        }
        public T? SearchItem(Func<T, bool> predicate)
        {
            for (int i = 0; i < count; i++)
            {
                if (predicate(data[i]))
                {
                    return data[i];
                }
            }
            return default;
        }
    }
}
