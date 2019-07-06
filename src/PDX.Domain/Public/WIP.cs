using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Common; 
using PDX.Domain.Helpers; 

namespace PDX.Domain.Public {
    [Table("wip", Schema = "public")]
    public class WIP:BaseEntity {

        [Required]
        [Column("type")]
        [MaxLength(100)]
        public string Type {get; set; }

        [Required]
        [Column("content")]
        public string Content {get; set; }

        [Column("user_id")]
        public int UserID { get; set; }        
        
        [NavigationProperty]
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [NotMapped]
        public dynamic ContentObject{get;set;}
        
        [NotMapped]
        public string WIPNumber{get{return $"{ID.ToString().PadLeft(5, '0')}/Draft/{CreatedDate.Year}"; }}
    }
}