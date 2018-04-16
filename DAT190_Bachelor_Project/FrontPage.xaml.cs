using Xamarin.Forms;
using System;
using System.Net;
using System.IO;
using Plugin.Toasts;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project
{
    public partial class FrontPage : ContentPage
    {

        OBPUtil obp;

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

            obp = new OBPUtil("oob", "0yao55bdkuhka0g0ocxpsklwshfx5bh2jd3rqdbz", "qfwzyqdhmm4xz1zmqobfboa4sofnxhkoydgjsbjs");
            obp.getRequestToken(FinishWebRequest);

        }

        // ** CALLBACK METODE
        // ** Henter respons streng og åpner device browser og med OBP uri pg mottatt oauth token
        public void FinishWebRequest(IAsyncResult result)
        {
            WebResponse wr = obp.request.EndGetResponse(result);
            Stream dataStream = wr.GetResponseStream();
            wr.Dispose();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Dispose();
            dataStream.Dispose();
            System.Diagnostics.Debug.WriteLine(responseFromServer);
            string oauth_token = responseFromServer.Split('&')[0];
            Uri uri = new Uri("https://apisandbox.openbankproject.com/oauth/authorize?" + oauth_token);
            System.Diagnostics.Debug.WriteLine(uri.AbsoluteUri);

            ShowToast(responseFromServer);
        }

        private void ShowToast(string text)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = "Open Bank Project Response:",
                Description = text,
                IsClickable = true
            };

            notificator.Notify(options);
        }
        
    }
}
