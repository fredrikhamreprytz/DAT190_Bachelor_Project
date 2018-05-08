using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage(Page page)
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");

            Navigation.PushAsync(page);
        }
    }
}
