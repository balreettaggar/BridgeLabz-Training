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
    }
}
