using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Account;
using PDX.Domain.Catalog;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.License
{
    [Table("ma_checklist", Schema = "license")]
    public class MAChecklist:BaseEntity
    {
        [Column("ma_id")]
        public int MAID { get; set; }
        [Column("checklist_id")]
        public int ChecklistID { get; set; }
        
        [Column("responder_type_id")]
        public int ResponderTypeID { get; set; }
        [Column("responder_id")]
        public int ResponderID { get; set; }
        [Column("option_id")]
        public int? OptionID { get; set; }
        [Column("answer")]
        public string Answer { get; set; }

        
        [JsonIgnore]
        [ForeignKey("MAID")]
        public virtual MA MA { get; set; }

        [NavigationProperty]
        [ForeignKey("ChecklistID")]
        public virtual Checklist Checklist { get; set; }
        [NavigationProperty]
        [ForeignKey("ResponderTypeID")]
        public virtual ResponderType ResponderType { get; set; }
        [NavigationProperty]
        [ForeignKey("ResponderID")]
        public virtual User Responder { get; set; }
        [NavigationProperty]
        [ForeignKey("OptionID")]
        public virtual Option Option { get; set; }
    }
}