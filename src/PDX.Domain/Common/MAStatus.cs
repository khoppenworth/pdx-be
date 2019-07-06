using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("ma_status", Schema = "common")]
    public class MAStatus : LookUpEntity
    {
        [Required]
        [Column("ma_status_code")]
        public string MAStatusCode { get; set; }
        [Column("priority")]
        public int? Priority { get; set; }
        [Column("display_name")]
        public string DisplayName { get; set; }
    }
}