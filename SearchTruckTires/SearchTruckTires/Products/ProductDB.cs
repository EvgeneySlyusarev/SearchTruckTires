using SQLite;

namespace SearchTruckTires
{
    public class ProductDB
    {
        [PrimaryKey, AutoIncrement]
        public string Id => _id.ToString();
        public string TitleTires { get; set; }
        public string ModelTires { get; set; }
        public decimal PriseUsedTires { get; set; }
        public string WidthTires { get; set; }
        public string HeightTires { get; set; }
        public string DiametrTires { get; set; }
        public string SerialNumber { get; set; }
        public string DOT { get; set; }
        public string ResidualTreadDepth { get; set; }
        public string ImageTread { get; set; }
        public string ImageSide { get; set; }
        public string ImageDOT { get; set; }
        public string ImageSerialNumber { get; set; }
        public string ImageRepeir1 { get; set; }
        public string ImageRepair2 { get; set; }
        public string ImageRepair3 { get; set; }
        public string Description { get; set; }

        public ProductDB(string titleTires, string modelTires, decimal priseUsedTires, string widthTires,
                         string heightTires, string diametrTires, string serialNumber, string DOT, string residualTreadDepth,
                         string description, string imageTread, string imageSide, string imageDOT, string imageSerialNumber,
                         string imageRepeir1, string imageRepair2, string imageRepair3)
        {
            _id = ++_idCounter;  // generation of unique id

            TitleTires = titleTires;
            ModelTires = modelTires;
            PriseUsedTires = priseUsedTires;
            WidthTires = widthTires;
            HeightTires = heightTires;
            DiametrTires = diametrTires;
            SerialNumber = serialNumber;
            this.DOT = DOT;
            ResidualTreadDepth = residualTreadDepth;
            ImageTread = imageTread;
            ImageSide = imageSide;
            ImageDOT = imageDOT;
            ImageSerialNumber = imageSerialNumber;
            ImageRepeir1 = imageRepeir1;
            ImageRepair2 = imageRepair2;
            ImageRepair3 = imageRepair3;
            Description = description;
        }

        public ProductDB()
        {
        }
        public ProductDB Clone()
        {
            return new ProductDB(this);
        }

        public ProductDB(ProductDB original)
        {
            _id = ++_idCounter;
            TitleTires = original.TitleTires;
            ModelTires = original.ModelTires;
            PriseUsedTires = original.PriseUsedTires;
            WidthTires = original.WidthTires;
            HeightTires = original.HeightTires;
            DiametrTires = original.DiametrTires;
            SerialNumber = original.SerialNumber;
            DOT = original.DOT;
            ResidualTreadDepth = original.ResidualTreadDepth;
            ImageTread = original.ImageTread;
            ImageSide = original.ImageSide;
            ImageDOT = original.ImageDOT;
            ImageSerialNumber = original.ImageSerialNumber;
            ImageRepeir1 = original.ImageRepeir1;
            ImageRepair2 = original.ImageRepair2;
            ImageRepair3 = original.ImageRepair3;
            Description = original.Description;
        }

        
        private readonly uint _id;
        private static uint _idCounter = uint.MaxValue;
    }
}

