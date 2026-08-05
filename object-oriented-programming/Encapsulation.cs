using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{

    // Employee management system

    abstract internal class EmployeeEncap
    {
        internal string? employeeName;
        internal int employeeId;
        internal int baseSalary;

        abstract internal void CalculateSalary();

        internal void DisplayDetails()
        {
            Console.WriteLine($"Employee Name : {employeeName}\nEmployee ID : {employeeId}\nBase Salary : {baseSalary}");
        }
    }

    internal class FullTimeEmployee : EmployeeEncap
    {
        internal override void CalculateSalary()
        {
            Console.WriteLine("Full Time Employee's salary is fixed");
        }
        
    }

    internal class PartTimeEmployee : EmployeeEncap
    {
        private int hours;
        
        internal PartTimeEmployee() : this(0)
        {
        }

        internal PartTimeEmployee(int hours)
        {
            this.hours = hours;
        }
        internal override void CalculateSalary()
        {
            Console.WriteLine($"Your salary is {hours * 300}");
        }
    }

    internal interface IDepartment
    {
        internal void AssignDepartment();
        internal void GetDepartmentDetails();
    }


    // E-COMMERCE

    //interface ITaxable
    //{
    //    double CalculateTax();
    //    void GetTaxDetails();            
    //}
    //abstract internal class Product
    //{
    //    internal string Name { get; set; } = "";
    //    internal int Id { get; set; } = 0;
    //    internal double Price { get; set; } =0.0;

    //    internal Product(string name, int id, double price) 
    //    {
    //        Name = name;
    //        Id = id;
    //        Price = price;
    //    }

    //    internal abstract double CalculateDiscount();
    //}


    //internal class Electronics : Product, ITaxable
    //{

    //    internal Electronics(string name, int id, double price) : base(name, id, price)
    //    {

    //    }
    //    internal override double CalculateDiscount()
    //    {
    //        return Price-(Price*0.1);
    //    }

    //    public double CalculateTax()
    //    {
    //        return Price * 1.7;
    //    }

    //    public void GetTaxDetails()
    //    {
    //        Console.WriteLine("The Details are as follow :");
    //        Console.WriteLine($"The name of the product is : {Name}");
    //        Console.WriteLine($"The id of the product is : {Id}");
    //        Console.WriteLine($"The price of the product is : {Price}");
    //        Console.WriteLine($"The total discount offered is : {CalculateDiscount()}");
    //        Console.WriteLine($"With additional tax of : {CalculateTax()}");
    //    }

    //}

    //internal class Clothing : Product, ITaxable 
    //{

    //}

    //internal class Groceries : Product, ITaxable
    //{

    //}


    // VEHICLE QUESTION

    public interface IInsurable
    {
        int CalculateInsurance();
        void GetInsuranceDetails();
    }

    abstract internal class VehicleClass
    {
        abstract internal int CalculateRentCost(int rentDays);

        internal VehicleClass() : this(0, "", 0) { }

        internal int VehicleNumber { get; set; }
        internal string Type { get; set; }
        internal int RentalRate { get; set; }

        internal VehicleClass(int vehicleNumber, string type, int rentalRate)
        {
            VehicleNumber = vehicleNumber;
            Type = type;
            RentalRate = rentalRate;
        }
    }

    internal class CarClass : VehicleClass, IInsurable
    {

        internal CarClass() : this(0, "", 0) { }
        internal CarClass(int vehicleNumber, string type, int rentalRate) : base(vehicleNumber, type, rentalRate)
        {
        }

        readonly int carRentPrice = 3000;
        internal override int CalculateRentCost(int days)
        {
            return days * carRentPrice;
        }
        
        public int CalculateInsurance()
        {
            return carRentPrice * 100;
        }

        public void GetInsuranceDetails()
        {
            Console.WriteLine($"The vehicle of {Type} has an insurance of {CalculateInsurance()}");
        }

    }

    internal class Bike : VehicleClass, IInsurable
    {
        internal Bike() : this(0, "", 0) { }
        internal Bike(int vehicleNumber, string type, int rentalRate) : base(vehicleNumber, type, rentalRate)
        {
        }

        readonly int bikeRentPrice = 1000;
        internal override int CalculateRentCost(int days)
        {
            return days * bikeRentPrice;
        }

        public int CalculateInsurance()
        {
            return bikeRentPrice * 100;
        }

        public void GetInsuranceDetails()
        {
            Console.WriteLine($"The vehicle of {Type} has an insurance of {CalculateInsurance()}");
        }


    }

    internal class Truck : VehicleClass, IInsurable
    {
        internal Truck() : this(0, "", 0) { }
        internal Truck(int vehicleNumber, string type, int rentalRate) : base(vehicleNumber, type, rentalRate)
        {
        }

        readonly int truckRentPrice = 5000;
        internal override int CalculateRentCost(int days)
        {
            return days * truckRentPrice;
        }

        public int CalculateInsurance()
        {
            return truckRentPrice * 100;
        }

        public void GetInsuranceDetails()
        {
            Console.WriteLine($"The vehicle of {Type} has an insurance of {CalculateInsurance()}");
        }


    }

    // HOSPITAL

    interface IMedicalRecord
    {
        List<string> AddRecord();
        void ViewRecord();
    }

    abstract class Patient
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        internal Patient(int patientId, string name, int age)
        {
            PatientID = patientId;
            Name = name;
            Age = age;
        }

        internal abstract int CalculateBill();

        internal void PatientDetails()
        {
            Console.WriteLine("==Hospital Management System==");
            Console.WriteLine($"Patient's Name : {Name}");
            Console.WriteLine($"Patient's id : {PatientID}");
            Console.WriteLine($"Patient't age : {Age}");
        }

    }

    internal class InPatient : Patient, IMedicalRecord
    {
        internal InPatient(int id, string name, int age) : base(id, name, age) { }

        public static readonly int entreeFee = 500;

        internal override int CalculateBill()
        {
            return entreeFee;
        }

        public List<string> AddRecord()
        {
            
        }
    }

    internal class OutPatient : Patient
    {
        internal OutPatient(int id, string name, int age) : base(id, name, age) { }

        internal override int CalculateBill()
        {
            return InPatient.entreeFee;
        }
    }
    internal class Encapsulation
    {
        public static void EncapsulationMethods()
        {
            //EmployeeEncap e1 = new FullTimeEmployee();
            //EmployeeEncap e2 = new PartTimeEmployee(8);
            //e1.CalculateSalary();
            //e2.CalculateSalary();

            VehicleClass v1 = new CarClass();

            VehicleClass v2 = new Bike();

            VehicleClass v3 = new Bike();

            List<VehicleClass> vehicles = new List<VehicleClass> { v1, v2, v3 };
            
            foreach(VehicleClass vehicle in vehicles)
            {
                Console.WriteLine(vehicle.CalculateRentCost(5));
            }
        }
    }
}
