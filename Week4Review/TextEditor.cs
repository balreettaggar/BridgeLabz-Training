using System.Diagnostics.Metrics;
using System.Formats.Tar;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace Week4Review
{
    public class Node
    {
        public char data;
        public Node next;
        public Node prev;
        public Node() { }
        public Node(char data, Node next, Node prev)
        {
            this.data = data;
            this.next = next;
            this.prev = prev;
        }

    }
    public class Document
    {
        Node node = null;
        public static Node head;
        Node prev;
        public void AddCharacter(char ch)
        {
            if (node == null)
            {
                node = new Node();
                node.data = ch;
                head = node;
                node.next = null;
                node.prev = null;
                prev = node;
            }
            else
            {
                node = new Node();
                node.data = ch;
                node.next = null;
                node.prev = prev;
                prev.next = node;
                prev = node;
            }
        }

        public void UpdateDeleteChar(char ch, string input, char toUpdate)
        {
            Node node = head;
            while (node != null)
            {
                if (node.data == ch)
                {
                    if (input == "update")
                    {
                        node.data = toUpdate;
                        return;
                    }
                    else if (input == "delete")
                    {
                        if (node.prev != null) node.prev.next = node.next;
                        else head = node.next;
                        if (node.next != null) node.next.prev = node.prev;
                        return;
                    }
                    else throw new Exception("Enter correct operation");   
                }
                node = node.next;
            }
        }

        public static void Cursor(int input, string L_R, int length)
        {
            if (head == null)
            {
                Console.WriteLine("Empty document");
                return;
            }
            Node node = head;
            int counter = 0;
            if (input >= length) throw new Exception("Input out of range");
            if (L_R == "Left")
            {
                while (node.next != null)
                {
                    node = node.next;
                }
                while (node!=null && counter<input)
                {
                    node = node.prev;
                    counter++;
                }
            }
            else if (L_R == "Right")
            {
                node = head;
                while (node != null && counter < input)
                {
                    node = node.next;
                    counter++;
                }
            }
            else
            {
                throw new Exception("Provide correct cursor movement");
            }

            if (node != null) Console.WriteLine(node.data);
            else throw new Exception("Null Node");
        }
    }

    public class RecentFileNode
    {
        public string fileName;
        public RecentFileNode next;

        public RecentFileNode(string fileName)
        {
            this.fileName = fileName;
        }
    }
    public class RecentFile
    {
        RecentFileNode head = null;
        RecentFileNode current = null;

        public void AddFile(string fileName)
        {
            RecentFileNode newNode = new RecentFileNode(fileName);

            if (head == null)
            {
                head = newNode;
                newNode.next = head;
                current = head;
                return;
            }
            newNode.next = head;
            current.next = newNode;
            current = newNode;
        }

        public string CtrlTab()
        {
            if (current == null) return null;
            current = current.next;
            return current.fileName;
        }
    }

    public class RedoUndo
    {
        Stack<char> undoStack = new Stack<char>();
        Stack<char> redoStack = new Stack<char>();
        public void AddOperation(char ch)
        {
            undoStack.Push(ch);
            redoStack.Clear();
        }
        public void Undo()
        {
            if (undoStack.Count == 0) return;

            char ch = undoStack.Pop();
            redoStack.Push(ch);
        }
        public void Redo()
        {
            if (redoStack.Count == 0) return;
            char ch = redoStack.Pop();
            undoStack.Push(ch);
        }
    }
    public class BackgroundJobs
    {
        public static void Autosave()
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue("Autosave");
            while (q.Count > 0)
            {
                string job = q.Dequeue();
                if (job == "Autosave")
                {
                    Node node = Document.head;
                    string text = "";
                    while (node != null)
                    {
                        text += node.data;
                        node = node.next;
                    }
                    Console.WriteLine("Autosaved Result is : " + text);
                }
            }
        }
    }

    public class WordFreq
    {
        public static Dictionary<string, int> Frequency()
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            Node node = Document.head;
            string word = "";

            while (node != null)
            {
                if (node.data != ' ')
                {
                    word += node.data;
                }
                else
                {
                    if (word != "")
                    {
                        if (freq.ContainsKey(word))
                            freq[word]++;
                        else
                            freq.Add(word, 1);

                        word = "";
                    }
                }

                node = node.next;
            }

            if (word != "")
            {
                if (freq.ContainsKey(word))
                    freq[word]++;
                else
                    freq.Add(word, 1);
            }

            return freq;
        }
    }

    public class Sort
    {
        public static void QuickSort(List<int> myList, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Parts(myList, left, right);
                QuickSort(myList, left, pivotIndex);
                QuickSort(myList, pivotIndex + 1, right);
            }
        }

        internal static int Parts(List<int> myList, int left, int right)
        {
            int pivot = myList[left + (right - left) / 2];
            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (myList[i] < pivot);

                do
                {
                    j--;
                } while (myList[j] > pivot);

                if (i >= j) return j;

                int temp = myList[i];
                myList[i] = myList[j];
                myList[j] = temp;
            }
        }


        public static void SortingMeth()
        {
            Dictionary<string, int> freqDict = WordFreq.Frequency();

            List<int> myList = new List<int>();

            foreach (var item in freqDict)
            {
                myList.Add(item.Value);
            }

            Sort.QuickSort(myList, 0, myList.Count - 1);

            Console.WriteLine("Sorted Frequencies:");

            foreach (int value in myList)
            {
                Console.WriteLine(value);
            }
        }

        public static bool BinarySearch(List<int>myList, int start, int end, int target)
        {
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (myList[mid] == target)
                {
                   
                    return true;
                } else if (myList[mid] < target)
                {
                    start=mid+1;
                } else
                {
                    end=mid-1;
                }
            }
            return false;
        }
    }

    public class TextEditor
    {
        public static void Main()
        {
            Document document = new Document();
            string input = "hello my name is Balreet. hello";

            foreach (char ch in input)
            {
                document.AddCharacter(ch);
            }
            Console.WriteLine("Word Frequencies");

            foreach (var item in WordFreq.Frequency())
            {
                Console.WriteLine(item.Key + " : " + item.Value);
            }

            Console.WriteLine();

            Sort.SortingMeth();

            List<int> frequencies = new List<int>();

            foreach (var item in WordFreq.Frequency())
            {
                frequencies.Add(item.Value);
            }

            bool result = Sort.BinarySearch(frequencies, 0, frequencies.Count - 1, 2);

            Console.WriteLine("Frequency Found : " + result);

            BackgroundJobs.Autosave();
        }

    }
}