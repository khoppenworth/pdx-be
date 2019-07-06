using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Account;
using PDX.Domain.Helpers;
using PDX.Domain.License;

namespace PDX.Domain.License
{
    [Table("ma_log_status", Schema = "license")]
    public class MALogStatus:BaseEntity
    {
        [Column("ma_id")]
        public int MAID { get; set; }

        [Column("from_status_id")]
        public Nullable<int> FromStatusID { get; set; }

        [Column("to_status_id")]
        public int ToStatusID { get; set; }

        [Column("is_current")]
        public bool IsCurrent { get; set; }

        [Column("comment")]
        [MaxLength(1000)]
        public string Comment { get; set; }

        [Column("modified_by_user_id")]
        public int ModifiedByUserID { get; set; }
        
        
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }
        [ForeignKey("FromStatusID")]

        [NavigationProperty]
        public virtual MAStatus FromMAStatus { get; set; }
        [ForeignKey("ToStatusID")]

        [NavigationProperty]
        public virtual MAStatus ToMAStatus { get; set; }
        [ForeignKey("ModifiedByUserID")]

        [NavigationProperty]
        public virtual User ModifiedByUser { get; set; }
    }
}