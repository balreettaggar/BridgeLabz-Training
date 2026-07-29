using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_csharp_practice
{
    internal class PracticeAssignment
    {
        private static Random random = new Random();
        public static void Guess()
        {
            int num = random.Next(1, 100);
            int comp = random.Next(1, 100);
            Console.WriteLine($"{num} and {comp}");
            if (num > comp) Console.WriteLine("The number is greater than computer's guess");
            else if (num < comp) Console.Write("The number is lesser than computer's guess");
            else Console.WriteLine("Both are same");
        }

        public static void maxNo()
        {
            int num1 = Convert.ToInt32(Console.ReadLine());
            int num2 = Convert.ToInt32(Console.ReadLine());
            int num3 = Convert.ToInt32(Console.ReadLine());

            int greatest = num1;
            if (num2 >= num1 && num2 >= num3) greatest = num2;
            else if (num3 >= num1 && num3 >= num2) greatest = num3;
            Console.Write(greatest);
        }

        public static void prime()
        {
            int num1 = Convert.ToInt32(Console.ReadLine());
            for (int i = 2; i * i <= num1; i++)
            {
                if (num1 % i == 0)
                {
                    Console.WriteLine($"{num1} is not a prime");
                    return;
                }
            }
            Console.WriteLine($"{num1} is a prime number");
        }

        public static void Fibonacci()
        {
            int term = Convert.ToInt32(Console.ReadLine());
            int a = 0, b = 1, c = 3;
            Console.Write(a + " " + b + " ");
            while (c <= term)
            {
                int temp = b;
                b = b + a;
                a = temp;
                Console.Write(b + " ");
                c++;
            }
        }

        public static void GCDandLCM()
        {
            int num1 = Convert.ToInt32(Console.ReadLine());
            int num2 = Convert.ToInt32(Console.ReadLine());
            int gcd = 0, lcm = 0;
            int originalNum1 = num1, originalNum2 = num2;
            while (num2 != 0)
            {
                int temp = num2;
                num2 = num1 % num2;
                num1 = temp;
            }
            gcd = num1;
            lcm = (originalNum1 * originalNum2) / gcd;

            Console.Write($"Two numbers are {originalNum1} and {originalNum2}, their gcd is {gcd} and lcm is {lcm}");
        }

        public static void Calculator()
        {
            int num1 = Convert.ToInt32(Console.ReadLine());
            int num2 = Convert.ToInt32(Console.ReadLine());
            string? opp = Console.ReadLine();
            if (opp == null) return;
            if (opp == "+") Console.WriteLine($"{num1} {opp} {num2} = {num1 + num2}");
            else if (opp == "-") Console.WriteLine($"{num1} {opp} {num2} = {num1 - num2}");
            else if (opp == "*") Console.WriteLine($"{num1} {opp} {num2} = {num1 * num2}");
            else if (opp == "/") Console.WriteLine($"{num1} {opp} {num2} = {num1 / num2}");
        }

        public static void DateAndTime()
        {
            DateTimeOffset current = DateTimeOffset.Now;
            Console.WriteLine(current.ToString());

        }

    }
}
