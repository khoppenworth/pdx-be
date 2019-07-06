using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("agent_type", Schema = "common")]
    public class AgentType:LookUpBaseEntity
    {
        [Required]
        [Column("agent_type_code")]
        public string AgentTypeCode { get; set; }
    }
}