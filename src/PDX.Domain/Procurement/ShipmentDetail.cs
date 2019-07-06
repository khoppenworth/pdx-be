using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Helpers;

namespace PDX.Domain.Procurement
{
    [Table("shipment_detail", Schema = "procurement")]
    public class ShipmentDetail : BaseEntity
    {
        [Column("shipment_id")]
        public int ShipmentID { get; set; }
        [Column("import_permit_detail_id")]
        public int ImportPermitDetailID { get; set; }
        [Column("quantity")]
        public decimal Quantity { get; set; }
        [Column("unit_quantity")]
        public decimal UnitQuantity { get; set; }
        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }
        [Column("batch_number")]
        public string BatchNumber { get; set; }

        [JsonIgnore]
        [ForeignKey("ShipmentID")]
        public virtual Shipment Shipment { get; set; }

        [NavigationProperty]
        [ForeignKey("ImportPermitDetailID")]
        public virtual ImportPermitDetail ImportPermitDetail { get; set; }
    }
}