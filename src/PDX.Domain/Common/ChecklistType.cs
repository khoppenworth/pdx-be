using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("checklist_type", Schema = "common")]
    public class ChecklistType:LookUpEntity
    {
        [Required]
        [Column("checklist_type_code")]
        public string ChecklistTypeCode { get; set; }
    }
}