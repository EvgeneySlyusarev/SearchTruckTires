using System.IO;
using System.Net;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public class ProduktTires
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
                byte[] byteArray = Client.DownloadData(ImageURL);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
        }
    }
}
