using SearchTruckTires.DB_ConectServis;
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
            buttonData = new Dictionary<string, ImageSource>();

            //buttonData.Add("ButtonTread", _imageTreadCash);
            //buttonData.Add("ButtonSide", _imageSideCash);
            //buttonData.Add("ButtonSerialNumber", _imageSerialNumberCash);
            //buttonData.Add("ButtonDOT", _imageDOTCash);
            //buttonData.Add("ButtonRepeir1", _imageRepeir1Cash);
            //buttonData.Add("ButtonRepeir2", _imageRepeir2Cash);
            //buttonData.Add("ButtonRepeir3", _imageRepeir3Cash);
        }

        private async void TakePhotoAsync(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                string buttonName = button.CommandParameter?.ToString();
                FileResult fileResult = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions());
                
                try
                {
                    if (fileResult != null)
                    {
                        ImageSource imageSource = fileResult.FullPath;
                        buttonData[buttonName] = imageSource;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Фото не сделанно", ex.Message, "OK");
                }
            }
        }

        private void BD_AddItem()
        {
            using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath()))
            {
                sQLiteConnectDBTires.CreateTable<ProduktEntery>();
                ProduktEntery newItem = new ProduktEntery
                {
                    TitleTires = EnteryTitle.Text,
                    ModelTires = EnteryModel.Text,
                    PriseUsedTires = Convert.ToDecimal(EnteryPrise.Text)
                };
                //newItem.WidthTires = Convert.ToUInt32(PickerWight.Items);
                //newItem.HeightTires = Convert.ToUInt32(PickerHeight.Items);
                //newItem.DiametrTires = Convert.ToUInt32(PickerDiametr.Items);
                //newItem.SerialNumber = EnterySerialNumber.Text;
                //newItem.ResidualTreadDepth = Convert.ToUInt32(EnteryResidualTreadDepth.Text);
                //newItem.SerialNumber = EnterySerialNumber.Text;
                //newItem.ResidualTreadDepth = Convert.ToUInt32(EnteryResidualTreadDepth.Text);
                //newItem.Description = EditorDescription.Text;
                //newItem.ImageTread = _imageTreadCash;
                //newItem.ImageSide = _imageSideCash;
                //newItem.ImageDOT = _imageDOTCash;
                //newItem.ImageSerialNumber = _imageSerialNumberCash;
                //newItem.ImageRepeir1 = _imageRepeir1Cash;
                //newItem.ImageRepair2 = _imageRepeir2Cash;
                //newItem.ImageRepair3 = _imageRepeir3Cash;
                _ = sQLiteConnectDBTires.Insert(newItem);
                sQLiteConnectDBTires.Close();
            }
        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            BD_AddItem();
        }

        private readonly Dictionary<string, ImageSource> buttonData;

        //private ImageSource _imageTreadCash = ImageSource.FromFile("camera360.png");
        //private ImageSource _imageSideCash = ImageSource.FromFile("camera360.png");
        //private ImageSource _imageSerialNumberCash = ImageSource.FromFile("camera360.png");
        //private ImageSource _imageDOTCash = ImageSource.FromFile("camera360.png");
        //private ImageSource _imageRepeir1Cash = ImageSource.FromFile("camera360.png");
        //private ImageSource _imageRepeir2Cash = ImageSource.FromFile("camera360.png");
        //private ImageSource _imageRepeir3Cash = ImageSource.FromFile("camera360.png");
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