using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Fuel : IEmission
    {
        public Vehicle Vehicle { get; set; }
        public int Id { get; set; }
        public double KgCO2 { get; set; }
        public DateTime Date  { get; set; }

        public Fuel(int Id, Vehicle Vehicle)

        {
            this.Id = Id;
            this.Vehicle = Vehicle;
        }
        // Calculate CO2-Emission from purchase of fuel and vehicle type
        public double CalculateCO2(double Amount)
        {
            double PricePerLitreFuel;
            // Set the average price per litre of fuel based on vehicle fuel type
            // Prices are from no.globalpetrolprices.com
            // Last updated on 09.04.2018
            // Price is in NOK

            // Fuel type is Petrol
            if (Vehicle.FuelType == FuelType.Petrol || Vehicle.FuelType == FuelType.PlugInHybrid) {
                PricePerLitreFuel = 15.96;
            } 
            // Fuel type is Diesel
            else if (Vehicle.FuelType == FuelType.Diesel) {
                PricePerLitreFuel = 14.78;
            } else {
                PricePerLitreFuel = 15.00;
            }

            double LitreOfFuel = Amount / PricePerLitreFuel;
            double KmFromFuel = LitreOfFuel / Vehicle.FuelConsumptionPerKm;
            KgCO2 = KmFromFuel * Vehicle.FuelConsumptionPerKm;

            return KgCO2;
      
            }
        }
    }

