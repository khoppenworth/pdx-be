using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;
using Newtonsoft.Json;
using System.Linq;

namespace PDX.Domain.Commodity {
    [Table ("md_device_presentation", Schema = "commodity")]
    public class MDDevicePresentation : BaseEntity {
        [Column ("md_model_size_id")]
        public int MDModelSizeID { get; set; }

        [Column ("pack_size_id")]
        public int PackSizeID { get; set; }
        [NotMapped]
        public string Model { get{return MDModelSize?.Model;} }
        [NotMapped]
        public string Size{ get{return MDModelSize?.Size;} }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey ("MDModelSizeID")]
        public virtual MDModelSize MDModelSize { get; set; }

        [NavigationProperty]
        [ForeignKey ("PackSizeID")]
        public virtual PackSize PackSize { get; set; }
    }
}