using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Commodity {
    [Table ("food_composition", Schema = "commodity")]
    public class FoodComposition : BaseEntity {
        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("product_id")]
        public int ProductID { get; set; }

        [Column ("amount")]
        public decimal Amount { get; set; }

        [Column ("unit_function")]
        public string UnitFunction { get; set; }
        [Column ("composition_type")]
        public string CompositionType { get; set; }
    }
}