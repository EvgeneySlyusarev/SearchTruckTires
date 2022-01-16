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
         
            //myListView.ItemTemplate = new DataTemplate(() =>
            //{
            //    // привязка к свойству Title
            //    Label title = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
            //    title.SetBinding(Label.TextProperty, "Title");

            //    // привязка к свойству PriseN
            //    Label priseN = new Label { FontAttributes = FontAttributes.Bold};
            //    priseN.SetBinding(Label.TextProperty, "PriseN");

            //    // привязка к свойству PriceBN
            //    Label priseBN = new Label { FontAttributes = FontAttributes.Bold };
            //    priseBN.SetBinding(Label.TextProperty, "PriseBN");

            //    Label url = new Label { FontAttributes = FontAttributes.Bold };
            //    url.SetBinding(Label.TextProperty, "ImageURL");

            //    Image imageURL = new Image();
            //    imageURL.Source = ImageSource.FromUri(new Uri("ImageURL"));
            //    //imageURL.SetBinding(Image.SourceProperty, "ImageProdukt");
            //    // создаем объект ViewCell.
            //    return new ViewCell
            //    {
            //        View = new StackLayout
            //        {
            //            Padding = new Thickness(0, 10),
            //            Orientation = StackOrientation.Vertical,
            //            Children = { title, priseN, priseBN, url, imageURL }
            //        }
            //    };
            //});
        }
        //public Image LoadImage(string ImageURL)
        //{
        //    Image image = new Image
        //    {
        //        Source = ImageSource.FromUri(new Uri(ImageURL))
        //    };
        //    return image;
        //}

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //if (e.Item is Produkt selectedProdukt)
            //{
            //    await DisplayAlert("Выбранная модель", $"{selectedProdukt.Title} - {selectedProdukt.ImageURL}", "OK");
            //}
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
                produkts.Add(new Produkt { Title = title, PriseN = priseNUP, PriseBN = priseBNUP, ImageURL = imageURL });
                //http://mpk-tyres.com.ua/files/products/stormers216_2021.200x200.jpg?be6e55ef64de826f63e78dc19421fe5f и эта ссылка не работает
                //https://aka.ms/campus.jpg - Эта ссылка работает!!!!!!!!!!!!!!!!!!
                //https://i.ytimg.com/an_webp/0EsUUeW6V-8/mqdefault_6s.webp?du=3000&sqp=CNiY8o4G&rs=AOn4CLD_tqKpVedvB8-rjbQCO6tF8C2FLQ и даже эта работает!!!!!!!!!!!!!!!!!!!!!!!!!
                }

        }
    }
}