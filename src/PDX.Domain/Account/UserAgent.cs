using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;
using Newtonsoft.Json;

namespace PDX.Domain.Account
{
    [Table("user_agent", Schema = "account")]
    public class UserAgent:BaseEntity
    {
        [Column("user_id")]
        public int UserID { get; set; }

        [Column("agent_id")]
        public int AgentID { get; set; }

        [JsonIgnore]
        [NavigationProperty]

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [NavigationProperty]
        
        [ForeignKey("AgentID")]
        public virtual Agent Agent { get; set; }
    }
}