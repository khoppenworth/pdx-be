using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain
{
    public class LookUpEntity:BaseEntity
    {
        [Required]         
        [Column("name")]
        public string Name { get; set; }
    }
}