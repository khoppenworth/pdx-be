using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Helpers;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("device_class_submodule", Schema = "commodity")]
    public class DeviceClassSubmodule:BaseEntity
    {
        [Column("device_class_id")]
        public int DeviceClassID { get; set; }
        [Column("submodule_id")]
        public int SubmoduleID { get; set; }        

        [NavigationProperty]
        [ForeignKey("DeviceClassID")]
        public virtual DeviceClass DeviceClass { get; set; }

        [NavigationProperty]
        [ForeignKey("SubmoduleID")]
        public virtual Submodule Submodule { get; set; } 
    }
}