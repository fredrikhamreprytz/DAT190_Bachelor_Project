using System;
using SkiaSharp;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project.Bakery
{
    public class PieceOfCake
    {
        public float StartAngle { get; set; }
        public float EndAngle { get; set; }
        public float ArcAngle { get; set; }
        public IEmission Emission { get; set; }
        public SKColor Color { get; set; }
        public Icon Icon { get; set; }
        public SKPoint CakeCenter { get; set; }


        public PieceOfCake(IEmission emission, SKPoint cakeCenter, float startAngle, float sweepAngle, float cakeRadius, float iconSpacing, float iconRadius)
        {
            Emission = emission;
            StartAngle = startAngle;
            EndAngle = startAngle + sweepAngle;
            ArcAngle = EndAngle - StartAngle;
            Color = CakeUtil.GetColor(emission);
            CakeCenter = cakeCenter;
            // Calculate coordinates for icons
            float offsetAngle = StartAngle + (sweepAngle / 2);
            float offsetX = (cakeRadius + iconSpacing) * (float)Math.Cos(offsetAngle * Math.PI * 2 / 360);
            float offsetY = (cakeRadius + iconSpacing) * (float)Math.Sin(offsetAngle * Math.PI * 2 / 360);
            SKPoint IconCenter = new SKPoint(cakeCenter.X + offsetX, cakeCenter.Y + offsetY);
            Icon = new Icon(emission, cakeCenter, iconRadius, iconSpacing, IconCenter);
        }
    }
}