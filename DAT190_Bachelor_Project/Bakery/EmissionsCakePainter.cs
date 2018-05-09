using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using DAT190_Bachelor_Project.Model;
using PCLAppConfig;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAT190_Bachelor_Project.Bakery
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

            PopOver popOver = CakeOrientation.PopOver;

            float amountFontSize = popOver.AmountFontSize;
            float unitFontSize = popOver.UnitFontSize;
            float titleFontSize = popOver.TitleFontSize;
            SKColor testColor = SKColors.DimGray;
            SKColor titleFontColor = testColor;
            SKColor amountFontColor = testColor;
            SKColor unitFontColor = testColor;


            // Define paint

            SKPaint titlePaint = new SKPaint
            {
                Color = titleFontColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = titleFontSize,
                Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Bold,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright)
            };

            SKPaint amountPaint = new SKPaint
            {
                Color = amountFontColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = amountFontSize,
                Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Bold,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright)
            };

            SKPaint unitPaint = new SKPaint
            {
                Color = unitFontColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = unitFontSize,
                Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Bold,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright)
            };

            SKPoint amountTextCenter = CakeOrientation.Center;
            SKPoint titleTextCenter = CakeOrientation.Center;
            SKPoint unitTextCenter = CakeOrientation.Center;
            float amountTextHeight = amountFontSize / 0.75f;
            float titleTextHeight = titleFontSize / 0.75f;
            amountTextCenter.Y += amountFontSize / 2 * 0.75f;
            titleTextCenter.Y -= (amountTextHeight / 2 - titleTextHeight / 2 + 5);
            unitTextCenter.Y += (amountTextHeight / 2 + 5);


            // Draw Text

            canvas.DrawText(((int)CakeOrientation.TotalValues).ToString(), amountTextCenter, amountPaint);
            canvas.DrawText("Totalt", titleTextCenter, titlePaint);
            canvas.DrawText("kg CO2", unitTextCenter, unitPaint);

        }

        public void DrawPopover() 
        {
            // Define paint

            if (CakeOrientation.CurrentlySelected != null)
            {

                SKPaint fillPaint = new SKPaint
                {
                    Color = SKColors.Transparent,
                    IsAntialias = true
                };


                PopOver popOver = CakeOrientation.PopOver;
                PieceOfCake Slice = CakeOrientation.CurrentlySelected;
                SKPath centerCircle = new SKPath();

                fillPaint.Color = CakeOrientation.CurrentlySelected.Color;
                fillPaint.ImageFilter = SKImageFilter.CreateDropShadow(1, 1, 20*popOver.Factor, 20*popOver.Factor, SKColors.Black.WithAlpha(80), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);

                float radius = popOver.Radius * popOver.Factor;
                float amountFontSize = popOver.AmountFontSize * popOver.Factor;
                float unitFontSize = popOver.UnitFontSize * popOver.Factor;
                float titleFontSize = popOver.TitleFontSize * popOver.Factor;
                SKColor testColor = SKColors.White;
                SKColor titleFontColor = testColor;
                SKColor amountFontColor = testColor;
                SKColor unitFontColor = testColor;

                //connectRect.AddRect(rect);
                centerCircle.AddCircle(CakeOrientation.Center.X, CakeOrientation.Center.Y, radius);

                // Draw path
                canvas.DrawPath(centerCircle, fillPaint);

                // Define paint

                SKPaint titlePaint = new SKPaint
                {
                    Color = titleFontColor,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = titleFontSize,
                    Typeface = SKTypeface.FromFamilyName(
                        "Arial",
                        SKFontStyleWeight.Bold,
                        SKFontStyleWidth.Normal,
                        SKFontStyleSlant.Upright)
                };

                SKPaint amountPaint = new SKPaint
                {
                    Color = amountFontColor,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = amountFontSize,
                    Typeface = SKTypeface.FromFamilyName(
                        "Arial",
                        SKFontStyleWeight.Bold,
                        SKFontStyleWidth.Normal,
                        SKFontStyleSlant.Upright)
                };

                SKPaint unitPaint = new SKPaint
                {
                    Color = unitFontColor,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = unitFontSize,
                    Typeface = SKTypeface.FromFamilyName(
                        "Arial",
                        SKFontStyleWeight.Bold,
                        SKFontStyleWidth.Normal,
                        SKFontStyleSlant.Upright)
                };

                SKPoint amountTextCenter = CakeOrientation.Center;
                SKPoint titleTextCenter = CakeOrientation.Center;
                SKPoint unitTextCenter = CakeOrientation.Center;
                float amountTextHeight = amountFontSize / 0.75f;
                float titleTextHeight = titleFontSize / 0.75f;
                amountTextCenter.Y += amountFontSize / 2 * 0.75f;
                titleTextCenter.Y -= (amountTextHeight / 2 - titleTextHeight / 2 + 5);
                unitTextCenter.Y += (amountTextHeight / 2 + 5);


                // Draw Text

                canvas.DrawText(((int)Slice.Emission.KgCO2).ToString(), amountTextCenter, amountPaint);
                canvas.DrawText(CakeUtil.GetTitle(Slice.Emission), titleTextCenter, titlePaint);
                canvas.DrawText("kg CO2", unitTextCenter, unitPaint);
            }
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
