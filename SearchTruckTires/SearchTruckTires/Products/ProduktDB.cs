using Xamarin.Forms;

namespace SearchTruckTires
{
    public class ProduktDB
    {
        public string Id => _id.ToString();

        public string TitleTires { get => _titleTires; }
        public string ModelTires { get => _modelTires; }
        public decimal PriseUsedTires { get => _priseUsedTires; }
        public uint WidthTires { get => _widthTires; }
        public uint HeightTires { get => _heightTires; }
        public uint DiametrTires { get => _diametrTires; }
        public string SerialNumber { get => _serialNumber; }
        public uint ResidualTreadDepth { get => _residualTreadDepth; }
        public ImageSource ImageTread { get => _imageTread; }
        public ImageSource ImageSide { get => _imageSide; }
        public ImageSource ImageDOT { get => _imageDOT; }
        public ImageSource ImageSerialNumber { get => _imageSerialNumber; }
        public ImageSource ImageRepeir1 { get => _imageRepeir1; }
        public ImageSource ImageRepair2 { get => _imageRepair2; }
        public ImageSource ImageRepair3 { get => _imageRepair3; }
        public string Description { get => _description; }

        public ProduktDB(string titleTires, string modelTires, decimal priseUsedTires, uint widthTires,
                         uint heightTires, uint diametrTires, string serialNumber, uint residualTreadDepth,
                         ImageSource imageTread, ImageSource imageSide, ImageSource imageDOT, ImageSource imageSerialNumber,
                         ImageSource imageRepeir1, ImageSource imageRepair2, ImageSource imageRepair3, string description)
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
            _residualTreadDepth = residualTreadDepth;
            _imageTread = imageTread;
            _imageSide = imageSide;
            _imageDOT = imageDOT;
            _imageSerialNumber = imageSerialNumber;
            _imageRepeir1 = imageRepeir1;
            _imageRepair2 = imageRepair2;
            _imageRepair3 = imageRepair3;
            _description = description;
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
        private uint _widthTires;
        private uint _heightTires;
        private uint _diametrTires;
        private string _serialNumber;
        private uint _residualTreadDepth;
        private ImageSource _imageTread;
        private ImageSource _imageSide;
        private ImageSource _imageDOT;
        private ImageSource _imageSerialNumber;
        private ImageSource _imageRepeir1;
        private ImageSource _imageRepair2;
        private ImageSource _imageRepair3;
        private string _description;
    }
}
