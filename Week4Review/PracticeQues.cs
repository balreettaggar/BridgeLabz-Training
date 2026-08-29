using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week4Review
{
    internal class Pair
    {
        internal int timestamp;
        internal int count;

        internal Pair(int timestamp, int count)
        {
            this.timestamp = timestamp;
            this.count = count;
        }
    }
    internal class PracticeQues
    {
        static Queue<Pair> q = new Queue<Pair>();

        static void Hit(int timestamp)
        {
            q.Enqueue(new Pair(timestamp, 1));
        }

        static int GetHits(int timestamp)
        {
            while (q.Count > 0 && q.Peek().timestamp <= timestamp - 300)
            {
                q.Dequeue();
            }

            int total = 0;
            foreach (Pair pair in q)
            {
                total += pair.count;
            }
            return total;
        }

        internal static void Main()
        {
            Hit(1);
            Hit(2);
            Hit(300);
            Console.WriteLine(GetHits(300));
            Console.WriteLine(GetHits(301));
        }
    }
}
