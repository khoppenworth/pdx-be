using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;
using PDX.Domain.License;

namespace PDX.Domain.Commodity
{
    [Table("supplier_product", Schema = "commodity")]
    public class SupplierProduct : BaseEntity
    {
        [Column("product_id")]
        public int ProductID { get; set; }

        [Column("supplier_id")]
        public int SupplierID { get; set; }
        [Column("ma_id")]
        public int? MAID { get; set; }
        [Column("registration_date")]
        public DateTime? RegistrationDate { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductID")]
        [NavigationProperty]
        public virtual Product Product { get; set; }

        [NavigationProperty]
        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }
        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }

        [NotMapped]
        public bool? IsExpired { get { return ExpiryDate == null ? (bool?)null : ExpiryDate < DateTime.UtcNow; } }
    }
}