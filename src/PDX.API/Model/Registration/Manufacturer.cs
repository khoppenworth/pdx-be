namespace PDX.API.Model.Registration
{
    public class Manufacturer {
        public Manufacturer (PDX.Domain.Commodity.ProductManufacturer manufacturer) {
            this.name = manufacturer.ManufacturerAddress.Manufacturer?.Name;
            this.address = manufacturer.ManufacturerAddress.Manufacturer?.Site;
            this.type = manufacturer.ManufacturerType?.Name;
        }
        public string name { get; set; }
        public string address { get; set; }
        public string type { get; set; }

    }
}