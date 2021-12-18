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
using System.Collections.ObjectModel;
using Plugin.CurrentActivity;

namespace SearchTruckTires.Droid
{
    [Activity(Label = "FindTires", Icon = "@mipmap/logo_parsing", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public ListView listView;
        public List<string> produkt;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "Main" layout resource
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            SetContentView(Resource.Layout.Main);

            //set spiner
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.TireSizes_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            // set listView
            produkt = new List<string>();
            listView = FindViewById<ListView>(Resource.Id.listView);
            ArrayAdapter<string> adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, produkt);
            listView.Adapter = adapter1;
        }
        public int RoundUP(int value)
        {
            value = value + 100 - (value % 100);
            return value;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format((string)spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
            ParsingMPK(toast);
            ParsingOmega(toast);
            Parsing100Shin(toast);
        }
        private void ParsingOmega(string toast)
        { 
        
        }
        private void Parsing100Shin(string toast)
        {

        }
        private void ParsingMPK(string toast)
        {
            produkt.Clear();
            string standardSize;
            standardSize = toast.Replace("/", "");
            standardSize = standardSize.Replace(".", "");
            standardSize = standardSize.ToLower();
            var url = "http://mpk-tyres.com.ua/catalog/" + standardSize + "/";
        
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var page = htmlDoc.DocumentNode;

            foreach (var item in page.QuerySelectorAll("li.product")) // поиск в файле данных
            {
                string title = item.QuerySelector("div.product_info a").InnerText.Trim();
                string strPrice = item.QuerySelector("td.price-td").InnerText.Trim();
                strPrice = strPrice.Replace(" ", "");
                strPrice = strPrice.Replace("грн", "");
                double priceBN = Convert.ToDouble(strPrice);
                double priceN = Convert.ToDouble(strPrice);
                double marginBN = 1.1;
                double marginN = 1.05;
                priceBN *= marginBN;
                priceN *= marginN;
                priceN = RoundUP(Convert.ToInt32(priceN));
                priceBN = RoundUP(Convert.ToInt32(priceBN));
                produkt.Add(title + " НАЛ - " + Convert.ToString(Convert.ToInt32(priceN)) + " ГРН , " + " с НДС - " + Convert.ToString(Convert.ToInt32(priceBN)) + " ГРН.");
            }
            ArrayAdapter<string> adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, produkt);
            listView.Adapter = adapter1;
        }
    }
}