using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanced_c_sharp
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
    }

    public class myStudent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Marks { get; set; }
    }
    public class CSVdata
    {

        public static void ReadStudents()
        {
            string filePath = "students.csv";
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(',');
                Console.WriteLine($"Student's ID is {words[0]}");
                Console.WriteLine($"Student's Name is {words[1]}");
                Console.WriteLine($"Student's Age is {words[2]}");
                Console.WriteLine($"Student's Marks are {words[3]}");
            }
        }

        public static void WriteEmployees()
        {
            List<Employee> myList = new List<Employee>()
            {
                new Employee
                {
                    ID = 1,
                    Name = "Balreet",
                    Department = "CSE",
                    Salary = 25000.00,
                },
                new Employee
                {
                    ID = 2,
                    Name = "Vishvas",
                    Department = ".NET",
                    Salary = 30000.00,
                },
                new Employee
                {
                    ID = 3,
                    Name = "harsh",
                    Department = "Backend",
                    Salary = 30000,
                }
            };
            using (StreamWriter writer = new StreamWriter("employees.csv"))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(myList);
            }
            
        }

        public static void countEmployees()
        {
            using (StreamReader sr = new StreamReader("employees.csv"))
            using(CsvReader csvr = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                int count = csvr.GetRecords<Employee>().Count();
                Console.WriteLine(count);
            }
        }

       public static void FilterStudentss()
        {
            using(StreamReader sr = new StreamReader("students.csv"))
            using(CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var students = csv.GetRecords<myStudent>();
                var quilifiedStudents = students.Where(student => student.Marks > 80);
                foreach (var student in quilifiedStudents)
                {
                    Console.WriteLine("Student ID is : " + student.ID);
                    Console.WriteLine("Student Name is : " + student.Name);
                    Console.WriteLine("Student Age is : " + student.Age);
                    Console.WriteLine("Student Marks is : " + student.Marks);
                }
            }
        }

        public static void SearchEmployee()
        {
            string name = "Balreet";
            using (StreamReader sr = new StreamReader("employees.csv"))
            using (CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var employees = csv.GetRecords<Employee>();
                foreach(Employee emp in employees)
                {
                    if(emp.Name == name)
                    {
                        Console.WriteLine(emp.ID + " " + emp.Salary);
                    }
                }
            }
        }

        public static void UpdateSalary()
        {

            using (StreamReader sr = new StreamReader("employees.csv")) 
            using(CsvReader csr = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                List<Employee> myList = csr.GetRecords<Employee>().ToList();
                foreach (Employee emp in myList)
                {
                    if(emp.Department == "CSE")
                    {
                        emp.Salary = emp.Salary * 1.1;
                    }
                }

                using (StreamWriter sw = new StreamWriter("updated_Employees.csv"))
                using (CsvWriter csw = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    csw.WriteRecords(myList);
                }

            }

        }

        public static void SortEmployee()
        {
            using(StreamReader sr = new StreamReader("employees.csv"))
                using(CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var topTwo = csv.GetRecords<Employee>().OrderByDescending(emp => emp.Salary).Take(2);
                foreach(var emp in topTwo){
                    Console.Write(emp.ID + " " + emp.Name +" " + emp.Department+ " " + emp.Salary);
                }
            }
        }
    }
}
