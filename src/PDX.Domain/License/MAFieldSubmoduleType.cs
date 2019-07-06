using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Helpers;
using Newtonsoft.Json;

namespace PDX.Domain.License
{
    [Table("ma_field_submodule_type", Schema = "license")]
    public class MAFieldSubmoduleType:BaseEntity
    {
        [Column("ma_id")]
        public int MAID { get; set; }
        [Column("field_submodule_type_id")]
        public int FieldSubmoduleTypeID { get; set; }
        [Column("is_variation")]
        public bool IsVariation { get; set; }
        [Column("created_by_user_id")]
        public int CreatedByUserID { get; set; }

        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }

        [NavigationProperty]
        [ForeignKey("FieldSubmoduleTypeID")]
        public virtual FieldSubmoduleType FieldSubmoduleType { get; set; }
        [NavigationProperty]
        [ForeignKey("CreatedByUserID")]
        public virtual User CreatedByUser { get; set; }   
        
    }
}