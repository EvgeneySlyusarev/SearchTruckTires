using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class ProduktBasket
    {
        private ProduktTires selectedProdukt;

        public ProduktBasket(ProduktTires selectedProdukt)
        {
            this.selectedProdukt=selectedProdukt;
        }

        public string TitleProduktBasket { get; set; }
        public string PriseNProduktBasket { get; set; }
        public string PriseBNProduktBasket { get; set; }
        public string ImageURLProduktBasket { get; set; }
        public ImageSource ImageProduktBasket
        {
            get
            {
                WebClient Client = new WebClient();
                byte[] byteArray = Client.DownloadData(ImageURLProduktBasket);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
        }
    }
}