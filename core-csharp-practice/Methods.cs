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
            if (delta > 0)
            {
                root1 = (-y + delta) / 2 * x;
                root2 = (-y - delta) / 2 * x;
            }
            else if (delta == 0) {
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

            for (i = 0; i < arr.Length; i++)
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
            for (int i = 0; i < arr.Length; i++)
            {
                arms += Math.Pow(arr[i], arr.Length - 1);
            }
            if (original == (int)arms) Console.WriteLine("This number is armstrong");
            else Console.WriteLine("Not an armstrong");

        }

        public static void OTPgenerator()
        {
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Random.Shared.Next(100000, 1000000);
                Console.WriteLine(arr[i]);
            }
            for (int i = 0; i < arr.Length; i++)
            {

                bool exists = Array.Exists(arr, element => element == arr[i]);
                if (exists) Console.WriteLine("Not random");
            }
        }

        public static void Calender()
        {
            int year = Convert.ToInt32(Console.ReadLine());
            int month = Convert.ToInt32(Console.ReadLine());
            string[] months = {"January", "february", "March", "April", "May", "June", "July", "August", "September", "October",
                "November", "December"};
            string actualMonth = months[month - 1];
            bool leap = false;
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) leap = true;
            int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            string[] week = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (leap == true) days[1] = 29;

            int day = 1;
            int y0 = year - (14 - month) / 12;
            int x = y0 + y0 / 4 - y0 / 100 + y0 / 400;
            int m0 = month + 12 * ((14 - month) / 12) - 2;
            int d0 = (day + x + (31 * m0) / 12) % 7;

            Console.WriteLine($"The {actualMonth} month of year {year} started on {week[d0 - 1]}");

            Console.WriteLine($"\n\t{actualMonth} {year}");
            Console.WriteLine("Sun Mon Tue Wed Thu Fri Sat Sun");

            for(int i=0; i<d0; i++)
            {
                Console.Write("    ");
            }
            int currentDay = d0;
            for(int i=1; i <= days[month-1]; i++)
            {
                Console.Write($"{i,4}");
                currentDay++;
                if(currentDay==7)
                {
                    Console.WriteLine();
                    currentDay = 0;
                }
            }
        }

        public static void EuclideanDistance()
        {
            int x1 = Convert.ToInt32(Console.ReadLine());
            int y1 = Convert.ToInt32(Console.ReadLine());
            int[] point1 = { x1, y1 };
            int x2 = Convert.ToInt32(Console.ReadLine());
            int y2 = Convert.ToInt32(Console.ReadLine());
            int[] point2 = { x2, y2 };

            double distance = Math.Pow(point2[0] - point1[0], 2) + Math.Pow(point2[1] - point1[1], 2);

            double m = (double) (y2 - y1) / (x2 - x1);
            double b = y1 - m * x1;

            Console.WriteLine($"The equation of line is y = {m}x+{b}");
        }

        public static void CollinearPoints()
        {
            int x1 = Convert.ToInt32(Console.ReadLine());
            int y1 = Convert.ToInt32(Console.ReadLine());
            int x2 = Convert.ToInt32(Console.ReadLine());
            int y2 = Convert.ToInt32(Console.ReadLine());
            int x3 = Convert.ToInt32(Console.ReadLine());
            int y3 = Convert.ToInt32(Console.ReadLine());

            double slope1 = (double)(y2 - y1) / (x2 - x1);
            double slope2 = (double)(y3 - y2) / (x3 - x2);
            double slope3 = (double)(y3 - y1) / (x3 - x1);
            if (slope1 == slope2 && slope2 == slope3) Console.WriteLine("Collinear Points");
            else Console.WriteLine("Non-collinear points");
            
        }
        public static void Matrix()
        {
            Random random = new Random();
            int[,] matrix1 = new int[3, 3];
            int[,] matrix2 = new int[3, 3];
            for(int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    matrix1[i,j] = random.Next(1, 10);
                    matrix2[i,j] = random.Next(1, 10);
                }
            }

            int[,] add = new int[3, 3];
            int[,] sub = new int[3, 3];
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    add[i, j] = matrix1[i, j] + matrix2[i,j];
                    sub[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            int[,] mul = new int[3, 3];
            for(int i=0; i<3; i++)
            {
                for(int j=0;j<3; j++)
                {
                    for(int k=0; k<3; k++)
                    {
                        mul[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            int[,] transpose = new int[3, 3];
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    transpose[i, j] = matrix1[j, i];
                }
            }
        }
    }
}
