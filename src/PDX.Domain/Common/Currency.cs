using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("currency", Schema = "common")]
    public class Currency:BaseEntity
    {
        [Required]
        [Column("name")]
        public string Name{get;set;}

        [Column("symbol")]
        public string Symbol { get; set; }

        [Column("short_name")]
        [MaxLength(100)]
        public string ShortName { get; set; }
    }
}