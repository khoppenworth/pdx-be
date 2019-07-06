using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PDX.Domain.Account
{
    [Table("menu_role", Schema = "account")]
    public class MenuRole: BaseEntity
    {
        [Column("role_id")]
        public int RoleID { get; set; }

        [Column("menu_id")]
        public int MenuID { get; set; }

        [JsonIgnore]
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        
        [JsonIgnore]
        [ForeignKey("MenuID")]
        public virtual Menu Menu { get; set; }
    }
}
