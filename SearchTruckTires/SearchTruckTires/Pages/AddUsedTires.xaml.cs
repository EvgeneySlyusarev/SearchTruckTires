using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Collections.Generic;
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
            PickerDiametr.ItemsSource = _diametrTire_array;
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
                    PriseUsedTires = Convert.ToDecimal(EnteryPrise.Text),
                    WidthTires = _wigthTires,
                    HeightTires = _higthTires,
                    DiametrTires = _diametrTires,
                    SerialNumber = EnterySerialNumber.Text,
                    ResidualTreadDepth = EnteryResidualTreadDepth.Text
                };
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
            // Сброс значений полей после сохранения
            EnteryTitle.Text = string.Empty;
            EnteryModel.Text = string.Empty;
            EnteryPrise.Text = string.Empty;
            PickerWight.SelectedItem = null;
            PickerHeight.SelectedItem = null;
            PickerDiametr.SelectedItem = null;

            // Отображение уведомления
            _ = DisplayAlert("Уведомление", "Данные успешно сохранены в базе данных.", "OK");
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
        private readonly string[] _diametrTire_array = new string[]
            {
                "17,5",
                "19,5",
                "20",
                "22,5",
                "24"
            };
        private string _wigthTires;
        private string _higthTires;
        private string _diametrTires;

        private void PickerWight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerWight.SelectedIndex >= 0 && PickerWight.SelectedIndex < PickerWight.Items.Count)
            {
                _wigthTires = PickerWight.Items[PickerWight.SelectedIndex].ToString();
            }
        }
        private void PickerHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerHeight.SelectedIndex >= 0 && PickerHeight.SelectedIndex < PickerHeight.Items.Count)
            {
                _higthTires = PickerHeight.Items[PickerHeight.SelectedIndex].ToString();
            }
        }
        private void PickerDiametr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerDiametr.SelectedIndex >= 0 && PickerDiametr.SelectedIndex < PickerDiametr.Items.Count)
            {
                _diametrTires = PickerDiametr.Items[PickerDiametr.SelectedIndex].ToString();
            }
        }
    }
} 