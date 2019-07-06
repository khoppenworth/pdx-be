using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("agent_level", Schema = "common")]
    public class AgentLevel:LookUpBaseEntity
    {
        [Required]
        [Column("agent_level_code")]
        public string AgentLevelCode { get; set; }
    }
}