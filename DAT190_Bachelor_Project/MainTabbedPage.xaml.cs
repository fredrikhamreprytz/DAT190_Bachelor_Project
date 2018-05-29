using System;
using System.Collections.Generic;
using PCLAppConfig;

using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();

            NewNavigationPage overviewNavigationStack = new NewNavigationPage(new FrontPage())
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                BarTextColor = Color.White,
                Title ="CO2 Avtrykk",
                Icon = "fp.png"
            };

            NewNavigationPage challengesNavigationStack = new NewNavigationPage(new ChallengesPage())
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                BarTextColor = Color.White,
                Title = "Utfordringer",
                Icon = "ch.png"
            };

            NewNavigationPage achievementsNavigationStack = new NewNavigationPage(new AchievementsPage())
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                BarTextColor = Color.White,
                Title = "Profil",
                Icon = "profile_icon.png"
            };

            Children.Add(overviewNavigationStack);
            Children.Add(challengesNavigationStack);
            Children.Add(achievementsNavigationStack);

        }
    }
}
