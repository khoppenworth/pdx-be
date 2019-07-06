using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Customer
{
    [Table("supplier", Schema = "customer")]
    public class Supplier:BaseEntity
    {
        [Required]
        [Column("name")]
        [MaxLength(500)]
        public string Name { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Column("address_id")]
        public int AddressID { get; set; }

        [Column("phone")]
        [MaxLength(100)]
        public string Phone {get; set; }
        [Column("phone2")]
        [MaxLength(100)]
        public string Phone2 { get; set; }
        
        [Column("phone3")]
        [MaxLength(100)]
        public string Phone3 { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("fax")]
        [MaxLength(100)]
        public string Fax { get; set; }

        [Column("website")]
        [MaxLength(100)]
        public string Website { get; set; }

        [NavigationProperty]
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        [NotMapped]
        public string Remark { get; set; }
    }
}