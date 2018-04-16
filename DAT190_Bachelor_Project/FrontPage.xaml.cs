using Xamarin.Forms;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project
{
    public partial class FrontPage : ContentPage
    {
        public FrontPage()
        {
            InitializeComponent();

            // Standard vehicle
            Vehicle StandardVehicle = new Vehicle("AA 12345", VehicleSize.Medium, FuelType.Petrol, 0.7);

            IEmission fuelEmissions = new Fuel(1, StandardVehicle);
            IEmission flightEmissions = new Flight();
            IEmission householdEmissions = new Household();

            carTravelEmissionsValueLabel.Text = fuelEmissions.CalculateCO2(500.00).ToString("0.#");
            airplaneTravelEmissionsValueLabel.Text = flightEmissions.CalculateCO2(2000.00).ToString("0.#");
            householdEmissionsValueLabel.Text = householdEmissions.CalculateCO2(1000.00).ToString("0.#");

            totalCarbonFootprintLabel.Text = (fuelEmissions.CalculateCO2(500.00) + flightEmissions.CalculateCO2(2000.00) + householdEmissions.CalculateCO2(1000.00)).ToString("0.0");
        }
    }
}
