using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DAT190_Bachelor_Project.Bakery;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project
{
    public partial class EmissionHighlightView : ContentView
    {

        IEmission emission;

        public EmissionHighlightView(IEmission emission)
        {
            
            InitializeComponent();
            this.emission = emission;
            HighlightedEmissionFrame.BackgroundColor = Color.FromHex(CakeUtil.GetColor(emission).ToString());
            HighlightedEmissionHeaderLabel.Text = "Nyeste i " + CakeUtil.GetTitle(emission).ToLower() + "utslipp:";
            //HighlightedEmissionValueLabel.Text = (int)emission.KgCO2 + " kg CO2.";

        }

        public void EmissionDetailsButtonClicked(object sender, EventArgs args) {
            Navigation.PushAsync(new EmissionDetailsPage(emission));
        }
    }
}
