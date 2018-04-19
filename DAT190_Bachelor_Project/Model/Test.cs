using SkiaSharp;
namespace DAT190_Bachelor_Project.Model
{
    public class Test
    {

        public Test(int value, SKColor color)
        {
            Value = value;
            Color = color;
        }

        public int Value { private set; get; }

        public SKColor Color { private set; get; }
    }
}
