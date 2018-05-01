using System;
using SkiaSharp;
using SQLite;

namespace DAT190_Bachelor_Project.Model
{
    public interface IEmission
    {
        // Properties
        int Id { get; set; }
        double KgCO2 { get; set; }
        DateTime Date { get; set; }
        SKColor Color { get; set; }
        string Name { get; set; }
        string SVGIcon { get; set; }

        // Methods
        double CalculateCO2(double amount);
        string BiggestEmissionFactorDescription();
        string SimplestEmissionReductionMeasure();

    }
}
