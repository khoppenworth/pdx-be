using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Catalog
{
    [Table("option", Schema = "catalog")]
    public class Option: LookUpEntity
    {
        [Column("option_group_id")]
        public int OptionGroupID { get; set; }
        [Column("priority")]
        public int Priority { get; set; }

        [JsonIgnore]
        [ForeignKey("OptionGroupID")]
        public virtual OptionGroup OptionGroup { get; set; }
    }
}