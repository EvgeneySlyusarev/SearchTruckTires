
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoDetailPage : ContentPage
    {
        public PhotoDetailPage(Product selectedItem)
        {
            InitializeComponent();
            _ = LoadImagesAsync(selectedItem);
        }

        public async Task LoadImagesAsync(Product selectedItem)
        {
            PhotoTread.Source = await LoadImageAsync(selectedItem.ImageTread);
            PhotoSide.Source = await LoadImageAsync(selectedItem.ImageSide);
            PhotoDot.Source = await LoadImageAsync(selectedItem.ImageDOT);
            PhotoSerialNumber.Source = await LoadImageAsync(selectedItem.ImageSerialNumber);
            PhotoRepeir1.Source = await LoadImageAsync(selectedItem.ImageRepeir1);
            PhotoRepeir2.Source = await LoadImageAsync(selectedItem.ImageRepair2);
            PhotoRepeir3.Source = await LoadImageAsync(selectedItem.ImageRepair3);
        }
        private async Task<ImageSource> LoadImageAsync(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return null;
            }
            try
            {
                byte[] imageBytes = await Task.Run(() =>
                {
                    if (File.Exists(imagePath))
                    {
                        return File.ReadAllBytes(imagePath);
                    }
                    return null;
                });

                if (imageBytes != null)
                {
                    return ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        //private async Task<ImageSource> LoadImageAsync(string imagePath)
        //{
        //    if (string.IsNullOrWhiteSpace(imagePath))
        //    {
        //        return null;
        //    }

        //    byte[] imageBytes = await YourImageDownloadMethodAsync(imagePath);
        //    if (imageBytes != null)
        //    {
        //        return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        //    }

        //    return null;
        //}
        //private async Task<byte[]> YourImageDownloadMethodAsync(string imagePath)
        //{
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            // Замените "YourApiBaseUrl" на базовый URL вашего API, который возвращает изображение по заданному пути
        //            string apiUrl = $"{YourApiBaseUrl}/{imagePath}";
        //            HttpResponseMessage response = await client.GetAsync(apiUrl);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return await response.Content.ReadAsByteArrayAsync();
        //            }
        //            else
        //            {
        //                // Обработка ошибки, если запрос не успешен
        //                Console.WriteLine($"Error: {response.StatusCode}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка других исключений
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }

        //    return null;
        //}

        //// Замените "YourApiBaseUrl" на фактический базовый URL вашего API
        //private const string YourApiBaseUrl = "https://example.com/api";
    }
}
