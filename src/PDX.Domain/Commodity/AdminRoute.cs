using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;

namespace PDX.Domain.Commodity
{
    [Table("admin_route", Schema = "commodity")]
    public class AdminRoute : LookUpEntity
    {
        [Required]
        [Column("admin_route_code")]
        [MaxLength(10)]
        public string AdminRouteCode { get; set; }
    }
}