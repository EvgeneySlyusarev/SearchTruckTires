using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class ProduktBasket
    {
        public string Id { get => _id.ToString(); }

        public string TitleProduktBasket { get; set; } // TODO: only getters
        public string PriseNProduktBasket { get; set; }
        public string PriseBNProduktBasket { get; set; }
        public string ImageURLProduktBasket { get; set; }

        public int QuantityProduktBasket
        {
            get => _quantityProduktBasket;
            set => _quantityProduktBasket = value;
        }

        public ProduktBasket() // TODO: pass all params
        {
            _id = ++_idCounter;  // generation of unique id
        }

        private uint _id;
        private static uint _idCounter = uint.MaxValue;

        private int _quantityProduktBasket = 1;

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