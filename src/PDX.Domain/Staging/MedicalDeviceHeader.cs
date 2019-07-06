using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;

namespace PDX.Domain.Staging {
    [Table ("medical_device_header", Schema = "staging")]
    public class MedicalDeviceHeader : BaseEntity {
        [Column ("certeficate_number")]
        public string CerteficateNumber { get; set; }

        [Column ("registration_type")]
        public string RegistrationType { get; set; }

        [Column ("registration_date")]
        public DateTime RegistrationDate { get; set; }

        [Column ("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [Column ("manufacturer_address_id")]
        public int ManufacturerAddressID { get; set; }

        [Column ("agent_id")]
        public int AgentID { get; set; }

        [Column ("supplier_id")]
        public int SupplierID { get; set; }

        [NotMapped]
        public string AgentName { get { return Agent?.Name; } }

        [NotMapped]
        public string SupplierName { get { return Supplier?.Name; } }

        [NotMapped]
        public string ManufacturerName { get { return ManufacturerAddress?.Manufacturer.Name; } }

        [NavigationProperty]
        [ForeignKey ("ManufacturerAddressID")]
        public virtual ManufacturerAddress ManufacturerAddress { get; set; }

        [NavigationProperty]
        [ForeignKey ("AgentID")]
        public virtual Agent Agent { get; set; }

        [NavigationProperty]
        [ForeignKey ("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        [NavigationProperty]
        public virtual ICollection<MedicalDeviceDetail> Details { get; set; }
    }
}