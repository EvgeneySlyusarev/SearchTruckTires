using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class Produkt
    {
        public string Id { get => _id.ToString(); }
        public string Title { get; set; } // TODO: only getters
        public string PriceCash { get; set; }
        public string PriceBank { get; set; }
        public string ImageURL { get; set; }

        public int QuantityProduktBasket
        {
            get => _quantityProduktBasket;
            set => _quantityProduktBasket = value;
        }

        public Produkt() // TODO: pass all params
        {
            _id = ++_idCounter;  // generation of unique id
        }

        private uint _id;
        private static uint _idCounter = uint.MaxValue;

        private int _quantityProduktBasket = 1;

        public ImageSource Image
        {
            get
            {
                WebClient Client = new WebClient();
                byte[] byteArray = Client.DownloadData(ImageURL);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
        }// метод для скачивание изображения 
    }
}