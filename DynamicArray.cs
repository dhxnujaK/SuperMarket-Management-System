using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class DArray
    {
        private User[] data;
        private int count;
        private int capacity;

        public DArray()
        {
            capacity = 4;
            data = new User[capacity];
            count = 0;
        }

        public void PrintData()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"ID: {data[i].Id}, Name: {data[i].Name}, NIC: {data[i].NIC}, Contact: {data[i].ContactNumber}");
            }
            Console.WriteLine();
        }

        public void Add(User user)
        {
            if (capacity == count)
            {
                ExpandArray();
            }

            data[count] = user;
            count++;
        }

        private void ExpandArray()
        {
            capacity *= 2;
            User[] newArray = new User[capacity];
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

            if (count > 0 && count <= capacity / 4)
            {
                Shrink();
            }
        }

        private void Shrink()
        {
            capacity /= 2;
            User[] newArray = new User[capacity];
            Array.Copy(data, newArray, count);
            data = newArray;
        }

        public User? SearchById(int userId)
        {
            for (int i = 0; i < count; i++)
            {
                if (data[i].Id == userId)
                {
                    Console.WriteLine($"User Found: {data[i].Name} - {data[i].NIC} - {data[i].ContactNumber}");
                    return data[i];
                }
            }
            Console.WriteLine("User not found.");
            return null;
        }
    }
}
