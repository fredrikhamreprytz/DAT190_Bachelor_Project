using System;
namespace DAT190_Bachelor_Project.Model
{
    public class Vehicle
    {
        // Properties
        public string RegistrationNumber { get; set; }
        public VehicleSize Size { get; set; }
        public FuelType FuelType { get; set; }
        public double FuelConsumptionPerKm { get; set; }
        public double AverageCO2EmissionPerKm { get; set; }

        // Constructor
        public Vehicle(string registrationNumber, VehicleSize size, FuelType fuelType, double consumptionPerKm )
        {
            this.RegistrationNumber = registrationNumber;
            this.Size = size;
            this.FuelType = fuelType;
            this.FuelConsumptionPerKm = consumptionPerKm;

            // If vehicle is motorbike, fuel type must be petrol
            if (size == VehicleSize.Motorbike && FuelType != FuelType.Petrol)
            {
                this.FuelType = FuelType.Petrol;
            }
            SetAvergageCO2EmissionPerKm(FuelType, Size);
        }

        // Methods
        public void SetAvergageCO2EmissionPerKm(FuelType type, VehicleSize size) {

            // All numbers used in the calculation are provided by the UK Government
            // Conversion Factors for greenhouse gas (GHG) reporting (2017).

           // Fuel type is Petrol
            if (type == FuelType.Petrol) {
                switch (size)
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
            else if (type == FuelType.Diesel) {
                    switch (size)
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
            else if (type == FuelType.PlugInHybrid) {
                switch (size)
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

    }
}
