using System;
using System.Collections.Generic;
using DAT190_Bachelor_Project.Model;
using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class ChallengeDetailsPage : ContentPage
    {

        public Color BgColor { get; set; }
        public Color HighlightColor { get; set; }
        public Challenge Challenge { get; set; }
        public double Progress { get; set; }
        public string ProgressText { get; set; }

        public ChallengeDetailsPage(Challenge challenge)
        {
            InitializeComponent();
            Challenge = challenge;
            BgColor = Color.FromHex(challenge.Color);
            HighlightColor = Color.FromRgba(0, 0, 0, 0.1);
            DescriptionLabel.Text = challenge.Description;

            Progress = challenge.Progress / challenge.Goal;
            ProgressText = challenge.Progress.ToString() + "/" + challenge.Goal.ToString();
            BindingContext = this;
        }
    }
}
