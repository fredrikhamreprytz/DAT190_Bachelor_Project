using System;
namespace DAT190_Bachelor_Project.Model
{
    public class CarbonFootprint
    {

        public IEmission[] emissions { private set; get; }

        public CarbonFootprint()
        {
            Vehicle StandardVehicle = new Vehicle("AA 12345", VehicleSize.Medium, FuelType.Petrol, 0.7);
            IEmission fuelEmissions = new Fuel(StandardVehicle);
            // 1500 NOK as example of montly fuel purchase, 3 x 500 NOK
            fuelEmissions.CalculateCO2(1500);
            IEmission flightEmissions = new Flight();
            // 2500 NOK as example of montly air travel expenses
            flightEmissions.CalculateCO2(2500);
            IEmission householdEmissions = new Household();
            // 1000 NOK as example of montly electricity bill
            householdEmissions.CalculateCO2(1000);

            emissions = new IEmission[]
            {
                fuelEmissions,
                flightEmissions,
                householdEmissions
            };
        }

    }
}
