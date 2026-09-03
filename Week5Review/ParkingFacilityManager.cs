using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Week5Review
{
    public class ReservedForAttribute : Attribute
    {
        public string Type { get; set; }
        public ReservedForAttribute() : base() {}
        public ReservedForAttribute(string type)
        {
            Type = type;
        }
    }

    public class PermitRequiredAttribute : Attribute
    {
        public bool HasPermit { get; set; }
        public PermitRequiredAttribute() : base()
        {
        }
        public PermitRequiredAttribute(bool hasPermit)
        {
            HasPermit = hasPermit;
        }
    }

    public class VehicleSession
    {
        public int VehicleId { get; set; }
        public int SpaceId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public bool HasPermit { get; set; }
        public bool IsEV { get; set; }
        public decimal Fee { get; set; }

        public VehicleSession() : this(0, 0, DateTime.Now, null)
        {
        }
        public VehicleSession(int vehicleId, int spaceId, DateTime entryTime, DateTime? exitTime)
        {
            VehicleId = vehicleId;
            SpaceId = spaceId;
            EntryTime = entryTime;
            ExitTime = exitTime;
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

    public class NoAvailableSpaceException : Exception
    {
        public int VehicleId { get; set; }
        public string RequestedCategory { get; set; }
        public NoAvailableSpaceException(int vehicleId, string requestedCategory) : base($"No available space for vehicle {vehicleId}")
        {
            VehicleId = vehicleId;
            RequestedCategory = requestedCategory;
        }
    }

    public class PermitViolationException : Exception
    {
        public PermitViolationException(string message) : base(message)
        {
        }
    }

    public class GateController
    {
        public bool IsOpen { get; private set; }
        public void Open()
        {
            IsOpen = true;
        }
        public void Close()
        {
            IsOpen = false;
        }
    }

    public class ParkingSession : IDisposable
    {
        private GateController gate;
        private StreamWriter billingWriter;
        private bool disposed = false;
        public List<string> DisposalOrder { get; private set; }
        public ParkingSession(GateController gate, StreamWriter billingWriter)
        {
            this.gate = gate;
            this.billingWriter = billingWriter;
            DisposalOrder = new List<string>();
        }

        public void WriteBilling(VehicleSession session, decimal fee)
        {
            billingWriter.WriteLine($"{session.VehicleId},{session.SpaceId},{fee}");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposed) return;
            try
            {
                gate.Close();
                DisposalOrder.Add("Gate");
            }
            finally
            {
                billingWriter.Flush();
                billingWriter.Close();
                DisposalOrder.Add("Billing");
                disposed = true;
            }
        }

        ~ParkingSession()
        {
            Dispose(false);
        }
    }

    public class VehicleTransaction
    {
        public int VehicleId { get; set; }
        public bool IsEV { get; set; }
        public bool HasPermit { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public double BaseRate { get; set; }
        public double Threshold { get; set; }
        public TimeSpan GraceLimit { get; set; }
        public VehicleTransaction(int vehicleId, bool isEV, bool hasPermit, DateTime entryTime,
            DateTime exitTime, double baseRate, double threshold, TimeSpan graceLimit)
        {
            VehicleId = vehicleId;
            IsEV = isEV;
            HasPermit = hasPermit;
            EntryTime = entryTime;
            ExitTime = exitTime;
            BaseRate = baseRate;
            Threshold = threshold;
            GraceLimit = graceLimit;
        }
    }
    public class ParkingFacilityManager
    {
        public event EventHandler CapacityThresholdReach;
        public event EventHandler VehicleOverStayedDetected;

        private List<ParkingSpace> myList = new List<ParkingSpace>();
        private List<VehicleSession> sessions = new List<VehicleSession>();
        private Dictionary<int, VehicleSession> openSessions = new Dictionary<int, VehicleSession>();

        private int occupiedCount = 0; 
        private bool capacityThresholdReached = false;
        public GateController LastGate { get; private set; }
        public List<string> LastDisposalOrder { get; private set; } = new List<string>();
        public void AddSpace(ParkingSpace space)
        {
            myList.Add(space);
            if (space.IsOccupied) occupiedCount++;
        }

        public decimal CalculateBilling(VehicleSession session)
        {
            double duration = CalculateDuration(session);
            if (duration < 0) throw new InvalidOperationException("Billing calculation failed");
            return session.Fee * (decimal)duration;
        }
        public Func<int, decimal> CreateOccupancyPricingRule(double baseRate, double occupancyMultiplierThreshold)
        {
            return (occupancy) =>
            {
                if (occupancy >= occupancyMultiplierThreshold) return (decimal)(baseRate * 1.5);
                return (decimal)baseRate;
            };
        }

        public bool ProcessVehicleTransaction(VehicleTransaction transaction, string billingFile)
        {
            GateController gate = new GateController();
            StreamWriter writer = new StreamWriter(billingFile, true);
            ParkingSession parkingSession = new ParkingSession(gate, writer);

            LastGate = gate;
            try
            {
                gate.Open();
                VehicleSession session = EnterVehicle(transaction.VehicleId, transaction.IsEV, transaction.HasPermit, transaction.EntryTime, 
                    transaction.BaseRate, transaction.Threshold);
                decimal fee = ExitVehicle(transaction.VehicleId, transaction.ExitTime, transaction.GraceLimit);
                parkingSession.WriteBilling(session, fee);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                parkingSession.Dispose();
                LastDisposalOrder = parkingSession.DisposalOrder.ToList();
            }
        }

        public int ProcessBatch(List<VehicleTransaction> transactions, string billingFile)
        {
            int completed = 0;

            foreach (VehicleTransaction transaction in transactions)
            {
                bool result = ProcessVehicleTransaction(transaction, billingFile);
                if (result) completed++;
            }
            return completed;
        }

        public Predicate<VehicleSession> CreateOverstayRule(TimeSpan graceLimit)
        {
            return (session) =>
            {
                if (session.ExitTime == null) return false;
                return session.ExitTime.Value - session.EntryTime > graceLimit;
            };
        }
        public Action<VehicleSession> Logging()
        {
            return (VehicleSession) => Console.WriteLine($"Vehicle Entered at {VehicleSession.EntryTime} and exited at {VehicleSession.ExitTime}");
        }
        public Predicate<ParkingSpace> Eligibility(bool isEV = false)
        {
            return (ParkingSpace) =>
            {
                if (ParkingSpace.IsOccupied) return false;
                if (isEV && !ParkingSpace.IsEVCharging) return false;
                return true;
            };
        }
        public int NearestSpaceID(int vehicleId, bool isEV)
        {
            Predicate<ParkingSpace> eligibility = Eligibility(isEV);
            var result = myList.FirstOrDefault(entry => eligibility(entry));
            if (result == null) throw new NoAvailableSpaceException(vehicleId, isEV ? "EV" : "Normal");
            return result.SpaceID;
        }

        [ReservedFor("EV")]
        [PermitRequired]
        public void PermittedVehicles()
        {
        }
        public void CheckPermit(VehicleSession session)
        {
            Type type = typeof(ParkingFacilityManager);
            MethodInfo method = type.GetMethod(nameof(PermittedVehicles));
            if (method == null)return;
            ReservedForAttribute reserved = method.GetCustomAttribute<ReservedForAttribute>();
            PermitRequiredAttribute permit = method.GetCustomAttribute<PermitRequiredAttribute>();
            if (permit != null && !session.HasPermit) throw new PermitViolationException(
                    $"Vehicle {session.VehicleId} does not have a permit");

            if (reserved != null &&reserved.Type == "EV" &&!session.IsEV)
                throw new PermitViolationException(
                    $"Vehicle {session.VehicleId} is not an EV");
        }

        public VehicleSession EnterVehicle(int vehicleId, bool isEV, bool hasPermit, DateTime entryTime,
            double baseRate, double occupancyMultiplierThreshold)
        {
            if (openSessions.ContainsKey(vehicleId))throw new InvalidOperationException($"Vehicle {vehicleId} is already inside");
            int spaceId = NearestSpaceID(vehicleId, isEV);
            ParkingSpace space = myList.First(entry => entry.SpaceID == spaceId);
            VehicleSession session = new VehicleSession(vehicleId, spaceId, entryTime, null);
            session.IsEV = isEV;
            session.HasPermit = hasPermit;
            if (space.IsReserved) CheckPermit(session);
            int occupancyPercentage = occupiedCount * 100 / myList.Count;
            Func<int, decimal> pricingRule = CreateOccupancyPricingRule(baseRate, occupancyMultiplierThreshold);
            session.Fee = pricingRule(occupancyPercentage);
            space.IsOccupied = true;
            occupiedCount++;
            openSessions.Add(vehicleId, session);
            CheckCapacity();
            Logging()(session);
            return session;
        }

        public decimal ExitVehicle(int vehicleId, DateTime exitTime, TimeSpan graceLimit)
        {
            if (!openSessions.ContainsKey(vehicleId)) throw new InvalidOperationException($"Vehicle {vehicleId} never entered");
            VehicleSession session = openSessions[vehicleId];
            session.ExitTime = exitTime;
            ParkingSpace space = myList.First(entry => entry.SpaceID == session.SpaceId);
            space.IsOccupied = false;
            occupiedCount--;
            openSessions.Remove(vehicleId);
            sessions.Add(session);
            CheckOverStay(session, graceLimit);
            Action<VehicleSession> logger = Logging();
            logger(session);
            CheckCapacity();
            return CalculateBilling(session);
        }

        public double CalculateDuration(VehicleSession session)
        {
            if (session.ExitTime == null) return 0;
            return (session.ExitTime.Value - session.EntryTime).TotalHours;
        }

        public void CheckCapacity()
        {
            if (myList.Count == 0) return;
            int percentage = occupiedCount * 100 / myList.Count;
            if (percentage >= 90 && !capacityThresholdReached)
            {
                CapacityThresholdReach?.Invoke(this, EventArgs.Empty);
                capacityThresholdReached = true;
            }

            if (percentage < 90) capacityThresholdReached = false;
        }

        public void CheckOverStay( VehicleSession session,TimeSpan graceLimit)
        {
            Predicate<VehicleSession> overstayRule =CreateOverstayRule(graceLimit);
            if (overstayRule(session))
            {
                VehicleOverStayedDetected?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<IGrouping<int, VehicleSession>> GroupSessionsByHour()
        {
            return sessions.GroupBy(session => session.EntryTime.Hour).ToList();
        }

        public int PeakOccupancyHour()
        {
            var result = sessions.GroupBy(session => session.EntryTime.Hour).OrderByDescending(group => group.Count())
             .FirstOrDefault();
            if (result == null) return -1;
            return result.Key;
        }

        public double AverageSessionDuration()
        {
            if (sessions.Count == 0) return 0;
            return sessions.Average(session => CalculateDuration(session));
        }

        public Dictionary<string, decimal> RevenueBySpaceCategory()
        {
            return sessions.GroupBy(session => session.IsEV ? "EV" : "Normal")
                .ToDictionary(group => group.Key, group => group.Sum(session =>session.Fee * (decimal)CalculateDuration(session)));
        }

        public int OccupiedCount() 
        { 
            return occupiedCount;
        }

        public int SpaceCount()
        {
            return myList.Count;
        }

        public List<VehicleSession> GetSessions()
        {
            return sessions;
        }
    }

    public class FacilityOperations
    {
        public int CapacityAlerts { get; private set; }
        public void Subscribe(ParkingFacilityManager manager)
        {
            manager.CapacityThresholdReach += CapacityMessage;
        }
        public void CapacityMessage(object sender, EventArgs e)
        {
            CapacityAlerts++;
            Console.WriteLine("Operations: Parking is near capacity");
        }
    }

    public class BillingDepartment
    {
        public int OverstayAlerts { get; private set; }
        public void Subscribe(ParkingFacilityManager manager)
        {
            manager.VehicleOverStayedDetected += OverstayMessage;
        }

        public void OverstayMessage(object sender, EventArgs e)
        {
            OverstayAlerts++;
            Console.WriteLine("Billing: Vehicle overstay detected");
        }
    }
    public class EventCalls
    {
        public static void EventCallsMethod()
        {
            ParkingFacilityManager pfm = new ParkingFacilityManager();
            pfm.CapacityThresholdReach += CapacityMessage;
            pfm.VehicleOverStayedDetected += OverStayMessage;
            pfm.AddSpace(new ParkingSpace(1, false, false, false));
            pfm.AddSpace(new ParkingSpace(2, false, true, false));
            VehicleSession session = pfm.EnterVehicle(101, true, true, DateTime.Now,100,90);
            decimal fee = pfm.ExitVehicle(101,DateTime.Now.AddHours(2),TimeSpan.FromMinutes(30));
            Console.WriteLine($"Fee: {fee}");
        }
        public static void CapacityMessage(object sender, EventArgs e)
        {
            Console.WriteLine("Alert! Parking is near capacity");
        }
        public static void OverStayMessage(object sender,EventArgs e)
        {
            Console.WriteLine("Your vehicle has been parked more than allotted time");
        }
    }
}