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

            FrontPage frontPage = new FrontPage()
            {
                Title = "Mitt CO2 Avtrykk"
            };

            ChallengesPage challengesPage = new ChallengesPage()
            {
                Title = "Utfordringer"
            };

            AchievementsPage achievementsPage = new AchievementsPage()
            {
            };

            NewNavigationPage overviewNavigationStack = new NewNavigationPage(frontPage)
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                Title = "CO2 Avtrykk",
                BarTextColor = Color.White,
                Icon = "fp.png"
            };

            NewNavigationPage challengesNavigationStack = new NewNavigationPage(challengesPage)
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                Title = "Utfordringer",
                BarTextColor = Color.White,
                Icon = "ch.png"
            };

            NewNavigationPage achievementsNavigationStack = new NewNavigationPage(achievementsPage)
            {
                BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"]),
                Title = "Profil",
                BarTextColor = Color.White,
                Icon = "profile_icon.png"
            };

            Children.Add(overviewNavigationStack);
            Children.Add(challengesNavigationStack);
            Children.Add(achievementsNavigationStack);

        }
    }
}
