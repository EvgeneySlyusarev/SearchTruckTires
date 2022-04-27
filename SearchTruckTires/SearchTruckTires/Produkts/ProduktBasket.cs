using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class ProduktBasket
    {
        public string TitleProduktBasket { get; set; }
        public string PriseNProduktBasket { get; set; }
        public string PriseBNProduktBasket { get; set; }
        public string ImageURLProduktBasket { get; set; }
        private int _quantityProduktBasket;
        public int QuantityProduktBasket
        {
            get => _quantityProduktBasket;
            set => QuantityProduktBasket = _quantityProduktBasket;
        }

        public ProduktBasket()
        {
            _quantityProduktBasket = 1;
        }

        public ImageSource ImageProduktBasket
        {
            get
            {
                WebClient Client = new WebClient();
                byte[] byteArray = Client.DownloadData(ImageURLProduktBasket);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
        }// метод для скачивание изображения 
    }
}