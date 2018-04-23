using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project.UnitTests
{

    public class FlightTests
    {
        

        public FlightTests()
        {
            
        }

        [SetUp]
        public void BeforeEachTest()
        {
            
        }

        [Test]
        public void CalculateCO2ZeroForZero()
        {
            Flight flight = new Flight();
            Equals(0, flight.CalculateCO2(0));
        }
    }
}
