using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_csharp_practice
{
    public class Patterns
    {

        public static void StarPattern()
        {
            int n = 5;

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }

        public static void FloyddPattern()
        {
            int n = 5;
            int num = 1;
            for(int i=0; i<n; i++)
            {
                for(int j=0; j<=i; j++)
                {
                    Console.Write(num++ + " ");
                }
                Console.WriteLine();
            }
        }

        public static void HollowRectanglePattern()
        {
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<5; j++)
                {
                    if (i == 0 || i == 4 || j == 0 || j == 4) Console.Write("*");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
