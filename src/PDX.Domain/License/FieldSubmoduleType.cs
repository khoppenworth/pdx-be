using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PDX.Domain.License
{
    [Table("field_submodule_type", Schema = "license")]
    public class FieldSubmoduleType:BaseEntity
    {
        [Column("submodule_type_id")]
        public int SubmoduleTypeID { get; set; }
        [Column("field_id")]
        public int FieldID { get; set; }
        [Column("is_major")]
        public bool IsMajor { get; set; }
        [Column("is_variation_type")]
        public bool IsVariationType { get; set; }


        [ForeignKey("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }
        [NavigationProperty]
       
        [ForeignKey("FieldID")]
        public virtual Field Field { get; set; }

        [NotMapped]
        public virtual IEnumerable<FieldSubmoduleType> Children { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        [NotMapped]
        public Nullable<int> ParentFieldID { get; set; }
        
    }
}