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

        public int ProductsCount { get => _productsCount; }
        public int ProductsCost { get => _productsCost; }

        public Basket()
        {
            Debug.Assert(_instance == null);
            _instance = this;

            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            
            ListViewBasket.ItemsSource = new ObservableCollection<Product>();
            ListViewBasket.HasUnevenRows = true;
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

        private void ProductsCounter()
        {
            _productsCount = 0;
            foreach (Product item in Products)
            {
                _productsCount += item.QuantityProductBasket;
            }
            lableQwantProdBasket.Text = Convert.ToString(_productsCount);
        }

        private void ProductsСost()
        {
            _productsCost = 0;
            foreach (Product item in Products)
            {
                bool success = int.TryParse(string.Join("", item.PriceCash.Where(c => char.IsDigit(c))), out int value);
                if (success)
                {
                    _productsCost += value * item.QuantityProductBasket;
                }
            }
            lableСostProdBasket.Text = Convert.ToString(_productsCost);
        }

        private void _RefreshFooter()
        {
            ProductsCounter();
            ProductsСost();
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
                product.QuantityProductBasket = (int)e.NewValue;
                _RefreshFooter();
            }
        }

        private void ListViewBasket_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)  // TODO: switch to Add Remove callbacks
        {
            _RefreshFooter();
        }

        private static Basket _instance = null;

        private int _productsCount;
        private int _productsCost;
    }
}
