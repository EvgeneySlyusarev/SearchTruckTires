using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public int count = 0;

        private void Button_Clicked(object sender, EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"You clicked + {count} times.";
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            count--;
            ((Button)sender).Text = $"You clicked - {count} times.";
        }
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            count *= count;
            ((Button)sender).Text = $"Count - {count} times.";
        }
    }
}
