using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace PDX.Domain.Schedule
{
    [Table("schedule_type", Schema = "schedule")]
    public class ScheduleType:LookUpBaseEntity
    {
        [Required]
        [Column("interval")]
        public int Interval{get;set;}
        [Required]
        [Column("schedule_type_code")]
        public string ScheduleTypeCpde { get; set; }
    }
}