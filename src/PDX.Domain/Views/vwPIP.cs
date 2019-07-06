using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views
{
    [Table("vwpip", Schema = "procurement")]
    public class vwPIP:BaseEntity
    {
        [Column("agent_id")]
        public int AgentID { get; set; }

        [Column("delivery")]
        public string Delivery { get; set; }

        [Column("import_permit_number")]
        public string ImportPermitNumber { get; set; }

        [Column("port_of_entry_id")]
        public int PortOfEntryID { get; set; }

        [Column("payment_mode_id")]
        public int PaymentModeID { get; set; }

        [Column("shipping_method_id")]
        public int ShippingMethodID { get; set; }

        [Column("currency_id")]
        public int CurrencyID { get; set; }

        [Column("import_permit_status_id")]
        public int ImportPermitStatusID { get; set; }

        [Column("performa_invoice_number")]
        public string PerformaInvoiceNumber { get; set; }
        [Column("requested_date")]
        public Nullable<DateTime> RequestedDate { get; set; }

        [Column("expiry_date")]
        public Nullable<DateTime> ExpiryDate { get; set; }

        [Column("freight_cost")]
        public decimal FreightCost { get; set; }
        
        [Column("discount")]
        public decimal? Discount { get; set; }

        [Column("insurance")]
        public decimal? Insurance { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("created_by_user_id")]
        public int CreatedByUserID { get; set; }

        [Column("assigned_user_id")]
        public int? AssignedUserID { get; set; }
        [Column("remark")]
        public string Remark{get;set;}

        [Column("agent_name")]
        public string AgentName { get; set; }
        [Column("supplier_name")]
        public string SupplierName { get; set; }

        [Column("port_of_entry")]
        public string PortOfEntry { get; set; }
        [Column("port_of_entry_sh")]
        public string PortOfEntrySH { get; set; }

        [Column("payment_mode")]
        public string PaymentMode { get; set; }

        [Column("payment_mode_sh")]
        public string PaymentModeSH { get; set; }

        [Column("shipping_method")]
        public string ShippingMethod { get; set; }
        [Column("shipping_method_sh")]
        public string ShippingMethodSH { get; set; }

        [Column("currency")]
        public string Currency { get; set; }
        [Column("currency_symbol")]
        public string CurrencySymbol { get; set; }
        [Column("currency_sh")]
        public string CurrencySH { get; set; }

        [Column("import_permit_status")]
        public string ImportPermitStatus { get; set; }
        [Column("import_permit_status_code")]
        public string ImportPermitStatusCode { get; set; }

        [Column("import_permit_status_sh")]
        public string ImportPermitStatusSH { get; set; }

        [Column("import_permit_status_priority")]
        public int? ImportPermitStatusPriority { get; set; }
        
        [Column("import_permit_status_display_name")]
        public string ImportPermitStatusDisplayName { get; set; }

        [Column("created_by_username")]
        public string CreatedByUsername { get; set; }

        [Column("assigned_user")]
        public string AssignedUser { get; set; }

        [Column("submission_date")]
        public DateTime SubmissionDate { get; set; }
        
        [Column("assigned_date")]
        public Nullable<DateTime> AssignedDate { get; set; }

        [Column("decision_date")]
        public DateTime? DecisionDate { get; set; }
    }
}