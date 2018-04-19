using System;
namespace DAT190_Bachelor_Project.Model
{
    public class CarbonFootprint
    {

        public IEmission[] emissions { private set; get; }

        public CarbonFootprint()
        {
            Vehicle StandardVehicle = new Vehicle("AA 12345", VehicleSize.Medium, FuelType.Petrol, 0.7);
            IEmission fuelEmissions = new Fuel(1, StandardVehicle);
            IEmission flightEmissions = new Flight();
            IEmission householdEmissions = new Household();

            emissions = new IEmission[]
            {
                fuelEmissions,
                flightEmissions,
                householdEmissions
            };
        }

    }
}
