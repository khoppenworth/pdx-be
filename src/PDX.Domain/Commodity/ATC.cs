using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("atc", Schema = "commodity")]
    public class ATC : LookUpEntity
    {
        [Required]
        [Column("atc_code")]
        [MaxLength(10)]
        public string ATCCode { get; set; }

       
    }
}