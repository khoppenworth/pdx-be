using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("pharmacological_classification", Schema = "commodity")]
    public class PharmacologicalClassification : LookUpEntity
    {

        [Required]
        [Column("prefix")]
        public string Prefix { get; set; }
    }
}