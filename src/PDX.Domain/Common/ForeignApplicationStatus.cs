using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("foreign_application_status", Schema = "common")]
    public class ForeignApplicationStatus : LookUpEntity
    {
        [Required]
        [Column("foreign_application_status_code")]
        public string ForeignApplicationStatusCode { get; set; }
    }
}