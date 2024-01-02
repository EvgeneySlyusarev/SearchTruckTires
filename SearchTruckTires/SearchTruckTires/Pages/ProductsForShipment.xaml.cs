using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TiresForShipment : ContentPage
    {
        public static TiresForShipment Instance { get => _instance; }

        public ObservableCollection<ProductDB> ProductsDB => (ObservableCollection<ProductDB>)ShipmentListView.ItemsSource;

        public TiresForShipment()
        {
            Debug.Assert(_instance == null);
            _instance = this;

            //BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;

            ShipmentListView.ItemsSource = new ObservableCollection<ProductDB>();
            ShipmentListView.HasUnevenRows = true;
            BindingContext = this;
        }
        private void ButtonDellShipment_Clicked(object sender, EventArgs e)
        {

        }
        private static TiresForShipment _instance = null;
    };
}
