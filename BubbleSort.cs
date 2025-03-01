using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class BubbleSort
    {
        public void BubbleSortAlgo(int[] array)
        {
            int arrayLen = array.Length;
            bool Swapped = false;
            int tmp;

            for (int i = 0; i < arrayLen - 1; i++)
            {
                Swapped = false;
                for (int j = 0; j < arrayLen - i - 1; j++)
                {

                    if (array[j] > array[j + 1])
                    {
                        tmp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                        Swapped = true;
                    }
                }
                if (!Swapped)
                {
                    break;
                }
            }
        }
    }
}
