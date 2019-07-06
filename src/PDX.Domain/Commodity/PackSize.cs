using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity {
    [Table ("pack_size", Schema = "commodity")]
    public class PackSize : LookUpEntity {
        [Column ("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }

        [NavigationProperty]
        [ForeignKey ("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }

        [NotMapped]
        public string SubmoduleTypeCode { get; set; }
    }
}