using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Account
{
    [Table("permission", Schema = "account")]
    public class Permission:BaseEntity
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("permission_code")]
        public string PermissionCode { get; set; }
        
        [Column("category")]
        public string Category { get; set; }
    }
}