using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SearchTruckTires
{
    public class Produkt : INotifyPropertyChanged
    {
        //private string produktName;
        public string Title { get; set; }
        public string PriseN { get; set; }
        public string PriseBN { get; set; }
        //public string ProduktProperty
        //{
        //    get => produktName.ToString();
        //    set { produktName = value; NotifyPropertyChanged(); }
        //}
        //public override string ToString()
        //{
        //    return produktName.ToString();
        //}
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
