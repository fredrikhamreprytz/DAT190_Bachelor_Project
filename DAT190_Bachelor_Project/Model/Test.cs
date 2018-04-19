using SkiaSharp;
namespace DAT190_Bachelor_Project.Model
{
    public class Test
    {

        public Test(int value, SKColor color, string imageSvgPath)
        {
            Value = value;
            Color = color;
            ImageSvgPath = imageSvgPath;
        }

        public int Value { private set; get; }

        public SKColor Color { private set; get; }

        public string ImageSvgPath { private set; get; }
    }
}
