using System;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public class CarbonFootprint
    {
        [PrimaryKey]
        public int Id { get; set; }

        // Properties
        [Ignore]
        public User User { get; set; }
        public DateTime LastUpdate { get; private set; }
        [Ignore]
        public IEmission[] Emissions { private set; get; }
        public double Flight { get; private set; }
        public double Fuel { get; private set; }
        public double Household { get; private set; }

        // Constructor
        public CarbonFootprint()
        {
        }
        // Methods
        // Method that fetch the latest bank transactions 
        //and updates user's carbon footprint
        public void UpdateFootprint() {

            // TODO, only dummy data at this point

            // 1500 NOK as example of montly fuel purchase, 3 x 500 NOK
            IEmission fuelEmissions = new Fuel(User.Vehicle);
            fuelEmissions.CalculateCO2(1500);

            IEmission flightEmissions = new Flight();
            // 2500 NOK as example of montly air travel expenses
            flightEmissions.CalculateCO2(2500);

            IEmission householdEmissions = new Household();
            // 1000 NOK as example of montly electricity bill
            householdEmissions.CalculateCO2(1000);

            Emissions = new IEmission[]
            {
                fuelEmissions,
                flightEmissions,
                householdEmissions
            };
            Flight = flightEmissions.KgCO2;
            Fuel = fuelEmissions.KgCO2;
            Household = householdEmissions.KgCO2;
        }

    }
}
