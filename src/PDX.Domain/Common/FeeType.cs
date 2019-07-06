using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using PDX.Domain.Common; 
using PDX.Domain.Helpers; 

namespace PDX.Domain.Common
{
    [Table("fee_type", Schema = "common")]
    public class FeeType:LookUpEntity
    {
        [Column("currency_id")]
        public int CurrencyID { get; set; }

        [NavigationProperty]
        [ForeignKey("CurrencyID")]
        public virtual Currency Currency { get; set; }
    }
}