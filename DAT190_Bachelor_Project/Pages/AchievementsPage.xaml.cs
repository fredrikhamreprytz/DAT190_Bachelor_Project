using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class AchievementsPage : ContentPage
    {
        public AchievementsPage()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Settings", "settings_icon.png", async () => { var page = new ContentPage(); var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel"); System.Diagnostics.Debug.WriteLine("success: {0}", result); }));
        }
    }
}
