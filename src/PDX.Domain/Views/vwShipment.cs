using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views
{
    [Table("vwshipment", Schema = "procurement")]
    public class vwShipment:BaseEntity
    {
        [Column("import_permit_id")]
        public int ImportPermitID { get; set; }
        [Column("release_number")]
        public string ReleaseNumber { get; set; }
        [Column("shipment_status_id")]
        public int ShipmentStatusID { get; set; }
        [Column("is_partial_shipment")]
        public bool IsPartialShipment { get; set; }
        [Column("created_by_user_id")]
        public int CreatedByUserID { get; set; }
        [Column("shipment_status")]
        public string ShipmentStatus { get; set; }
        [Column("shipment_status_code")]
        public string ShipmentStatusCode { get; set; }
        [Column("created_by_user")]
        public string CreatedByUser { get; set; }
        [Column("application_date")]
        public Nullable<DateTime> ApplicationDate { get; set; }
        [Column("inspection_start_date")]
        public Nullable<DateTime> InspectedStartDate { get; set; }
        [Column("inspection_end_date")]
        public Nullable<DateTime> InspectedEndDate { get; set; }
        [Column("release_date")]
        public Nullable<DateTime> ReleasedDate { get; set; }

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

        [Column("agent_name")]
        public string AgentName { get; set; }
        [Column("supplier_name")]
        public string SupplierName { get; set; }

        [Column("port_of_entry")]
        public string PortOfEntry { get; set; }

        [Column("payment_mode")]
        public string PaymentMode { get; set; }

        [Column("shipping_method")]
        public string ShippingMethod { get; set; }

        [Column("currency")]
        public string Currency { get; set; }
        [Column("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [Column("import_permit_status")]
        public string ImportPermitStatus { get; set; }

        [Column("inspectors")]
        public string Inspectors { get; set; }
    }
}