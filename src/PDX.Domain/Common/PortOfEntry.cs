using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;

namespace PDX.Domain.Common
{
    [Table("port_of_entry", Schema = "common")]
    public class PortOfEntry:LookUpBaseEntity
    {
        [Column("port_code")]
        public string PortCode { get; set; }

        [Column("shipping_method_id")]
        public int ShippingMethodID { get; set; }

        [NavigationProperty]
        [ForeignKey("ShippingMethodID")]
        public virtual ShippingMethod ShippingMethod{get;set;}
    }
}