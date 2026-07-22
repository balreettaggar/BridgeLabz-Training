using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_csharp_practice
{
    internal class BasicProblems
    {
        public static void ReverseString()
        {
            string s = "Hello, World!";
            char[] charArray = s.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for(int i=charArray.Length-1; i>=0; i--)
            {
                sb.Append(charArray[i]);
            }
            for(int i=0; i<sb.Length; i++)
            {
                Console.Write(sb[i]);
            }
        }

        public static void FibonacciSeries()
        {
            int first = 0, second = 1, n = 8;
            for(int i=2; i<n; i++)
            {
                int c = first + second;
                first = second;
                second = c;
            }
            Console.WriteLine(second);
        }

        public static bool StringAnagram()
        {
            String s1 = "listen", s2 = "silent";
            if (s1.Length != s2.Length) return false;
            char[] charArray = new char[s1.Length];
            char[] charArray2 = new char[s2.Length];
            Array.Sort(charArray);
            Array.Sort(charArray2);
            for(int i=0; i<s1.Length; i++)
            {
                if (charArray[i] != charArray2[i]) return false;
            }
            return true;
        }
    }
}
