using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("commodity_type", Schema = "common")]
    public class CommodityType:LookUpBaseEntity
    {
        [Required]
        [Column("commodity_type_code")]
        public string CommodityTypeCode { get; set; }
    }
}