using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("country", Schema = "common")]
    public class Country:BaseEntity
    {
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("country_code")]
        [MaxLength(5)]
        public string CountryCode { get; set; }
    }
}