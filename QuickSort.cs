using System;
using System.Collections.Generic;

namespace DSA_SuperMarket_Management_System
{
    public class QuickSort
    {
        public void Sort<T, TKey>(List<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            QuickSortAlgorithm(list, 0, list.Count - 1, keySelector);
        }

        private void QuickSortAlgorithm<T, TKey>(List<T> list, int low, int high, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (low < high)
            {
                int pivotIndex = LomutoPartition(list, low, high, keySelector);
                QuickSortAlgorithm(list, low, pivotIndex - 1, keySelector);
                QuickSortAlgorithm(list, pivotIndex + 1, high, keySelector);
            }
        }

        private int LomutoPartition<T, TKey>(List<T> list, int low, int high, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            T pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (keySelector(list[j]).CompareTo(keySelector(pivot)) < 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }
            Swap(list, i + 1, high);
            return i + 1;
        }

        private void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
