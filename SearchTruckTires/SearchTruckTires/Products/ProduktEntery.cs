﻿using SQLite;
using System;

namespace SearchTruckTires
{
    public class ProduktEntery
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get => _id.ToString(); }
        public string TitleTires { get => _titleTires; set => _titleTires = value; }
        public string ModelTires { get => _modelTires; set => _modelTires = value; }
        public decimal PriseUsedTires { get => _priseUsedTires; set => _priseUsedTires = value; }
        public string WidthTires { get => _widthTires; set => _widthTires = value; }
        public string HeightTires { get => _heightTires; set => _heightTires = value; }
        public string DiametrTires { get => _diametrTires; set => _diametrTires = value; }
        public string SerialNumber { get => _serialNumber; set => _serialNumber = value; }
        public string DOT { get => _DOT; set => _DOT = value; }
        public string ResidualTreadDepth { get => _residualTreadDepth; set => _residualTreadDepth = value; }
        //public ImageSource ImageTread { get => _imageTread; set => _imageTread = value; }
        //public ImageSource ImageSide { get => _imageSide; set => _imageSide = value; }
        //public ImageSource ImageDOT { get => _imageDOT; set => _imageDOT = value; }
        //public ImageSource ImageSerialNumber { get => _imageSerialNumber; set => _imageSerialNumber = value; }
        //public ImageSource ImageRepeir1 { get => _imageRepeir1; set => _imageRepeir1 = value; }
        //public ImageSource ImageRepair2 { get => _imageRepair2; set => _imageRepair2 = value; }
        //public ImageSource ImageRepair3 { get => _imageRepair3; set => _imageRepair3 = value; }
        //public string Description { get => _description; set => _description = value; }

        public ProduktEntery(string titleTires, string modelTires, decimal priseUsedTires, string widthTires,
                         string heightTires, string diametrTires, string serialNumber, string DOT, string residualTreadDepth
                         // ImageSource imageTread, ImageSource imageSide, ImageSource imageDOT, ImageSource imageSerialNumber,
                         //ImageSource imageRepeir1, ImageSource imageRepair2, ImageSource imageRepair3,
                         //string description
                         )
        {
            _id = ++_idCounter;  // generation of unique id
            _count = 1;

            _titleTires = titleTires;
            _modelTires = modelTires;
            _priseUsedTires = priseUsedTires;
            _widthTires = widthTires;
            _heightTires = heightTires;
            _diametrTires = diametrTires;
            _serialNumber = serialNumber;
            _DOT = DOT;
            _residualTreadDepth = residualTreadDepth;
            //_imageTread = imageTread;
            //_imageSide = imageSide;
            //_imageDOT = imageDOT;
            //_imageSerialNumber = imageSerialNumber;
            //_imageRepeir1 = imageRepeir1;
            //_imageRepair2 = imageRepair2;
            //_imageRepair3 = imageRepair3;
            //_description = description;
        }
        public ProduktEntery()
        {
        }

        public uint Count
        {
            get => _count;
            set => _count = value;
        }

        private readonly uint _id;
        private static uint _idCounter = uint.MaxValue;

        private uint _count;

        private string _titleTires;
        private string _modelTires;
        private decimal _priseUsedTires;
        private string _widthTires;
        private string _heightTires;
        private string _diametrTires;
        private string _serialNumber;
        private string _residualTreadDepth;
        private string _DOT;

        //private ImageSource _imageTread;
        //private ImageSource _imageSide;
        //private ImageSource _imageDOT;
        //private ImageSource _imageSerialNumber;
        //private ImageSource _imageRepeir1;
        //private ImageSource _imageRepair2;
        //private ImageSource _imageRepair3;
        //private string _description;
    }
}
