using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("payment_mode", Schema = "common")]
    public class PaymentMode:LookUpBaseEntity
    {
        [Required]
        [Column("payment_code")]
        public string PaymentCode { get; set; }
    }
}