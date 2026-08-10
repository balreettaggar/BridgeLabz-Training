using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;

namespace advanced_c_sharp
{
    internal class CollectionsClass
    {
        public static void ReverseList()
        {
            List<int> list = new List<int>(){ 1,2,3,4,5};
            //ArrayList numbers = new ArrayList() { 1, 2, 3, 4, 5 };
            int i = 0, j = list.Count - 1;

            while (i < j)
            {
                int temp = list[i];
                list[i]= list[j];
                list[j] = temp;
                i++;
                j--;
            }

            foreach (int item in list) Console.Write(item);

        }

        public static void ReverseLinkedList()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            LinkedListNode<int> first = list.First!;
            LinkedListNode<int> last = list.Last!;

            int i = 0, j = list.Count - 1;
            while (i < j)
            {
                int temp = first.Value;
                first.Value = last.Value;
                last.Value = temp;
                first = first.Next!;
                last = last.Previous!;
                i++;
                j--;
            }

            foreach (int item in list) Console.Write(item + " ");
        }

        public static void Freq()
        {
            List<string> list = new List<string> { "apple", "banana", "apple", "orange"};
            Dictionary<string, int> freq = new Dictionary<string, int>();
            for(int i=0; i<list.Count; i++)
            {
                if (freq.ContainsKey(list[i]))
                {
                    freq[list[i]]++;
                }else
                {
                    freq.Add(list[i], 1);
                }
            }
            
            foreach (KeyValuePair<string, int>pair in freq){ // KeyPair is a struct
                Console.WriteLine(pair.Key + " " + pair.Value);
            }
           
        }

        public static void RotateList()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            int rotate = 2;
            for (int i = 0; i < rotate; i++)
            {
                int first = list[0];
                for (int j = 0; j < list.Count - 1; j++)
                {
                    list[j] = list[j + 1];
                }
                list[list.Count - 1] = first;
            }
            foreach(int num in list)
            {
                Console.Write(num+" ");
            }
        }

        public static void BinaryNumbers()
        {
            Queue<string> queue = new Queue<string>();
            int n = 5;
            queue.Enqueue("1");
            for(int i=0; i<n; i++)
            {
                string curr = queue.Dequeue();
                Console.Write(curr + " ");
                queue.Enqueue(curr + "0");
                queue.Enqueue(curr + "1");
            }
        }
    }
}
