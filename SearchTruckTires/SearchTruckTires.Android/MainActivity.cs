using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Widget;
using HtmlAgilityPack;
using System.IO;


namespace SearchTruckTires.Droid
{
    [Activity(Label = "FindTires", Icon = "@mipmap/logo_parsing", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "Main" layout resource 
            SetContentView(Resource.Layout.Main);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.TireSizes_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Selected tire size {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            PathParsing(toast);
        }
        private void PathParsing(string toast)
        {
           
            var standardSize = toast;
            var web = new HtmlWeb();//переменная класса (HtmlAgilityPack) для работы с web страницами 
            var htmlDocument = web.Load("http://mpk-tyres.com.ua/catalog/" + standardSize + "/");// переменная получающая HTML код заданный в скобках адрес
            var page = htmlDocument.DocumentNode;// переменная страниц сайта
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string filename = "DataParsingMPK.html";
            File.WriteAllText(Path.Combine(folderPath, filename), page.InnerText);
        }
    }


}