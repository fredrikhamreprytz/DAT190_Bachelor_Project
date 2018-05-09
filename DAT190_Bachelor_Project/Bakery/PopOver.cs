using System;
using SkiaSharp;

namespace DAT190_Bachelor_Project.Bakery
{
    public class PopOver
    {

        public float Radius { get; set; }
        public float AmountFontSize { get; set; }
        public float UnitFontSize { get; set; }
        public float TitleFontSize { get; set; }
        public float Factor { get; set; }
        public SKColor TitleFontColor { get; set; }
        public SKColor AmountFontColor { get; set; }
        public SKColor UnitFontColor { get; set; }

        public PopOver(float radius, float amountFontSize, float titleFontSize, float unitFontSize)
        {
            Radius = radius;
            AmountFontSize = amountFontSize;
            UnitFontSize = unitFontSize;
            TitleFontSize = titleFontSize;
            TitleFontColor = SKColors.White;
            AmountFontColor = SKColors.White;
            UnitFontColor = SKColors.White;
            Factor = 0;
        }
    }
}
