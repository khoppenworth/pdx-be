using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("product_atc", Schema = "commodity")]
    public class ProductATC : BaseEntity
    {        
        [Column("atc_id")]
        public int ATCID { get; set; }
        [Column("product_id")]
        public int ProductID { get; set; }

        [NavigationProperty]
        [ForeignKey("ATCID")]
        public virtual ATC ATC { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}