using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class SelectionSort
    {
        public void GetSelectionSort(int[] array)
        {
            SelectionSortAlgo(array);
        }
        private void SelectionSortAlgo(int[] array)
        {


            for (int i = 0; i < array.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    int tmp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = tmp;
                }
            }
        }
    }
}
