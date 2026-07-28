using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_csharp_practice
{
    internal class Strings
    {
        public static void Calculate()
        {
            string? abc = Console.ReadLine();
            if (abc != null) abc = abc.ToLower();
            int count = 0;
            for (int i = 0; abc != null && i < abc.Length; i++)
            {
                char c = abc[i];
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') count++;
            }
            Console.WriteLine($"The number of vowels is {count}");
            if (abc != null) Console.WriteLine($"The number of consonants are {abc.Length - count}");
        }

        public static void ReverseString()
        {
            string? abc = Console.ReadLine();
            if (abc == null) return;
            int j = abc.Length - 1;
            StringBuilder sb = new StringBuilder();
            while (j >= 0)
            {
                sb.Append(abc[j]);
                j--;
            }
            Console.WriteLine(sb);
        }

        public static void PalindromeCheck()
        {
            string? abc = Console.ReadLine();
            if (abc == null) return;
            int i = 0, j = abc.Length - 1;
            while (j >= 0)
            {
                if (abc[i] != abc[j])
                {
                    Console.WriteLine($"{abc} is not a palindrome");
                    return;
                }
                i++;

                j--;
            }
            Console.WriteLine($"{abc} is a palindrome");
        }

        public static void RemoveDuplicates()
        {
            string? abc = Console.ReadLine();
            int[] freq = new int[26];
            if (abc == null) return;
            for (int i = 0; i < abc.Length; i++)
            {
                freq[abc[i] - 'a']++;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < abc.Length; i++)
            {
                if (freq[abc[i] - 'a'] != 0)
                {
                    sb.Append(abc[i]);
                    freq[abc[i] - 'a'] = 0;
                }
            }
            Console.WriteLine(sb);
        }

        public static void LongestWord()
        {
            string? sentence = Console.ReadLine();
            if (sentence == null) return;
            string[] arr = sentence.Split(" ");
            //for (int i = 0; i < arr.Length; i++) Console.Write(arr[i] + " ");
            int maxL = 0;
            string answer = "null";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Length > maxL)
                {
                    answer = arr[i];
                    maxL = arr[i].Length;
                }
            }
            Console.Write(answer);
        }

        public static void StringOccurences()
        {
            string? abc = Console.ReadLine();
            if (abc == null) return;
            string? subs = Console.ReadLine();
            if (subs == null) return;
            int left = 0, right = 0, count = 0;
            while (right < abc.Length)
            {
                if (right - left + 1 > subs.Length) left++;
                if (right - left + 1 == subs.Length)
                {
                    bool found = true;
                    for (int i = 0; i < subs.Length; i++)
                    {
                        if (abc[i + left] != subs[i])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found) count++;
                }
                right++;
            }
            Console.Write($"The number of occurences are {count}");
        }

        public static void Toggle()
        {
            string? abc = Console.ReadLine();
            if (abc == null) return;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < abc.Length; i++)
            {
                char ch = abc[i];
                if (ch >= 'a' && ch <= 'z') sb.Append(char.ToUpper(ch));
                else sb.Append(char.ToLower(ch));
            }
            Console.WriteLine(sb);
        }

        public static void Lexiographically()
        {
            string? str1 = Console.ReadLine();
            string? str2 = Console.ReadLine();
            if (str1 == null || str2 == null) return;
            bool first = false;
            bool second = false;
            for (int i = 0; i < Math.Min(str1.Length, str2.Length); i++)
            {
                char ch1 = str1[i];
                char ch2 = str2[i];
                if (ch1 < ch2)
                {
                    first = true;
                    break;
                }
                else if (ch2 < ch1)
                {
                    second = true;
                    break;
                }
            }
            if (first) Console.Write($"{str1} comes before {str2}");
            else if (second) Console.Write($"{str2} comes before {str1}");
            else
            {
                if (str1.Length < str2.Length) Console.Write($"{str1} comes before {str2}");
                else Console.Write($"{str2} comes before {str1}");
            }
        }

        public static void Frequent()
        {
            string? str1 = Console.ReadLine();
            int[] freq = new int[26];
            if (str1 == null) return;
            for (int i = 0; i < str1.Length; i++)
            {
                freq[str1[i] - 'a']++;
            }
            int maxFreq = 0;
            char ans = '0';
            for (int i = 0; i < str1.Length; i++)
            {
                if (maxFreq < str1[i])
                {
                    maxFreq = freq[str1[i] - 'a'];
                    ans = str1[i];
                }
            }
            Console.WriteLine($"The max freq of char {ans} is {maxFreq}");
        }

        public static void RemoveChar()
        {
            string? str1 = Console.ReadLine();
            if (str1 == null) return;
            string? ch = Console.ReadLine();
            if (ch == null) return;
            str1 = str1.Replace($"{ch}", "");
            Console.WriteLine(str1);
        }

        public static void Anagrams()
        {
            string? str1 = Console.ReadLine();
            string? str2 = Console.ReadLine();
            if (str1 == null || str2 == null) return;
            int[] freq = new int[26];
            int[] freq1 = new int[26];
            for (int i = 0; i < str1.Length; i++)
            {
                freq[str1[i] - 'a']++;
            }
            for (int i = 0; i < str2.Length; i++)
            {
                freq1[str2[i] - 'a']++;
            }
            for (int i = 0; i < freq.Length; i++)
            {
                if (freq[i] != freq1[i])
                {
                    Console.WriteLine("Not anagrams");
                    return;
                }
            }
            Console.WriteLine("Anagrams");
        }

        public static void ReplaceWord()
        {
            string? str1 = Console.ReadLine();
            string? str2 = Console.ReadLine();
            if (str1 == null || str2 == null) return;
            string[] arr = str1.Split(" ");
             
            for(int i=0; i<arr.Length; i++)
            {
                if (string.Equals(arr[i], str2)) { 
                    arr[i] = "";
                }
            }
            for(int i=0; i<arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }

    }
}
