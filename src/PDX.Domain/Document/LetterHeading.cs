using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Document
{
    [Table("letter_heading", Schema = "document")]
    public class LetterHeading:BaseEntity
    {
        public string LogoUrl { get; set; }
        public string CompanyName { get; set; }
    }
}