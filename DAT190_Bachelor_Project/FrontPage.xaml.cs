using Xamarin.Forms;
using System;
using System.Net;
using System.IO;
using Plugin.Toasts;
using DAT190_Bachelor_Project.Model;
using DAT190_Bachelor_Project.View;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using PCLAppConfig;
using DAT190_Bachelor_Project.Data;

namespace DAT190_Bachelor_Project
{
    public partial class FrontPage : ContentPage
    {

        OBPUtil obp;
        User dummyUser;
        RestService restService = new RestService();

        public FrontPage()
        {
            InitializeComponent();

            // Instantiate dummy user
            dummyUser = new User();
            dummyUser.FirstName = "Knut";
            dummyUser.Email = "knut@statoil.com";
            dummyUser.LastName = "Helland";
            dummyUser.Password = "passord";

            // Create dummy vehicle
            Vehicle dummyVehicle = new Vehicle("AA 12345", VehicleSize.Medium, FuelType.Petrol, 0.7);
            dummyUser.Vehicle = dummyVehicle;

            CarbonFootprint carbonFootprint = new CarbonFootprint();

            // Setting dependencies
            dummyUser.CarbonFootprint = carbonFootprint;
            dummyUser.CarbonFootprint.User = dummyUser;
            // Update footprint with dummy data
            dummyUser.CarbonFootprint.UpdateFootprint();

            string OBPDevKey = ConfigurationManager.AppSettings["OBPDevKey"];
            string OBPDevSecret = ConfigurationManager.AppSettings["OBPDevSecret"];

            obp = new OBPUtil("oob", OBPDevKey, OBPDevSecret);
            obp.getRequestToken(FinishWebRequest);

            dummyUser.SocialSecurityNr = "";
            dummyUser.ClientId = "";
            dummyUser.ClientSecret = "";

            // Save user to RESTapi
            // restService.SaveUserAsync(dummyUser);
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

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            float width = (float)carbonFootprintCanvas.Width;
            float height = (float)carbonFootprintCanvas.Height;

            EmissionsCakeView emissionsCake = new EmissionsCakeView(32, 6, 33, height, width, e, dummyUser.CarbonFootprint);
            emissionsCake.DrawCake();
            emissionsCake.DrawCenterHole();
            emissionsCake.DrawText();
            emissionsCake.DrawDots();
            emissionsCake.DrawIcons();

        }

        //private async void Save()
        //{
        //    // Saving to database
        //    await App.Database.SaveUserAsync(dummyUser);
        //}
        
    }


}
