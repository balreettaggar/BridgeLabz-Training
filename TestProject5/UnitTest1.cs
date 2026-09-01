

using NUnit.Framework;
using System;
using System.IO;
using Week5Review;

[TestFixture]
public class ParkingManagerTests
{
    ParkingFacilityManager parkingManager = new ParkingFacilityManager();

    [Test]
    public void EmptyEligibility()
    {
        var space = new ParkingSpace {IsOccupied = false};
        var eligibility = parkingManager.Eligibility();
        bool result = eligibility(space);
        Assert.That(result, Is.True);
    }

    [Test]
    public void OccupiedEligibilty()
    {
        var space = new ParkingSpace{IsOccupied = true};
        var eligibility = parkingManager.Eligibility();
        bool result = eligibility(space);
        Assert.That(result, Is.False);
    }
}