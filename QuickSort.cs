using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class QuickSort
    {
        public int LomutoPartition(int[] array, int Low, int High)
        {
            int pivot = High;
            int i = Low - 1;

            for (int j = Low; j < High; j++)
            {
                if (array[j] < array[pivot])
                {
                    i++;
                    swap(array, i, j);
                }
            }
            swap(array, i + 1, High);
            return i + 1;

        }

        public void swap(int[] array, int i, int j)
        {
            int tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }

        public void QuickSortAlgorithm(int[] array, int Low, int High)
        {
            if (Low < High)
            {
                int Pivot = LomutoPartition(array, Low, High);
                QuickSortAlgorithm(array, Low, Pivot - 1);
                QuickSortAlgorithm(array, Pivot + 1, High);
            }
        }
    }
}
