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

        public uint ProductsCount { get => _productsCount; }
        public uint ProductsCost { get => _productsCost; }

        public Basket()
        {
            Debug.Assert(_instance == null);
            _instance = this;

            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

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

        private void _ProductsСalculateCounter()
        {
            _productsCount = 0;
            foreach (Product item in Products)
            {
                _productsCount += item.Count;
            }
            lableQwantProdBasket.Text = Convert.ToString(_productsCount);
        }

        private void _ProductsСost()
        {
            _productsCost = 0;
            foreach (Product item in Products)
            {
                bool success = int.TryParse(string.Join("", item.PriceCash.Where(c => char.IsDigit(c))), out int value);
                if (success)
                {
                    _productsCost += (uint)value * item.Count;
                }
            }
            lableСostProdBasket.Text = Convert.ToString(_productsCost);
        }

        private void _RefreshFooter()
        {
            _ProductsСalculateCounter();
            _ProductsСost();
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
                product.Count = (uint)e.NewValue;
                if (e.NewValue == 0)
                {
                    Products.Remove(product);
                }
                _RefreshFooter();
            }
        }

        private void ListViewBasket_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _RefreshFooter();
        }

        private static Basket _instance = null;

        private uint _productsCount = 0;
        private uint _productsCost = 0;
    };
}
