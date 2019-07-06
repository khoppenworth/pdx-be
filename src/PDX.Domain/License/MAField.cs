using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Account;
using PDX.Domain.Catalog;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.License
{
    [Table("ma_field", Schema = "license")]
    public class MAFieldObselete:BaseEntity
    {
        [Column("ma_id")]
        public int MAID { get; set; }
        [Column("field_id")]
        public int FieldID { get; set; }
        [Column("created_by_user_id")]
        public int CreatedByUserID { get; set; }
        [Column("is_variation")]
        public bool IsVariation { get; set; }

        
        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }

        [NavigationProperty]
        [ForeignKey("FieldID")]
        public virtual Field Field { get; set; }
        [NavigationProperty]
        [ForeignKey("CreatedByUserID")]
        public virtual User CreatedByUser { get; set; }        
    }
}