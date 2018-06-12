using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class NewNavigationPage : NavigationPage
    {
        public NewNavigationPage(ContentPage page)
        {
            InitializeComponent();
            Navigation.PushAsync(page);
        }
    }
}
