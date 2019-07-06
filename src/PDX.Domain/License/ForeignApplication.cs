using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.License
{
    [Table("foreign_application", Schema = "license")]
    public class ForeignApplication : BaseEntity
    {
        [Column("ma_id")]
        public int MAID { get; set; }
        [Column("country_id")]
        public int CountryID { get; set; }

        [Column("foreign_application_status_id")]
        public int ForeignApplicationStatusID { get; set; }

        [Column("certificate_number")]
        public string CertificateNumber { get; set; }
        [Column("decision_date")]
        public Nullable<DateTime> DecisionDate { get; set; }

        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }

        [NavigationProperty]
        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
        
        [NavigationProperty]
        [ForeignKey("ForeignApplicationStatusID")]
        public virtual ForeignApplicationStatus ForeignApplicationStatus { get; set; }
    }
}