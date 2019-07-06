using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views
{
    [Table("vwma", Schema = "license")]
    public class vwMA : BaseEntity
    {
        [Column("ma_number")]
        public string MANumber { get; set; }

        [Column("verification_number")]
        public string VerificationNumber { get; set; }
        [Column("certificate_number")]
        public string CertificateNumber { get; set; }

        [Column("supplier_id")]
        public int SupplierID { get; set; }
        [Column("ma_type_id")]
        public int MATypeID { get; set; }
        [Column("agent_id")]
        public int AgentID { get; set; }
        [Column("ma_status_id")]
        public int MAStatusID { get; set; }
        [Column("is_sra")]
        public bool IsSRA { get; set; }
        [Column("sra")]
        public string SRA { get; set; }
        [Column("registration_date")]
        public Nullable<DateTime> RegistrationDate { get; set; }
        [Column("expiry_date")]
        public Nullable<DateTime> ExpiryDate { get; set; }
        [Column("created_by_user_id")]
        public int CreatedByUserID { get; set; }
        [Column("modified_by_user_id")]
        public int ModifiedByUserID { get; set; }
        [Column("original_ma_id")]
        public int? OriginalMAID { get; set; }
        [Column("is_premarket_lab_request")]
        public bool IsPremarketLabRequest { get; set; }
        [Column("is_notification_type")]
        public bool IsNotificationType { get; set; }
        [Column("remark")]
        public string Remark { get; set; }



        [Column("agent_name")]
        public string AgentName { get; set; }
        [Column("brand_name")]
        public string BrandName{get;set;}
        [Column("generic_name")]
        public string GenericName { get; set; }
         [Column("product_id")]
        public string ProductID{get;set;}
         [Column("full_item_name")]
        public string FullItemName{get;set;}
        [Column("supplier_name")]
        public string SupplierName { get; set; }
        [Column("ma_status")]
        public string MAStatus { get; set; }
        [Column("ma_status_priority")]
        public int? MAStatusPriority { get; set; }
        [Column("ma_status_display_name")]
        public string MAStatusDisplayName { get; set; }
        [Column("ma_status_code")]
        public string MAStatusCode { get; set; }
        [Column("ma_type")]
        public string MAType { get; set; }
        [Column("ma_type_code")]
        public string MATypeCode { get; set; }
        [Column("expiry_days")]
        public double? ExpiryDays { get; set; }
        [Column("application_type")]
        public string ApplicationType { get; set; }
        [Column("is_fast_tracking")]
        public bool? IsFastTracking { get; set; }
        [Column("prescreener_user_id")]
        public Nullable<int> PrescreenerUserID { get; set; }
        [Column("primary_assessor_user_id")]
        public Nullable<int> PrimaryAssessorUserID { get; set; }
        [Column("secondary_assessor_user_id")]
        public Nullable<int> SecondaryAssessorUserID { get; set; }
        [Column("prescreener")]
        public string Prescreener { get; set; }
        [Column("primary_assessor")]
        public string PrimaryAssessor { get; set; }
        [Column("secondary_assessor")]
        public string SecondaryAssessor { get; set; }
        [Column("is_primary_assessed")]
        public bool? IsPrimaryAssessed { get; set; }
        [Column("is_secondary_assessed")]
        public bool? IsSecondaryAssessed { get; set; }

        [Column("submission_date")]
        public Nullable<DateTime> SubmissionDate { get; set; }

        [Column("prescreened_date")]
        public Nullable<DateTime> PrescreenedDate { get; set; }

        [Column("decision_date")]
        public DateTime? DecisionDate { get; set; }
        [Column("processing_time_in_day")]
        public double? ProcessingTimeInDay { get; set; }
        [Column("is_lab_requested")]
        public bool IsLabRequested { get; set; }
        [Column("is_lab_result_uploaded")]
        public bool IsLabResultUploaded { get; set; }
        [Column("is_clinical_review_uploaded")]
        public bool IsClinicalReviewUploaded { get; set; }
        [Column("prescreener_due_date")]
        public Nullable<DateTime> PrescreenerDueDate { get; set; }
        [Column("primary_assessor_due_date")]
        public Nullable<DateTime> PrimaryAssessorDueDate { get; set; }
        [Column("secondary_assessor_due_date")]
        public Nullable<DateTime> SecondaryAssessorDueDate { get; set; }
        [Column("fir_replied_date")]
        public Nullable<DateTime> FIRRepliedDate { get; set; }
    }
}