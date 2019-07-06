using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("report_type", Schema = "common")]
    public class ReportType:LookUpBaseEntity
    {
        [Required]
        [Column("report_type_code")]
        public string ReportTypeCode { get; set; }
    }
}