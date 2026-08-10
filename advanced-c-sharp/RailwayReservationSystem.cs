using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace advanced_c_sharp
{

    public interface IPaymentGateway
    {
        public void Payment();
    }

    public abstract class OreRailway
    {
        private double timing;
        public string Name { get; private set; }
        public void getTiming()
        {
            Console.WriteLine("The timing of this train is as follow");
        }

        public void SetTiming(double timing)
        { 
            if (timing < 0.0 || timing > 24.00)
                {
                    throw new ArgumentException("FILL OUT THE CORRECT TIMING");
                }

            this.timing = timing;
        }
        public string Destination { get; private set; }

        public OreRailway(string name, double timing, string destination)
        {
            Name = name;
            SetTiming(timing);
            Destination = destination;
        }

        public abstract void OreMaterial();
    }

    public class IronMaterial : OreRailway
    {
        public string Material { get; private set; }
        public IronMaterial(string name, double timing, string destination, string material) : base(name, timing, destination)
        {
            Material = material;
        }
        public override void OreMaterial()
        {
            Console.WriteLine($"This train {Name} carries {Material} to {Destination}");
        }
    }

    public abstract class PassengerTrain
    {
        public string Name { get; private set; }
        public double Timing { get; private set; }
        public string Destination { get; private set; }
        public int Hours { get; private set; }

        protected PassengerTrain(string name, double timing, string destination, int hours)
        {
            Name = name;
            Timing = timing;
            Destination = destination;
            Hours = hours;
        }

        public virtual void CoachFacility() { }
    }


    public class ThirdAc :  PassengerTrain, IPaymentGateway
    {
        public double TicketAmount { get; }

        public ThirdAc(string name, double timing, string destination, int hours) : base(name, timing, destination, hours)
        {
       
            TicketAmount = (double)hours * 300;
        }
        public void Payment()
        {
            Console.WriteLine($"The net amount of payment is {TicketAmount}");
        }

        public override void CoachFacility()
        {
            Console.WriteLine("You are going to get listed facilities");
            Console.WriteLine("Three Time meal with veg and non veg menu");
            Console.WriteLine("Snacks and tea");
            Console.WriteLine("washed and cleaned bedsheets and pillows");
        }

        public void Ac_coach(string name)
        {
            Console.WriteLine("You have been assigned with {name} Ac Coach");
        }
    }

    public class Passenger
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        
        public string Destination { get; private set; }
        
        public int Hours { get; private set; }

        public Passenger(string name, int age, string destination, int hours)
        {
            Name = name;
            Age = age;
            Destination = destination;
            Hours = hours;
        }
    }
    public class General : PassengerTrain, IPaymentGateway
    {
        public double TicketPrice { get; }
        public int SeatNo { get;  set; }
        List<Passenger> passengers;
        Dictionary<Passenger, int> detail;
        public General(string name, double timing, string destination, int hours, int seatno) : base(name, timing, destination, hours)
        {
            TicketPrice = 100;
            passengers = new List<Passenger>();
            detail = new Dictionary<Passenger, int>();
            SeatNo = seatno;
        }

        public void SeatConfirmed(Passenger passenger)
        {
            //if (detail.ContainsKey(passenger.SeatNo))
            //{
            //    Console.WriteLine($"The {SeatNo} is already taken");
            //}
            Console.WriteLine($"Dear {Name}, your seat number {SeatNo} has been confirmed");
            passengers.Add(passenger);
        }

        public void Payment()
        {
            Console.WriteLine($"The total bill amounts to {Hours*TicketPrice}");
        }

        public override void CoachFacility()
        {
            Console.WriteLine("The facilities are as follows ");
            Console.WriteLine("proper comfortable seats");
            Console.WriteLine("Hygenic washrooms");
        }
    }

    internal class RailwayReservationSystem
    {
        public static void RailwayReservationSystemMethod()
        {
            OreRailway Rail1 = new IronMaterial("FE-express", 8.00, "Delhi", "Iron");

            PassengerTrain Rail2 = new ThirdAc("Rajdhani", 16.30, "Mumbai", 5);

            PassengerTrain Rail3 = new General("Local-Train", 19.00, "Rajpura", 3, 17);

            ThirdAc Rail4 = new ThirdAc("Rajdhani", 16.30, "Mumbai", 5);

            Console.WriteLine(Rail1.Name);
            Rail2.CoachFacility();
            Rail4.Payment();
        }
    }
}
