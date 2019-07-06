using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PDX.Domain.Account
{
    [Table("user_role", Schema = "account")]
    public class UserRole: BaseEntity
    {       
        [Column("user_id")]
        public int UserID { get; set; }

        [Column("role_id")]
        public int RoleID { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        [NavigationProperty]
        public virtual ICollection<UserSubmoduleType> UserSubmoduleTypes { get; set; }
        
        
    }
}
