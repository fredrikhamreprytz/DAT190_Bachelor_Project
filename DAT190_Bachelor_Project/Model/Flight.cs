using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Flight : IEmission
    {
        public Flight()
        {
        }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double KgCO2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double CalculateCO2(double Amount)
        {
            // Average price per km is from kiwi.com/flightpriceindex
            // Short-haul 27.92 USD, updated 2017
            // Long-haul 36.14 USD, updated 2017
            // Average cost/100km = 17.04 USD = 132.06 NOK/100 km = 1.3206 NOK/km
            // 1 USD = 7,75 NOK

            double PricePerKm = 1.3206;
            double Km = Amount / PricePerKm;
           // double KgCO2PerKm = 45346;

            return 329.68;
        }
    }
}
