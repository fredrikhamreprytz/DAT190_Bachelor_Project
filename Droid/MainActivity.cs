using System;
using Xamarin.Forms;
using Plugin.Toasts;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DAT190_Bachelor_Project.Droid
{
    [Activity(Label = "DAT190_Bachelor_Project.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init(this);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}
