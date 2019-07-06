using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("import_permit_status", Schema = "common")]
    public class ImportPermitStatus:LookUpBaseEntity
    {
        [Required]
        [Column("import_permit_status_code")]
        public string ImportPermitStatusCode { get; set; }
        [Column("priority")]
        public int? Priority { get; set; }
        [Column("display_name")]
        public string DisplayName{get;set;}
    }
}