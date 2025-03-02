using System;
using System.Collections.Generic;

namespace DSA_SuperMarket_Management_System
{
    public class SelectionSort
    {
        public void Sort<T, TKey>(List<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (keySelector(list[j]).CompareTo(keySelector(list[minIndex])) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    Swap(list, i, minIndex);
                }
            }
        }

        private void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
