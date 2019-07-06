using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using PDX.Domain.Common;
using PDX.Domain.Helpers; 

namespace PDX.Domain.Schedule
{
    [Table("email", Schema = "schedule")]
    public class Email:BaseEntity
    {
        [Required]
        [Column("reference_id")]
        public int ReferenceID {get; set; }
        [Required]
        [Column("email_type_id")]
        public int EmailTypeID {get; set; }
        [Required]
        [Column("created_by_id")]
        public int CreatedByID {get; set; }

        [Column("updated_by_id")]
        public int UpdatedByID {get; set; }

        [Column("receiver")]
        public string Receiver{get;set;}

        
        [NavigationProperty]
        [ForeignKey("EmailTypeID")]
        public virtual EmailType EmailType {get; set; }
    }
}