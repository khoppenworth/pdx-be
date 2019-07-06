using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PDX.Domain.Common
{
    [Table("responder_type", Schema = "common")]
    public class ResponderType : LookUpEntity
    {
        [Required]
        [Column("responder_type_code")]
        public string ResponderTypeCode { get; set; }
    }
}