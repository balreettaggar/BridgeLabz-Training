using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    internal class SearchingStreaming
    {
        internal static void StringBuilderPerformace()
        {
            StringBuilder sb = new StringBuilder();
            string str1 = "";

            int iterations = 1000;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < iterations; i++)
            {
                str1 += "Hello";
            }

            stopwatch.Stop();

            Console.Write("Time for string appendinng : " + stopwatch.ElapsedMilliseconds + "\n");

            stopwatch.Restart();

            for (int i = 0; i < iterations; i++)
            {
                sb.Append("Hello");
            }

            stopwatch.Stop();

            Console.Write("Time for stringbuilder appendinng : " + stopwatch.ElapsedMilliseconds + "\n");

        }
        internal static void StreamRead()
        {
            string filePath = "students.txt";

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        internal static void CountOccurences()
        {
            string filePath = "students.txt";

            string word = "Singh";
            int count = 0;

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string curr;
                while((curr = streamReader.ReadLine())!= null)
                {
                    if (curr.Contains(word))
                    {
                        foreach(var words in curr)
                        {
                            if (words.Equals(word)) count++;
                        }
                    }
                }
            }

            Console.WriteLine(count);

        }

        internal static void ByteToChar()
        {
            string filename = "students.txt";

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                int character;
                while((character = sr.Read()) != -1)
                {
                    Console.Write($"{(char)character}");
                }
            }
        }
        
        internal static void InputandWrite()
        {
            string line = Console.ReadLine();

            
        }
    }
}
