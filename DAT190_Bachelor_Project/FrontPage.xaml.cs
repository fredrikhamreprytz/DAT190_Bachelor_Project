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
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;

namespace DAT190_Bachelor_Project
{
    public partial class FrontPage : ContentPage
    {

        OBPUtil obp;
        User dummyUser;
        EmissionsCakePainter cakePainter;
        RestService restService = new RestService();
        CarbonFootprint carbonFootprint;

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

            carbonFootprint = new CarbonFootprint();

            //titleLabel1.Text = dummyUser.FirstName;

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

            ShowToast("OBP response: ", responseFromServer);
        }

        private void ShowToast(string header, string text)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = header,
                Description = text,
                IsClickable = true
            };

            notificator.Notify(options);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            float width = (float)carbonFootprintCanvas.Width;
            float height = (float)carbonFootprintCanvas.Height;
            cakePainter = new EmissionsCakePainter(32, 6, 33, height, width, e, dummyUser.CarbonFootprint);

            cakePainter.DrawCake();
            cakePainter.DrawCenterHole();
            cakePainter.DrawText();
            cakePainter.DrawIcons();


        }

        void Handle_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            
            SKPoint touchLocation = e.Location;
            IEmission emissionClicked = null;
            int i = 0;
            foreach (SKPoint iconCenter in cakePainter.IconCenterList)
            {
                float minX = iconCenter.X - cakePainter.iconRadius;
                float maxX = iconCenter.X + cakePainter.iconRadius;
                float minY = iconCenter.Y - cakePainter.iconRadius;
                float maxY = iconCenter.Y + cakePainter.iconRadius;

                minX *= cakePainter.scale;
                maxX *= cakePainter.scale;
                minY *= cakePainter.scale;
                maxY *= cakePainter.scale;

                bool insideX = touchLocation.X > minX && touchLocation.X < maxX;
                bool insideY = touchLocation.Y > minY && touchLocation.Y < maxY;

                if (insideX && insideY)
                {
                    emissionClicked = carbonFootprint.Emissions[i];

                }

                i++;
            }

            if (emissionClicked != null) {
                OpenEmissionDetailsPopupPage(emissionClicked);
            }
        }

        private async void OpenEmissionDetailsPopupPage(IEmission emission)
        {
            var page = new EmissionDetailsPage(emission);
            await Navigation.PushAsync(page);
        }
    }


}
