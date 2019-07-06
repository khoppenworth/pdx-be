using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.Finance;
using PDX.Domain.Helpers;

namespace PDX.Domain.License {
    //Market Authorizaion 
    [Table ("ma", Schema = "license")]
    public class MA : BaseEntity {
        [Required]
        [MaxLength (100)]
        [Column ("ma_number")]
        public string MANumber { get; set; }

        [MaxLength (100)]
        [Column ("verification_number")]
        public string VerificationNumber { get; set; }

        [MaxLength (100)]
        [Column ("certificate_number")]
        public string CertificateNumber { get; set; }

        [Column ("supplier_id")]
        public int SupplierID { get; set; }

        [Column ("ma_type_id")]
        public int MATypeID { get; set; }

        [Column ("agent_id")]
        public int AgentID { get; set; }

        [Column ("ma_status_id")]
        public int MAStatusID { get; set; }

        [Column ("is_sra")]
        public bool IsSRA { get; set; }

        [Column ("sra")]
        public string SRA { get; set; }

        [Column ("registration_date")]
        public Nullable<DateTime> RegistrationDate { get; set; }

        [Column ("expiry_date")]
        public Nullable<DateTime> ExpiryDate { get; set; }

        [Column ("created_by_user_id")]
        public int CreatedByUserID { get; set; }

        [Column ("modified_by_user_id")]
        public int ModifiedByUserID { get; set; }

        [Column ("original_ma_id")]
        public int? OriginalMAID { get; set; }

        [Column ("is_premarket_lab_request")]
        public bool IsPremarketLabRequest { get; set; }

        [Column ("is_notification_type")]
        public bool IsNotificationType { get; set; }

        [Column ("is_food_notification")]
        public bool? IsFoodNotification { get; set; }

        [Column ("is_legacy_updated")]
        public bool IsLegacyUpdated { get; set; }

        [Column ("remark")]
        public string Remark { get; set; }

        [NavigationProperty]
        [ForeignKey ("AgentID")]
        public virtual Agent Agent { get; set; }

        [NavigationProperty]
        [ForeignKey ("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [NavigationProperty]
        [ForeignKey ("MATypeID")]
        public virtual MAType MAType { get; set; }

        [NavigationProperty]
        [ForeignKey ("MAStatusID")]
        public virtual MAStatus MAStatus { get; set; }

        [NavigationProperty]
        [ForeignKey ("CreatedByUserID")]
        public virtual User CreatedByUser { get; set; }

        [NavigationProperty]
        [ForeignKey ("ModifiedByUserID")]
        public virtual User ModifiedByUser { get; set; }

        [NavigationProperty]
        [ForeignKey ("OriginalMAID")]
        public virtual MA OriginalMA { get; set; }

        [NavigationProperty]
        public virtual ICollection<ForeignApplication> ForeignApplications { get; set; }

        [NavigationProperty]
        public virtual ICollection<MAChecklist> MAChecklists { get; set; }

        [NavigationProperty]
        public virtual ICollection<MAPayment> MAPayments { get; set; }

        [NavigationProperty]
        public virtual ICollection<MAAssignment> MAAssignments { get; set; }
        [NavigationProperty]
        public virtual MAVariationSummary MAVariationSummary { get; set; }

        [NotMapped]
        public bool IsExpired { get { return ExpiryDate < DateTime.Now; } }

    }
}