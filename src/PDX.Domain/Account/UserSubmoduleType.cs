using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Account
{
    [Table ("user_submodule_type", Schema = "account")]
    public class UserSubmoduleType:BaseEntity
    {
         [Column("user_role_id")]
        public int UserRoleID { get; set; }

        [Column("submodule_type_id")]
        public int SubmoduleTypeID { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey("UserRoleID")]
        public virtual UserRole UserRole { get; set; }

        [NavigationProperty]
        [ForeignKey("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }
    }
}