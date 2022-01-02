using Fizzler.Systems.HtmlAgilityPack;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HtmlAgilityPack;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Produkt : INotifyPropertyChanged
    {
        private string produktProperty;
        public string ProduktProperty
        {
            get => produktProperty.ToString();
            set { produktProperty = value; NotifyPropertyChanged(); }
        }
        public override string ToString()
        {
            return produktProperty.ToString();
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public partial class Tires : ContentPage
    {
        private readonly ObservableCollection<Produkt> produkts = new ObservableCollection<Produkt>();
        public Tires()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
            InitializeComponent();
            myListView.ItemsSource = produkts;
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParsingMPK(picker.Items[picker.SelectedIndex].ToString());
        }
        public int RoundUP(int value)
        {
            value = value + 100 - (value % 100);
            return value;
        }
        private void ParsingMPK(string toast)
        {
            produkts.Clear();
            string standardSize;
            standardSize = toast.Replace("/", "");
            standardSize = standardSize.Replace(".", "");
            standardSize = standardSize.ToLower();
            string url = "http://mpk-tyres.com.ua/catalog/" + standardSize + "/";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(url);
            HtmlNode page = htmlDoc.DocumentNode;

            foreach (HtmlNode item in page.QuerySelectorAll("li.product")) // поиск в файле данных
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
                string tempObjekt = title + " НАЛ - " + Convert.ToString(Convert.ToInt32(priceN)) + " ГРН , " + " с НДС - " + Convert.ToString(Convert.ToInt32(priceBN)) + " ГРН.";
                produkts.Add(new Produkt { ProduktProperty = tempObjekt });
            }

        }
    }
}