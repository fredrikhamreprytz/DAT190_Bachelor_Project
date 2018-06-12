using System;
using PCLAppConfig;

namespace DAT190_Bachelor_Project.Model
{
    public class Flight : IEmission
    {

        // Properties
        public int Id { get; set; }
        public double KgCO2 { get; set; }  // Total amount og CO2-emission from flights [kg]
        public DateTime Date { get; set; }
        public EmissionType Type { get; set; }


        // Variables and constants used in emission calculation
        private double shortHaulTreshold = 1000;    // [NOK]
        private double longHaulTreshold = 4000;     // [NOK]
        private double flightDistance;              // [km]


        // Constructor
        public Flight()
        {
            this.Type = EmissionType.Flight;
        }

        // Methods
        public double CalculateCO2(double ticketPrice)
        {
            if (ticketPrice < shortHaulTreshold) 
            {
                flightDistance = 1.2995 * ticketPrice - 415.88;
            }
            else if (ticketPrice >= shortHaulTreshold && ticketPrice < longHaulTreshold)
            {
                flightDistance = 1.963 * ticketPrice - 1237;
            }
            else if (ticketPrice >= longHaulTreshold)
            {
                flightDistance = 5.9277 * ticketPrice - 22568;
            }
            else 
            {
                flightDistance = 0;
            }
            double co2Emission = 0.0736 * flightDistance + 37.755;
            this.KgCO2 += co2Emission;
            return co2Emission;
        }

        public string BiggestEmissionFactorDescription() 
        {
            // TODO: Metoden skal sjekke hvilken enkelt transaksjon eller like transaksjoner
            // som bidrar mest til de totale utslippene for kategorien.
            string description = "29.01.18: Flyreise tilsvarende Oslo - Los Angeles. 328 kg CO2.";
            return description;
        }

        public string SimplestEmissionReductionMeasure() 
        {
            string description = "Det ser ut som at du har en del jobbreiser mellom Bergen og Oslo. Nattoget er et fint alternativ, og du er førstemann på jobb om morgenen. Ved å erstatte morgenflyet med natttog kan du redusere flyutslippene dine med 323 kg CO2, eller 40% i året";
            return description;
        }
    }
}
