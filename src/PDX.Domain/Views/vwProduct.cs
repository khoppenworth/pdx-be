using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views
{
    [Table("vwproduct", Schema = "commodity")]
    public class vwProduct : BaseEntity
    {

        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("supplier_id")]
        public int SupplierID { get; set; }

        [Column("agent_id")]
        public int AgentID { get; set; }

        [Column("registration_date")]
        public DateTime? RegistrationDate { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("supplier_name")]
        public string SupplierName { get; set; }

        [Column("agent_name")]
        public string AgentName { get; set; }
        [Column("manufacturer_name")]
        public string ManufacturerName { get; set; }
        [Column("brand_name")]
        public string BrandName { get; set; }
        [Column("generic_name")]
        public string GenericName { get; set; }
        [Column("full_item_name")]
        public string FullItemName { get; set; }
        [Column("shelf_life")]
        public string ShelfLife { get; set; }
        [Column("presentation")]
        public string Presentation { get; set; }

        [Column("ma_number")]
        public string MANumber { get; set; }
        [Column("ma_status")]
        public string MAStatus { get; set; }
        [Column("ma_status_display_name")]
        public string MAStatusDisplayName { get; set; }
        [Column("ma_status_code")]
        public string MAStatusCode { get; set; }
        [Column("dosage_form")]
        public string DosageForm { get; set; }

        [Column("dosage_unit")]
        public string DosageUnit { get; set; }

        [Column("dosage_strength")]
        public string DosageStrength { get; set; }
        [Column("submodule_type_code")]
        public string SubmoduleTypeCode { get; set; }


        [NotMapped]
        public bool? IsExpired  =>  ExpiryDate != null ? ExpiryDate < DateTime.UtcNow : (bool?)null;
    }
}