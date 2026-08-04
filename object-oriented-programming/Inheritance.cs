using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{
    internal class Animal
    {
        internal string? name;
        internal int age;

        internal virtual void MakeSound()
        {
            Console.WriteLine("Animal makes a sound");
        }
    }

    internal class Dog : Animal
    {
        internal override void MakeSound()
        {
            Console.WriteLine("Dog barks");
        }
    }

    internal class Cat : Animal
    {
        
        internal override void MakeSound()
        {
            Console.WriteLine("Cat Meows");
        }
    }

    internal class Bird : Animal
    {
       
        internal override void MakeSound()
        {
            Console.WriteLine("Bird chirps");
        }
    }

    internal class EmployeeClass
    {
        internal string name;
        internal int id;
        internal double salary;

        internal EmployeeClass() : this("", 0, 0.0)
        {

        }

        internal EmployeeClass(string name, int id, double salary)
        {
            this.name = name;
            this.id = id;
            this.salary = salary;
        }

        internal virtual void DisplayDetails()
        {
            Console.WriteLine($"{name} with id {id} has a salary of {salary}");
        }
    }

    internal class Manager : EmployeeClass
    {
        private int teamSize;
        internal Manager() : this("", 0, 0.0, 0)
        {
        }

        internal Manager(string name, int age, double salary, int teamSize) : base(name, age, salary)
        {
            this.teamSize = teamSize;
        }

        internal override void DisplayDetails()
        {
            Console.WriteLine($"{name} with id {id} has a salary of {salary} and team size is {teamSize}");
        }
    }

    internal class Developer : EmployeeClass
    {
        private string? programmingLanguage;

        internal Developer() : this("", 0, 0.0, "")
        {

        }

        internal Developer(string name, int id, double salary, string programmingLanguage) : base(name, id, salary)
        {
            this.programmingLanguage = programmingLanguage;
        }

        internal override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"The language is {programmingLanguage}");
        }
    }

    internal class Intern : EmployeeClass
    {
        private string internshipDuration;

        internal Intern() : this("",0, 0.0, "")
        {

        }

        internal Intern(string name, int id, double salary, string internshipDuration) : base(name, id, salary)
        {
            this.internshipDuration = internshipDuration;
        }

        internal override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"The internshipDuration is {internshipDuration}");
        }
    }

    internal class Inheritance
    {
        public static void InheritanceMethod()
        {
            //Animal a = new Animal();
            //a.MakeSound();
            //Dog d = new Dog();
            //d.MakeSound();
            //Cat c = new Cat();
            //c.MakeSound();
            //Bird b = new Bird();
            //b.MakeSound();

            EmployeeClass emp = new EmployeeClass();
            Intern intern = new Intern("Balreet", 101, 25000.00, "1");
            intern.DisplayDetails();
        }
    }
}
