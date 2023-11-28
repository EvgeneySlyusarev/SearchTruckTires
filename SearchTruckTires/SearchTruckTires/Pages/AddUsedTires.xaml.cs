using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUsedTires : ContentPage
    {
        public AddUsedTires()
        {
            InitializeComponent();

            PickerWight.ItemsSource = _weightTire_array;
            PickerHeight.ItemsSource = _hightTire_array;
            PickerDiametr.ItemsSource = _RadiusTire_array;

            buttonData.Add(Convert.ToString(ButtonTread), _imageTreadCash);
            buttonData.Add(Convert.ToString(ButtonSide), _imageSideCash);
            buttonData.Add(Convert.ToString(ButtonSerialNumber), _imageSerialNumberCash);
            buttonData.Add(Convert.ToString(ButtonDOT), _imageDOTCash);
            buttonData.Add(Convert.ToString(ButtonRepeir1), _imageRepeir1Cash);
            buttonData.Add(Convert.ToString(ButtonRepeir2), _imageRepeir2Cash);
            buttonData.Add(Convert.ToString(ButtonRepeir3), _imageRepeir3Cash);
        }

        private async void TakePhotoAsync(object sender, EventArgs e)
        {

            var button = sender as Button;

            if (button != null)
            {
                string buttonName = button.CommandParameter?.ToString();

                try
                {
                    FileResult fileResult = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions());
                    ImageSource imageSource = await ConvertFileResultToImageSourceAsync(fileResult);

                    buttonData[buttonName] = imageSource;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                }
            }
        }

        private Task<ImageSource> ConvertFileResultToImageSourceAsync(FileResult fileResult)
        {
            throw new NotImplementedException();
        }

        private void BD_AddItem()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "database.db");
            var db = new SQLiteConnection(databasePath);
            db.CreateTable<ProduktDB>();

            // Добавление элемента
            ProduktDB newItem = new ProduktDB();
            newItem.TitleTires = EnteryTitle.Text;
            newItem.ModelTires = EnteryModel.Text;
            newItem.PriseUsedTires = Convert.ToDecimal(EnteryPrise.Text);
            newItem.WidthTires = Convert.ToUInt32(PickerWight.Items);
            newItem.HeightTires = Convert.ToUInt32(PickerHeight.Items);
            newItem.DiametrTires = Convert.ToUInt32(PickerDiametr.Items);
            newItem.SerialNumber = EnterySerialNumber.Text;
            newItem.ResidualTreadDepth = Convert.ToUInt32(EnteryResidualTreadDepth.Text);
            newItem.SerialNumber = EnterySerialNumber.Text;
            newItem.ResidualTreadDepth = Convert.ToUInt32(EnteryResidualTreadDepth.Text);
            newItem.Description = EditorDescription.Text;
            newItem.ImageTread = _imageTreadCash;
            newItem.ImageSide = _imageSideCash;
            newItem.ImageDOT = _imageDOTCash;
            newItem.ImageSerialNumber = _imageSerialNumberCash;
            newItem.ImageRepeir1 = _imageRepeir1Cash;
            newItem.ImageRepair2 = _imageRepeir2Cash;
            newItem.ImageRepair3 = _imageRepeir3Cash;

            db.Insert(newItem);
        }

        private void Seved_Clicked(object sender, EventArgs e)
        {
            BD_AddItem();
        }

        private readonly Dictionary<string, ImageSource> buttonData = new Dictionary<string, ImageSource>();


        private ImageSource _imageTreadCash;
        private ImageSource _imageSideCash;
        private ImageSource _imageSerialNumberCash;
        private ImageSource _imageDOTCash;
        private ImageSource _imageRepeir1Cash;
        private ImageSource _imageRepeir2Cash;
        private ImageSource _imageRepeir3Cash;

        private readonly string[] _weightTire_array = new string[]
        {
            "205",
            "215",
            "225",
            "235",
            "245",
            "255",
            "265",
            "275",
            "285",
            "295",
            "305",
            "315",
            "385",
            "425",
            "435",
            "445"
        };
        private readonly string[] _hightTire_array = new string[]
        {
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "90",
            "00"
        };
        private readonly string[] _RadiusTire_array = new string[]
            {
                "17,5",
                "19,5",
                "20",
                "22,5",
                "24"
            };
    }

}