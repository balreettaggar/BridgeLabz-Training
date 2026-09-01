using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Week5Review
{   
    public class ReservedForAttribute : Attribute{

        public string Type { get; set; }
        ReservedForAttribute() : base() { }
        public ReservedForAttribute(string type) {
            Type = type;
        }
    }

    internal class PermitRequiredAttribute : Attribute
    {
        public bool HasPermit { get; set; }
        PermitRequiredAttribute() : base() { }
        public PermitRequiredAttribute(bool hasPermit)
        {
            HasPermit = hasPermit;
        }
    }
public class VehicleSession
    {
        public static int VehicleId { get; set; }
        public static int SpaceId { get; set; }
        public static int EntryTime { get; set; }
        public static int ExitTime { get; set; }

        public VehicleSession() : this(0,0,0,0)
        {

        }
        public VehicleSession(int vehicleId, int spaceId, int entryTime, int exitTime)
        {
            VehicleId = vehicleId;
            SpaceId = spaceId;
            EntryTime = entryTime;
            ExitTime = exitTime;
        }

    }
    public class TimeSpan
    {
       public readonly int GraceTime;
       public TimeSpan()
       {
            GraceTime = 6;
       }
    }
    public class ParkingSpace
    {
        public int SpaceID { get; set; }
        public bool IsReserved { get; set; }
        public bool IsEVCharging { get; set; }
        public bool IsOccupied { get; set; }

        public ParkingSpace() : this(0, false, false, false)
        {
        }

        public ParkingSpace(int spaceId, bool isReserved, bool isEVCharging, bool isOccupied)
        {
            SpaceID = spaceId;
            IsReserved = isReserved;
            IsEVCharging = isEVCharging;
            IsOccupied = isOccupied;
        }

    }
    public class ParkingFacilityManager
    {
        public event EventHandler CapacityThresholdReach;
        public event EventHandler VehicleOverStayedDetected;

        public Action <VehicleSession> Logging()
        {
            return (VehicleSession) => Console.WriteLine($"Vehicle Entered at {VehicleSession.EntryTime} and" +
                $"exited at {VehicleSession.ExitTime}");
        }

        public Predicate<ParkingSpace> Eligibility()
        {
            bool result = false;
            return (ParkingSpace) =>
            {
                if (ParkingSpace.IsOccupied == false) result = true;
                return result;
            };
        }

        List<ParkingSpace> myList = new List<ParkingSpace>();
        public int NearestSpaceID()
        {
            var result = myList.FirstOrDefault(entry => entry.IsOccupied == false);
            return result.SpaceID;
        }

        [PermitRequired(false)]
        public void PermittedVehicles()
        {
            Type type = typeof(ParkingSpace);
            FieldInfo[] fields = type.GetFields(BindingFlags.Static);
            for(int i=0; i<fields.Length; i++)
            {
                //if(fields[i].isOccupied) throw new Exception("Not allowed");
            }
        }

        public void CheckCapacity()
        {
            CapacityThresholdReach?.Invoke(this, EventArgs.Empty);
        }

        public void CheckOverStay()
        {
            VehicleOverStayedDetected?.Invoke(this, EventArgs.Empty); 
        }
    }

    public class EventCalls
    {
        public static void EventCallsMethod()
        {
            ParkingFacilityManager pfm = new ParkingFacilityManager();

            
            pfm.CapacityThresholdReach += CapacityMessage;
            pfm.CheckCapacity();

            ParkingFacilityManager pfm1 = new ParkingFacilityManager();
            pfm1.VehicleOverStayedDetected += OverStayMessage;

            pfm1.CheckOverStay();
        }

        public static void CapacityMessage(object sender, EventArgs e)
        {
            Console.WriteLine("Alert! Parking is full");
        }

        public static void OverStayMessage(object sender, EventArgs e)
        {
            Console.WriteLine("Your vehicle has been parked more than alloted time");
        }
    }
}