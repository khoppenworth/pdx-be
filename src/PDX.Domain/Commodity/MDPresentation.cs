using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity {
    [Table ("md_presentation", Schema = "commodity")]
    public class MDPresentation : BaseEntity {
        [Column ("product_id")]
        public int ProductID { get; set; }

        [Column ("pack_size_id")]
        public int? PackSizeID { get; set; }

        [Column ("size")]
        public string Size { get; set; }
        [Column ("description")]
        public string Description { get; set; }

        [NavigationProperty]
        [JsonIgnore]
        [ForeignKey ("ProductID")]
        public virtual Product Product { get; set; }

        [NavigationProperty]
        [ForeignKey ("PackSizeID")]
        public virtual PackSize PackSize { get; set; }

        [Column ("model")]
        public string Model { get; set; }  
  
    }
}