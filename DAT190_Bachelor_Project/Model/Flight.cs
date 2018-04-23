using System;
using SkiaSharp;
namespace DAT190_Bachelor_Project.Model
{
    public class Flight : IEmission
    {
        /* Constants used in formula explained, for more information see          * myclimate.org          * Average seat number (S), average seat number across al cabin classes.           * Passenger Load Factor (PLF) is the average load factor on flights          * Detour Constant (DC) is async distance correction for detours and           * holding patterns, and inefficiencies in the air traffic control systems.           * Cargo Factor (CF), to allocate some of the emissions to cargo load such          * as freight and mail. Number used is in reality (1 - CF).           * Weight Class (WC), takes into account the average seat area in the cabin class.          * Emission Factor (EF), emission factor for combustion of jet fuel.          * PreProduction accounts for the CO2 emissions through pre-production          * of jet fuel/kerosene and fuel combustion.           * Multiplier accounts for the warming effect due to non-CO2 aircraft emissions.          * a , b and c are used in ax^2 + bx + c which is a nonlinear approximation          * for fuel emissions during landing and takeoff cycle including taxi          */

        // Properties
        public int Id { get; set; }
        public double KgCO2 { get; set; }
        public DateTime Date { get; set; }
        public SKColor Color { get; set; }
        public string SVGIcon { get; set; }

        // Constants used for emission calculation
        private double shortHaulTreshold = 1500;
        private double longHaulTreshold = 2500;
        private double s = 158.44;
        private double plf = 0.77;
        private double dc = 50.00;
        private double cf = 0.951;
        private double cw = 1.26;
        private double ef = 3.15;
        private double preProduction = 0.51;
        private double multiplier = 2.00;
        private double a;
        private double b;
        private double c;

        // Average price per km is from kiwi.com/flightpriceindex
        // Short-haul 27.92 USD, updated 2017
        // Long-haul 36.14 USD, updated 2017
        // Average cost/100km = 17.04 USD = 132.06 NOK/100 km = 1.3206 NOK/km
        // 1 USD = 7,75 NOK
        private double pricePerKm = 1.3206;

        // Constructor
        public Flight()
        {
            this.Color = SKColor.Parse("FFB0F6");
            this.SVGIcon = "M58,73.5 C58,73.5 152.5,26.5 200,3.5 C227,-9 251.5,17 227,35.5 C219,41.5 187,56.5 187,56.5 L149,162.5 L124,175.5 L132,83.5 C132,83.5 67,113.5 45,107.5 C26.5,102.5 0,64.5 0,64.5 L9,57.5 L58,73.5 M55,15.5 L95,43.5 L136,23.5 L76,3.5 L55,15.5 Z";
        }

        // Methods
        public double CalculateCO2(double amount)
        {             double km = amount / pricePerKm;              // Short og long haul flight             if(km < shortHaulTreshold) {                 a = 0.0000387871;                 b = 2.9866;                 c = 1263.42;             } else if (km > longHaulTreshold) {                 a = 0.000134576;                 b = 6.1798;                 c = 3446.20;             } else {                 a = (0.0000387871 + 0.000134576) / 2;                 b = (2.9866 + 6.1798) / 2;                 c = (1263.42 + 3446.20) / 2;             }             double emission = ((a * Math.Pow(km + dc, 2) + b * (km + dc) + c) / (s * plf)) * cf * cw * (ef * multiplier + preProduction);
            this.KgCO2 = emission;             return emission;
        }
    }
}
