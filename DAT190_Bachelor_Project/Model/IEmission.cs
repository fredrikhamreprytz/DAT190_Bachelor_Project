using System;
namespace DAT190_Bachelor_Project.Model
{
    public interface IEmission
    {
        // Properties
        int Id {get; set;}
        double KgCO2 { get; set; }
        DateTime Date { get; set; }

        // Methods
        double CalculateCO2(double Amount);

    }
}
