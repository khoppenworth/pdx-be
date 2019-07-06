using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("answer_type", Schema = "common")]
    public class AnswerType:LookUpEntity
    {
        [Required]
        [Column("answer_type_code")]
        public string AnswerTypeCode { get; set; }
    }
}