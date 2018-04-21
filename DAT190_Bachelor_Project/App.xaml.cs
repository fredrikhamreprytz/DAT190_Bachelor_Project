﻿using Xamarin.Forms;
using System.Reflection;
using PCLAppConfig;

namespace DAT190_Bachelor_Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream("DAT190_Bachelor_Project.App.config"));

            MainPage = new FrontPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
