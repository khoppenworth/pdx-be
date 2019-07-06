using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using PDX.Domain.Helpers; 

namespace PDX.Domain.Schedule
{
    [Table("schedule", Schema = "schedule")]
    public class Schedule
    {
        [Required]
        [Column("reference_id")]
        public int ReferenceID {get; set; }
        [Required]
        [Column("schedule_type_id")]
        public int ScheduleTypeID {get; set; }
        [Required]
        [Column("schedule_status_id")]
        public int ScheduleStatusID {get; set; }

        [Column("result")]
        public string Result {get; set; }

        
        [NavigationProperty]
        [ForeignKey("ScheduleTypeID")]
        public virtual ScheduleType ScheduleType {get; set; }

        [NavigationProperty]
        [ForeignKey("ScheduleStatusID")]
        public virtual ScheduleStatus ScheduleStatus {get; set; }
    }
}