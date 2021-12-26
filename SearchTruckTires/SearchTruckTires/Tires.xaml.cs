using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tires : ContentPage
    {
        public ListView listView;
        public List<string> produkt;

        public Tires()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
            InitializeComponent();
            produkt = new List<string>();
            listView = new ListView();
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAlert("Уведомление", "Вы выбрали: " + picker.Items[picker.SelectedIndex], "ОK");
            ParsingMPK(picker.Items[picker.SelectedIndex].ToString());
        }
        public int RoundUP(int value)
        {
            value = value + 100 - (value % 100);
            return value;
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

            foreach (var item in page.SelectNodes("li.product")) // поиск в файле данных
            {
                string title = item.SelectNodes("div.product_info a").ToString().Trim();
                string strPrice = item.SelectNodes("td.price-td").ToString().Trim();
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
        }
    }
}