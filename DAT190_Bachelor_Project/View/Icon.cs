using System;
using SkiaSharp;
using PCLAppConfig;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project.View
{
    public class Icon
    {
        public float Radius { get; set; }
        public float Offset { get; set; }
        public SKPoint Center { get; set; }
        public SKPath SVG { get; set; }
        public SKColor Color { get; set; }
        public SKImageFilter DropShadow { get; set; }


        public Icon(IEmission emission, SKPoint cakeCenter, float radius, float offset, SKPoint center)
        {
            Radius = radius;
            Offset = offset;
            Center = center;
            SVG = CakeUtil.GetSVGPath(emission);
            Color = CakeUtil.GetColor(emission);
            DropShadow = SKImageFilter.CreateDropShadow(-(Center.X - cakeCenter.X) / 12, -(Center.Y - cakeCenter.Y) / 12, 9, 9, SKColors.Black.WithAlpha(50), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);
            ScaleSVGPath();
            CenterSVGPath();
        }


        private void ScaleSVGPath() {
            // Scale down to fit inside of "dot"
            SKRect iconBounds = new SKRect();
            SVG.GetBounds(out iconBounds);
            float iconMax = Math.Max(iconBounds.Width, iconBounds.Height);
            float iconWidth = (float)Math.Sqrt(2 * Math.Pow(Radius, 2));
            var iconScale = iconWidth / iconMax;
            SKMatrix scaleMatrix = SKMatrix.MakeScale(iconScale, iconScale);
            SVG.Transform(scaleMatrix);
        }


        private void CenterSVGPath() {
            // Move icon to center of "dot"
            SKRect iconBounds = new SKRect();
            SVG.GetBounds(out iconBounds);
            SKMatrix translate = SKMatrix.MakeTranslation(Center.X - iconBounds.Width / 2, Center.Y - iconBounds.Height / 2);
            SVG.Transform(translate);
        }
    }
}
