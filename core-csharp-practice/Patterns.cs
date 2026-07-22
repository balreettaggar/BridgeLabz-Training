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
    }
}
