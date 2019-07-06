using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views
{
    [Table("vwagent", Schema = "customer")]
    public class vwAgent : BaseEntity
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("address_id")]
        public int AddressID { get; set; }

        [Column("agent_type_id")]
        public int AgentTypeID { get; set; }

        [Column("license_number")]
        public string LicenseNumber { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("website")]
        public string Website { get; set; }

        [Column("contact_person")]
        public string ContactPerson { get; set; }
        [Column("is_approved")]
         public bool IsApproved {get; set; }
        [Column("agent_type_name")]
        public string AgentTypeName { get; set; }

        [Column("country_name")]
        public string CountryName { get; set; }
    }
}