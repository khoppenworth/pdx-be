using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Helpers;

namespace PDX.Domain.Account
{
    [Table("report_role", Schema = "account")]
    public class ReportRole: BaseEntity
    {
        [Column("role_id")]
        public int RoleID { get; set; }

        [Column("report_id")]
        public int ReportID { get; set; }

        [JsonIgnore]
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        
        [NavigationProperty]
        [ForeignKey("ReportID")]
        public virtual PDX.Domain.Report.Report Report { get; set; }
    }
}
