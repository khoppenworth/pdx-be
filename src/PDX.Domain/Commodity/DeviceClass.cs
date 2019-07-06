using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Commodity {
    [Table ("device_class", Schema = "commodity")]
    public class DeviceClass : BaseEntity {
        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("description")]
        [MaxLength (1000)]
        public string Description { get; set; }
        [Column ("device_class_code")]
        public string DeviceClassCode { get; set; }
    }
}