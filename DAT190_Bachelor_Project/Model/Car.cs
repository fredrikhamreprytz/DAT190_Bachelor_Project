using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Car : IEmission
    {
        public Car()
        {
        }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double KgCO2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double CalculateCO2()
        {
            return 24.89;
        }
    }
}
