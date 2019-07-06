using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Helpers;

namespace PDX.Domain.Account
{
    [Table("menu", Schema = "account")]
    public class Menu : BaseEntity
    {
        [Column("parent_menu_id")]
        public Nullable<int> ParentMenuID { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("url")]
        public string URL { get; set; }

        [Column("icon")]
        public string Icon { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Required]
        [Column("menu_code")]
        public string MenuCode { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey("ParentMenuID")]
        public virtual Menu ParentMenu { get; set; }

        public virtual ICollection<MenuRole> MenuRoles { get; set; }

        [NotMapped]
        public int ParentMenuPriority { get { return ParentMenu != null ? ParentMenu.Priority : 0; } }
    }
}