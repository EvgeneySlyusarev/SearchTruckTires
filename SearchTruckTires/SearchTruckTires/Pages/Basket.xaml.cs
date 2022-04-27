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
        public int QuantityProduktBasket { get; set; }
        public int СostProductsCart { get; set; }
        public int DeleteBasketId { get; set; }

        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Dark;
            ListViewBasket.ItemsSource = produktsBasket;
            ListViewBasket.HasUnevenRows = true;
            _ = new Stepper();
            BindingContext = this;
        }

        private void ProductBasketCounter()
        {
            QuantityProduktBasket = 0;
            foreach (ProduktBasket item in produktsBasket)
            {
                QuantityProduktBasket += item.QuantityProduktBasket;
            }
            lableQwantProdBasket.Text = Convert.ToString(QuantityProduktBasket);
        }

        private void ProductsBasketСost()// работает 
        {
            СostProductsCart = 0;
            foreach (ProduktBasket item in produktsBasket)
            {
                _ = int.TryParse(string.Join("", item.PriseNProduktBasket.Where(c => char.IsDigit(c))), out int value);
                value *= item.QuantityProduktBasket;
                СostProductsCart += value;
            }
            lableСostProdBasket.Text = Convert.ToString(СostProductsCart);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (sender is Button)
            {
                DeleteBasketId = button.);
            }
            produktsBasket.RemoveAt(DeleteBasketId);
        }

        private void ListViewBasket_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ProductBasketCounter();
            ProductsBasketСost();
        }

        private void Stepper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void ListViewBasket_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DeleteBasketId = e.ItemIndex;
        }
    }
}