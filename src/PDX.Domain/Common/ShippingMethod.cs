using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("shipping_method", Schema = "common")]
    public class ShippingMethod:LookUpBaseEntity
    {
        [Column("shipping_code")]
        public string ShippingCode { get; set; }
    }
}