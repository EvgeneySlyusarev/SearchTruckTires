using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoDetailPage : ContentPage
    {
        public PhotoDetailPage(ProduktDB selectedItem)
        {
            InitializeComponent();
            backButton.Clicked += BackButton_Clicked;
            PhotoTread.Source = ImageSource.FromFile(selectedItem.ImageTread);
            PhotoSide.Source = ImageSource.FromFile(selectedItem.ImageSide);
            PhotoDot.Source = ImageSource.FromFile(selectedItem.ImageDOT);
            PhotoSerialNumber.Source = ImageSource.FromFile(selectedItem.ImageSerialNumber);
            PhotoRepeir1.Source = ImageSource.FromFile(selectedItem.ImageRepeir1);
            PhotoRepeir2.Source = ImageSource.FromFile(selectedItem.ImageRepair2);
            PhotoRepeir3.Source = ImageSource.FromFile(selectedItem.ImageRepair3);
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }
    }
}