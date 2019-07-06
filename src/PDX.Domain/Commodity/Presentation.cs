using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("presentation", Schema = "commodity")]
    public class Presentation : BaseEntity
    {
        [Column("dosage_unit_id")]
        public int? DosageUnitID { get; set; }
        [Column("pack_size_id")]
        public int PackSizeID { get; set; }
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("packaging")]
        public string Packaging { get; set; }

        [NavigationProperty]
        [ForeignKey("DosageUnitID")]
        public virtual DosageUnit DosageUnit { get; set; }
        [NavigationProperty]
        [ForeignKey("PackSizeID")]
        public virtual PackSize PackSize { get; set; }
        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

    }
}