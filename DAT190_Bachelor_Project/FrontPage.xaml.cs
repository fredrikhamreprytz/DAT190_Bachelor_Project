using Xamarin.Forms;
using System;
using System.Net;
using System.IO;
using Plugin.Toasts;
using DAT190_Bachelor_Project.Model;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Numerics;

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

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            float width = (float)carconFootprintCanvas.Width;
            float height = (float)carconFootprintCanvas.Height;

            // get scaling and set screen density
            var scale = (float)(e.Info.Width / width);
            canvas.Scale(scale);

            canvas.Clear();

            float totalValues = 0f;

            Test[] fs =
            {
                new Test(45, SKColor.Parse("FFE6A7")),
                new Test(13, SKColor.Parse("E8A18C")),
                new Test(27, SKColor.Parse("FCA6FF")),
                new Test(19, SKColor.Parse("B1FFFC"))
            };

            foreach (Test f in fs)
            {
                totalValues += f.Value;
            }

            SKPoint center = new SKPoint(width / 2, height / 2);
            float radius = Math.Min(width / 2, height / 2) - 45;
            SKRect rect = new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);
            float startAngle = 0;

            foreach (Test f in fs)
            {
                float sweepAngle = 360f * f.Value / totalValues;

                using (SKPath path = new SKPath())
                using (SKPaint fillPaint = new SKPaint())
                {
                    path.MoveTo(center);
                    path.ArcTo(rect, startAngle, sweepAngle, false);
                    path.Close();

                    fillPaint.Style = SKPaintStyle.Fill;
                    fillPaint.Color = f.Color;
                    fillPaint.IsAntialias = true;

                    canvas.Save();

                    // Fill and stroke the path
                    canvas.DrawPath(path, fillPaint);

                    float offsetAngle = startAngle + (sweepAngle / 2);
                    float offsetX = (radius + 6) * (float)Math.Cos(offsetAngle*Math.PI*2/360);
                    float offsetY = (radius + 6) * (float)Math.Sin(offsetAngle*Math.PI*2/360);


                    SKPoint iconCenter = center;
                    iconCenter.X += offsetX;
                    iconCenter.Y += offsetY;
                    fillPaint.ImageFilter = SKImageFilter.CreateDropShadow(-offsetX/11, -offsetY/11, (float)9, (float)9, SKColors.Black.WithAlpha(50), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);
                    canvas.DrawCircle(iconCenter, 35, fillPaint);
                    canvas.Restore();
                }

                startAngle += sweepAngle;
            }

            using (SKPaint fillPaint = new SKPaint())
            {
                fillPaint.Color = SKColors.White;
                fillPaint.IsAntialias = true;
                canvas.Save();
                canvas.DrawCircle(width / 2, height / 2, radius - 40, fillPaint);

                var paint = new SKPaint
                {
                    Color = SKColors.DimGray,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = 54
                };

                center.Y += 15;

                canvas.DrawText("312,4", center, paint);
                canvas.Restore();
            }

        }
        
    }


}
