using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("sequence", Schema = "common")]
    public class Sequence : BaseEntity
    {
        [Required]
        [Column("table_name")]
        [MaxLength(100)]
        public string TableName { get; set; }

        [Column("prefix")]
        [MaxLength(100)]
        public string Prefix { get; set; }

        [Column("suffix")]
        [MaxLength(100)]
        public string Suffix { get; set; }

        [Column("delimiter")]
        public string Delimiter { get; set; }
        [Column("length")]
        public int Length { get; set; }
        [Column("last_value")]
        public int LastValue { get; set; }
    }
}