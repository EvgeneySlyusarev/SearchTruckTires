using SearchTruckTires.DB_ConectServis;
using SkiaSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
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
            buttonData = new Dictionary<string, string>
            {
                {"ButtonTread", "imageTreadCash" },
                {"ButtonSide", "imageSideCash" },
                {"ButtonDOT", "imageDOTCash" },
                {"ButtonSerialNumber", "imageSerialNumberCash" },
                {"ButtonRepeir1", "imageRepeir1Cash" },
                {"ButtonRepeir2", "imageRepeir2Cash" },
                {"ButtonRepeir3", "imageRepeir3Cash" }
            };
        }

        private async void TakePhotoAsyncAndSave(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                string buttonName = button.CommandParameter?.ToString();
                try
                {
                    FileResult fileResult = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions());

                    if (fileResult != null)
                    {
                        string filePath = fileResult.FullPath;

                        // Преобразование файла в массив байтов
                        byte[] fileBytes = await File.ReadAllBytesAsync(filePath);

                        if (fileBytes != null && fileBytes.Length > 0)
                        {
                            // Коррекция ориентации изображения
                            byte[] correctedBytes = AdjustOrientation(fileBytes);

                            if (correctedBytes != null)
                            {
                                string directory = FileSystem.AppDataDirectory;
                                string uniqueFileName = $"photo_{DateTime.Now:yyyyMMddHHmmssfff}.jpeg";
                                string filename = Path.Combine(directory, uniqueFileName);

                                // Сохранение скорректированного изображения в формате JPEG
                                File.WriteAllBytes(filename, correctedBytes);

                                ImageSource imageSource = ImageSource.FromFile(filename);
                                buttonData[buttonName] = filename;
                                button.Source = imageSource;
                            }
                            else
                            {
                                // Ошибка при коррекции ориентации
                                await DisplayAlert("Ошибка", "Не удалось скорректировать ориентацию изображения", "OK");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Фото не сделано", ex.Message, "OK");
                }
            }
        }
        public static byte[] AdjustOrientation(byte[] imageBytes)
        {
            try
            {
                using MemoryStream originalStream = new MemoryStream(imageBytes);
                using SKBitmap originalBitmap = SKBitmap.Decode(originalStream);
                using MemoryStream outputStream = new MemoryStream();
                // Создаем новый битмап с фиксированной вертикальной ориентацией
                using (SKBitmap verticalBitmap = new SKBitmap(originalBitmap.Width, originalBitmap.Height))
                {
                    using (SKCanvas canvas = new SKCanvas(verticalBitmap))
                    {
                        canvas.DrawBitmap(originalBitmap, 0, 0);
                    }

                    // Сохраняем измененное изображение
                    _ = verticalBitmap.Encode(outputStream, SKEncodedImageFormat.Jpeg, 75);
                }

                return outputStream.ToArray();
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование или выброс исключения
                Console.WriteLine($"Ошибка при обработке изображения: {ex.Message}");
                return null;
            }
        }

        private void BD_AddItem()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            _ = sQLiteConnectDBTires.CreateTable<ProductDB>();
            ProductDB newItem = new ProductDB
            {
                TitleTires = EnteryTitle.Text,
                ModelTires = EnteryModel.Text,
                PriseUsedTires = Convert.ToDecimal(EnteryPrise.Text),
                WidthTires = _wigthTires,
                HeightTires = _higthTires,
                DiametrTires = _diametrTires,
                SerialNumber = EnterySerialNumber.Text,
                DOT = EnteryDOT.Text,
                ResidualTreadDepth = EnteryResidualTreadDepth.Text,
                Description = EditorDescription.Text,
                ImageTread = buttonData["ButtonTread"],
                ImageSide = buttonData["ButtonSide"],
                ImageDOT = buttonData["ButtonDOT"],
                ImageSerialNumber = buttonData["ButtonSerialNumber"],
                ImageRepeir1 = buttonData["ButtonRepeir1"],
                ImageRepair2 = buttonData["ButtonRepeir2"],
                ImageRepair3 = buttonData["ButtonRepeir3"]
            };
            _ = sQLiteConnectDBTires.Insert(newItem);
            sQLiteConnectDBTires.Close();
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
            EnterySerialNumber.Text = string.Empty;
            EnteryDOT.Text = string.Empty;
            EnteryResidualTreadDepth.Text = string.Empty;
            ButtonSide.Source = ImageSource.FromFile("camera360.png");
            ButtonTread.Source = ImageSource.FromFile("camera360.png");
            ButtonDOT.Source = ImageSource.FromFile("camera360.png");
            ButtonSerialNumber.Source = ImageSource.FromFile("camera360.png");
            ButtonRepeir1.Source = ImageSource.FromFile("camera360.png");
            ButtonRepeir2.Source = ImageSource.FromFile("camera360.png");
            ButtonRepeir3.Source = ImageSource.FromFile("camera360.png");
            // Отображение уведомления
            _ = DisplayAlert("Уведомление", "Данные успешно сохранены в базе данных.", "OK");
        }

        private readonly Dictionary<string, string> buttonData;

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