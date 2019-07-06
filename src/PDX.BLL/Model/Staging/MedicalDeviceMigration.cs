using System;
using System.Collections.Generic;
namespace PDX.BLL.Model.Staging {
    public class MedicalDeviceMigration {
        public int ID { get; set; }        
        public string CerteficateNumber { get; set; }
        public string RegistrationType { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ManufacturerAddressID { get; set; }
        public int AgentID { get; set; }
        public int SupplierID { get; set; }
        public string AgentName { get; set; }
        public string SupplierName { get; set; }
        public string ManufacturerName { get; set; }
        public List<MedicalDeviceMigrationProduct> Products{get;set;} 
    }
    public class MedicalDeviceMigrationProduct {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public int InnID { get; set; }
        public string Description { get; set; }
        public int DeviceClassID { get; set; }
        public string GenericName { get; set; }
        public string DeviceClassName { get; set; }
        public List<MedicalDeviceMigrationModelSize> ModelSizes{get;set;} 
    }

    public class MedicalDeviceMigrationModelSize {
        public string Size { get; set; }
        public string Model { get; set; }
        public int PackSizeID { get; set; }
        public string PackSizeName { get; set; }
        public List<MedicalDeviceMigrationPackSize> PackSizes{get;set;} 
    }
    public class MedicalDeviceMigrationPackSize {
        public int PackSizeID { get; set; }
    }

}