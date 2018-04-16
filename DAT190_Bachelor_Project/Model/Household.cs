using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Household : IEmission
    {

        public Household()
        {
        }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double KgCO2 { get; set; }
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double CalculateCO2(double Amount)

        {
            // CO2-ekvivalent per kW/h in Norway from electricitymap.org
            double KgCO2EkvPerkWh = 0.016;

            // Price per kW/h from ssb.no
            // Last updated 4. quarter 2017
            // Price in NOK
            double PricePerKWh = 0.36;
            KgCO2 = (Amount / PricePerKWh) * KgCO2EkvPerkWh;

            return KgCO2;
        }
    }
}
