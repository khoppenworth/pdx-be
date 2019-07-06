using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity {
    [Table ("md_grouping", Schema = "commodity")]
    public class MDGrouping : BaseEntity {
        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("md_grouping_code")]
        public string MDGroupingCode { get; set; }

    }
}