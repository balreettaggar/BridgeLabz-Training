using NUnit.Framework;
using System;
using Week5Review;

namespace TestProject5
{
    public class UnitTest1
    {
        private ParkingFacilityManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new ParkingFacilityManager();
        }
        [Test]
        public void PricingRule_ReturnsBaseRate()
        {
 
            var rule = manager.CreateOccupancyPricingRule(100, 90);
            Assert.AreEqual(100, rule(50));
        }

        [Test]
        public void PricingRule_ReturnsHigherRateAtThreshold()
        {
            
            var rule = manager.CreateOccupancyPricingRule(100, 90);
            Assert.AreEqual(150, rule(90));
        }

        [Test]
        public void EVVehicle_GetsEVSpace()
        {
            manager.AddSpace(new ParkingSpace(1, false, false, false));
            manager.AddSpace(new ParkingSpace(2, false, true, false));
            int space = manager.NearestSpaceID(101, true);
            Assert.AreEqual(2, space);
        }

        [Test]
        public void EVVehicle_ThrowsWhenNoEVSpace()
        {
            manager.AddSpace(new ParkingSpace(1, false, false, false));
            Assert.Throws<NoAvailableSpaceException>(() => manager.NearestSpaceID(101, true));
        }

        [Test]
        public void EntryExit_CalculatesFee()
        {
            manager.AddSpace(new ParkingSpace(1, false, false, false));
            manager.EnterVehicle(101, false,false,DateTime.Now,100,90);
            decimal fee =manager.ExitVehicle(101,DateTime.Now.AddHours(2),TimeSpan.FromMinutes(30));
            Assert.AreEqual(200, (int)fee);
        }

        [Test]
        public void OverstayRule_ReturnsTrue()
        {
            VehicleSession session =new VehicleSession( 101,1,DateTime.Now.AddHours(-2),DateTime.Now);
            var rule = manager.CreateOverstayRule( TimeSpan.FromMinutes(30));
            Assert.IsTrue(rule(session));
        }

        [Test]
        public void CapacityEvent_FiresWhen90PercentReached()
        {
            bool eventCalled = false;
            manager.CapacityThresholdReach += (sender, e) => eventCalled = true;
            for (int i = 1; i <= 10; i++)
            {
                manager.AddSpace(new ParkingSpace(i, false,false,i <= 8));
            }
            manager.EnterVehicle(101,false,false,DateTime.Now, 100,90);
            Assert.IsTrue(eventCalled);
        }

        [Test]
        public void PeakHour_ReturnsCorrectHour()
        {
            manager.AddSpace(new ParkingSpace(1, false, false, false));
            manager.EnterVehicle(101,false, false,new DateTime(2026, 9, 1, 10, 0, 0),100,90);
            manager.ExitVehicle(101,new DateTime(2026, 9, 1, 11, 0, 0),TimeSpan.FromMinutes(30));
            manager.EnterVehicle(102,false, false, new DateTime(2026, 9, 1, 10, 30, 0), 100, 90);
            manager.ExitVehicle(102,new DateTime(2026, 9, 1, 11, 30, 0),
            TimeSpan.FromMinutes(30));
            Assert.AreEqual(10, manager.PeakOccupancyHour());
        }
    }
}