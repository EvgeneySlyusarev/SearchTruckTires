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
        private readonly ObservableCollection<ProduktDisc> produktsDiscs = new ObservableCollection<ProduktDisc>();
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
            if (e.Item is ProduktDisc selectedProdukt)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{selectedProdukt.TitleDisc}", "Да", "Нет");
                if (result == true)
                {
                    Basket.produktsBasket.Add(new ProduktBasket { TitleProduktBasket = selectedProdukt.TitleDisc, PriseNProduktBasket = selectedProdukt.PriseNDisc, PriseBNProduktBasket = selectedProdukt.PriseBNDisc, ImageURLProduktBasket = selectedProdukt.ImageURLDisc});// to do
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
                produktsDiscs.Add(new ProduktDisc { TitleDisc = title, PriseNDisc = priseNUP, PriseBNDisc = priseBNUP, ImageURLDisc = imageURL });
            }

        }
    }
}