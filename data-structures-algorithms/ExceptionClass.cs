using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures_algorithms
{

    internal class InvalidAge : Exception
    {
        internal InvalidAge(string message) : base(message) { }
    }
    internal class ExceptionClass
    {
        internal static void FileMethod()
        {
            try
            {
                string content = File.ReadAllText("data.txt");
                Console.WriteLine(content);
            }

            catch(FileNotFoundException ex)
            {
                Console.WriteLine("File not found" + ex.Message);
            }
        }

        internal static void ValidMaths()
        {
            try
            {
                int a = Convert.ToInt32(Console.ReadLine());
                int b = Convert.ToInt32(Console.ReadLine());
                int c = 0;

                Console.WriteLine(a+b);
                Console.WriteLine(a/c);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void AgeValiditity(int age)
        {
            if (age < 18) throw new InvalidAge("Enter age more than 18");
            else Console.WriteLine("passed");
        }

        internal static void checkAge()
        {
            try
            {
                int age = Convert.ToInt32(Console.ReadLine());
                AgeValiditity(age);
            }
            catch(InvalidAge ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void ArrExc()
        {
            try
            {
                int[] arr = new int[10];
                int index = Convert.ToInt32(Console.ReadLine());
                string[] str = null;

                Console.WriteLine(arr[index]);
                Console.WriteLine(str[index]);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void StreamExc()
        {
            try
            {
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    string? line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " ");
            }          
        }

        internal static void InterestExc()
        {
            try
            {
                int principle = Convert.ToInt32(Console.ReadLine());
                int rate = Convert.ToInt32(Console.ReadLine());
                int time = Convert.ToInt32(Console.ReadLine());

                if (rate < 0 || principle<0) throw new Exception("Rate can't be negative");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
