using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace core_csharp_practice
{
    internal class PracticeProblems
    {
        // profit and loss
        public static void ProfitandLoss()
        {
            int costPrice = 129;
            int sellingPrice = 191;
            int profit = sellingPrice - costPrice;
            double profitPercentage = profit / (double)costPrice * 100;

            Console.WriteLine($"The cost price is {costPrice:C} and the selling price is {sellingPrice:C}");
            Console.WriteLine($"The profit percentage is {profitPercentage:F3}%");
        }

        public static void DiscountFee()
        {
            int fees = Convert.ToInt32(Console.ReadLine());
            int discountPercent = 10;
            double discountFees = (fees - fees * discountPercent/100.0);
            Console.WriteLine($"The original fees was {fees} and discountFees is {discountFees}");
        }

        public static void HeightConverted()
        {
            int heightInCm = Convert.ToInt32(Console.ReadLine());
            double inches = heightInCm / 2.54;
            int feet = (int) inches / 12;
            double remainingInches = (double) inches % 12;

            Console.WriteLine($"Height in cm is {heightInCm} and in feet is {feet}ft{remainingInches:F1}in and in inches is {inches:F2}");
        }

        public static void Calculator()
        {
            double num1 = Convert.ToDouble(Console.ReadLine());
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"addition is {num1 + num2}, subtraction is {num1 - num2}, multiplication is {num1 * num2}, and division is {num1 / num2}");


        }

        public static void temperatureConversion()
        {
            int Cal = Convert.ToInt32(Console.ReadLine());
            double Far = (Cal * 9 / 5) + 32;
            Console.WriteLine($"temp in deg c is {Cal} and in deg f is {Far}");
        }

        public static void SumUntilZero()
        {
            int input = Convert.ToInt32(Console.ReadLine());
            int sum = 0;
            while (input > 0)
            {
                sum += input;
                input = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine(sum);
        }

        public static void Factors()
        {
            int num = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[10];
            int ptr = 0;
            for(int i=1; i<=num/2; i++)
            {
                if (ptr >= arr.Length) break;
                if (num % i == 0) arr[ptr++] = i;
            }
            for(int i=0; i<arr.Length; i++)
            {
                if(arr[i]!=0) Console.WriteLine(arr[i]);
            }
        }
    }
}
