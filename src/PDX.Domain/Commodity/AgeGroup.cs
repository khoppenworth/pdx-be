using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("age_group", Schema = "commodity")]
    public class AgeGroup : LookUpEntity
    {
    }
}