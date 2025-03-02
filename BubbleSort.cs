using System;
using System.Collections.Generic;

namespace DSA_SuperMarket_Management_System
{
    public class BubbleSort
    {
        public void Sort<T, TKey>(List<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            int listLen = list.Count;
            bool swapped;

            for (int i = 0; i < listLen - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < listLen - i - 1; j++)
                {
                    if (keySelector(list[j]).CompareTo(keySelector(list[j + 1])) > 0)
                    {
                        // Swap the elements
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    break;
                }
            }
        }
    }
}
