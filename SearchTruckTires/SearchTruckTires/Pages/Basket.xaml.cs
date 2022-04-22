using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.ComponentModel;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Basket : ContentPage, INotifyPropertyChanged
    {
        public static readonly ObservableCollection<ProduktBasket> produktsBasket = new ObservableCollection<ProduktBasket>();
        public int DeleteBasketClicked;
        public int QuantityProduktBasket;
        public int СostProductsCart;

        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Dark;
            ListViewBasket.ItemsSource = produktsBasket;
            ListViewBasket.HasUnevenRows = true;
            lableQwantProdBasket.Text = Convert.ToString(QuantityProduktBasket);
            lableСostProdBasket.Text = Convert.ToString(СostProductsCart);
            BindingContext = this;
        }

        private void ProductBasketCounter()
        {
            foreach (ProduktBasket item in produktsBasket)
            {
                QuantityProduktBasket += item.QuantityProduktBasket;
            }
        }

       private void ProductsBasketСost()
        {
            foreach (ProduktBasket item in produktsBasket)
            {
                _ = int.TryParse(string.Join("", item.PriseNProduktBasket.Where(c => char.IsDigit(c))), out int value);
                СostProductsCart += value;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            produktsBasket.RemoveAt(DeleteBasketClicked);
        }

        private void ListViewBasket_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DeleteBasketClicked = e.SelectedItemIndex;
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ProductBasketCounter();
                ProductsBasketСost();
            });
        }
    }
}