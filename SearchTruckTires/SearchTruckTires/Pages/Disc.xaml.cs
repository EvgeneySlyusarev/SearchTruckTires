using Fizzler.Systems.HtmlAgilityPack;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Disc : ContentPage
    {
        private readonly ObservableCollection<Product> produktsDiscs = new ObservableCollection<Product>();
        public Disc()
        {
            BackgroundImageSource = "@Resources/Drawable/DiscsBackground.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewDiscs.ItemsSource = produktsDiscs;
            BindingContext = this;
            ListViewDiscs.HasUnevenRows = true;
        }
        private void PickerDiscs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParsingMPKDiscs(pickerDiscs.Items[pickerDiscs.SelectedIndex].ToString());
        }
        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Product selectedProdukt)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{selectedProdukt.Title}", "Да", "Нет");
                if (result)
                {
                    Basket.Instance.Products.Add(new Product { Title = selectedProdukt.Title, PriceCash = selectedProdukt.PriceCash, PriceBank = selectedProdukt.PriceBank, ImageURL = selectedProdukt.ImageURL});// to do
                }
            }
        }
        private void ParsingMPKDiscs(string toast)
        {
            produktsDiscs.Clear();
            string standardSize = toast;
            standardSize = standardSize.Replace(".", "");
            standardSize = standardSize.Replace("*", "");
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
                produktsDiscs.Add(new Product { Title = title, PriceCash = priceCashUP, PriceBank = priceBankUP, ImageURL = imageURL });
            }

        }
    }
}