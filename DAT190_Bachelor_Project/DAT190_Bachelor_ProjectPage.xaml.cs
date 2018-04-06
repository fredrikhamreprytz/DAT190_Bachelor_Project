using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class DAT190_Bachelor_ProjectPage : ContentPage
    {
        public DAT190_Bachelor_ProjectPage()
        {
            InitializeComponent();

            // Create button to display CO2-footprint
            Button ShowCO2Button = new Button();
            ShowCO2Button.Text = "Show my CO2 - Footprint";
            ShowCO2Button.VerticalOptions = LayoutOptions.Center;
            ShowCO2Button.HorizontalOptions = LayoutOptions.Center;

            // Setting page content
            Content = ShowCO2Button;


        }
    }
}
