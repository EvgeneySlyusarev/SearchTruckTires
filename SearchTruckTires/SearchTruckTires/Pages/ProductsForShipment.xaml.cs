using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TiresForShipment : ContentPage
    {
        public static TiresForShipment Instance { get => _instance; }

        public ObservableCollection<ProductDB> selectedProducts = new ObservableCollection<ProductDB>();
       
        public TiresForShipment()
        {
            Debug.Assert(_instance == null);
            _instance = this;

            //BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

            ShipmentListView.ItemsSource = selectedProducts;
            ShipmentListView.HasUnevenRows = true;
            BindingContext = this;
        }
        private void ButtonDellSelectedShipment_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ProductDB productDB)
            {
                _ = selectedProducts.Remove(productDB);
            }
        }

        private void RemoveSelectedItems_Clicked(object sender, EventArgs e)
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            if (selectedProducts != null && selectedProducts.Any())
            {
                foreach (ProductDB product in selectedProducts)
                {
                    
                    DeletingFilesFromProgramMemory(product.ImageTread);
                    DeletingFilesFromProgramMemory(product.ImageSide);
                    DeletingFilesFromProgramMemory(product.ImageSerialNumber);
                    DeletingFilesFromProgramMemory(product.DOT);
                    DeletingFilesFromProgramMemory(product.ImageRepeir1);
                    DeletingFilesFromProgramMemory(product.ImageRepair2);
                    DeletingFilesFromProgramMemory(product.ImageRepair3);
                    _ = sQLiteConnectDBTires.Delete<ProductDB>(product.Id);
                }
                    _ = DisplayAlert("Уведомление", "Отгружено!", "OK");
            }
            else
            {
                _ = DisplayAlert("Уведомление", "Отгрузка не произошла отсутствуют или блок отгрузки", "OK");
            }
        }

        private void DeletingFilesFromProgramMemory(string filePathToDelete)
        {
            if (File.Exists(filePathToDelete))
            {
                try
                {
                    File.Delete(filePathToDelete);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка при удалении файла: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }
        }

        private static TiresForShipment _instance = null;
    };
}
