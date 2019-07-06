using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PDX.Domain.License {
    [Table ("ma_variation_summary", Schema = "license")]
    public class MAVariationSummary : BaseEntity {
        [Column ("ma_id")]
        public int MAID { get; set; }

        [Column ("major_variation_count")]
        public int MajorVariationCount { get; set; }

        [Column ("minor_variation_count")]
        public int MinorVariationCount { get; set; }
        [Column ("created_by_user_id")]
        public int CreatedByUserID { get; set; }

        [Required]
        [Column ("variation_summary")]
        public string VariationSummary { get; set; }

        [JsonIgnore]
        [ForeignKey ("MAID")]
        public virtual MA MA { get; set; }
    }
}