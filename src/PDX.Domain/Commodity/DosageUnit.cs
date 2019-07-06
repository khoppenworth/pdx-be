using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("dosage_unit", Schema = "commodity")]
    public class DosageUnit : LookUpEntity
    {
    }
}