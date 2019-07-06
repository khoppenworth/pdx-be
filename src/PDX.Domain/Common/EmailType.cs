using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace PDX.Domain.Common {

    [Table("email_type", Schema = "common")]
    public class EmailType:LookUpBaseEntity {
        [Required]
        [Column("subject")]
        public string Subject {get; set; }
        [Required]
        [Column("body")]
        public string Body {get; set; }
        [Required]
        [Column("email_type_code")]
        public string EmailTypeCode { get; set; }
        [Required]
        [Column("template")]
        public string Template{get;set;}
    }
}