using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("use_category", Schema = "commodity")]
    public class UseCategory : LookUpEntity
    {  
        [Column("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }   

        [NavigationProperty]
        [ForeignKey("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }        
    }
}