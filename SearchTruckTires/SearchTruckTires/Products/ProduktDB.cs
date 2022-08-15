using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SearchTruckTires
{
    class ProduktDB
    {
        public uint IdUsedTires { get => _idUsedTires; }
        public string TitleTires { get => _titleTires; }
        public string ModelTires { get => _modelTires; }
        public decimal PriseUsedTires { get => _price; }
        public uint WidthTires { get => _widthTires; }
        public uint HeightTires { get => _heightTires; }
        public uint DiametrTires { get => _diametrTires; }
        public string SerialNumber { get => _serialNumber; }
        public uint ResidualTreadDepth { get => _residualTreadDepth; }
        public ImageSource ImageTread { get => _imageTread; }
        public ImageSource ImageSide { get => _imageSide; }
        public ImageSource ImageDOT { get => _imageDOT; }
        public ImageSource ImageSerialNumber { get => _imageSerialNumber; }
        public ImageSource ImageRepeir1 { get => _imageRepair1; }
        public ImageSource ImageRepair2 { get => _imageRepair2; }
        public ImageSource ImageRepair3 { get => _imageRepair3; }
        public string Description { get => _description; }

        public ProduktDB()
        {
            _idUsedTires = ++_idCounter;
        }

        private static uint _idCounter = uint.MaxValue;

        private uint _idUsedTires;
        private string _titleTires;
        private string _modelTires;
        private decimal _price;
        private uint _widthTires;
        private uint _heightTires;
        private uint _diametrTires;
        private string _serialNumber;
        private readonly uint _residualTreadDepth;
        private ImageSource _imageTread;
        private ImageSource _imageSide;
        private ImageSource _imageDOT;
        private ImageSource _imageSerialNumber;
        private ImageSource _imageRepair1;
        private ImageSource _imageRepair2;
        private ImageSource _imageRepair3;
        private string _description;
    }
}
