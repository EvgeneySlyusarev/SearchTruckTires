using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindUsedTires : ContentPage
    {
        public FindUsedTires()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = _products;
            BindingContext = this;
            ProductsListView.HasUnevenRows = true;
        }

        private List<ProduktEntery> GetProduktsFromDatabase()
        {
            using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath()))
            {
                // Получение всех элементов
                bool DB_Empty = File.Exists(DB_Conekt.GetDatabasePath());
                if (!DB_Empty)
                {
                    _ = DisplayAlert("Уведомление", "База данних не содержит данних.", "OK");
                }
                List<ProduktEntery> items = sQLiteConnectDBTires.Table<ProduktEntery>().ToList();
                return items;
            }
        }

        private void ButtonDB_Download_Clicked(object sender, EventArgs e)
        {
            _products = new ObservableCollection<ProduktEntery>(GetProduktsFromDatabase());
            ProductsListView.ItemsSource = _products;
        }

        private ObservableCollection<ProduktEntery> _products;

        private void ButtonDB_DellAllItemDB_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath()))
            {
                _ = sQLiteConnectDBTires.DeleteAll<ProduktEntery>();
            }
        }
    }
}