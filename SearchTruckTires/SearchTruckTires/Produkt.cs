
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class Produkt
    {
        public string Title { get; set; }
        public string PriseN { get; set; }
        public string PriseBN { get; set; }
        public string ImageURL { get; set; }
        public ImageSource ImageProdukt
        {
            get
            {
                WebClient Client = new WebClient();

                var byteArray = Client.DownloadData(ImageURL);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
                //return ImageSource.FromUri(new System.Uri("https://mpk-tyres.com.ua/files/products/stormers216_2021.200x200.jpg"));
            }
        }
    }
}
