﻿using SearchTruckTires.DB_ConectServis;
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
    }
}