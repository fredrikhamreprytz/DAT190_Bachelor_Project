using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using DAT190_Bachelor_Project.Model;
using PCLAppConfig;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAT190_Bachelor_Project.View
{

    // View som skal høre til view-model (FrontPage.xaml.cs), og tar imot et object fra model (CO2Emissions) laget
    // Test[] erstattes av CO2Emissions object som inneholder en instans av Fuel, Flight, House etc..
    // For at denne blir enklest mulig bør IEmission implementasjoner ha en liste med rådata, typ en samling av floats?
    // og ha en property som er den samlede utregningen av disse (TotalFuelEmissionsInKg) elns.
    public class EmissionsCakePainter
    {
        public SKCanvas canvas;
        public CakeOrientation CakeOrientation { get; set; }

        public EmissionsCakePainter(CakeOrientation Cake, SKCanvas canvas) 
        {
            this.canvas = canvas;
            this.CakeOrientation = Cake;
            canvas.Clear();
        }


        public void DrawIcons() {

            // Define paint
            SKPaint iconPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                IsAntialias = true
            };

            SKPaint backgroundPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                IsAntialias = true
            };

            int i = 0;

            // Loop through all pieces of cake
            foreach (PieceOfCake Slice in CakeOrientation.PiecesOfCake)
            {

                if (Slice.Emission.KgCO2/CakeOrientation.TotalValues > 0.04) 
                {
                    SKPoint iconCenter = Slice.Icon.Center;
                    float iconRadius = Slice.Icon.Radius;

                    // Define drop shadow toward center of cake
                    backgroundPaint.ImageFilter = Slice.Icon.DropShadow;

                    backgroundPaint.Color = Slice.Color;

                    // Draw path
                    canvas.Save();
                    canvas.DrawCircle(iconCenter, iconRadius, backgroundPaint);
                    canvas.Restore();

                    // Create path from SVG
                    SKPath icon = Slice.Icon.SVG;

                    // Draw path
                    canvas.DrawPath(icon, iconPaint);
                }
                i++;
            }
        }


        public void DrawCenterHole() {

            // Define paint
            SKPaint fillPaint = new SKPaint
            {
                Color = SKColor.Parse("#e6e6e6"),
                IsAntialias = true
            };

            float thickness = CakeOrientation.Thickness;
            float radius = CakeOrientation.Radius;
                
            // Draw path
            canvas.Save();
            canvas.DrawCircle(CakeOrientation.Center.X , CakeOrientation.Center.Y, radius - thickness, fillPaint);
            canvas.Restore();
        }


        public void DrawText() {

            int fontSize = 80;

            // Define paint
            SKPaint paint = new SKPaint
            {
                Color = SKColors.DimGray,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = fontSize,
                Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Bold,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright)
            };

            var newCenter = CakeOrientation.Center;
            newCenter.Y += fontSize / 2*0.75f;

            // Draw Text
            int totalCO2 = (int)CakeOrientation.TotalValues;
            canvas.DrawText(totalCO2.ToString(), newCenter, paint);
        }


        public void DrawCake() {

            SKPoint Center = CakeOrientation.Center;
            float CakeRadius = CakeOrientation.Radius;

            // Define bounds for arc
            SKRect rect = new SKRect(Center.X - CakeRadius, Center.Y - CakeRadius, Center.X + CakeRadius, Center.Y + CakeRadius);

            // Define paint
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            // Loop through emission categories
            foreach (PieceOfCake Slice in CakeOrientation.PiecesOfCake)
            {

                // Define part of arc
                SKPath path = new SKPath();
                paint.Color = Slice.Color;
                path.MoveTo(Center);
                path.ArcTo(rect, Slice.StartAngle, Slice.ArcAngle, false);
                path.Close();

                // Draw path
                canvas.Save();
                canvas.DrawPath(path, paint);
                canvas.Restore();
            }
        }
    }
}
