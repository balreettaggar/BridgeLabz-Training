using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{
    internal class Employee
    {
        private string name;
        private int age;
        private int id;
        private string phone;

        public Employee(string name, int age, int id, string phone)
        {
            Name = name;
            Age = age;
            Id = id;
            Phone = phone;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Name), "Name cannot be null");
                }
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Name cannot be null");
                if (value.Length < 3 || value.Length > 20) throw new ArgumentException("Enter valid name");
                name = value;

            }
        }

        public int Age
        {
            get { return age; }
            set 
            {
                if (value < 18 || value > 60) throw new ArgumentOutOfRangeException(nameof(Age), "Age must be between 18 and 60.");
                age = value;

            }
        }

        public int Id
        {
            get { return id; }
            set 
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id), "Employee ID must be positive.");
                id = value;
            }
        }


        public string Phone
        {
            get { return phone; }
            set 
            {
                if (value == null) throw new ArgumentNullException(nameof(phone), "phone number cannot be null");
                if (value.Length != 10) throw new ArgumentException("Enter valid phone number");
                foreach(char ch in value)
                {
                    if (!char.IsDigit(ch))
                    {
                        throw new ArgumentException("Phone number only contain numbers");
                    }
                }
                phone = value;

            }
        }

        public void Display()
        {
            Console.WriteLine("The user details are below");
            Console.WriteLine($"Name : {name}");
            Console.WriteLine($"Age : {age}");
            Console.WriteLine($"Id : {id}");
            Console.WriteLine($"Phone Number : {phone}");
        }

    }

    public class MyProgram
    {
        public static void MyProgram1()
        {
            //Console.WriteLine("Enter your details below : ");
            //string? name = Console.ReadLine();
            //while (name == null || name=="")
            //{
            //    Console.WriteLine("Enter your name");
            //    name = Console.ReadLine();
            //}
            //int age = Convert.ToInt32(Console.ReadLine());
            //while(age<=18 || age>=60)
            //{
            //    Console.WriteLine("Enter your valid age");
            //    age = Convert.ToInt32(Console.ReadLine());
            //}
            //int id = Convert.ToInt32(Console.ReadLine());
            //string? phone = (Console.ReadLine());
            //if (phone == null) return;
            //while (phone.Length<10 || phone.Length > 10)
            //{
            //    Console.WriteLine("Enter valid phone number ");
            //    phone = Console.ReadLine();
            //    if (phone == null) return;
            //}
            //Employee emp = new Employee(name, age, id, phone);
            //emp.Display();

            try
            {
                Console.Write("Enter your name : ");
                string? name = Console.ReadLine();

                Console.Write("Enter your age : ");
                int age = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter your ID : ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter your phone number : ");
                string? phone = Console.ReadLine();

                Employee emp = new Employee(name!, age, id, phone!);
                emp.Display();
            }
            catch (ArgumentNullException ex)
            {
                Console.Write("Null Exception : " + ex.Message);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("Invalid argument : " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Execution is done");
            }

        }
       
    }

}
