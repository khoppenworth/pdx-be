using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;
using Newtonsoft.Json;

namespace PDX.Domain.Customer
{
    [Table("manufacturer", Schema = "customer")]
    public class Manufacturer : BaseEntity
    {
        [Required]
        [Column("name")]
        [MaxLength(300)]
        public string Name { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Column("site")]
        public string Site { get; set; }

        [Column("country_id")]
        public int CountryID { get; set; }

        //v2.0

        [Column("phone")]
        [MaxLength(100)]
        public string Phone { get; set; }
        [Column("phone2")]
        [MaxLength(100)]
        public string Phone2 { get; set; }

        [Column("phone3")]
        [MaxLength(100)]
        public string Phone3 { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("fax")]
        [MaxLength(100)]
        public string Fax { get; set; }

        [Column("website")]
        [MaxLength(100)]
        public string Website { get; set; }
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

        public virtual ICollection<ManufacturerAddress> ManufacturerAddresses { get; set; }

        /// <summary>
        /// Json.NET has the ability to conditionally serialize properties by looking for corresponding ShouldSerialize methods in the class. 
        /// To use this feature, add a boolean ShouldSerializeBlah() method to your class where Blah is replaced with the name of the property 
        /// that you do not want to serialize. Make the implementation of this method always return false.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeManufacturerAddresses()
        {
            return false;
        }
    }
}