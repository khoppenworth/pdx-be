using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;
using System.Linq;
namespace PDX.Domain.Commodity {
    [Table ("md_model_size", Schema = "commodity")]
    public class MDModelSize : BaseEntity {
        [Column ("product_id")]
        public int ProductID { get; set; }

        [Column ("size")]
        public string Size { get; set; }

        [Column ("model")]
        public string Model { get; set; }
        [NotMapped]
        public string BrandName { get{return Product?.BrandName;} }
        [NotMapped]
        public string ProductDescription { get{return Product?.Description;} }
        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey ("ProductID")]
        public virtual Product Product { get; set; }

        [NavigationProperty]
        public virtual ICollection<MDDevicePresentation> MDDevicePresentations { get; set; }

        [NotMapped]
        public string Presentation { get { return MDDevicePresentations?.Aggregate<MDDevicePresentation, string> (String.Empty, (i, j) => i + $"{j?.PackSize?.Name}"); } }

    }
}