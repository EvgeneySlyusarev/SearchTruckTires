using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class Product
    {
        public string Id { get => _id.ToString(); }

        public uint Count
        {
            get => _count;
            set => _count = value;
        }

        public string Title { get => _title; }
        public string PriceCash { get => _priceCash; }
        public string PriceBank { get => _priceBank; }
        public string ImageURL { get => _imageURL; }

        public ImageSource Image { get => _imageSource; }

        public Product(string title, string priceCash, string priceBank, string imageURL)
        {
            _id = ++_idCounter;  // generation of unique id

            _count = 1;

            _title = title;
            _priceCash = priceCash;
            _priceBank = priceBank;
            _imageURL = imageURL;

            _imageSource = _FetchImage(imageURL);
        }

        private ImageSource _FetchImage(string url)
        {
            WebClient Client = new WebClient();
            byte[] byteArray = Client.DownloadData(url);
            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        private readonly uint _id;
        private static uint _idCounter = uint.MaxValue;

        private uint _count;

        private string _title;
        private string _priceCash;
        private string _priceBank;
        private string _imageURL;

        private ImageSource _imageSource;  // only for caching
    };
}