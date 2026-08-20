using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace data_structures_algorithms
{
    internal class Performance
    {
        internal static void SearchingMethod()
        {
            int[] arr = new int[100000];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            int target = 99999;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target) sw.Stop();
            }

            Console.WriteLine("Linear Search time : " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();

            int left = 0, right = arr.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == target)
                {
                    sw.Stop();
                    return;
                }
                else if (arr[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            Console.WriteLine("Binary Search Time : " + sw.Elapsed.TotalMilliseconds);
        }

        internal static void SortingMethod()
        {
            int[] arr = new int[100000];
            int ptr = 999999;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ptr--;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i; j < arr.Length - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("Time for Bubble Sort : " + sw.Elapsed.TotalMilliseconds);

            ptr = 999999;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ptr--;
            }
            sw.Restart();

            int left = 0, right = arr.Length - 1;
            MergeSort(arr, left, right);
            sw.Stop();
            Console.WriteLine("Time for Merge Sort : " + sw.Elapsed.TotalMilliseconds);

            ptr = 999999;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ptr--;
            }
            sw.Restart();

            QuickSort(arr, 0, arr.Length - 1);
            sw.Stop();
            Console.WriteLine("Time for Quick Sort : " + sw.Elapsed.TotalMilliseconds);

        }

        internal static void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);

                Merge(arr, left, mid, right);
            }
        }

        internal static void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            Array.Copy(arr, left, leftArr, 0, n1);
            Array.Copy(arr, mid + 1, rightArr, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = leftArr[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = rightArr[j];
                j++;
                k++;
            }

        }

        internal static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right);
                QuickSort(arr, left, pivotIndex);
                QuickSort(arr, pivotIndex + 1, right);
            }
        }

        internal static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left + (right - left) / 2];
            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (arr[i] < pivot);

                do
                {
                    j--;
                } while (arr[j] > pivot);

                if (i >= j) return j;

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        internal static void StringPerf()
        {
            Stopwatch sw = new Stopwatch();
            string toAdd = "Hello";

            string res1 = "";
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                res1 += toAdd;
            }
            sw.Stop();
            Console.WriteLine("String Perfomance : " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append(toAdd);
            }
            sw.Stop();
            Console.WriteLine("StringBuilder Perfomance : " + sw.Elapsed.TotalMilliseconds);
        }

        internal static void CreateFile(string filename, int size)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                byte[] byteArr = new byte[1024 * 1024];
                for (int i = 0; i < size; i++)
                {
                    fs.Write(byteArr, 0, byteArr.Length);
                }
            }
        }

        internal static void FilePerf()
        {
            string filename = "largeFile.txt";
            CreateFile(filename, 500);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                while (fs.ReadByte() != -1) { }
            }
            sw.Stop();
            Console.WriteLine("Time for File Stream : " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();

            using (StreamReader sr = new StreamReader(filename))
            {
                while (sr.Read() != -1) { }
            }
            sw.Stop();
            Console.WriteLine("TIme for stream reader : " + sw.Elapsed.TotalMilliseconds);
        }

        internal static void FiboPerf()
        {

            int a = 0, b = 1;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int fib = 0;
            for (int i = 2; i <= 30; i++)
            {
                fib += b;
                a = b;
                b = fib;
            }
            sw.Stop();
            Console.WriteLine("Time for iteration : " + sw.Elapsed.TotalMilliseconds);

            sw.Restart();
            RecursiveFibo(30);
            sw.Stop();
            Console.WriteLine("Time for recursion : " + sw.Elapsed.TotalMilliseconds);


        }

        internal static int RecursiveFibo(int term)
        {
            if (term == 0 || term == 1)
            {
                return term;
            }
            return RecursiveFibo(term - 1) + RecursiveFibo(term - 2);
        }
    }
}
