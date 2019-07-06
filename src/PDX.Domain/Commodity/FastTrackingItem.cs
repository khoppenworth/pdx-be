using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity
{
    [Table("fast_tracking_item", Schema = "commodity")]
    public class FastTrackingItem:BaseEntity
    {
         [Column("inn_id")]
         public int INNID{get;set;}

         [Column("therapeutic_group_id")]
         public int TherapeuticGroupID{get;set;}

         [Column("medicine_indication")]
         public string MedicineIndication{get;set;}

         [NavigationProperty]
         [ForeignKey("INNID")]
         public virtual INN INN{get;set;}

         [NavigationProperty]
         [ForeignKey("TherapeuticGroupID")]
         public virtual TherapeuticGroup TherapeuticGroup{get;set;}
        
    }
}