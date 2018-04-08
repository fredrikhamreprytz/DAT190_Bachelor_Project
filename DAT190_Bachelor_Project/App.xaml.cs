using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
