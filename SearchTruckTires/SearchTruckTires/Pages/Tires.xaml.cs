using Fizzler.Systems.HtmlAgilityPack;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tires : ContentPage
    {
        public readonly ObservableCollection<Product> produktsTires = new ObservableCollection<Product>();
        public Tires()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewTires.ItemsSource = produktsTires;
            BindingContext = this;
            ListViewTires.HasUnevenRows = true;
        }
        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Product selectedProdukt)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{selectedProdukt.Title}?", "Да", "Нет");
                if (result)
                {
                    Basket.Instance.Products.Add(new Product { Title = selectedProdukt.Title, PriceCash = selectedProdukt.PriceCash, PriceBank = selectedProdukt.PriceBank, ImageURL = selectedProdukt.ImageURL});// to do
                }
            }
        }
        private void PickerTires_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParsingMPKTires(pickerTires.Items[pickerTires.SelectedIndex].ToString());
            //ParsingKapitan(pickerTires.Items[pickerTires.SelectedIndex].ToString());
        }
        private void ParsingMPKTires(string toast)
        {
            produktsTires.Clear();
            string standardSize;
            standardSize = toast.Replace("/", "");
            standardSize = standardSize.Replace(".", "");
            standardSize = standardSize.ToLower();
            string url = "http://mpk-tyres.com.ua/catalog/" + standardSize + "/";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(url);
            HtmlNode page = htmlDoc.DocumentNode;

            int RoundUP(int value)
            {
                value = value + 100 - (value % 100);
                return value;
            }

            foreach (HtmlNode item in page.QuerySelectorAll("li.product")) // поиск в файле данных
            {
                string imageURL = item.QuerySelector("img").GetAttributeValue("src", null);
                imageURL = imageURL.Substring(0, imageURL.IndexOf('?'));
                string title = item.QuerySelector("div.product_info a").InnerText.Trim();
                string strPrice = item.QuerySelector("td.price-td").InnerText.Trim();
                strPrice = strPrice.Replace(" ", "");
                strPrice = strPrice.Replace("грн", "");
                double priceBank = Convert.ToDouble(strPrice);
                double priceCash = Convert.ToDouble(strPrice);
                double marginBank = 1.1;
                double marginCash = 1.05;
                priceBank *= marginBank;
                priceCash *= marginCash;
                priceCash = RoundUP(Convert.ToInt32(priceCash));
                priceBank = RoundUP(Convert.ToInt32(priceBank));
                string priceCashUP = " НАЛ - " + Convert.ToString(Convert.ToInt32(priceCash)) + " ГРН , ";
                string priceBankUP = " с НДС - " + Convert.ToString(Convert.ToInt32(priceBank)) + " ГРН.";
                produktsTires.Add(new Product { Title = title, PriceCash = priceCashUP, PriceBank = priceBankUP, ImageURL = imageURL});
            }
        }
        private void ParsingKapitan(string toast)// Нужно изменить для парсинга капитан
        {
            //produktsTires.Clear();
            string standardSize;
            standardSize = toast.Replace("/", "");
            standardSize = standardSize.Replace(".", "");
            standardSize = standardSize.ToLower();
            string url = "http://mpk-tyres.com.ua/catalog/" + standardSize + "/";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(url);
            HtmlNode page = htmlDoc.DocumentNode;

            int RoundUP(int value)
            {
                value = value + 100 - (value % 100);
                return value;
            }

            foreach (HtmlNode item in page.QuerySelectorAll("li.product")) // поиск в файле данных
            {
                string imageURL = item.QuerySelector("img").GetAttributeValue("src", null);
                imageURL = imageURL.Substring(0, imageURL.IndexOf('?'));
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
                string priseNUP = " НАЛ - " + Convert.ToString(Convert.ToInt32(priceN)) + " ГРН , ";
                string priseBNUP = " с НДС - " + Convert.ToString(Convert.ToInt32(priceBN)) + " ГРН.";
                produktsTires.Add(new Product { Title = title, PriceCash = priseNUP, PriceBank = priseBNUP, ImageURL = imageURL });
            }

        }
    }
}