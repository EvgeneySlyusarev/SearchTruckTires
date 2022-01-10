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
        private readonly ObservableCollection<Produkt> produkts = new ObservableCollection<Produkt>();
        public Tires()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
            InitializeComponent();
            myListView.ItemsSource = produkts;
            BindingContext = this;
            myListView.HasUnevenRows = true;
            // Определяем формат отображения данных
            myListView.ItemTemplate = new DataTemplate(() =>
            {
                // привязка к свойству Title
                Label title = new Label { FontSize = 18 };
                title.SetBinding(Label.TextProperty, "Title");

                // привязка к свойству PriseN
                Label priseN = new Label();
                priseN.SetBinding(Label.TextProperty, "PriseN");

                // привязка к свойству PriceBN
                Label priceBN = new Label();
                priceBN.SetBinding(Label.TextProperty, "PriseBN");

                Image imageURL = new Image();
                imageURL.Source = new UriImageSource
                {
                    CachingEnabled = false,
                    Uri = new Uri("http://mpk-tyres.com.ua/files/products/stormers216_2021.200x200.jpg")
                };
                imageURL.SetBinding(Image.AspectProperty, "ImageURL");



                // создаем объект ViewCell.
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(0, 10),
                        Orientation = StackOrientation.Vertical,
                        Children = { title, priseN, priceBN, imageURL }
                    }
                };
            });
        }



        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Produkt selectedProdukt)
            {
                await DisplayAlert("Выбранная модель", $"{selectedProdukt.Title} - {selectedProdukt.ImageURL}", "OK");
            }
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParsingMPK(picker.Items[picker.SelectedIndex].ToString());
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

            int RoundUP(int value)
            {
                value = value + 100 - (value % 100);
                return value;
            }

            foreach (HtmlNode item in page.QuerySelectorAll("li.product")) // поиск в файле данных
            {
                string imageURL = item.QuerySelector("img").GetAttributeValue("src", null);
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
                produkts.Add(new Produkt { Title = title, PriseN = priseNUP, PriseBN = priseBNUP, ImageURL = imageURL });
            }

        }
    }
}