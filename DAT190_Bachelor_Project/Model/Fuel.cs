using System;
using SkiaSharp;
namespace DAT190_Bachelor_Project.Model
{
    public class Fuel : IEmission
    {
        // Properties
        public Vehicle Vehicle { get; set; }
        public int Id { get; set; }
        public double KgCO2 { get; set; }
        public DateTime Date  { get; set; }
        public SKColor Color { get; set; }
        public string SVGIcon { get; set; }
        public double PricePerLitreFuel { get; set; }

        // Constructor
        public Fuel(Vehicle Vehicle)
        {
            this.Vehicle = Vehicle;
            this.Color = SKColor.Parse("E894B4");
            this.SVGIcon = "M0,57 L37.5,57 L83.5,0 L184.5,0 L228.5,56 C228.5,56 289.5,52 295.5,97 L296.5,136.5 L261.5,136.5 C258,93 190,88 184.5,136.5 L113.5,136.5 C113.5,98.5 41,84.5 35.5,136.5 C35.5,136.5 35.5,138.5 0,136.5 L0,57 M67,57 L93,23 L126,23 L125.5,57 L67,57 M149,57 L148.5,23 L173.5,23 L200.5,57 L149,57 M48,142.5 C48,128 60,116.5 74,116.5 C88.5,116.5 100,128 100,142.5 C100,156.5 88.5,168.5 74,168.5 C60,168.5 48,156.5 48,142.5 M196.5,142.5 C196.5,128 208,116.5 222.5,116.5 C237,116.5 248.5,128 248.5,142.5 C248.5,156.5 237,168.5 222.5,168.5 C208,168.5 196.5,156.5 196.5,142.5 Z";
            this.KgCO2 = CalculateCO2(1234);
        }

        // Calculate CO2-Emission from purchase of fuel and vehicle type
        public double CalculateCO2(double amount)
        {
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
            double litreOfFuel = amount / PricePerLitreFuel;
            double kmFromFuel = litreOfFuel / Vehicle.FuelConsumptionPerKm;
            double emission = kmFromFuel * Vehicle.FuelConsumptionPerKm;
            this.KgCO2 = emission;
            return emission;
            }
        }
    }

