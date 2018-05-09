using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Threading.Tasks;
using System.Threading;
using DAT190_Bachelor_Project.Model;
using System.Diagnostics;

namespace DAT190_Bachelor_Project.Bakery
{
    public class CakeOrientation
    {
        public PieceOfCake[] PiecesOfCake { get; set; }
        public float OldOffset { get; set; }
        public float OffsetDifference { get; set; }
        public float TotalValues { get; set; }
        public float Radius;
        public float Scale;
        public bool IsAnimating { get; set; }
        CarbonFootprint CO2;
        public PieceOfCake CurrentlySelected;
        public SKPoint Center { get; set; }
        public float Thickness { get; set; }
        public float IconOffset { get; set; }
        public float IconRadius { get; set; }
        public SKCanvasView CanvasView { get; set; }
        public PopOver PopOver { get; set; }

        public CakeOrientation(CarbonFootprint co2, SKCanvasView canvasView)
        {
            this.CanvasView = canvasView;
            float Width = (float)canvasView.Width;
            float Height = (float)canvasView.Height;
            this.CurrentlySelected = null;
            this.Center = new SKPoint(Width / 2, Height / 2);
            this.Thickness = 34;
            this.IconOffset = 6;
            this.IconRadius = 32;

            this.Radius = Math.Min(Width / 2, Height / 2) - 1.75f * 32f;
            this.TotalValues = 0;
            this.CO2 = co2;
            this.PopOver = new PopOver(Radius - Thickness * 1.3f, 80, 30, 25);

            foreach (IEmission Emission in CO2.Emissions)
            {
                TotalValues += (float)Emission.KgCO2;
            }

            PiecesOfCake = new PieceOfCake[co2.Emissions.Length];
            InitializeCake();
        }

        // Initialises a CakeView based on the given CarbonFootprint
        // Rotation set to default origin values.
        private void InitializeCake() {

            int i = 0;
            float startAngle = 0;

            foreach (IEmission Emission in CO2.Emissions)
            {
                float sweepAngle = 360f * (float)Emission.KgCO2 / TotalValues;
                PiecesOfCake[i] = new PieceOfCake(Emission, Center, startAngle, sweepAngle, Radius, IconOffset, IconRadius);
                startAngle += sweepAngle;
                i++;
            }
        }


        public void SetOrientation(float offset)
        {
            for (int i = 0; i < PiecesOfCake.Length; i++)
            {
                IEmission emission = PiecesOfCake[i].Emission;
                float startAngle = PiecesOfCake[i].StartAngle += offset;
                float arcAngle = PiecesOfCake[i].ArcAngle;

                float iconRadius = IconRadius;
                float iconOffset = IconOffset;

                bool isSelected = PiecesOfCake[i].Emission.Equals(CurrentlySelected);

                // Make icon of selected category bigger
                if (isSelected)
                {
                    float factor = 1f;
                    iconRadius = IconRadius * factor;
                    //iconOffset = iconRadius / 2 + IconOffset;
                }

                PiecesOfCake[i] = new PieceOfCake(emission, Center, startAngle, arcAngle, Radius, iconOffset, iconRadius);
            }
        }


        // Calculate angle to rotate the cake orientation so that it aligns
        // the selected emission icon on on the top arc of the cake.
        // Sets the currently selected emission.
        public float UpdateOrientation(IEmission Selected)
        {
            // Reset first (avoid accumulating huge sums in startAngle)
            //InitializeCake();

            bool add = false;
            float newStartAngleOffset = 0;

            // Calculate the amount to offset
            foreach (PieceOfCake Slice in PiecesOfCake)
            {
                    
                if (add)
                {
                    newStartAngleOffset += Slice.ArcAngle;
                }

                if (Slice.Emission.Equals(Selected))
                {
                    CurrentlySelected = Slice;
                    newStartAngleOffset += (Slice.ArcAngle / 2) - 90;
                    add = true;
                }
            }

            OffsetDifference = newStartAngleOffset - OldOffset;
            OldOffset = newStartAngleOffset;

            return OffsetDifference;
        }


        // Check if a touch event occured inside an icon and
        // returns the corresponding piece of cake
        public PieceOfCake SelectPieceOfCake(SKTouchEventArgs e) 
        {
            
            SKPoint touchLocation = e.Location;
            PieceOfCake SelectedSlice = null;
            int i = 0;

            foreach (PieceOfCake Slice in PiecesOfCake)
            {
                float minX = Slice.Icon.Center.X - Slice.Icon.Radius;
                float maxX = Slice.Icon.Center.X + Slice.Icon.Radius;
                float minY = Slice.Icon.Center.Y - Slice.Icon.Radius;
                float maxY = Slice.Icon.Center.Y + Slice.Icon.Radius;

                minX *= Scale;
                maxX *= Scale;
                minY *= Scale;
                maxY *= Scale;

                bool insideX = touchLocation.X > minX && touchLocation.X < maxX;
                bool insideY = touchLocation.Y > minY && touchLocation.Y < maxY;

                if (insideX && insideY)
                {
                    SelectedSlice = Slice;
                }

                i++;
            }

            if (SelectedSlice != null)
            {
                CanvasView.InvalidateSurface();
            }

            return SelectedSlice;
        }

        public async Task AnimateSelection(IEmission Select, float millis)
        {
            float offset = UpdateOrientation(Select);
            float i = 0;

            // These two values constitute the animation speed 
            // angleInterval is the resolution of animation
            float angleInterval = Math.Abs(offset) > 220 ? offset/28 : offset/16;
            // ms is the frame rate. Acts unexpectedly for vaues below 10.
            int ms = 10;
            IsAnimating = true;
            PopOver.Factor = 0;
            if (offset < 0)
            {
                while (i > offset - angleInterval)
                {
                    SetOrientation(angleInterval);
                    CanvasView.InvalidateSurface();

                    await Task.Delay(TimeSpan.FromMilliseconds(ms));
                    i += angleInterval;
                    System.Diagnostics.Debug.WriteLine("i: " + i);
                    if (i < offset*0.1)
                    {
                        float remaining = offset * 0.9f / angleInterval; 
                        PopOver.Factor += 1f / remaining;
                    }
                    CanvasView.InvalidateSurface();
                }
                SetOrientation(offset - i);
                PopOver.Factor = 1;
                await Task.Delay(TimeSpan.FromMilliseconds(ms));
                CanvasView.InvalidateSurface();

            }
            else
            {
                while (i < offset - angleInterval)
                {
                    SetOrientation(angleInterval);
                    CanvasView.InvalidateSurface();
                    await Task.Delay(TimeSpan.FromMilliseconds(ms));
                    i += angleInterval;
                    System.Diagnostics.Debug.WriteLine("i: " + i);
                    if (i > offset * 0.1)
                    {
                        float remaining = offset * 0.9f / angleInterval;
                        PopOver.Factor += 1f / remaining;
                    }
                    CanvasView.InvalidateSurface();
                }
                SetOrientation(offset - i);
                PopOver.Factor = 1;
                await Task.Delay(TimeSpan.FromMilliseconds(ms));
                CanvasView.InvalidateSurface();
            }
            System.Diagnostics.Debug.WriteLine("offset: " + offset);

            IsAnimating = false;
        }
    }
}