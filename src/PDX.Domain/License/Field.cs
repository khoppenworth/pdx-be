using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Helpers;

namespace PDX.Domain.License
{
    [Table("field", Schema = "license")]
    public class Field:BaseEntity
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("type")]
        public string Type { get; set; }
        [Column("is_editable")]
        public bool IsEditable { get; set; }

        [Column("parent_field_id")]
        public Nullable<int> ParentFieldID { get; set; }

        [Column("priority")]
        public int Priority { get; set; }
        [Required]
        [Column("field_code")]
        public string FieldCode { get; set; }

        [Column("is_variation_type")]
        public bool IsVariationType { get; set; }

        [Column("is_major")]
        public bool IsMajor { get; set; }
        [Column("is_food_type")]
        public Nullable<bool> IsFoodType { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey("ParentFieldID")]
        public virtual Field ParentField { get; set; }

        [NotMapped]
        public virtual IEnumerable<Field> Children { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } = false;
        [JsonIgnore]
        [NavigationProperty]
        public virtual IEnumerable<FieldSubmoduleType> FieldSubmoduleTypes{get;set;}
    }
}