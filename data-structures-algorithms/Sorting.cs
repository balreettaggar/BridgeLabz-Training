using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures_algorithms
{
    static internal class Sorting
    {
        static internal void BubbleSort()
        {
            int[] arr = { 5, 2, 9, 1, 19, 13, 14, 23, 10, 21 };
            for(int i=0; i<arr.Length; i++)
            {
                for(int j=i+1; j<arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            foreach(int num in arr)
            {
                Console.Write(num + " ");
            }
        }

        internal static void SelectionSort()
        {
            int[] arr = { 5, 2, 9, 1, 19, 13, 14, 23, 10, 21, 43, 32 };
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;
                for (int j = i; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                int temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;

            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]+" ");
            }
        }

        internal static void InsertionSort()
        {

        }
    }
}
