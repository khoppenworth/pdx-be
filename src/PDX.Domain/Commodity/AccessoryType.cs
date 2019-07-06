using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Commodity
{
    [Table ("accessory_type", Schema = "commodity")]
    public class AccessoryType:BaseEntity
    {
        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("description")]
        [MaxLength (1000)]
        public string Description { get; set; }

        [Column ("accessory_type_code")]
        public string AccessoryTypeCode { get; set; }
    }
}