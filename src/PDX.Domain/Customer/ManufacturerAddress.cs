using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;

namespace PDX.Domain.Customer
{

    [Table("manufacturer_address", Schema = "customer")]
    public class ManufacturerAddress : BaseEntity
    {
        [Column("manufacturer_id")]
        public int ManufacturerID { get; set; }

        [Column("address_id")]
        public int AddressID { get; set; }

        [NavigationProperty]
        [ForeignKey("ManufacturerID")]
        public virtual Manufacturer Manufacturer { get; set; }

        [NavigationProperty]
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
    }
}