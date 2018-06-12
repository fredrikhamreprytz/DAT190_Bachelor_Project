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

            BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]);

            FrontPage fp = new FrontPage()
            {
                BackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["BackgroundColor"])
            };


            NewNavigationPage overviewNavigationStack = new NewNavigationPage(fp)
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                BarTextColor = Color.White,
                Title ="CO2-fotavtrykk",
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
