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
        public Disc()
        {
            BackgroundImageSource = "@Resources/Drawable/DiscsBackground.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

            _products = new ObservableCollection<Product>();
            ListViewDiscs.ItemsSource = _products;
            BindingContext = this;
            ListViewDiscs.HasUnevenRows = true;
        }

        private void PickerDiscs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParseMPKDiscs(pickerDiscs.Items[pickerDiscs.SelectedIndex].ToString());
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Product product)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{product.Title}", "Да", "Нет");
                if (result)
                {
                    Basket.Instance.Products.Add(new Product(product.Title, product.PriceCash, product.PriceBank, product.ImageURL));
                }
            }
        }

        private void ParseMPKDiscs(string toast)
        {
            _products.Clear();

            string standartSize = toast;
            standartSize = standartSize.Replace(".", "");
            standartSize = standartSize.Replace("*", "");
            standartSize = standartSize.ToLower();
            string url = "http://mpk-tyres.com.ua/catalog/" + standartSize + "/";

            HtmlDocument htmlDoc = null;
            try
            {
                HtmlWeb web = new HtmlWeb();
                htmlDoc = web.Load(url);
            }
            catch (System.Net.WebException e)
            {
                DisplayAlert("Ошибка", "Нет сети\n\n" + e.Message, "ОК");
                return;
            }

            HtmlNode page = htmlDoc.DocumentNode;

            int RoundUP(int value)
            {
                value = value + 100 - (value % 100);
                return value;
            }

            foreach (HtmlNode item in page.QuerySelectorAll("li.product"))
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
                _products.Add(new Product(title, priceCashUP, priceBankUP, imageURL));
            }
        }

        private readonly ObservableCollection<Product> _products;
    };
}