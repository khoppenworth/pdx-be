using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Procurement
{
    [Table("shipment", Schema = "procurement")]
    public class Shipment : BaseEntity
    {
        [Column("import_permit_id")]
        public int ImportPermitID { get; set; }
        [Column("release_number")]
        public string ReleaseNumber { get; set; }
        [Column("shipment_status_id")]
        public int ShipmentStatusID { get; set; }
        [Column("is_partial_shipment")]
        public bool IsPartialShipment { get; set; }
        [Column("created_by_user_id")]
        public int CreatedByUserID { get; set; }
        [Column("inspectors")]
        public string Inspectors{get;set;}

        [NavigationProperty]
        [ForeignKey("ImportPermitID")]
        public virtual ImportPermit ImportPermit { get; set; }

        [NavigationProperty]
        [ForeignKey("ShipmentStatusID")]
        public virtual ShipmentStatus ShipmentStatus { get; set; }
        [NavigationProperty]
        [ForeignKey("CreatedByUserID")]
        public virtual User User { get; set; }
        [NavigationProperty]
        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }
    }
}