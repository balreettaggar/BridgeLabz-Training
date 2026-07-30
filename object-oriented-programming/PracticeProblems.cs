using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{
    public class PracticeProblems
    {
        public class AreaOfCircle
        {
            private int radius;
            private const double PI = 3.14;

            public AreaOfCircle(int radius)
            {
                this.radius = radius;
            }
            public double CalculateArea(int radius)
            {
                return PI * Math.Pow(radius, 2);
            }

            public double CalculateCircumferce(int radius)
            {
                return (double) 2 * PI * radius;
            }


        }

        public static void Circle()
        {
            Console.Write("Enter radius : ");
            int radius = Convert.ToInt32(Console.ReadLine());
            AreaOfCircle area = new AreaOfCircle(radius);
            Console.WriteLine($"The radius of circle is {radius} and its circumference and area is {area.CalculateCircumferce(radius
                )} and {area.CalculateArea(radius)}");
        }

        public class Book
        {
            private string title;
            private string author;
            private int price;

            public Book(string title, string author, int price)
            {
                this.title = title;
                this.author = author;
                this.price = price;
            }

        }

        public static void BookDetail()
        {
            Book book = new Book("Deep Work", "Carl Rapport", 600);
            
        }


    }
}
