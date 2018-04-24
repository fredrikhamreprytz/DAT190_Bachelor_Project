using Xamarin.Forms;
using System.Reflection;
using PCLAppConfig;
using DAT190_Bachelor_Project.Model;
using DAT190_Bachelor_Project.Data;

namespace DAT190_Bachelor_Project
{
    public partial class App : Application
    {
        static UserDatabase database;

        public App()
        {
            InitializeComponent();
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream("DAT190_Bachelor_Project.App.config"));

            MainPage = new FrontPage();
        }

        public static UserDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new UserDatabase(DependencyService.Get<ISQLite>().GetLocalFilePath("User.db3"));
                }
                return database;
            }
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
