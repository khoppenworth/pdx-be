using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Customer;
using PDX.Domain.Commodity;
using Newtonsoft.Json;
using PDX.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PDX.Domain.Procurement
{
    [Table("import_permit_detail", Schema = "procurement")]
    public class ImportPermitDetail:BaseEntity
    {
        [Column("import_permit_id")]
        public int ImportPermitID { get; set; }

        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("manufacturer_address_id")]
        public int ManufacturerAddressID { get; set; }

        [Column("quantity")]
        public decimal Quantity { get; set; }

        [Column("unit_price")]
        public decimal UnitPrice { get; set; }
        
        [Column("discount")]
        public decimal? Discount { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("md_device_presentation_id")]
        public int? MDDevicePresentationID { get; set; }

        [JsonIgnore]
        [ForeignKey("ImportPermitID")]
        public virtual ImportPermit ImportPermit { get; set; }

        [NavigationProperty]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [NavigationProperty]
        [ForeignKey("MDDevicePresentationID")]
        public virtual MDDevicePresentation MDDevicePresentation { get; set; }

        [NavigationProperty]
        [ForeignKey("ManufacturerAddressID")]
        public virtual ManufacturerAddress ManufacturerAddress { get; set; }
        [JsonIgnore]
        [NavigationProperty]
        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }

        [NotMapped]
        public decimal Received { get { return ShipmentDetails == null ? 0 : ShipmentDetails.Sum(sd=>sd.Quantity); } }
    }
}