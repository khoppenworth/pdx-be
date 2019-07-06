using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity {
    [Table ("product_type", Schema = "commodity")]
    public class ProductType : LookUpEntity {
        [Column ("product_type_code")]
        public string ProductTypeCode { get; set; }

        [Column ("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }

        [Column ("submodule_id")]
        public int? SubmoduleID { get; set; }

        [NavigationProperty]
        [ForeignKey ("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }

        [NavigationProperty]
        [ForeignKey ("SubmoduleID")]
        public virtual Submodule Submodule { get; set; }

        [NotMapped]
        public string SubmoduleTypeCode { get; set; }

        [NotMapped]
        public string SubmoduleCode { get; set; }
    }
}