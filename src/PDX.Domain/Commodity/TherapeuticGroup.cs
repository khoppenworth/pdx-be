using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("therapeutic_group", Schema = "commodity")]
    public class TherapeuticGroup : LookUpEntity
    {
    }
}