using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Vehicle
    {
        private string RegistrationNumber;
        private VehicleSize Size;
        public FuelType FuelType { get; set; }
        public double FuelConsumptionPerKm { get; set; }
        private double AverageCO2EmissionPerKm;


        public Vehicle(string RegistrationNumber, VehicleSize Size, FuelType FuelType, double ConsumptionPerKm )
        {
            this.RegistrationNumber = RegistrationNumber;
            this.Size = Size;
            this.FuelType = FuelType;
            this.FuelConsumptionPerKm = ConsumptionPerKm;

            // If vehicle is motorbike, fuel type must be petrol
            if (Size == VehicleSize.Motorbike && FuelType != FuelType.Petrol) {
                this.FuelType = FuelType.Petrol;
            }

        }

        public void SetAvergageCO2EmissionPerKm(FuelType Type, VehicleSize Size) {

            // All numbers used in the calculation are provided by the UK Government
            // Conversion Factors for greenhouse gas (GHG) reporting (2017).

           // Fuel type is Petrol
            if (Type == FuelType.Petrol) {
                switch (Size)
                {
                    case VehicleSize.Small:
                        AverageCO2EmissionPerKm = 0.15649;
                        break;
                    case VehicleSize.Medium:
                        AverageCO2EmissionPerKm = 0.19407;
                        break;
                    case VehicleSize.Large:
                        AverageCO2EmissionPerKm = 0.28539;
                        break;
                    case VehicleSize.Motorbike:
                        AverageCO2EmissionPerKm = 0.11662;
                        break;
                    default:
                        // Default is set to average car value
                        AverageCO2EmissionPerKm = 18568;
                        break;
                }
            } 
            // Fuel type is Diesel
            else if (Type == FuelType.Diesel) {
                    switch (Size)
                    {
                        case VehicleSize.Small:
                            AverageCO2EmissionPerKm = 0.14545;
                            break;
                        case VehicleSize.Medium:
                            AverageCO2EmissionPerKm = 0.1738;
                            break;
                        case VehicleSize.Large:
                            AverageCO2EmissionPerKm = 0.21834;
                            break;
                        default:
                            // Default is set to average car value
                            AverageCO2EmissionPerKm = 17887;
                            break;
                }
            }
            // Fuel type is Hybrid
            else if (Type == FuelType.PlugInHybrid) {
                switch (Size)
                {
                    case VehicleSize.Small:
                        AverageCO2EmissionPerKm = 0.10973;
                        break;
                    case VehicleSize.Medium:
                        AverageCO2EmissionPerKm = 0.11243;
                        break;
                    case VehicleSize.Large:
                        AverageCO2EmissionPerKm = 0.13052;
                        break;
                    default:
                        // Default is set to average car value
                        AverageCO2EmissionPerKm = 11659;
                        break;
                }
            }
        }
        public double GetAverageCO2EmissionPerKm() {
            return AverageCO2EmissionPerKm;
        }

    }
}
