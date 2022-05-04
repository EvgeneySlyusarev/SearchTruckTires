using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Diagnostics;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Basket : ContentPage
    {
        public static Basket Instance { get => _instance; }

        public ObservableCollection<Product> Products
        {
            get => (ObservableCollection<Product>)ListViewBasket.ItemsSource;
        }

        public int QuantityProduct { get => _quantityProduct; }
        public int СostProducts { get => _costProducts; }

        public Basket()
        {
            Debug.Assert(_instance == null);
            _instance = this;

            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Dark;

            ListViewBasket.ItemsSource = new ObservableCollection<Product>();
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

        private Product _GetProductByItem(ViewCell item)
        {
            if (item != null)
            {
                return Products.First((Product p) => { return p.Id == item.ClassId; });
            }
            return null;
        }

        private void ProductBasketCounter()
        {
            _quantityProduct = 0;
            foreach (Product item in Products)
            {
                _quantityProduct += item.QuantityProduktBasket;
            }
            lableQwantProdBasket.Text = Convert.ToString(_quantityProduct);
        }

        private void ProductsBasketСost()
        {
            _costProducts = 0;
            foreach (Product item in Products)
            {
                bool success = int.TryParse(string.Join("", item.PriceCash.Where(c => char.IsDigit(c))), out int value);
                if (success)
                {
                    _costProducts += value * item.QuantityProduktBasket;
                }
            }
            lableСostProdBasket.Text = Convert.ToString(_costProducts);
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
                Products.Remove(product);
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

        private static Basket _instance = null;

        private int _quantityProduct;
        private int _costProducts;
    }
}
