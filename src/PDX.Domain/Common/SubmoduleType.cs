using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PDX.Domain.Common {
    [Table ("submodule_type", Schema = "common")]
    public class SubmoduleType : BaseEntity {
        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("description")]
        [MaxLength (1000)]
        public string Description { get; set; }

        [Column ("submodule_type_code")]
        public string SubmoduleTypeCode { get; set; }
    }
}