using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
using PDX.Domain.Common; 
using PDX.Domain.Customer; 
using PDX.Domain.Helpers; 

namespace PDX.Domain.Customer {
    [Table("agent_supplier", Schema = "customer")]
    public class AgentSupplier:BaseEntity {
         [Column("supplier_id")]
         public int SupplierID {get; set; }

         [Column("agent_id")]
         public int AgentID {get; set; }
         
         [Column("agent_level_id")]
         public int AgentLevelID {get; set; }
         [Column("start_date")]
         public Nullable<DateTime> StartDate {get; set; }
         [Column("end_date")]
         public Nullable<DateTime> EndDate {get; set; }
         [Column("remark")]
         public string Remark {get; set; }

         [NavigationProperty]
         [ForeignKey("SupplierID")]
         public virtual Supplier Supplier {get; set; }

         [NavigationProperty]
         [ForeignKey("AgentID")]
         public virtual Agent Agent {get; set; }

         [NavigationProperty]
         [ForeignKey("AgentLevelID")]
         public virtual AgentLevel AgentLevel {get; set; }

         [NotMapped]
         public virtual Domain.Document.Document AgencyAgreementDoc {get; set; }
    }
}