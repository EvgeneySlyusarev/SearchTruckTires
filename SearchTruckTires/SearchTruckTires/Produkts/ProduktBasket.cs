using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
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
        public int QuantityProduktBasket { get => _quantityProduktBasket; set => SetProperty(ref _quantityProduktBasket, value); }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ImageSource ImageProduktBasket
        {
            get
            {
                WebClient Client = new WebClient();
                byte[] byteArray = Client.DownloadData(ImageURLProduktBasket);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
        public ProduktBasket()
        {
            _quantityProduktBasket = 1;
        }
    }
}