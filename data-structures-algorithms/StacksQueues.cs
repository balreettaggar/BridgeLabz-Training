//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Collections;
//using Microsoft.VisualBasic;

//namespace data_structures_algorithms
//{
//    public class EnqueDeque
//    {
//        Stack<int> st1 = new Stack<int>();
//        Stack<int> st2 = new Stack<int>();

//        internal void enqueue(int element)
//        {
//            st1.Push(element);
//        }

//        internal void dequeue(int element)
//        {
//            if (st2.Count == 0)
//            {
//                while (st1.Count > 0)
//                {
//                    st2.Push(st1.Pop());
//                }
//            }
//            else st2.Pop();
//        }
//    }

//    internal class SortingClass
//    {
//        static void SortStack(Stack<int> stack)
//        {
//            // Base case
//            if (stack.Count == 0)
//                return;

//            // Remove the top element
//            int top = stack.Pop();

//            // Recursively sort the remaining stack
//            SortStack(stack);

//            // Insert the removed element at the correct position
//            InsertSorted(stack, top);
//        }

//        static void InsertSorted(Stack<int> stack, int value)
//        {
//            // Correct position found
//            if (stack.Count == 0 || value <= stack.Peek())
//            {
//                stack.Push(value);
//                return;
//            }

//            // Remove the blocking element
//            int top = stack.Pop();

//            // Recursively find the correct position
//            InsertSorted(stack, value);

//            // Put the removed element back
//            stack.Push(top);
//        }
//    }

//    internal class CircularTour
//    {
//        public static int FindStartingPoint(int[] petrol, int[] distance)
//        {
//            int n = petrol.Length;

//            Queue<int> queue = new Queue<int>();

//            for (int i = 0; i < n; i++)
//            {
//                queue.Enqueue(i);
//            }

//            while (queue.Count > 0)
//            {
//                int start = queue.Peek();

//                int petrolBalance = 0;
//                int pumpsVisited = 0;

//                while (pumpsVisited < n)
//                {
//                    int current = (start + pumpsVisited) % n;

//                    petrolBalance += petrol[current];
//                    petrolBalance -= distance[current];

//                    if (petrolBalance < 0)
//                    {
//                        break;
//                    }

//                    pumpsVisited++;
//                }

//                if (pumpsVisited == n)
//                {
//                    return start;
//                }

//                queue.Dequeue();
//            }

//            return -1;
//        }
//    }

//    class LongestConsecutiveSequence
//    {
//        public static int FindLongestConsecutive(int[] arr)
//        {
//            Dictionary<int, bool> map = new Dictionary<int, bool>();
//            foreach (int num in arr)
//            {
//                map[num] = true;
//            }

//            int longest = 0;

      
//            foreach (int num in arr)
//            {
//                if (!map.ContainsKey(num - 1))
//                {
//                    int current = num;
//                    int count = 1;

//                    // Check consecutive numbers
//                    while (map.ContainsKey(current + 1))
//                    {
//                        current++;
//                        count++;
//                    }

//                    longest = Math.Max(longest, count);
//                }
//            }

//            return longest;
//        }
//    }

//    internal class StacksQueues
//    {
//        public static void StacksQueueMethods()
//        {
//            int[] petrol = { 4, 6, 7, 4 };
//            int[] distance = { 6, 5, 3, 5 };

//            int result = 0;
//            //FindStartingPoint(petrol, distance);

//            if (result != -1)
//            {
//                Console.WriteLine("Starting petrol pump: " + result);
//            }
//            else
//            {
//                Console.WriteLine("No possible starting point.");
//            }
//        }
//    }
//}
