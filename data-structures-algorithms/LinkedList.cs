using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures_algorithms
{
    class Student
    {
        public int RollNumber;
        public string Name;
        public int Age;
        public char Grade;

        public Student(int rollNumber, string name, int age, char grade)
        {
            RollNumber = rollNumber;
            Name = name;
            Age = age;
            Grade = grade;
        }
    }

    class Node
    {
        public Student Data;
        public Node Next;
        public Node Previous;

        public Node(Student student)
        {
            Data = student;
            Next = null;
        }
    }

    class StudentLinkedList
    {
        private Node head;
        public void AddAtBeginning(Student student)
        {
            Node newNode = new Node(student);

            newNode.Next = head;
            head = newNode;

            Console.WriteLine("Student added at the beginning.");
        }
        public void AddAtEnd(Student student)
        {
            Node newNode = new Node(student);

            if (head == null)
            {
                head = newNode;
                Console.WriteLine("Student added at the end.");
                return;
            }

            Node current = head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;

            Console.WriteLine("Student added at the end.");
        }

        public void AddAtPosition(Student student, int position)
        {
            if (position < 1)
            {
                Console.WriteLine("Invalid position.");
                return;
            }

            if (position == 1)
            {
                AddAtBeginning(student);
                return;
            }

            Node newNode = new Node(student);
            Node current = head;

            for (int i = 1; i < position - 1; i++)
            {
                if (current == null)
                {
                    Console.WriteLine("Position out of range.");
                    return;
                }

                current = current.Next;
            }

            if (current == null)
            {
                Console.WriteLine("Position out of range.");
                return;
            }

            newNode.Next = current.Next;
            current.Next = newNode;

            Console.WriteLine("Student added at position " + position);
        }
        public void DeleteByRollNumber(int rollNumber)
        {
            if (head == null)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            if (head.Data.RollNumber == rollNumber)
            {
                head = head.Next;
                Console.WriteLine("Student deleted successfully.");
                return;
            }

            Node current = head;

            while (current.Next != null)
            {
                if (current.Next.Data.RollNumber == rollNumber)
                {
                    current.Next = current.Next.Next;
                    Console.WriteLine("Student deleted successfully.");
                    return;
                }

                current = current.Next;
            }

            Console.WriteLine("Student with Roll Number " + rollNumber + " not found.");
        }
        public void SearchByRollNumber(int rollNumber)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Data.RollNumber == rollNumber)
                {
                    Console.WriteLine("Student Found:");
                    return;
                }

                current = current.Next;
            }

            Console.WriteLine("not found.");
        }
        public void DisplayAll()
        {
            if (head == null)
            {
                Console.WriteLine("No student records available.");
                return;
            }

            Node current = head;

            Console.WriteLine("\n----- Student Records -----");

            while (current != null)
            {
                Console.WriteLine(
                    $"Roll Number: {current.Data.RollNumber}, " +
                    $"Name: {current.Data.Name}, " +
                    $"Age: {current.Data.Age}, " +
                    $"Grade: {current.Data.Grade}"
                );

                current = current.Next;
            }
        }

        // 7. Update student's grade
        public void UpdateGrade(int rollNumber, char newGrade)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Data.RollNumber == rollNumber)
                {
                    current.Data.Grade = newGrade;
                    Console.WriteLine("Grade updated successfully.");
                    return;
                }

                current = current.Next;
            }

            Console.WriteLine("Student with Roll Number " + rollNumber + " not found.");
        }
    }

    //
    internal class LinkedList
    {
        StudentLinkedList students = new StudentLinkedList();
    }
}
