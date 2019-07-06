using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("shelf_life", Schema = "commodity")]
    public class ShelfLife : LookUpEntity
    {
       
    }
}