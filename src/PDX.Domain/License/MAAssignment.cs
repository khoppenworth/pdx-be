using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Account;
using PDX.Domain.Helpers;
using PDX.Domain.License;
using Newtonsoft.Json;

namespace PDX.Domain.License
{
    [Table("ma_assignment", Schema = "license")]
    public class MAAssignment:BaseEntity
    {
        [Column("ma_id")]
        public int MAID { get; set; }

        [Column("assigned_by_user_id")]
        public int AssignedByUserID { get; set; }
        [Column("assigned_to_user_id")]
        public int AssignedToUserID { get; set; }
        [Column("responder_type_id")]
        public int ResponderTypeID { get; set; }
        [Column("due_date")]
        public Nullable<DateTime> DueDate { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
        
        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }
        
        [ForeignKey("AssignedByUserID")]
        [NavigationProperty]
        public virtual User AssignedByUser { get; set; }
        [ForeignKey("AssignedToUserID")]
        [NavigationProperty]
        public virtual User AssignedToUser { get; set; }
        [ForeignKey("ResponderTypeID")]
        [NavigationProperty]
        public virtual ResponderType ResponderType { get; set; }
    }
}