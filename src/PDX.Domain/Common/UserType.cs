using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("user_type", Schema = "common")]
    public class UserType:LookUpBaseEntity
    {
        [Required]
        [Column("user_type_code")]
        public string UserTypeCode { get; set; }
    }
}