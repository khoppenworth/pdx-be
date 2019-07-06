using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace PDX.Domain.Schedule
{
     [Table("schedule_status", Schema = "schedule")]
    public class ScheduleStatus:LookUpBaseEntity
    {   
        [Required]
        [Column("schedule_status_code")]
        public string ScheduleStatusCode { get; set; }
    }
}