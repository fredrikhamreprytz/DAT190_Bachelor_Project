using System;
using SkiaSharp;
namespace DAT190_Bachelor_Project.Model
{
    public class Flight : IEmission
    {
        public Flight()
        {
            this.Color = SKColor.Parse("FFB0F6");
            this.SVGIcon = "M58,73.5 C58,73.5 152.5,26.5 200,3.5 C227,-9 251.5,17 227,35.5 C219,41.5 187,56.5 187,56.5 L149,162.5 L124,175.5 L132,83.5 C132,83.5 67,113.5 45,107.5 C26.5,102.5 0,64.5 0,64.5 L9,57.5 L58,73.5 M55,15.5 L95,43.5 L136,23.5 L76,3.5 L55,15.5 Z";
            this.KgCO2 = CalculateCO2(94);
        }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double KgCO2 { get; set; }
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SKColor Color { get; set; }
        public string SVGIcon { get; set; }

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

            return 169.68;
        }
    }
}
