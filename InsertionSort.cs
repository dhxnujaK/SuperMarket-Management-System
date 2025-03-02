using System;
using System.Collections.Generic;

namespace DSA_SuperMarket_Management_System
{
    public class InsertionSort
    {
        public void Sort<T, TKey>(List<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            int n = list.Count;

            for (int i = 1; i < n; i++)
            {
                T key = list[i];
                int j = i - 1;

                while (j >= 0 && keySelector(list[j]).CompareTo(keySelector(key)) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = key;
            }
        }
    }
}
