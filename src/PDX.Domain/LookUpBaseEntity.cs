using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain
{
    public class LookUpBaseEntity:BaseEntity
    {
        [Required]         
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Column("short_name")]
        [MaxLength(100)]
        public string ShortName { get; set; }
    }
}