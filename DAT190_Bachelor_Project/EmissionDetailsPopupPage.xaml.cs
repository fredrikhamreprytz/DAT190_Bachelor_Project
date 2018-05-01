using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using DAT190_Bachelor_Project.Model;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace DAT190_Bachelor_Project
{

    public partial class EmissionDetailsPopupPage : PopupPage
    {

        IEmission emission;

        public EmissionDetailsPopupPage(IEmission emission)
        {
            this.emission = emission;
            InitializeComponent();
            stackLayout.BackgroundColor = Color.FromHex(emission.Color.ToString());
            emissionHeader.Text = "Dine " + emission.Name.ToLower() + " hittil i år er ";
            emissionAmountLabel.Text = ((int)emission.KgCO2).ToString() + " kg CO2";
            emissionDescriptionLabel.Text = emission.BiggestEmissionFactorDescription();

                
        }

        private void OnPaintPopup(object sender, SKPaintSurfaceEventArgs e)
        {

            SKPaint iconPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                IsAntialias = true
            };
            SKCanvas canvas = e.Surface.Canvas;
            SKPath icon = SKPath.ParseSvgPathData(emission.SVGIcon);

            SKRect iconBounds = new SKRect();
            icon.GetBounds(out iconBounds);
            float iconMax = Math.Max(iconBounds.Width, iconBounds.Height);
            float iconWidth = 100;
            var iconScale = iconWidth / iconMax;
            SKMatrix scaleMatrix = SKMatrix.MakeScale(iconScale, iconScale);
            icon.Transform(scaleMatrix);

            canvas.DrawPath(icon, iconPaint);



        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
    }
}