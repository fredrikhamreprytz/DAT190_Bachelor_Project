using System;
using SkiaSharp;

namespace DAT190_Bachelor_Project.Model
{
    public class Fuel : IEmission
    {
        // Properties
        public int Id { get; set; }
        public double KgCO2 { get; set; }
        public DateTime Date { get; set; }
        public EmissionType Type { get; set; }

        public Vehicle Vehicle { get; set; }
        public double PricePerLitreFuel { get; set; }

        // Constructor
        public Fuel(Vehicle vehicle)
        {
            this.Vehicle = vehicle;
            this.KgCO2 = CalculateCO2(1234);
            this.Type = EmissionType.Fuel;
        }

        // Empty constructor
        public Fuel()
        {
        }

        // Calculate CO2-Emission from purchase of fuel and vehicle type
        public double CalculateCO2(double amount)
        {
            // Set the average price per litre of fuel based on vehicle fuel type
            // Prices are from no.globalpetrolprices.com
            // Last updated on 09.04.2018
            // Price is in NOK

            // Fuel type is Petrol
            if (Vehicle.FuelType == FuelType.Petrol || Vehicle.FuelType == FuelType.PlugInHybrid)
            {
                PricePerLitreFuel = 15.96;
            }
            // Fuel type is Diesel
            else if (Vehicle.FuelType == FuelType.Diesel)
            {
                PricePerLitreFuel = 14.78;
            }
            else
            {
                PricePerLitreFuel = 15.00;
            }
            double litreOfFuel = amount / PricePerLitreFuel;
            double kmFromFuel = litreOfFuel / Vehicle.FuelConsumptionPerKm;
            double emission = kmFromFuel * Vehicle.FuelConsumptionPerKm;
            this.KgCO2 = emission;
            return emission;
        }

        public string BiggestEmissionFactorDescription()
        {
            // TODO: Metoden skal sjekke hvilken enkelt transaksjon eller like transaksjoner
            // som bidrar mest til de totale utslippene for kategorien.
            string description = "21.04.18: Månedskort Skyss. 121 kg CO2";
            return description;
        }

        public string SimplestEmissionReductionMeasure()
        {
            string description = "Du er flinkere enn gjennomsnittet til å reise kollektivt. Hvis du vil redusere utslippene dine enda mer kan du erstatte buss med sykkel. Hvis du sykler istedenfor buss på sommerhalvåret kan du redusere transportutslippene dine med 53 kg CO2, eller 10% i året.";
            return description;
        }
    }
}

