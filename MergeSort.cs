using System;
using System.Collections.Generic;

namespace DSA_SuperMarket_Management_System
{
    public class MergeSort
    {
        public void Sort<T, TKey>(List<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (list.Count <= 1)
                return;

            List<T> sortedList = MergeSortAlgorithm(list, keySelector);
            list.Clear();
            list.AddRange(sortedList);
        }

        private List<T> MergeSortAlgorithm<T, TKey>(List<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (list.Count <= 1)
                return list;

            int mid = list.Count / 2;

            List<T> leftList = list.GetRange(0, mid);
            List<T> rightList = list.GetRange(mid, list.Count - mid);

            leftList = MergeSortAlgorithm(leftList, keySelector);
            rightList = MergeSortAlgorithm(rightList, keySelector);

            return Merge(leftList, rightList, keySelector);
        }

        private List<T> Merge<T, TKey>(List<T> leftList, List<T> rightList, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            List<T> mergedList = new List<T>();
            int i = 0, j = 0;

            while (i < leftList.Count && j < rightList.Count)
            {
                if (keySelector(leftList[i]).CompareTo(keySelector(rightList[j])) <= 0)
                {
                    mergedList.Add(leftList[i]);
                    i++;
                }
                else
                {
                    mergedList.Add(rightList[j]);
                    j++;
                }
            }

            while (i < leftList.Count)
            {
                mergedList.Add(leftList[i]);
                i++;
            }

            while (j < rightList.Count)
            {
                mergedList.Add(rightList[j]);
                j++;
            }

            return mergedList;
        }
    }
}
