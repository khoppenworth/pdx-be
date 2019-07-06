using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views
{
    [Table("vwsupplier", Schema = "customer")]
    public class vwSupplier:BaseEntity
    {
         
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("address_id")]
        public int AddressID { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("website")]
        public string Website { get; set; }
        
        [Column("country_name")]
        public string CountryName { get;set;}
        [Column("agent_count")]
        public int? AgentCount { get; set; }
        [Column("product_count")]
        public int? ProductCount { get; set; }
    }
}