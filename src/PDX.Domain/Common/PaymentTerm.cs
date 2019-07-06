using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("payment_term", Schema = "common")]
    public class PaymentTerm:LookUpBaseEntity
    {
        [Required]
        [Column("payment_term_code")]
        public string PaymentTermCode { get; set; }
    }
}