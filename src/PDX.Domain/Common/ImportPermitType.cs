using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("import_permit_type", Schema = "common")]
    public class ImportPermitType:LookUpBaseEntity
    {        
        [Required]
        [Column("import_permit_type_code")]
        public string ImportPermitTypeCode { get; set; }
    }
}