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

        public static void Multiply()
        {
            int num = 5;
            int[] arr = new int[10];
            for(int i=1; i<=10; i++)
            {
                arr[i-1] = num * i;
                Console.WriteLine($"{num} * {i} = {arr[i-1]}");
            }
        }
        
        public static void UntilZero()
        {
            int[] arr = new int[10];
            int ptr = 0;
            int sum = 0;
            while (true)
            {
                arr[ptr] = Convert.ToInt32(Console.ReadLine());
                if (ptr >= arr.Length || arr[ptr] == 0) break;
                else
                {
                    Console.WriteLine($"Value of present element is {arr[ptr]}");
                    sum += arr[ptr];
                }
                ptr++;
            }
            Console.WriteLine(sum);
        }

        public static void FootBallHeight()
        {
            double[] arr = new double[11];
            double sum = 0.0;
            for (int i = 0; i < 11; i++)
            {
                arr[i] = Convert.ToDouble(Console.ReadLine());
                sum += arr[i];
            }
            Console.WriteLine($"The mean of height of players is {sum / 11}");
        }

        public static void Factors()
        {
            int num = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[10];
            int ptr = 0;
            for (int i = 1; i <= num / 2; i++)
            {
                if (ptr >= arr.Length) break;
                if (num % i == 0) arr[ptr++] = i;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0) Console.WriteLine(arr[i]);
            }
        }

        public static void Copy2D()
        {
            int[,] arr = new int[3, 2];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    arr[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            int[] singleArr = new int[3 * 2];
            int ptr = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    singleArr[ptr] = arr[i, j];
                    Console.Write(singleArr[ptr] + " ");
                    ptr++;
                }
            }    
        }

        public static void FizzandBuzz()
        {
            int num = Convert.ToInt32(Console.ReadLine());
            string[] arr = new string[10];
            int ptr = 0;
            int j = 2;
            for (int i = num; ptr < 10; i += num)
            {
                if (i % 3 == 0 && i % 5 == 0) arr[ptr] = $"The value of {i} is FizzAndBuzz";
                else if (i % 3==0) arr[ptr] = $"The value of {i} is Fizz";
                else if (i % 5 == 0) arr[ptr] = $"The value of {i} is Buzz";
                Console.WriteLine(arr[ptr]);
                ptr++;
                j++;
            }
        }

        public static void Employees()
        {
            double[,] salaryAndYears = new double[10,2];
            double[,] salaryAndBonus = new double[10, 2];
            for(int i=0; i<10; i++)
            {
                salaryAndYears[i,0] = Convert.ToDouble(Console.ReadLine());
                while (salaryAndYears[i, 0] < 10000)
                {
                    Console.WriteLine("Enter Valid Salary");
                    salaryAndYears[i, 0] = Convert.ToDouble(Console.ReadLine());
                }
                salaryAndYears[i,1] = Convert.ToDouble(Console.ReadLine());
                while (salaryAndYears[i, 1] < 0)
                {
                    Console.WriteLine("Enter Valid Experience");
                    salaryAndYears[i, 1] = Convert.ToDouble(Console.ReadLine());
                }
                double bonus = 0;
                if (salaryAndYears[i, 1] > 5) bonus = salaryAndYears[i, 0] * 5 / 100.0;
                else bonus = salaryAndYears[i, 0] * 2 / 100.0;
                salaryAndBonus[i, 0] = salaryAndYears[i, 0] + bonus;
                salaryAndBonus[i, 1] = bonus;

                Console.WriteLine($"The old salary of an employee was {salaryAndYears[i, 0]} with {salaryAndYears[i,1]} " +
                    $"years of experience has been awarded with {salaryAndBonus[i,0]} with a bonus of {salaryAndBonus[i,1]}");

            }
        }
    }
}
