using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using DAT190_Bachelor_Project.Model;
namespace DAT190_Bachelor_Project.View
{

    // View som skal høre til view-model (FrontPage.xaml.cs), og tar imot et object fra model (CO2Emissions) laget
    // Test[] erstattes av CO2Emissions object som inneholder en instans av Fuel, Flight, House etc..
    // For at denne blir enklest mulig bør IEmission implementasjoner ha en liste med rådata, typ en samling av floats?
    // og ha en property som er den samlede utregningen av disse (TotalFuelEmissionsInKg) elns.
    public class EmissionsCakePainter
    {
        float cakeRadius;
        public float iconRadius { get; set; }
        public float scale { get; set; }
        float canvasHeight;
        float canvasWidth;
        float totalValues;
        float thickness;
        float iconSpacing;
        public SKPoint[] IconCenterList { get; set; }
        SKCanvas canvas;
        SKPoint center;
        IEmission[] Emissions;

        public EmissionsCakePainter(float iconRadius, float iconSpacing, float thickness, float canvasHeight, float canvasWidth, SKPaintSurfaceEventArgs e, CarbonFootprint Co2) 
        {
            
            this.cakeRadius = Math.Min(canvasWidth / 2, canvasHeight / 2) - 1.75f * iconRadius;
            this.iconRadius = iconRadius;
            this.thickness = thickness;
            this.iconSpacing = iconSpacing;
            this.canvasHeight = canvasHeight;
            this.canvasWidth = canvasWidth;
            this.canvas = e.Surface.Canvas;
            this.center = new SKPoint(canvasWidth / 2, canvasHeight / 2);
            this.Emissions = Co2.Emissions;

            scale = (float)(e.Info.Width / canvasWidth);
            canvas.Scale(scale);
            canvas.Clear();

            foreach (IEmission Emission in Emissions)
            {
                this.totalValues += (float)Emission.KgCO2;
            }

            CalculateCoordinates();

        }

        private void CalculateCoordinates() {

            this.IconCenterList = new SKPoint[this.Emissions.Length];
            float startAngle = 0;
            int i = 0;

            foreach (IEmission Emission in Emissions)
            {
                float sweepAngle = 360f * (float)Emission.KgCO2 / totalValues;

                // Calculate coordinates for icons
                float offsetAngle = startAngle + (sweepAngle / 2);
                float offsetX = (cakeRadius + iconSpacing) * (float)Math.Cos(offsetAngle * Math.PI * 2 / 360);
                float offsetY = (cakeRadius + iconSpacing) * (float)Math.Sin(offsetAngle * Math.PI * 2 / 360);
                IconCenterList[i] = new SKPoint(center.X + offsetX, center.Y + offsetY);
                startAngle += sweepAngle;
                i++;
            }
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

            // Loop through all emission categories
            foreach (IEmission Emission in Emissions)
            {

                if (Emission.KgCO2/totalValues > 0.01) 
                {
                    SKPoint iconCenter = IconCenterList[i];

                    // Define drop shadow toward center of cake
                    backgroundPaint.ImageFilter = SKImageFilter.CreateDropShadow(-(iconCenter.X - center.X) / 12, -(iconCenter.Y - center.Y) / 12, 9, 9, SKColors.Black.WithAlpha(50), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);
                    backgroundPaint.Color = Emission.Color;

                    // Draw path
                    canvas.Save();
                    canvas.DrawCircle(iconCenter, iconRadius, backgroundPaint);
                    canvas.Restore();

                    // Create path from SVG
                    SKPath icon = SKPath.ParseSvgPathData(Emission.SVGIcon);

                    // Scale down to fit inside of "dot"
                    SKRect iconBounds = new SKRect();
                    icon.GetBounds(out iconBounds);
                    float iconMax = Math.Max(iconBounds.Width, iconBounds.Height);
                    float iconWidth = (float)Math.Sqrt(2 * Math.Pow(iconRadius, 2));
                    var iconScale = iconWidth / iconMax;
                    SKMatrix scaleMatrix = SKMatrix.MakeScale(iconScale, iconScale);
                    icon.Transform(scaleMatrix);

                    // Move icon to center of "dot"
                    icon.GetBounds(out iconBounds);
                    SKMatrix translate = SKMatrix.MakeTranslation(iconCenter.X - iconBounds.Width / 2, iconCenter.Y - iconBounds.Height / 2);
                    icon.Transform(translate);

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
                
            // Draw path
            canvas.Save();
            canvas.DrawCircle(canvasWidth / 2, canvasHeight / 2, cakeRadius - thickness, fillPaint);
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

            var newCenter = center;
            newCenter.Y += fontSize / 2*0.75f;

            // Draw Text
            int totalCO2 = (int)totalValues;
            canvas.DrawText(totalCO2.ToString(), newCenter, paint);
        }

        public void DrawCake() {
            
            // Define bounds for arc
            SKRect rect = new SKRect(center.X - cakeRadius, center.Y - cakeRadius, center.X + cakeRadius, center.Y + cakeRadius);
            float startAngle = 0;

            // Define paint
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            // Loop through emission categories
            foreach (IEmission Emission in Emissions)
            {
                float sweepAngle = 360f * (float)Emission.KgCO2 / totalValues;

                // Define part of arc
                SKPath path = new SKPath();
                paint.Color = Emission.Color;
                path.MoveTo(center);
                path.ArcTo(rect, startAngle, sweepAngle, false);
                path.Close();

                // Draw path
                canvas.Save();
                canvas.DrawPath(path, paint);
                canvas.Restore();

                startAngle += sweepAngle;
            }
        }




    }
}
