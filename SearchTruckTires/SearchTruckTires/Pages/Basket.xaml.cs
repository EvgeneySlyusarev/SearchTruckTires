using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Basket : ContentPage
    {
        public static readonly ObservableCollection<ProduktBasket> produktsBasket = new ObservableCollection<ProduktBasket>();
        public int DeleteBasketClicked;
        public int QuantityProduktsBasket;
        public int СostProductsCart;

        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewBasket.ItemsSource = produktsBasket;
            BindingContext = this;
            ListViewBasket.HasUnevenRows = true;
        }
        private int ProductBasketCounter()
        {
            foreach (ProduktBasket item in produktsBasket)
            {
                QuantityProduktsBasket += item.QuantityProdukt;
            }
            return QuantityProduktsBasket;
        }
        public int ProductsCartСost()
        {
            foreach (ProduktBasket item in produktsBasket)
            {
                _ = int.TryParse(string.Join("", item.PriseNProduktBasket.Where(c => char.IsDigit(c))), out int value);
                СostProductsCart = value;
            }
            return СostProductsCart;
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
            _ = ProductBasketCounter();
            _ = ProductsCartСost();
        }
    }
}