using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using PDX.Domain.Common; 
using PDX.Domain.Helpers; 

namespace PDX.Domain.Customer {
    [Table("agent", Schema = "customer")]
    public class Agent:BaseEntity {
        [Required]
        [Column("name")]
        [MaxLength(500)]
        public string Name {get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description {get; set; }

        [Column("address_id")]
        public int AddressID {get; set; }

        [Column("agent_type_id")]
        public int AgentTypeID {get; set; }

        [Column("license_number")]
        [MaxLength(100)]
        public string LicenseNumber {get; set; }

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
        public string Email {get; set; }

        [Column("fax")]
        [MaxLength(100)]
        public string Fax {get; set; }

        [Column("website")]
        [MaxLength(100)]
        public string Website {get; set; }

        [Column("contact_person")]
        [MaxLength(100)]
        public string ContactPerson {get; set; }
        [Column("is_approved")]
         public bool IsApproved {get; set; }

        [NavigationProperty]
        [ForeignKey("AddressID")]
        public virtual Address Address {get; set; }

        [NavigationProperty]
        [ForeignKey("AgentTypeID")]
        public virtual AgentType AgentType {get; set; }

        [NotMapped]
        public string Remark { get; set; }
    }
}