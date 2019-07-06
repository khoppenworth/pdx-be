using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Finance
{
    [Table("submodule_fee", Schema = "finance")]
    public class SubmoduleFee : BaseEntity
    {
        [Column("submodule_id")]
        public int SubmoduleID { get; set; }
        [Column("fee_type_id")]
        public int FeeTypeID { get; set; }
        [Column("fee")]
        public decimal Fee { get; set; }

        [NavigationProperty]
        [ForeignKey("FeeTypeID")]
        public virtual FeeType FeeType { get; set; }
        [NavigationProperty]
        [ForeignKey("SubmoduleID")]
        public virtual Submodule Submodule { get; set; }
    }
}