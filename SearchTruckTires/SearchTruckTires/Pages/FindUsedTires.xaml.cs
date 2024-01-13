using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindUsedTires : ContentPage
    {
        public static FindUsedTires Instance => _instance;
        public ObservableCollection<Product> _products;

        public FindUsedTires()
        {
            InitializeComponent();
            Debug.Assert(_instance == null);
            _instance = this;
            ProductsCollectionView.ItemsSource = _products;
            PickerWight.ItemsSource = _weightTire_array;
            PickerHeight.ItemsSource = _hightTire_array;
            PickerDiametr.ItemsSource = _diametrTire_array;

            InitializePageAsync();
        }

        private async void InitializePageAsync()
        {
            _products = new ObservableCollection<Product>();
            ProductsCollectionView.ItemsSource = _products;

            await LoadAllProductsAsync();
        }

        private async Task LoadAllProductsAsync()
        {
            string databasePath = DB_Conekt.GetDatabasePath();
            try
            {
                if (!File.Exists(databasePath))
                {
                    using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(databasePath))
                    {
                        _ = sQLiteConnectDBTires.CreateTable<Product>();
                    }

                    await DisplayAlert("Notification", "The database has been created.", "OK");
                    return;
                }

                using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(databasePath))
                {
                    List<SQLiteConnection.ColumnInfo> tableInfo = sQLiteConnectDBTires.GetTableInfo("Product");

                    if (tableInfo.Count == 0)
                    {
                        _ = sQLiteConnectDBTires.CreateTable<Product>();

                        await DisplayAlert("Notification", "The 'Product' table has been created.", "OK");
                        return;
                    }

                    List<Product> items = sQLiteConnectDBTires.Table<Product>().ToList();

                    if (items.Count == 0)
                    {
                        await DisplayAlert("Notification", "The 'Product' table does not contain any items.", "OK");
                        return;
                    }

                    foreach (Product item in items)
                    {
                        _products.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while working with the database: {ex.Message}", "OK");
            }
        }

        private ObservableCollection<Product> GetFindTiresSizeFromDatabase()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            TableQuery<Product> filteredData = from item in sQLiteConnectDBTires.Table<Product>()
                                               where item.WidthTires == _wigthTires && item.HeightTires == _higthTires && item.DiametrTires == _diametrTires
                                               select item;

            return new ObservableCollection<Product>(filteredData);
        }
        private ObservableCollection<Product> GetAllProductsFromDatabase()
        {
            string databasePath = DB_Conekt.GetDatabasePath();
            try
            {
                if (!File.Exists(databasePath))
                {
                    using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(databasePath))
                    {
                        _ = sQLiteConnectDBTires.CreateTable<Product>();
                    }
                    _ = DisplayAlert("Notification", "The database has been created.", "OK");
                    return new ObservableCollection<Product>();
                }

                using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(databasePath))
                {
                    List<SQLiteConnection.ColumnInfo> tableInfo = sQLiteConnectDBTires.GetTableInfo("Product");
                    if (tableInfo.Count == 0)
                    {
                        _ = sQLiteConnectDBTires.CreateTable<Product>();

                        _ = DisplayAlert("Notification", "The 'Product' table has been created.", "OK");
                        return new ObservableCollection<Product>();
                    }

                    List<Product> items = sQLiteConnectDBTires.Table<Product>().ToList();
                    if (items.Count == 0)
                    {
                        _ = DisplayAlert("Notification", "The 'Product' table does not contain any items.", "OK");
                    }

                    return new ObservableCollection<Product>(items);
                }
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Error", $"An error occurred while working with the database: {ex.Message}", "OK");
                return new ObservableCollection<Product>();
            }
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
        private static FindUsedTires _instance = null;

        private async void ButtonMorePhoto_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Product productDB)
            {
                await Navigation.PushModalAsync(new PhotoDetailPage(productDB));
            }
        }

        private void ButtonFindTiresSizes_Clicked(object sender, EventArgs e)
        {
            _products = new ObservableCollection<Product>(GetFindTiresSizeFromDatabase());
            ProductsCollectionView.ItemsSource = _products;
        }
        private void ButtonALL_DB_Download_Clicked(object sender, EventArgs e)
        {
            _products = new ObservableCollection<Product>(GetAllProductsFromDatabase());
            ProductsCollectionView.ItemsSource = _products;
        }
        private void ButtonDB_DellAllItemDB_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath()))
            {
                _ = sQLiteConnectDBTires.DeleteAll<Product>();
            }
        }
        private void PickerWight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerWight.SelectedIndex >= 0 && PickerWight.SelectedIndex < PickerWight.Items.Count)
            {
                _wigthTires = PickerWight.Items[PickerWight.SelectedIndex].ToString();
            }
            else
            {
                _wigthTires = "---";
            }
        }
        private void PickerHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerHeight.SelectedIndex >= 0 && PickerHeight.SelectedIndex < PickerHeight.Items.Count)
            {
                _higthTires = PickerHeight.Items[PickerHeight.SelectedIndex].ToString();
            }
            else
            {
                _higthTires = "--";
            }
        }
        private void PickerDiametr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerDiametr.SelectedIndex >= 0 && PickerDiametr.SelectedIndex < PickerDiametr.Items.Count)
            {
                _diametrTires = PickerDiametr.Items[PickerDiametr.SelectedIndex].ToString();
            }
            else
            {
                _diametrTires = "--,-";
            }
        }

        private void ProductsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (e.CurrentSelection.FirstOrDefault() is Product productDB)
                {
                    bool result = await DisplayAlert("Add to basket: - ", $"{productDB.TitleTires} {productDB.ModelTires} {productDB.WidthTires} {productDB.HeightTires}{"/"} {productDB.DiametrTires} ?", "Yes", "No");
                    if (result)
                    {
                        TiresForShipment.Instance.selectedProducts.Add(productDB);
                        _ = _products.Remove(productDB);
                    }
                }

                // Сброс текущего выделения
                ((CollectionView)sender).SelectedItem = null;
            });
        }

    }
}