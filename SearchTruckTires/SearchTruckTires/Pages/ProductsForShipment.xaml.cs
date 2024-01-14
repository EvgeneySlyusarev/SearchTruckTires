using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TiresForShipment : ContentPage
    {
        public static TiresForShipment Instance { get => _instance; }

        public ObservableCollection<Product> selectedProducts = new ObservableCollection<Product>();
       
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
        private void DellSelectedItemFromShipment(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Product product)
            {
                FindUsedTires.Instance._products.Add(product);
                _ = selectedProducts.Remove(product);
                
            }
        }

        private void ShipmentSelectedItemsFromDB_Clicked(object sender, EventArgs e)
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            if (selectedProducts != null && selectedProducts.Any())
            {
                foreach (Product product in selectedProducts)
                {
                    DeletingFilesFromMemoryProgramy(product.ImageTread);
                    DeletingFilesFromMemoryProgramy(product.ImageSide);
                    DeletingFilesFromMemoryProgramy(product.ImageSerialNumber);
                    DeletingFilesFromMemoryProgramy(product.DOT);
                    DeletingFilesFromMemoryProgramy(product.ImageRepeir1);
                    DeletingFilesFromMemoryProgramy(product.ImageRepair2);
                    DeletingFilesFromMemoryProgramy(product.ImageRepair3);
                    _ = sQLiteConnectDBTires.Delete<Product>(product.Id);
                }
                selectedProducts.Clear();
                _ = DisplayAlert("Notification", "Shipped!", "OK");
            }
            else
            {
                _ = DisplayAlert("Notification", "The shipment did not occur or the shipment block is missing", "OK");
            }
        }

        private void DeletingFilesFromMemoryProgramy(string filePathToDelete)
        {
            if (File.Exists(filePathToDelete))
            {
                try
                {
                    File.Delete(filePathToDelete);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error deleting file: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("File does not exist!");
            }
        }

        private static TiresForShipment _instance = null;
    };
}
