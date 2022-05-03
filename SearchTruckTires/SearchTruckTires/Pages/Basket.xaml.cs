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

        private T _GetParent<T>(object element) where T : Element
        {
            var item = element as Element;
            while ((item != null) && !(item is T))
            {
                item = item.Parent;
            }
            return item as T;
        }

        private ProduktBasket _GetProductByItem(ViewCell item)
        {
            if (item != null)
            {
                return produktsBasket.First((ProduktBasket p) => { return p.Id == item.ClassId; });
            }
            return null;
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

        private void _RefreshFooter()
        {
            ProductBasketCounter();
            ProductsBasketСost();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var item = _GetParent<ViewCell>(sender);
            var product = _GetProductByItem(item);
            if (product != null)
            {
                produktsBasket.Remove(product);
            }
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var item = _GetParent<ViewCell>(sender);
            var product = _GetProductByItem(item);
            if (product != null)
            {
                product.QuantityProduktBasket = (int)e.NewValue;
                _RefreshFooter();
            }
        }

        private void ListViewBasket_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)  // TODO: switch to Add Remove callbacks
        {
            _RefreshFooter();
        }

        private void ListViewBasket_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void ListViewBasket_ItemTapped(object sender, ItemTappedEventArgs e)
        {
        }
    }
}
