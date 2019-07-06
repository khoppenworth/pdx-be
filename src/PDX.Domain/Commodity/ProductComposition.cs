using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("product_composition", Schema = "commodity")]
    public class ProductComposition : BaseEntity
    {
        [Column("dosage_strength")]
        public string DosageStrength { get; set; }
        [Column("dosage_unit_id")]
        public int DosageUnitID { get; set; }
        [Column("dosage_strength_id")]
        public int? DosageStrengthID { get; set; }
        [Column("pharmacopoeia_tandard_id")]
        public int PharmacopoeiaStandardID { get; set; }
        [Column("product_id")]
        public int ProductID { get; set; }
        [Column("is_active_composition")]
        public bool IsActiveComposition { get; set; }
        [Column("excipient_id")]
        public int? ExcipientID { get; set; }
        [Column("inn_id")]
        public int? INNID { get; set; }
        [Column("function")]
        public string Function { get; set; }

        [Column("is_diluent")]
        public bool IsDiluent { get; set; }

        [Column("parent_product_composition_id")]
        public int? ParentProductCompositionID { get; set; }

        [NavigationProperty]
        [ForeignKey("DosageUnitID")]
        public virtual DosageUnit DosageUnit { get; set; }
        [NavigationProperty]
        [ForeignKey("DosageStrengthID")]
        public virtual DosageStrength DosageStrengthObj { get; set; }
        [NavigationProperty]
        [ForeignKey("PharmacopoeiaStandardID")]
        public virtual PharmacopoeiaStandard PharmacopoeiaStandard { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [NavigationProperty]
        [ForeignKey("ExcipientID")]
        public virtual Excipient Excipient { get; set; }
        [NavigationProperty]
        [ForeignKey("INNID")]
        public virtual INN INN { get; set; }

    }
}