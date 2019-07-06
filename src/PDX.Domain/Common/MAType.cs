using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;

namespace PDX.Domain.Common {
    [Table ("ma_type", Schema = "common")]
    public class MAType : LookUpEntity {
        [Required]
        [Column ("ma_type_code")]
        public string MATypeCode { get; set; }

        [Column ("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }

        [NavigationProperty]
        [ForeignKey ("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }
    }
}