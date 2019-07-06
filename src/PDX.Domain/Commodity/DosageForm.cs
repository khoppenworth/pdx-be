using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("dosage_form", Schema = "commodity")]
    public class DosageForm : LookUpEntity
    {
        [Column("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }   

        [NavigationProperty]
        [ForeignKey("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }   
    }
}