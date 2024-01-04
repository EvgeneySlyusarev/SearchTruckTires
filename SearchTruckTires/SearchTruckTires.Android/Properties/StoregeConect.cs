using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System;

namespace SearchTruckTires.DB_ConectServis
{
    [Activity(Label = "SAFActivity")]
    public class SAFActivity : Activity
    {
        const int PickImageId = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Добавьте ваш код разметки, если необходимо

            // Запрос разрешения и выбор изображения
            RequestExternalStoragePermissionAndPickImage();
        }

        private void RequestExternalStoragePermissionAndPickImage()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (CheckSelfPermission(Android.Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Granted)
                {
                    // Разрешение уже предоставлено, можно выбрать изображение
                    PickImage();
                }
                else
                {
                    // Запрос разрешения у пользователя
                    RequestPermissions(new string[] { Android.Manifest.Permission.ReadExternalStorage }, PickImageId);
                }
            }
            else
            {
                // Для версий Android ниже 6.0 не требуется явного запроса разрешений
                PickImage();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [Android.Runtime.GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (requestCode == PickImageId)
            {
                if (grantResults[0] == Android.Content.PM.Permission.Granted)
                {
                    // Разрешение получено, можно выбрать изображение
                    PickImage();
                }
                else
                {
                    // Разрешение не предоставлено, обработайте этот случай по вашему усмотрению
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        private void PickImage()
        {
            Intent intent = new Intent(Intent.ActionOpenDocument);
            intent.AddCategory(Android.Content.Intent.CategoryOpenable);
            intent.SetType("image/*");

            StartActivityForResult(intent, PickImageId);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == PickImageId && resultCode == Result.Ok)
            {
                // Обработка выбора изображения
                // data.Data содержит URI выбранного изображения
                if (data != null && data.Data != null)
                {
                    Android.Net.Uri selectedImageUri = data.Data;
                    // Ваш код обработки выбранного изображения
                    Console.WriteLine("Selected Image URI: " + selectedImageUri.ToString());
                }
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }
    }
}
