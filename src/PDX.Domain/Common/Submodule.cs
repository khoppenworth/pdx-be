using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Document;
using PDX.Domain.Helpers;

namespace PDX.Domain.Common {
    [Table ("submodule", Schema = "common")]
    public class Submodule : LookUpBaseEntity {
        [Column ("submodule_code")]
        public string SubmoduleCode { get; set; }

        [Column ("module_id")]
        public int ModuleID { get; set; }

        [Column ("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }

        [NavigationProperty]
        [ForeignKey ("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }

        [NavigationProperty]
        [ForeignKey ("ModuleID")]
        public virtual Module Module { get; set; }

        [JsonIgnore]
        [NavigationProperty]
        public virtual ICollection<ModuleDocument> ModuleDocuments { get; set; }
    }
}