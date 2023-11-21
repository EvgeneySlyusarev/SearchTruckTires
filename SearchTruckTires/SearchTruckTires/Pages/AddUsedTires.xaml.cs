using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUsedTires : ContentPage
    { 
        private readonly Image img = new Image();

        public AddUsedTires()
        {
            InitializeComponent();

            PickerWight.ItemsSource = _weightTire_array;
            PickerHeight.ItemsSource = _hightTire_array;
            PickerDiametr.ItemsSource = _RadiusTire_array;
        }

        private async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.png"
                });

                // для примера сохраняем файл в локальном хранилище
                string newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }

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



            db.Insert(newItem);
        }

        private void Seved_Clicked(object sender, EventArgs e)
        {
            BD_AddItem();
        }


        private void PickerWight_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void PickerHeight_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PickerDiametr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


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
            "70",
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