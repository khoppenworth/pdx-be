using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Account;
using PDX.Domain.Helpers;

namespace PDX.Domain.Procurement
{
    [Table("import_permit_log_status", Schema = "procurement")]
    public class ImportPermitLogStatus:BaseEntity
    {
        [Column("import_permit_id")]
        public int ImportPermitID { get; set; }

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
        
        
        [ForeignKey("ImportPermitID")]
        public virtual ImportPermit ImportPermit { get; set; }
        [ForeignKey("FromStatusID")]

        [NavigationProperty]
        public virtual ImportPermitStatus FromImportPermitStatus { get; set; }
        [ForeignKey("ToStatusID")]

        [NavigationProperty]
        public virtual ImportPermitStatus ToImportPermitStatus { get; set; }
        [ForeignKey("ModifiedByUserID")]

        [NavigationProperty]
        public virtual User ModifiedByUser { get; set; }
    }
}