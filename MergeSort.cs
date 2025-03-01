using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class MergeSort
    {
        public void GetMergeSort(int[] array)
        {
            MergeSortAlgoritm(array);
        }
        private void MergeSortAlgoritm(int[] array)
        {
            if (array.Length <= 1)
            {
                return;
            }

            int arraySize = array.Length;
            int mid = arraySize / 2;

            int[] LeftSubArray = new int[mid];
            int[] RightSubArray = new int[arraySize - mid];

            copyArray(array, LeftSubArray, 0, mid);
            copyArray(array, RightSubArray, mid, arraySize);

            MergeSortAlgoritm(LeftSubArray);
            MergeSortAlgoritm(RightSubArray);

            Merge(LeftSubArray, RightSubArray, array);


        }

        private void Merge(int[] leftSubArray, int[] rightSubArray, int[] array)
        {
            int LeftArraySize = leftSubArray.Length;
            int RightArraySize = rightSubArray.Length;

            int i = 0, j = 0, k = 0; // i = merged array j=leftarray k=rightarray

            while (j < LeftArraySize && k < RightArraySize)
            {
                if (leftSubArray[j] <= rightSubArray[k])
                {
                    array[i] = leftSubArray[j];
                    i++;
                    j++;
                }
                else if (leftSubArray[j] > rightSubArray[k])
                {
                    array[i] = rightSubArray[k];
                    i++;
                    k++;
                }
            }

            while (j < LeftArraySize)
            {
                array[i] = leftSubArray[j];
                i++;
                j++;
            }

            // Copy remaining elements from the right array (if any)
            while (k < RightArraySize)
            {
                array[i] = rightSubArray[k];
                i++;
                k++;
            }
        }

        private void copyArray(int[] array, int[] newarray, int startingIndex, int endingIndex)
        {
            int j = 0;
            for (int i = startingIndex; i < endingIndex; i++)
            {
                newarray[j] = array[i];
                j++;
            }
        }
    }
}
