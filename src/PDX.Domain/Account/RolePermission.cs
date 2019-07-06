using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Account
{
    [Table("role_permission", Schema = "account")]
    public class RolePermission:BaseEntity
    {
        [Column("role_id")]
        public int RoleID {get;set;}

        [Column("permission_id")]
        public int PermissionID{get;set;}

        [JsonIgnore]
        [ForeignKey("RoleID")]
        public Role Role{get;set;}
        
        [NavigationProperty]
        [ForeignKey("PermissionID")]
        public Permission Permission{get;set;}
    }
}