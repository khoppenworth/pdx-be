using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using PDX.Domain.Document;
using PDX.Domain.Helpers;
using Newtonsoft.Json;

namespace PDX.Domain.Common
{
    [Table("module", Schema = "common")]
    public class Module:LookUpBaseEntity
    {
        [Column("module_code")]
        public string ModuleCode { get; set; }
    }
}