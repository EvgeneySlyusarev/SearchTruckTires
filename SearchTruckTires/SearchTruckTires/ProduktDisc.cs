using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class ProduktDisc
    {
        public string TitleDisc { get; set; }
        public string PriseNDisc { get; set; }
        public string PriseBNDisc { get; set; }
        public string ImageURLDisc { get; set; }
        public ImageSource ImageProduktDisc
        {
            get
            {
                WebClient Client = new WebClient();
                byte[] byteArray = Client.DownloadData(ImageURLDisc);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
        }
    }
}
