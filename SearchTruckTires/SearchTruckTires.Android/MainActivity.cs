using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;

namespace SearchTruckTires.Droid
{
    [Activity(Label = "FindTires", Icon = "@mipmap/logo_parsing", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public ListView listView;
        public List<string> produkt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}