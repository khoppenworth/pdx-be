using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity {
    [Table ("product_md", Schema = "commodity")]
    public class ProductMD : BaseEntity {

        [Column ("device_class_id")]
        public int DeviceClassID { get; set; }

        [Column ("classification_rule")]
        public string ClassificationRule { get; set; }

        [Column ("use_period")]
        public string UsePeriod { get; set; }

        [Column ("md_grouping_id")]
        public int MDGroupingID { get; set; }
        [Column ("family_name")]
        public string FamilyName { get; set; }

        [NavigationProperty]
        [ForeignKey ("DeviceClassID")]
        public virtual DeviceClass DeviceClass { get; set; }

        [NavigationProperty]
        [ForeignKey ("MDGroupingID")]
        public virtual MDGrouping MDGrouping { get; set; }

    }
}