using System;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;

namespace PDX.Domain.Document
{
    [Table("print_log", Schema = "document")]
    public class PrintLog : BaseEntity
    {
        [Column("printed_by_user_id")]
        public int PrintedByUserID { get; set; }
        [Column("document_id")]
        public int DocumentID { get; set; }
        [Column("printed_date")]
        public DateTime PrintedDate { get; set; }
        [ForeignKey("PrintedByUserID")]
        public virtual User PrintedByUser{get;set;}

        [ForeignKey("DocumentID")]
        public virtual Document Document{get;set;}
    }
}