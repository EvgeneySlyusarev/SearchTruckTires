using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoDetailPage : ContentPage
    {
        public PhotoDetailPage(ProductDB selectedItem)
        {
            InitializeComponent();
            PhotoTread.Source = ImageSource.FromFile(selectedItem.ImageTread);
            PhotoSide.Source = ImageSource.FromFile(selectedItem.ImageSide);
            PhotoDot.Source = ImageSource.FromFile(selectedItem.ImageDOT);
            PhotoSerialNumber.Source = ImageSource.FromFile(selectedItem.ImageSerialNumber);
            PhotoRepeir1.Source = ImageSource.FromFile(selectedItem.ImageRepeir1);
            PhotoRepeir2.Source = ImageSource.FromFile(selectedItem.ImageRepair2);
            PhotoRepeir3.Source = ImageSource.FromFile(selectedItem.ImageRepair3);
        }
    }
}