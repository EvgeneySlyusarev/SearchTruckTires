﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Basket : ContentPage
    {
        public static readonly ObservableCollection<ProduktBasket> produktsBasket = new ObservableCollection<ProduktBasket>();
        public static int DeleteBasketClicked { get; set; }
        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewBasket.ItemsSource = produktsBasket;
            BindingContext = this;
            ListViewBasket.HasUnevenRows = true;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            produktsBasket.RemoveAt(DeleteBasketClicked); 
        }

        private void ListViewBasket_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DeleteBasketClicked = e.SelectedItemIndex;
        }
    }
}