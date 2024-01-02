using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class Product
    {
        public string Id => _id.ToString();

        public uint Count { get; set; }

        public string Title => _title;
        public string PriceCash => _priceCash; 
        public string PriceBank => _priceBank;
        public string ImageURL => _imageURL; 

        public ImageSource Image => _imageSource;

        public Product(string title, string priceCash, string priceBank, string imageURL)
        {
            _id = ++_idCounter;  // generation of unique id

            Count = 1;

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
        private string _title;
        private string _priceCash;
        private string _priceBank;
        private string _imageURL;

        private ImageSource _imageSource;  // only for caching
    };
}