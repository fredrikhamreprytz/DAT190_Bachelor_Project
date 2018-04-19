using System;
using SkiaSharp;
namespace DAT190_Bachelor_Project.Model
{
    public class Household : IEmission
    {

        public Household()
        {
            this.Color = SKColor.Parse("D794E8");
            this.SVGIcon = "M239.5,120.5 L239.5,260.5 C223,260.5 203,260.5 181.5,260.5 L181.5,180.5 L100.5,180.5 L100.5,260.5 C66,260.5 40.5,260.5 40.5,260.5 L40.5,120.5 L0.5,120.5 L140.5,0.5 L280.5,120.5 L239.5,120.5 Z";
            this.KgCO2 = CalculateCO2(3192.8);
        }

        public int Id { get; set; }
        public double KgCO2 { get; set; }
        public DateTime Date { get; set; }
        public SKColor Color { get; set; }
        public string SVGIcon { get; set; }

        public double CalculateCO2(double Amount)

        {
            // CO2-ekvivalent per kW/h in Norway from electricitymap.org
            double KgCO2EkvPerkWh = 0.016;

            // Price per kW/h from ssb.no
            // Last updated 4. quarter 2017, price in NOK
            double PricePerKWh = 0.36;
            KgCO2 = (Amount / PricePerKWh) * KgCO2EkvPerkWh;

            return KgCO2;
        }
    }
}
