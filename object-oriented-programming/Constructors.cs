using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{

    internal class Circle
    {
        private int radius;
        
        internal Circle() : this(0)
        {
        }

        internal Circle(int radius)
        {
            this.radius = radius;
        } 

        internal void Display()
        {
            Console.WriteLine($"The radius of circle is {radius}");
        }

    }

    internal class Person
    {
        private string name;
        private int age;
        private string residence;
        private char gender;

        internal Person(string name, int age, string residence, char gender)
        {
            this.name = name;
            this.age = age;
            this.residence = residence;
            this.gender = gender;
        }

        internal Person() : this("", 0, "", '\0')
        {

        }

        internal Person(Person other)
        {
            this.name = other.name;
            this.age = other.age;
            this.residence = other.residence;
            this.gender = other.gender;
        }

        //internal Person(Person other) : this(other.name, other.age, other.residence, other.gender)
        //{

        //}

        internal void Display()
        {
            Console.WriteLine($"Name : {name}\n Age : {age}\n Residence : {residence} \n Gender : {gender}");
        }
    }

    internal class Hotel
    {
        private string guestName;
        private string roomType;
        private int nights;

        internal Hotel() : this("", "", 0)
        {

        }

        internal Hotel(string guestName, string roomType, int nights)
        {
            this.guestName = guestName;
            this.roomType = roomType;
            this.nights = nights;
        }

        internal Hotel(Hotel h) : this(h.guestName, h.roomType, h.nights)
        {

        }

        internal void Display()
        {
            Console.Write($"Guestname : {guestName}\nRoomType : {roomType}\nNo. of nights : {nights}");
        }

        
    }

    internal class Library
    {
        private string name;
        private string author;
        private int price;
        private bool availibility;

        internal Library() : this("", "", 0, false)
        {

        }
        
        internal Library(string name, string author, int price, bool availibility)
        {
            this.name = name;
            this.author = author;
            this.price = price;
            this.availibility = availibility;
        }

        internal void BorrowBook()
        {
            if (this.availibility == true) Console.WriteLine("You can borrow this book");
            else Console.WriteLine("No you cannot");
        }
    }

    internal class Car
    {
        private string customerName;
        private int carModel;
        private int days;
        private int rent;

        internal Car() : this("", 2000, 0)
        {

        }

        internal Car(string customerName, int carModel, int days)
        {
            this.customerName = customerName;
            this.carModel = carModel;
            this.days = days;
        }

        internal void TotalRent()
        {
            if (this.carModel < 2010) { 
                this.rent = 2000;
                Console.Write($"Total rent is {this.days * this.rent}");
            } else if (this.carModel>=2010 && this.carModel<=2020) {
                this.rent = 3000;
            Console.Write($"Total rent is {this.days * this.rent}");
            }
        }
        

    }

    internal class Vehicle
    {
        private string ownerName;
        private string vehicleType;

        internal static int registerationFees = 1000;

        internal Vehicle() : this("", "")
        {

        }

        internal Vehicle(string ownerName, string vehicleType)
        {
            this.ownerName = ownerName;
            this.vehicleType = vehicleType;
        }

        internal void Display()
        {
            Console.WriteLine($"{ownerName} owns {vehicleType}");
        }

        internal static void UpdateRegisterationFees()
        {
            registerationFees += 1000;
        }
        
    }

    internal class University
    {
        public int rollNumber;
        protected string name;
        private double cgpa;

        public double CGPA
        {
            get { return cgpa; }
            set
            {
                if (cgpa < 0) throw new ArgumentOutOfRangeException(nameof(cgpa), "Enter valid cgpa");
                cgpa = value;
            }
        }

        internal University() : this(0, "", 0.0)
        {
        }

        internal University(int rollNumber, string name, double cgpa)
        {
            this.rollNumber = rollNumber;
            this.name = name;
            this.cgpa = cgpa;
        }


    }

    internal class PostGraduate : University
    {
        internal void SetName(string newName)
        {
            name = newName;
        }
    }

    internal class Constructors
    {
        class Book
        {
            private string title;
            private string author;
            private int price;

            internal Book()
            {
                this.title = "";
                this.author = "";
                this.price = 0;
            }

            internal Book(string title, string author, int price)
            {
                this.title = title;
                this.author = author;
                this.price = price;
            }

            public void Display()
            {
                Console.Write($"The price of book {title} with the author { author} is {price}");
            }
        }

        public static void BookMethod()
        {
            Book book = new Book("Atomic Habits", "Carl Napport", 500);
            book.Display();
        }

        public static void CircleMethod()
        {
            int radius = Convert.ToInt32(Console.ReadLine());
            Circle circle = new Circle(radius);
            circle.Display();
            
        }

        public static void PersonMethod()
        {
            Person person = new Person("Balreet", 22, "Dharamgarh", 'M');
            person.Display();
            Person person2 = new Person(person);
            person2.Display();
        }

        public static void HotelMethod()
        {
            Hotel h1 = new Hotel();
            h1.Display();
            Hotel h2 = new Hotel("Balreet", "Luxury", 2);
            h2.Display();
        }

        public static void LibraryMethod()
        {
            Library library = new Library();
            library.BorrowBook();
            Library library1 = new Library("Deep Work", "Carl Newport", 500, true);
            library1.BorrowBook();
        }

        public static void CarMethod()
        {
            Car car = new Car("balreet", 2016, 5);
            car.TotalRent();
        }

        public static void VehicleMethod()
        {
            Vehicle vehicle = new Vehicle("Balreet", "car");
            vehicle.Display();
            Vehicle.UpdateRegisterationFees();
            Console.Write(Vehicle.registerationFees);
        }
    }
}
