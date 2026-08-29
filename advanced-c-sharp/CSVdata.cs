using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanced_c_sharp
{
    internal class CSVdata
    {
        internal static void ReadStudents()
        {
            string filePath = "students.csv";

            using (StreamReader reader = new StreamReader(filePath))
            {

                string header = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    int id = int.Parse(data[0]);
                    string name = data[1];
                    int age = int.Parse(data[2]);
                    double marks = double.Parse(data[3]);

                    Console.WriteLine(
                        $"ID: {id}, Name: {name}, Age: {age}, Marks: {marks}"
                    );
                }
            }
        }

        internal static void WriteEmployees()
        {
            string filePath = "employees.csv";

            using (StreamWriter writer = new StreamWriter(filePath))
            {

                writer.WriteLine("ID,Name,Department,Salary");

                writer.WriteLine("1,Raj,IT,50000");
                writer.WriteLine("2,Aman,HR,45000");
                writer.WriteLine("3,Simran,Finance,55000");
                writer.WriteLine("4,Neha,IT,60000");
                writer.WriteLine("5,Karan,Marketing,48000");
            }

            Console.WriteLine("Employee data written successfully.");
        }

        internal static void CountStudents()
        {
            string filePath = "students.csv";

            int count = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip header
                reader.ReadLine();

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                }
            }

            Console.WriteLine($"Number of records: {count}");
        }

        internal static void FilterStudents()
        {
            string filePath = "students.csv";

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip header
                reader.ReadLine();

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    int id = int.Parse(data[0]);
                    string name = data[1];
                    int age = int.Parse(data[2]);
                    double marks = double.Parse(data[3]);

                    if (marks > 80)
                    {
                        Console.WriteLine(
                            $"ID: {id}, Name: {name}, Age: {age}, Marks: {marks}"
                        );
                    }
                }
            }
        }

        internal static void SearchEmployee()
        {
            string filePath = "employees.csv";

            Console.Write("Enter employee name: ");
            string searchName = Console.ReadLine();

            bool found = false;

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    string name = data[1];

                    if (name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                    {
                        string department = data[2];
                        double salary = double.Parse(data[3]);

                        Console.WriteLine($"Department: {department}");
                        Console.WriteLine($"Salary: {salary}");

                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Employee not found.");
            }
        }
    }
}
