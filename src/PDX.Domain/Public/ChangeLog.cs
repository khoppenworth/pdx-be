using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Common; 
using PDX.Domain.Helpers; 

namespace PDX.Domain.Public {
    [Table("change_log", Schema = "public")]
    public class ChangeLog:BaseEntity {

        [Required]
        [Column("version")]
        [MaxLength(100)]
        public string Version {get; set; }

        [Column("release_date")]
        public DateTime ReleaseDate {get; set; }

        [Required]
        [Column("content")]
        public string Content {get; set; }

        [NotMapped]
        public dynamic ContentObject{get;set;}
    }
}