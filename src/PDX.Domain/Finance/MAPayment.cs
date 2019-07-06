using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common; 
using PDX.Domain.Helpers;
using PDX.Domain.License;

namespace PDX.Domain.Finance
{
    [Table("ma_payment", Schema = "finance")]
    public class MAPayment:BaseEntity
    {
        
        [Column("ma_id")]
        public int MAID { get; set; }
        [Column("submodule_fee_id")]
        public int SubmoduleFeeID { get; set; }
        [Column("paid_date")]
        public DateTime PaidDate{get;set;}
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("is_additional_variation")]
        public bool IsAdditionalVariation { get; set; }

        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }

        [NavigationProperty]
        [ForeignKey("SubmoduleFeeID")]
        public virtual SubmoduleFee SubmoduleFee {get; set; }
    }
}