using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("shipment_status", Schema = "common")]
    public class ShipmentStatus:LookUpBaseEntity
    {
        [Required]
        [Column("shipment_status_code")]
        public string ShipmentStatusCode { get; set; }
        
    }
}