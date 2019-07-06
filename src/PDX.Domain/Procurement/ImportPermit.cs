
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Account;
using PDX.Domain.Customer;
using System.Collections.ObjectModel;
using PDX.Domain.Helpers;

namespace PDX.Domain.Procurement
{
    [Table("import_permit", Schema = "procurement")]
    public class ImportPermit : BaseEntity
    {
        public ImportPermit()
        {
            this.ImportPermitDetails = new Collection<ImportPermitDetail>();
        }

        [Column("agent_id")]
        public int AgentID { get; set; }

        [Column("supplier_id")]
        public int SupplierID { get; set; }

        [Required]
        [Column("delivery")]
        [MaxLength(100)]
        public string Delivery { get; set; }

        [Required]
        [Column("import_permit_number")]
        [MaxLength(100)]
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

        [Column("import_permit_type_id")]
        public int ImportPermitTypeID { get; set; }

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
        public Nullable<int> AssignedUserID { get; set; }

        [Column("remark")]
        public string Remark { get; set; }
        [Column("assigned_date")]
        public Nullable<DateTime> AssignedDate { get; set; }
        [Column("submodule_id")]
        public Nullable<int> SubmoduleID { get; set; }


        [NavigationProperty]
        [ForeignKey("AgentID")]
        public virtual Agent Agent { get; set; }
        [NavigationProperty]
        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [NavigationProperty]
        [ForeignKey("PortOfEntryID")]
        public virtual PortOfEntry PortOfEntry { get; set; }
        [NavigationProperty]
        [ForeignKey("PaymentModeID")]
        public virtual PaymentMode PaymentMode { get; set; }
        [NavigationProperty]
        [ForeignKey("ShippingMethodID")]
        public virtual ShippingMethod ShippingMethod { get; set; }
        [NavigationProperty]
        [ForeignKey("CurrencyID")]
        public virtual Currency Currency { get; set; }
        [NavigationProperty]
        [ForeignKey("ImportPermitTypeID")]
        public virtual ImportPermitType ImportPermitType { get; set; }
        [NavigationProperty]
        [ForeignKey("ImportPermitStatusID")]
        public virtual ImportPermitStatus ImportPermitStatus { get; set; }

        [NavigationProperty]
        [ForeignKey("CreatedByUserID")]
        public virtual User User { get; set; }

        [NavigationProperty]
        [ForeignKey("AssignedUserID")]
        public virtual User AssignedUser { get; set; }
        [NavigationProperty]
        [ForeignKey("SubmoduleID")]
        public virtual Submodule Submodule { get; set; }
        [NavigationProperty]
        public virtual ICollection<ImportPermitDetail> ImportPermitDetails { get; set; }
        
        [Column ("submodule_type_code")]
        public string SubmoduleTypeCode { get; set; }
    }
}