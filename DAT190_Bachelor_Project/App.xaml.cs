using Xamarin.Forms;
using System.Reflection;
using PCLAppConfig;
using DAT190_Bachelor_Project.Model;
using DAT190_Bachelor_Project.Data;
using AppServiceHelpers;

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
            //// Create new EasyMobileServiceClient.
            //var client = EasyMobileServiceClient.Create();
            //// Initialize the library with URL of the Azure Mobile App.
            //client.Initialize("{Your_Mobile_App_Backend_Url_Here}");
            //// Register the models to create tables
            ////client.RegisterTable<User>();

            //// Finalize the schema for the database. All table registration must be done.
            //client.FinalizeSchema();

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
