using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Net;
using System.IO;
using Plugin.Toasts;
using DAT190_Bachelor_Project.Model;
using DAT190_Bachelor_Project.Bakery;
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
        CakeOrientation Cake;
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

            ToolbarItems.Add(new ToolbarItem("Statistikk", "graph_icon.png", async () => { var page = new ContentPage(); var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel"); System.Diagnostics.Debug.WriteLine("success: {0}", result); }));


            // Create dummy vehicle
            Vehicle dummyVehicle = new Vehicle("AA 12345", VehicleSize.Medium, FuelType.Petrol, 0.7);
            dummyUser.Vehicle = dummyVehicle;
            NavigationPage.SetTitleIcon(this, "logo.png");
            carbonFootprint = new CarbonFootprint();

            //titleLabel1.Text = dummyUser.FirstName;

            // Setting dependencies
            dummyUser.CarbonFootprint = carbonFootprint;
            dummyUser.CarbonFootprint.User = dummyUser;
            // Update footprint with dummy data
            dummyUser.CarbonFootprint.UpdateFootprint();

            string OBPDevKey = ConfigurationManager.AppSettings["OBPDevKey"];
            string OBPDevSecret = ConfigurationManager.AppSettings["OBPDevSecret"];

            //obp = new OBPUtil("oob", OBPDevKey, OBPDevSecret);
            //obp.getRequestToken(FinishWebRequest);

            dummyUser.SocialSecurityNr = "";
            dummyUser.ClientId = "";
            dummyUser.ClientSecret = "";

            //MainGrid.Children.Add(new EmissionHighlightView(dummyUser.CarbonFootprint.Emissions[0]), 0, 0);

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
            if (Cake == null)
            {
                Cake = new CakeOrientation(dummyUser.CarbonFootprint, carbonFootprintCanvas);
            }

            SKCanvas canvas = e.Surface.Canvas;
            float scale = (float)(e.Info.Width / carbonFootprintCanvas.Width);
            canvas.Scale(scale);
            Cake.Scale = scale;
            Cake.CanvasView = carbonFootprintCanvas;

            EmissionsCakePainter cakePainter = new EmissionsCakePainter(Cake, canvas);
            cakePainter.DrawCake();
            cakePainter.DrawCenterHole();
            cakePainter.DrawText();
            cakePainter.DrawPopover();
            cakePainter.DrawIcons();
        }

        async void Handle_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            SKPoint touchLocation = e.Location;

            float popOverWidth = (float)Math.Sqrt(2 * Math.Pow(Cake.PopOver.Radius, 2));

            float minX = Cake.Center.X - 0.5f * popOverWidth;
            float maxX = Cake.Center.X + 0.5f * popOverWidth;
            float minY = Cake.Center.Y - 0.5f * popOverWidth;
            float maxY = Cake.Center.X + 0.5f * popOverWidth;

            minX *= Cake.Scale;
            maxX *= Cake.Scale;
            minY *= Cake.Scale;
            maxY *= Cake.Scale;

            bool insideX = touchLocation.X > minX && touchLocation.X < maxX;
            bool insideY = touchLocation.Y > minY && touchLocation.Y < maxY;

            if (insideX && insideY && Cake.CurrentlySelected != null)
            {
                await Navigation.PushAsync(new EmissionDetailsPage(Cake.CurrentlySelected.Emission));
            }
            else 
            {
                PieceOfCake Slice = Cake.SelectPieceOfCake(e);
                if (Slice != null)
                {
                    //MainGrid.Children.RemoveAt(1);
                    //MainGrid.Children.Add(new EmissionHighlightView(Slice.Emission), 0, 0);
                    await Cake.AnimateSelection(Slice.Emission, 500);

                    Cake.CurrentlySelected = Slice;

                }
                else
                {
                    Cake.CurrentlySelected = null;

                }
                Cake.CanvasView.InvalidateSurface();
            }


        }

    }


}
