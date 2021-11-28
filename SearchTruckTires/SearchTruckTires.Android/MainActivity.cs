using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Widget;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;

namespace SearchTruckTires.Droid
{
    [Activity(Label = "FindTires", Icon = "@mipmap/logo_parsing", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private ListView listView;
        private List<string> list;
        private List<string> produkt;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "Main" layout resource 
            SetContentView(Resource.Layout.Main);

            //set spiner
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.TireSizes_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
             
            // set listView
            list = new List<string>();
            produkt = new List<string>();
            list.AddRange(produkt);
            listView = FindViewById<ListView>(Resource.Id.listView);
            ArrayAdapter<string> adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);
            listView.Adapter = adapter1;

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //string toast = string.Format("Selected tire size {0}", spinner.GetItemAtPosition(e.Position));
            string toast = string.Format((string)spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            PathParsing(toast);
        }
        private void PathParsing(string toast)
        {
            string standardSize;
            standardSize = toast.Replace("/", "");
            standardSize = standardSize.Replace(".", "");
            var url = "http://mpk-tyres.com.ua/catalog/" + standardSize + "/";

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var page = htmlDoc.DocumentNode;
            var nodes = page.QuerySelectorAll("li.product");

            foreach (var item in nodes)
            {
                produkt.Add(item.InnerText);
            }
        }
    }
}