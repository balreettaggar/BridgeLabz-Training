using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_csharp_practice
{
    internal class Methods
    {
        public static void Factors()
        {
            int[] arr = new int[10];
            int num = Convert.ToInt32(Console.ReadLine());
            int ptr = 0;
            int sum = 0;
            int prod = 1;
            double sq = 0;
            for (int i = 1; i <= num / 2 && ptr < arr.Length; i++)
            {
                if (num % i == 0)
                {
                    arr[ptr++] = i;
                    sum += i;
                    prod *= i;
                    sq += Math.Pow(i, 2);
                }
            }
            Console.WriteLine($"The sum of factors is {sum} and product of all factors is {prod}, also the " +
                $"sum of squares of its factors is {sq}");
        }

        public static int SumOfNaturals(int num)
        {
            if (num == 1) return 1;
            return num + SumOfNaturals(num - 1);
        }

        public static void Leap()
        {
            int year = Convert.ToInt32(Console.ReadLine());
            while (year <= 1572)
            {
                Console.WriteLine("Enter a valid year");
                year = Convert.ToInt32(Console.ReadLine());
            }
            if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            {
                Console.WriteLine($"The year {year} is a leap year");
            }
            else Console.WriteLine("The year is not leap year");
        }

        public static void Quadratic()
        {
            int x = Convert.ToInt32(Console.ReadLine());
            int y = Convert.ToInt32(Console.ReadLine());
            int z = Convert.ToInt32(Console.ReadLine());

            double root1 = 0, root2 = 0;
            double delta = Math.Pow(y, 2) - 4 * x * z;
            if(delta >0)
            {
                root1 = (-y + delta) / 2*x;
                root2 = (-y - delta )/ 2*x;
            }
            else if (delta==0) {
                root1 = root2 = -y / 2 * x;
            } else
            {
                Console.WriteLine("No real roots");
                return;
            }
            Console.WriteLine($"THe roots of this quadratic equation are {root1} and {root2}");
        }

        public static void AvgMinMax()
        {
            int[] arr = new int[4];
            int i = 0;
            while (i < arr.Length)
            {
                arr[i++] = Convert.ToInt32(Console.ReadLine());
            }

            int max = int.MinValue;
            int min = int.MaxValue;
            int avg = 0;

            for(i=0; i<arr.Length; i++)
            {
                avg += arr[i];
                max = Math.Max(max, arr[i]);
                min = Math.Min(min, arr[i]);
                
            }

            Console.WriteLine($"The value of avg is {avg / 4} and min is {min} and max is {max}");
        }

        public static void NumberChecker()
        {
            int num = Convert.ToInt32(Console.ReadLine());
            int original = num;
            int[] arr = new int[4];
            int ptr = 0;
            int count = 0;
            while (num > 0)
            {
                arr[ptr++] = num % 10;
                count++;
                num /= 10;
            }
            double arms = 0;
            for(int i=0; i<arr.Length; i++)
            {
                arms += Math.Pow(arr[i], arr.Length-1);
            }
            if (original == (int)arms) Console.WriteLine("This number is armstrong");
            else Console.WriteLine("Not an armstrong");
            
        }
    }
}
