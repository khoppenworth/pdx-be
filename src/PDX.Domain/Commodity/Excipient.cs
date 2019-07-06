using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("excipient", Schema = "commodity")]
    public class Excipient : LookUpEntity
    {
       
    }
}