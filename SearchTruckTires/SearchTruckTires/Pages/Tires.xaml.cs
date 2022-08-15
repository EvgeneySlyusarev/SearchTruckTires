using Fizzler.Systems.HtmlAgilityPack;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HtmlAgilityPack;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tires : ContentPage
    {
        public Tires()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

            _products = new ObservableCollection<Product>();
            ListViewTires.ItemsSource = _products;
            BindingContext = this;
            ListViewTires.HasUnevenRows = true;
            
            pickerTires.ItemsSource = _tiresSizesStringArrey;
            _tiresSizeRating = new Dictionary<string, uint>();
            _RefreshTiresSizeRating();
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Product product)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{product.Title}?", "Да", "Нет");
                if (result)
                {
                    Basket.Instance.Products.Add(new Product(product.Title, product.PriceCash, product.PriceBank, product.ImageURL));
                }
            }
        }

        private void _RefreshTiresSizeRating()
        {
            _tiresSizeRating.Clear();
            foreach (var item in _tiresSizesStringArrey)
            {
                _tiresSizeRating.Add(item, 0);
            }
        }

        private void PickerTires_SelectedIndexChanged(object sender, EventArgs e)
        {
            var toast = pickerTires.Items[pickerTires.SelectedIndex].ToString();
            _AddRating(toast);
            _ParseMPKTires(toast);
            //ParseKapitan(pickerTires.Items[pickerTires.SelectedIndex].ToString());
        }

        private void _AddRating(string key)
        {
            if (_tiresSizeRating.ContainsKey(key))
            {
                _tiresSizeRating[key] += 1;
            }
            //Application.Current.Properties["tiresSizeRating"] = _tiresSizeRating;
        }

        private void _ParseMPKTires(string toast)
        {
            _products.Clear();

            string standartSize;
            standartSize = toast.Replace("/", "");
            standartSize = standartSize.Replace(".", "");
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
        //private readonly Picker _pickerTires;
        private readonly Dictionary<string, uint> _tiresSizeRating = null;
        private readonly string[] _tiresSizesStringArrey = new string[]
        {
            "215/75R17.5",
            "225/75R17.5",
            "235/75R17.5",
            "245/70R17.5",
            "265/70R19.5",
            "285/70R19.5",
            "385/55R19.5",
            "435/50R19.5",
            "445/45R19.5",
            "8.25R20",
            "9.00R20",
            "10.00R20",
            "11.00R20",
            "12.00R20",
            "11R22.5",
            "13R22.5",
            "275/70R22.5",
            "295/60R22.5",
            "315/60R22.5",
            "295/80R22.5",
            "315/70R22.5",
            "315/80R22.5",
            "385/55R22.5",
            "385/65R22.5",
            "425/65R22.5"
        };

    }}