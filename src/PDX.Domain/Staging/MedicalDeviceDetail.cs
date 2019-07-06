using PDX.Domain.Commodity;
using PDX.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace PDX.Domain.Staging {
    
    [Table ("medical_device_detail", Schema = "staging")]
    public class MedicalDeviceDetail : BaseEntity {
        [Column ("brand_name")]
        public string BrandName { get; set; }

        [Column ("inn_id")]
        public int InnID { get; set; }

        [Column ("description")]
        public string Description { get; set; }

        [Column ("device_class_id")]
        public int DeviceClassID { get; set; }

        [Column ("device_size")]
        public string DeviceSize { get; set; }

        [Column ("device_model")]
        public string DeviceModel { get; set; }

        [Column ("pack_size_id")]
        public int PackSizeID { get; set; }
        [Column ("header_id")]
        public int HeaderID { get; set; }

        [NavigationProperty]
        [ForeignKey ("DeviceClassID")]
        public virtual DeviceClass DeviceClass { get; set; }

        [NavigationProperty]
        [ForeignKey ("InnID")]
        public virtual INN Inn { get; set; }

        [NavigationProperty]
        [ForeignKey ("PackSizeID")]
        public virtual PackSize PackSize { get; set; }
        [NavigationProperty]
        [ForeignKey ("HeaderID")]
        public virtual MedicalDeviceHeader Header { get; set; }

    }
}