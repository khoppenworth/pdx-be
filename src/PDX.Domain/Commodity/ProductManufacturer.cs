using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("product_manufacturer", Schema = "commodity")]
    public class ProductManufacturer : BaseEntity
    {
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("manufacturer_address_id")]
        public int ManufacturerAddressID { get; set; }

        [Column("manufacturer_type_id")]
        public Nullable<int> ManufacturerTypeID { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [NavigationProperty]
        [ForeignKey("ManufacturerTypeID")]
        public virtual ManufacturerType ManufacturerType { get; set; }
        
        [NavigationProperty]
        [ForeignKey("ManufacturerAddressID")]
        public virtual ManufacturerAddress ManufacturerAddress { get; set; }

    }
}