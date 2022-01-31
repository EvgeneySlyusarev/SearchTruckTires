using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Basket : ContentPage
    { 
        private readonly ObservableCollection<ProduktBasket> produktsBasket = new ObservableCollection<ProduktBasket>();
        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewBasket.ItemsSource = produktsBasket;
            BindingContext = this;
            ListViewBasket.HasUnevenRows = true;
        }
        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is ProduktTires selectedProdukt)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{selectedProdukt.Title}", "Да", "Нет");
                await DisplayAlert("Уведомление", "Вы выбрали: " + (result ? "Добавить" : "Отменить"), "OK");
            }
        }
    }
}