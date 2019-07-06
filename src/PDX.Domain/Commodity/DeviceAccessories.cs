using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;
using Newtonsoft.Json;
namespace PDX.Domain.Commodity {
    [Table ("device_accessories", Schema = "commodity")]
    public class DeviceAccessories : BaseEntity {
        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("product_id")]
        public int ProductID { get; set; }

        [Column ("accessory_type_id")]
        public int AccessoryTypeID { get; set; }

        [Column ("model")]
        public string Model { get; set; }

        [Column ("description")]
        public string Description { get; set; }

        [NavigationProperty]
        [ForeignKey ("AccessoryTypeID")]
        public virtual AccessoryType AccessoryType { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey ("ProductID")]
        public virtual Product Product { get; set; }
    }
}