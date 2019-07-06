using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;

namespace PDX.Domain.Account {
    [Table ("role", Schema = "account")]
    public class Role : BaseEntity {
        public Role () {
            this.Users = new List<User> ();
            this.MenuRoles = new List<MenuRole> ();
            this.Permissions = new List<RolePermission> ();
        }

        [Required]
        [Column ("name")]
        public string Name { get; set; }

        [Column ("description")]
        [MaxLength (1000)]
        public string Description { get; set; }

        [Required]
        [Column ("role_code")]
        public string RoleCode { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [NavigationProperty]
        public virtual ICollection<MenuRole> MenuRoles { get; set; }

        [NavigationProperty]
        public virtual ICollection<RolePermission> Permissions { get; set; }

        [NavigationProperty]
        public virtual ICollection<ReportRole> ReportRoles { get; set; }

        [NotMapped]
        public List<string> SubmoduleTypes { get; set; }
    }
}