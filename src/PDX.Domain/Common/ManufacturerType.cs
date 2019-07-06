using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("manufacturer_type", Schema = "common")]
    public class ManufacturerType:LookUpEntity
    {
        [Required]
        [Column("manufacturer_type_code")]
        public string ManufacturerTypeCode { get; set; }
    }
}