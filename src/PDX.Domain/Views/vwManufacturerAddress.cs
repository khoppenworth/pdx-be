using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Customer;

namespace PDX.Domain.Views
{
    [Table("vwmanufacturer_address", Schema = "customer")]
    public class vwManufacturerAddress: BaseEntity
    {
        [Column("manufacturer_id")]
        public int ManufacturerID { get; set; }

        [Column("address_id")]
        public int AddressID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("site")]
        public string Site { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("line1")]
        [MaxLength(500)]
        public string Line1 { get; set; }

        [Column("line2")]
        [MaxLength(500)]
        public string Line2 { get; set; }

        [Column("region")]
        [MaxLength(100)]
        public string Region { get; set; }

        [Column("city")]
        [MaxLength(100)]
        public string City { get; set; }

        [Column("sub_city")]
        [MaxLength(100)]
        public string SubCity { get; set; }

        [Column("woreda")]
        [MaxLength(100)]
        public string Woreda { get; set; }

        [Column("zip_code")]
        [MaxLength(100)]
        public string ZipCode { get; set; }

        [Column("is_gmp_inspected")]
        public Nullable<bool> IsGMPInspected { get; set; }
        [Column("gmp_certificate_number")]
        [MaxLength(100)]
        public string GMPCertificateNumber { get; set; }
        [Column("gmp_inspected_date")]
        public Nullable<DateTime> GMPInspectedDate { get; set; }

        [Column("master_file_number")]
        public string MasterFileNumber { get; set; }

        [Column("license_number")]
        public string LicenseNumber { get; set; }

    }
}