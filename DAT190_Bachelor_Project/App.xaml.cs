using Xamarin.Forms;
using System.Reflection;
using PCLAppConfig;
using DAT190_Bachelor_Project.Model;
using DAT190_Bachelor_Project.Data;

namespace DAT190_Bachelor_Project
{
    public partial class App : Application
    {

        public static double DisplayScreenWidth = 0f;
        public static double DisplayScreenHeight = 0f;
        public static double DisplayScaleFactor = 0f;

        public App()
        {
            InitializeComponent();
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream("DAT190_Bachelor_Project.App.config"));
            System.Diagnostics.Debug.WriteLine(DisplayScaleFactor);
            MainPage = new MainTabbedPage()
            {
                //BarBackgroundColor = Color.FromHex(ConfigurationManager.AppSettings["ThemeColor"])
            };
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
