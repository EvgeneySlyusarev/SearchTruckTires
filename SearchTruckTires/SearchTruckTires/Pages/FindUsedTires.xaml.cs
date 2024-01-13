using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
            ProductsListView.ItemsSource = _products;
            BindingContext = this;
            ProductsListView.HasUnevenRows = true;
            PickerWight.ItemsSource = _weightTire_array;
            PickerHeight.ItemsSource = _hightTire_array;
            PickerDiametr.ItemsSource = _diametrTire_array;
        }

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

        private void ButtonFindTiresSizes_Clicked(object sender, EventArgs e)
        {
            _products = new ObservableCollection<Product>(GetFindTiresSizeFromDatabase());
            ProductsListView.ItemsSource = _products;
        }
        private void ButtonALL_DB_Download_Clicked(object sender, EventArgs e)
        {
            _products = new ObservableCollection<Product>(GetAllProduktsFromDatabase());
            ProductsListView.ItemsSource = _products;
        }
        private void ButtonDB_DellAllItemDB_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath()))
            {
                _ = sQLiteConnectDBTires.DeleteAll<Product>();
            }
        }

        private List<Product> GetFindTiresSizeFromDatabase()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            TableQuery<Product> filteredData = from item in sQLiteConnectDBTires.Table<Product>()
                                               where item.WidthTires == _wigthTires && item.HeightTires == _higthTires && item.DiametrTires == _diametrTires
                                               select item;
            return filteredData.ToList();
        }
        private List<Product> GetAllProduktsFromDatabase()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            // Получение всех элементов
            bool DB_Empty = File.Exists(DB_Conekt.GetDatabasePath());
            if (!DB_Empty)
            {
                _ = DisplayAlert("Уведомление", "База данних не содержит данних.", "OK");
            }
            List<Product> items = sQLiteConnectDBTires.Table<Product>().ToList();
            return items;
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

        private async void ProductsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Product productDB)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{productDB.TitleTires} {productDB.ModelTires} {productDB.WidthTires} {productDB.HeightTires}{"/"} {productDB.DiametrTires} ?", "Да", "Нет");
                if (result)
                {
                    TiresForShipment.Instance.selectedProducts.Add(productDB);
                    _ = _products.Remove(productDB);
                }
            }
        }
    }
}