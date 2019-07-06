using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("pharmacopoeia_standard", Schema = "commodity")]
    public class PharmacopoeiaStandard : LookUpEntity
    {
    }
}