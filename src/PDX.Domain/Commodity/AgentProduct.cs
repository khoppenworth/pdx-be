using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("agent_product", Schema = "commodity")]
    public class AgentProduct:BaseEntity
    {
         [Column("product_id")]
         public int ProductID{get;set;}

         [Column("agent_id")]
         public int AgentID{get;set;}

         [NavigationProperty]
         [ForeignKey("ProductID")]
         public virtual Product Product{get;set;}

         [NavigationProperty]
         [ForeignKey("AgentID")]
         public virtual Agent Agent{get;set;}
    }
}