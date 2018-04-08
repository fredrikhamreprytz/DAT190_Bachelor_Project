using Xamarin.Forms;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project
{
    public partial class FrontPage : ContentPage
    {
        public FrontPage()
        {
            InitializeComponent();
            IEmission carEmissions = new Car();
            IEmission flightEmissions = new Flight();
            IEmission householdEmissions = new Household();

            carTravelEmissionsValueLabel.Text = carEmissions.CalculateCO2().ToString("0.#");
            airplaneTravelEmissionsValueLabel.Text = flightEmissions.CalculateCO2().ToString("0.#");
            householdEmissionsValueLabel.Text = householdEmissions.CalculateCO2().ToString("0.#");

            totalCarbonFootprintLabel.Text = (carEmissions.CalculateCO2() + flightEmissions.CalculateCO2() + householdEmissions.CalculateCO2()).ToString("0.0");
        }
    }
}
