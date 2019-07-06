using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("sra", Schema = "commodity")]
    public class SRA : LookUpEntity
    {
        [Required]
        [Column("sra_code")]
        [MaxLength(10)]
        public string SRACode { get; set; }
        
        [Column("sra_type_id")]
        public int SRATypeID { get; set; }

        [NavigationProperty]
        [ForeignKey("SRATypeID")]
        public virtual SRAType SRAType { get; set; }
      
    }
}