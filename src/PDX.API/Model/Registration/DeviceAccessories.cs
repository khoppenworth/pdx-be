namespace PDX.API.Model.Registration
{
    public class DeviceAccessories{
         public DeviceAccessories (PDX.Domain.Commodity.DeviceAccessories accessories) {
            this.name = accessories.Name;
            this.model = accessories.Model;
            this.description = accessories.Description;
            this.accessoryType = accessories.AccessoryType.AccessoryTypeCode;

        }
        public string name { get; set; }
        public string model { get; set; }
        public string description { get; set; }
        public string accessoryType{get;set;}
    }
}