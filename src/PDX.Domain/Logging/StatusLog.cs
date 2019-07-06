using System;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Helpers;

namespace PDX.Domain.Logging
{
    [Table("status_log", Schema = "logging")]
    public class StatusLog : BaseEntity
    {
        [Column("modified_by_user_id")]
        public int ModifiedByUserID { get; set; }
        [Column("entity_type")]
        public string EntityType { get; set; }
        [Column("entity_id")]
        public int EntityID { get; set; }
        [Column("column_name")]
        public string ColumnName { get; set; }
        [Column("column_type")]
        public string ColumnType { get; set; }
        [Column("old_value")]
        public string OldValue { get; set; }
        [Column("new_value")]
        public string NewValue { get; set; }
        [Column("comment")]
        public string Comment { get; set; }

        [NavigationProperty]
        [ForeignKey("ModifiedByUserID")]
        public virtual User ModifiedByUser { get; set; }
    }
}