using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_csharp_practice
{
    internal class Arrays
    {
        public static void LargestElement()
        {
            int[] arr = { 4, 1, 3, 1, 5, 7, 0 };
            int largest = arr[0];
            int secondLargest = largest;
            for(int i=1; i<arr.Length; i++)
            {
                if (arr[i] > largest) {
                    secondLargest = largest;
                    largest = arr[i];
                } 

            }
            Console.WriteLine(largest + " " + secondLargest);
        }

    

        public static void RotateLeft()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6 };
            int k = 3;
            k = k % arr.Length;
            ReverseArray(arr, 0, arr.Length - 1);
            ReverseArray(arr, k, arr.Length-1);
            ReverseArray(arr, 0, k-1);
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }

        static void ReverseArray(int[]arr, int i, int j)
        {
      
            while (i < j)
            {
                int first = arr[i];
                arr[i] = arr[j];
                arr[j] = first;
                i++;
                j--;
            }
        }
        
    }
}
