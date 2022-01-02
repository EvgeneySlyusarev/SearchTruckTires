﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Disc : ContentPage
    {
        public Disc()
        {
            BackgroundImageSource = "@Resources/Drawable/DiscsBackground.png";
            InitializeComponent();
        }
    }
}