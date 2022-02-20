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
        public static int DeleteBasketClicked { get; set; }
        public static int CountProduktsBasket { get; set; }
        public static int СostProductsCart { get; set; }

        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewBasket.ItemsSource = produktsBasket;
            CountProduktsBasket = ProductBasketCounter();
            СostProductsCart = ProductsCartСost();
            BindingContext = this;
            ListViewBasket.HasUnevenRows = true;
        }
        private int ProductBasketCounter()
        {
            for (int i = 0; i < produktsBasket.Count; i++)
            {
                CountProduktsBasket += produktsBasket[i].QuantityProduktBasket;
            }
            return CountProduktsBasket;
        }
        private int ProductsCartСost()
        {
            СostProductsCart = 0;
            for (int i = 0; i < produktsBasket.Count; i++)
            {
                int.TryParse(string.Join("", produktsBasket[i].PriseNProduktBasket.Where(c => char.IsDigit(c))), out int value);
                СostProductsCart += value;
            }
            return СostProductsCart;
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