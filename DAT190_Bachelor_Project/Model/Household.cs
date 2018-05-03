using System;
using SkiaSharp;

namespace DAT190_Bachelor_Project.Model
{
    public class Household : IEmission
    {

        // Properties
        public int Id { get; set; }
        public double KgCO2 { get; set; }
        public DateTime Date { get; set; }
        public EmissionType Type { get; set; }

        // CO2-ekvivalent per kW/h in Norway from electricitymap.org
        private double kgCO2EkvPerkWh = 0.016;

        // Price per kW/h from ssb.no
        // Last updated 4. quarter 2017, price in NOK
        private double pricePerKWh = 0.36;

        public Household()
        {
            this.Type = EmissionType.Household;
        }

        // Calculate CO2 emission from use of electricity in household
        // based on electricity bill
        public double CalculateCO2(double amount)
        {
            double emission = (amount / pricePerKWh) * kgCO2EkvPerkWh;
            this.KgCO2 = emission;
            return emission;
        }

        public string BiggestEmissionFactorDescription()
        {
            // TODO: Metoden skal sjekke hvilken enkelt transaksjon eller like transaksjoner
            // som bidrar mest til de totale utslippene for kategorien.
            string description = "12.04.18: Strømregning BKK. 78 kg CO2.";
            return description;
        }

        public string SimplestEmissionReductionMeasure()
        {
            string description = "I Norge er elektrisk oppvarming et rent alternativ. Det er vanlig å bruke mye strøm på oppvarming. Nattsenking kan hjelpe til å redusere dine husholdsutslipp med 51 kg CO2, eller 8% i året.";
            return description;
        }
    }
}
